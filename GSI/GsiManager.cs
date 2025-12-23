using StrikeLink.Services;
using StrikeLink.Services.Config;
using System.Net;

namespace StrikeLink.GSI
{
	internal static class GsiManager
	{
		internal static async Task<(IPAddress, int)> GenerateGsiFile(IPAddress address, int port, string? steamPath = null)
		{
			bool validPath = SteamService.TryGetGamePath(730, out string? counterPath);

			if (!validPath || counterPath.IsNullOrEmpty())
				throw new DirectoryNotFoundException("Could not find CS:2 installation path.");

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
					["timeout"] = ConfigNodeFactory.Value("2.0"),
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
