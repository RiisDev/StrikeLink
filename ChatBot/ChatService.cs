using StrikeLink.Services;
using StrikeLink.Services.Config;
using System.Collections.Concurrent;
using System.Globalization;

namespace StrikeLink.ChatBot
{
	/// <summary>
	/// Provides high-level chat orchestration and message delivery services.
	/// </summary>
	/// <remarks>
	/// This service manages the lifecycle of chat interactions and delegates
	/// message handling to underlying infrastructure components.
	/// </remarks>
	public class ChatService : IDisposable
	{
		private readonly string _chatCfgLocation;
		private readonly ConsoleService _consoleService;
		private readonly Config _config;

		private readonly ConcurrentQueue<(string Message, DateTime Timestamp)> _sentMessages = [];
		private static readonly TimeSpan SentMessageTtl = TimeSpan.FromSeconds(2);

		/// <summary>
		/// Initializes a new instance of the <see cref="ChatService"/> class.
		/// </summary>
		/// <param name="config">
		/// The configuration object containing chat service settings and dependencies, <see cref="Config"/>
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="config"/> is <c>null</c>.
		/// </exception>
		public ChatService(Config config)
		{
			_config = config;
			_consoleService = new ConsoleService();
			CheckCs2UserConfig();
			_consoleService.StartListening();

			_chatCfgLocation = Path.Combine(SteamService.GetGamePath(730), "game", "csgo", "cfg", "strike_link.cfg");

			string localUsername = GetLocalUsername();

			if (config.OnTeamChat is not null)
			{
				_consoleService.OnTeamChatMessageReceived += data =>
				{
					if (IsProgrammedMessage(data.Message))
						return;

					if (config.IgnoreLocalUser && data.Username.Contains(localUsername, StringComparison.InvariantCulture))
						return;

					config.OnTeamChat(data);
				};
			}

			if (config.OnGlobalChat is not null)
			{
				_consoleService.OnGlobalChatMessageReceived += data =>
				{
					if (IsProgrammedMessage(data.Message))
						return;

					if (config.IgnoreLocalUser && data.Username.Contains(localUsername, StringComparison.InvariantCulture))
						return;

					config.OnGlobalChat(data);
				};
			}
		}

		/// <summary>
		/// Sends a chat message asynchronously.
		/// </summary>
		/// <param name="message">
		/// The chat message payload to be sent <see cref="NewChatMessage"/>
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous send operation.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="message"/> is <c>null</c>.
		/// </exception>
		public async Task SendChatAsync(NewChatMessage message)
		{
			bool sent = false;
			Process[] csProcesses = Process.GetProcessesByName("cs2");
			if (csProcesses.Length == 0) { Log("CS:2 Process not found, skipping call"); return; }

			Process csProcess = csProcesses[0];
			
			string messageActual = message.Message.Length > 256 ? message.Message[..256] : message.Message;
			string messagePrefix = message.Channel == ChatChannel.Global ? "say " : "say_team ";

			_sentMessages.Enqueue((messageActual, DateTime.UtcNow));

			await File.WriteAllTextAsync(_chatCfgLocation, $"{messagePrefix}{messageActual}").ConfigureAwait(false);

			// Allow some time for CS2 to process the file write
			await Task.Delay(250).ConfigureAwait(false);

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

		private static string GetLocalUsername()
		{
			long userId = SteamService.GetCurrentUserId();
			string steamPath = SteamService.GetSteamPath();
			string counterStrikeKeybindPath = Path.Combine(steamPath, "userdata", userId.ToString(CultureInfo.InvariantCulture), "730");

			if (!Directory.Exists(counterStrikeKeybindPath))
				throw new DirectoryNotFoundException($"Failed to find user config path: {counterStrikeKeybindPath}");

			string localConfigPath = Path.Combine(counterStrikeKeybindPath, "local", "cfg");

			string firstUserKey = Directory
				.GetFiles(localConfigPath, "cs2_user_convars*.vcfg")
				.OrderBy(filePath => filePath, StringComparer.OrdinalIgnoreCase)
				.First();

			ValveCfgReader reader = new(firstUserKey);
			bool foundBindings = reader.Document.Root.TryGetProperty("convars", out ConfigNode conVars);
			if (!foundBindings) throw new InvalidOperationException("Cannot find local username (MISSING_CONVARS)");

			return conVars.TryGetProperty("name", out ConfigNode nameNode) ? nameNode.GetString() : throw new InvalidOperationException("Cannot find local username");
		}


		private bool IsProgrammedMessage(string incomingMessage)
		{
			string normalizedIncomingMessage = incomingMessage.NormalizeForComparison();
			DateTime now = DateTime.UtcNow;

			while (_sentMessages.TryPeek(out (string Message, DateTime Timestamp) entry))
			{
				if (now - entry.Timestamp > SentMessageTtl) { _sentMessages.TryDequeue(out _); continue; }
				if (entry.Message.NormalizeForComparison().Contains(normalizedIncomingMessage, StringComparison.InvariantCulture)) { _sentMessages.TryDequeue(out _); return true; }

				break;
			}

			return false;
		}

		/// <summary>
		/// Releases all resources used by the <see cref="ChatService"/>.
		/// </summary>
		/// <remarks>
		/// This method suppresses finalization and disposes managed resources.
		/// </remarks>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_consoleService.Dispose();
		}
	}
}
