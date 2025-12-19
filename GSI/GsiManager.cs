using Microsoft.Win32;
using StrikeLink.Extensions;
using System.Net;
using System.Runtime.InteropServices;

namespace StrikeLink.GSI
{
	public static class GsiManager
	{
		internal static string GetSteamPath()
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) throw new FileNotFoundException("Unable to automatically find steam, (OS_NOT_WINDOWS)");

			using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
			if (key == null) throw new KeyNotFoundException($"{key} could not be found.");

			object? value = key.GetValue("SteamPath");
			return value as string ?? throw new KeyNotFoundException("Steam path key could not be found.");
		}

		internal static async Task<string> GetCounterStrikePath(string steamPath)
		{
			string vdfPath = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");

			if (!File.Exists(vdfPath)) throw new FileNotFoundException($"Failed to find libraryfolders.vdf at {vdfPath}");

			string vdfText = await File.ReadAllTextAsync(vdfPath).ConfigureAwait(false);
			string[] paths = vdfText.Split("path\"").Skip(1).ToArray();

			string? pathContainingCs = paths.FirstOrDefault(x=> x.Contains("\"730\"", StringComparison.InvariantCulture));

			if (pathContainingCs.IsNullOrEmpty()) throw new KeyNotFoundException("Failed to detect Counter Strike in VDF.");

			int firstQuoteIndex = pathContainingCs.IndexOf('"', StringComparison.InvariantCulture);
			int secondQuoteIndex = pathContainingCs.IndexOf('"', firstQuoteIndex + 1);

			string pathContent = pathContainingCs[(firstQuoteIndex + 1)..secondQuoteIndex];

			string cleanedPath = pathContent.Replace(@"\\", @"\", StringComparison.InvariantCulture);

			if (!Directory.Exists(cleanedPath))
				throw new DirectoryNotFoundException($"{cleanedPath} does not exist");

			string counterstrikeInstallPath = Path.Combine(cleanedPath, "steamapps", "common", "Counter-Strike Global Offensive");

			return !Directory.Exists(counterstrikeInstallPath) ? throw new DirectoryNotFoundException($"{counterstrikeInstallPath} does not exist") : counterstrikeInstallPath;
		}

		internal static async Task GenerateGsiFile(IPAddress address, int port, string? steamPath = null)
		{
			string lookupPath = steamPath ?? GetSteamPath();

			if (!Directory.Exists(lookupPath))
				throw new DirectoryNotFoundException($"{lookupPath} does not exist");

			string counterPath = await GetCounterStrikePath(lookupPath).ConfigureAwait(false);

			string cfgPath = Path.Combine(counterPath, "game", "csgo", "cfg");
			string gsiCfg = Path.Combine(cfgPath, "gamestate_integration_strikelink.cfg");

			await File.WriteAllTextAsync(gsiCfg,
				$$"""
			            "Strikelink Connection File"
			            {
			             "uri" "http:{{address}}:{{port}}"
			             "timeout" "5.0"
			             "buffer"  "0"
			             "throttle" "0"
			             "heartbeat" "30.0"
			             "data"
			             {
			            	"map_round_wins" "1"          // history of round wins
			            	"map" "1"                     // mode, map, phase, team scores
			            	"player_id" "1"               // steamid
			            	"player_match_stats" "1"      // scoreboard info
			            	"player_state" "1"            // armor, flashed, equip_value, health, etc.
			            	"player_weapons" "1"          // list of player weapons and weapon state
			            	"provider" "1"                // info about the game providing info 
			            	"round" "1"                   // round phase and the winning team
			            	"allgrenades" "1"             // grenade effecttime, lifetime, owner, position, type, velocity
			            	"allplayers_id" "1"           // the steam id of each player
			            	"allplayers_match_stats" "1"  // the scoreboard info for each player
			            	"allplayers_position" "1"     // player_position but for each player
			            	"allplayers_state" "1"        // the player_state for each player
			            	"allplayers_weapons" "1"      // the player_weapons for each player
			            	"bomb" "1"                    // location of the bomb, who's carrying it, dropped or not
			            	"phase_countdowns" "1"        // time remaining in tenths of a second, which phase
			            	"player_position" "1"         // forward direction, position for currently spectated player
			             }
			            }                 
			            """)
				.ConfigureAwait(false);
		}
	}
}
