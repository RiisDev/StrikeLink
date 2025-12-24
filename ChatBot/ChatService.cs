using StrikeLink.Services;
using StrikeLink.Services.Config;
using System.Globalization;

namespace StrikeLink.ChatBot
{
	public class ChatService : IDisposable
	{
		private readonly string _chatCfgLocation;
		private readonly ConsoleService _consoleService;
		private readonly Config _config;

		public ChatService(Config config)
		{
			_config = config;
			_consoleService = new ConsoleService();
			CheckCs2UserConfig();
			_consoleService.StartListening();

			_chatCfgLocation = Path.Combine(SteamService.GetGamePath(730), "game", "csgo", "cfg", "strike_link.cfg");

			if (config.OnTeamChat is not null)
				_consoleService.OnTeamChatMessageReceived += config.OnTeamChat;
			if (config.OnGlobalChat is not null)
				_consoleService.OnGlobalChatMessageReceived += config.OnGlobalChat;
		}

		public async Task SendChatAsync(NewChatMessage message)
		{
			bool sent = false;
			Process[] csProcesses = Process.GetProcessesByName("cs2");
			if (csProcesses.Length == 0) { Log("CS:2 Process not found, skipping call"); return; }

			Process csProcess = csProcesses[0];
			
			string messageActual = message.Message.Length > 256 ? message.Message[..256] : message.Message;
			string messagePrefix = message.Channel == ChatChannel.Global ? "say " : "say_team ";

			await File.WriteAllTextAsync(_chatCfgLocation, $"{messagePrefix}{messageActual}").ConfigureAwait(false);

			for (int retryCount = 0; retryCount < 5; retryCount++)
			{
				if (!Win32.IsPrcoessActivated(csProcess))
				{
					await Task.Delay(250).ConfigureAwait(false);
					continue;
				}

				Win32.PressKey(_config.Keybind);
				sent = true;
				Log($"Sent chat message after {retryCount} retries");
				break;
			}

			if (!sent)
				Log("Failed to send chat message: CS2 window not focused.");
		}

		private void CheckCs2UserConfig()
		{
			long userId = SteamService.GetCurrentUserId();
			string steamPath = SteamService.GetSteamPath();
			string counterStrikeKeybindPath = Path.Combine(steamPath, "userdata", userId.ToString(CultureInfo.InvariantCulture), "730");

			if (!Directory.Exists(counterStrikeKeybindPath))
				throw new DirectoryNotFoundException($"Failed to find user config path: {counterStrikeKeybindPath}");

			string localConfigPath = Path.Combine(counterStrikeKeybindPath, "local", "cfg");

			(bool localConfigReady, string localKeybind) = CheckLocalCs2Config(localConfigPath);

			if (!localConfigReady)
				throw new InvalidOperationException("CS2 user keybind configuration not found. Please configure a keybind for Strike Link in CS2 settings.");

			if (localKeybind != Win32.VirtualKeyToChar[_config.Keybind])
				throw new InvalidOperationException($"CS2 user keybind configuration does not match the configured keybind '{_config.Keybind}'. Please update your CS2 keybind configuration accordingly.");
		}
		
		private static (bool, string) CheckLocalCs2Config(string localConfigPath)
		{
			string firstUserKey = Directory
				.GetFiles(localConfigPath, "cs2_user_keys*.vcfg")
				.OrderBy(filePath => filePath, StringComparer.OrdinalIgnoreCase)
				.First();

			ValveCfgReader reader = new(firstUserKey);
			bool foundBindings = reader.Document.Root.TryGetProperty("bindings", out ConfigNode bindings);
			if (!foundBindings) return (false, "");

			foreach (KeyValuePair<string, ConfigNode> property in bindings.EnumerateObject())
			{
				string command = property.Value.GetString();
				if (command == "exec strike_link.cfg") return (true, property.Key);
			}

			return (false, "");
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_consoleService.Dispose();
		}
	}
}
