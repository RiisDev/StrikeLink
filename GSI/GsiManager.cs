using Microsoft.Win32;
using StrikeLink.Services.Config;
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

		internal static string GetCounterStrikePath(string steamPath)
		{
			string vdfPath = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");

			if (!File.Exists(vdfPath)) throw new FileNotFoundException($"Failed to find libraryfolders.vdf at {vdfPath}");

			ValveCfgReader reader = new(vdfPath);

			foreach (KeyValuePair<string, ConfigNode> node in reader.Document.Root.EnumerateObject())
			{
				bool appsFound = node.Value.TryGetProperty("apps", out ConfigNode appsNode);
				if (!appsFound) continue;

				bool hasCs = appsNode.TryGetProperty("730", out ConfigNode _);
				if (!hasCs) continue;

				string path = node.Value.GetProperty("path").GetString();
				string counterstrikeInstallPath = Path.Combine(path.Replace(@"\\", @"\", StringComparison.InvariantCulture), "steamapps", "common", "Counter-Strike Global Offensive");

				return !Directory.Exists(counterstrikeInstallPath) ? throw new DirectoryNotFoundException($"{counterstrikeInstallPath} does not exist") : counterstrikeInstallPath;
			}

			throw new FileNotFoundException("Could not find Counter-Strike: Global Offensive installation in any Steam library folder.");
		}

		internal static async Task<(IPAddress, int)> GenerateGsiFile(IPAddress address, int port, string? steamPath = null)
		{
			string lookupPath = steamPath ?? GetSteamPath();

			if (!Directory.Exists(lookupPath))
				throw new DirectoryNotFoundException($"{lookupPath} does not exist");

			string counterPath = GetCounterStrikePath(lookupPath);

			string cfgPath = Path.Combine(counterPath, "game", "csgo", "cfg");
			string gsiCfg = Path.Combine(cfgPath, "gamestate_integration_strikelink.cfg");

			if (File.Exists(gsiCfg))
			{
				ValveCfgReader existingReader = new(gsiCfg);
				ConfigNode existingRoot = existingReader.Document.Root;
				if (existingRoot.TryGetProperty("Strikelink Connection File", out ConfigNode existingConnection))
				{
					bool foundUri = existingConnection.TryGetProperty("uri", out ConfigNode uriNode);
					if (foundUri)
					{
						Uri uri = new(uriNode.GetString());
						IPAddress existingAddress = IPAddress.Parse(uri.Host);
						int existingPort = uri.Port;
						return (existingAddress, existingPort);
					}
				}
			}

			ConfigNode data = ConfigNodeFactory.Object(
				new Dictionary<string, ConfigNode>
				{
					["map_round_wins"] = ConfigNodeFactory.Value("1"),
					["map"] = ConfigNodeFactory.Value("1"),
					["player_id"] = ConfigNodeFactory.Value("1"),
					["player_match_stats"] = ConfigNodeFactory.Value("1"),
					["player_state"] = ConfigNodeFactory.Value("1"),
					["player_weapons"] = ConfigNodeFactory.Value("1"),
					["provider"] = ConfigNodeFactory.Value("1"),
					["round"] = ConfigNodeFactory.Value("1"),
					["allgrenades"] = ConfigNodeFactory.Value("1"),
					["allplayers_id"] = ConfigNodeFactory.Value("1"),
					["allplayers_match_stats"] = ConfigNodeFactory.Value("1"),
					["allplayers_position"] = ConfigNodeFactory.Value("1"),
					["allplayers_state"] = ConfigNodeFactory.Value("1"),
					["allplayers_weapons"] = ConfigNodeFactory.Value("1"),
					["bomb"] = ConfigNodeFactory.Value("1"),
					["phase_countdowns"] = ConfigNodeFactory.Value("1"),
					["player_position"] = ConfigNodeFactory.Value("1"),
				});

			ConfigNode connectionFile = ConfigNodeFactory.Object(
				new Dictionary<string, ConfigNode>
				{
					["uri"] = ConfigNodeFactory.Value($"http://{address}:{port}"),
					["timeout"] = ConfigNodeFactory.Value("5.0"),
					["buffer"] = ConfigNodeFactory.Value("0"),
					["throttle"] = ConfigNodeFactory.Value("0"),
					["heartbeat"] = ConfigNodeFactory.Value("30.0"),
					["data"] = data
				});

			ConfigDocument document = new(ConfigNodeFactory.Object(new Dictionary<string, ConfigNode> { ["Strikelink Connection File"] = connectionFile }));

			await File.WriteAllTextAsync(gsiCfg, ValveCfgWriter.Write(document)).ConfigureAwait(false);

			return (address, port);
		}
	}
}
