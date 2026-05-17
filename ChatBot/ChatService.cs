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
	public class ChatService : IAsyncDisposable
	{
		private readonly ConsoleService _consoleService;
		private readonly ConsoleServiceConfig _consoleServiceConfig;
		private readonly bool _ownsConsoleService; // true only when we newed it up
		private bool _disposed;

		private readonly ConcurrentQueue<(string Message, DateTime Timestamp)> _sentMessages = [];
		private static readonly TimeSpan SentMessageTtl = TimeSpan.FromSeconds(2);

		/// <summary>
		/// Initializes a new instance of the <see cref="ChatService"/> class.
		/// </summary>
		/// <param name="consoleServiceConfig">
		/// The configuration object containing chat service settings and dependencies, <see cref="ConsoleServiceConfig"/>
		/// </param>
		/// <param name="console">
		/// A premade instance of the console service
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="consoleServiceConfig"/> is <c>null</c>.
		/// </exception>
		public ChatService(ConsoleServiceConfig consoleServiceConfig, ConsoleService? console = null)
		{
			ArgumentNullException.ThrowIfNull(consoleServiceConfig);
			_consoleServiceConfig = consoleServiceConfig;

			_ownsConsoleService = console is null;
			_consoleService = console ?? new ConsoleService();

			CheckCs2UserConfig();
			
			string localUsername = GetLocalUsername();

			if (consoleServiceConfig.OnTeamChat is not null)
			{
				_consoleService.OnTeamChatMessageReceived += data =>
				{
					if (IsProgrammedMessage(data.Message))
						return;

					if (consoleServiceConfig.IgnoreLocalUser && data.Username.Contains(localUsername, StringComparison.InvariantCulture))
						return;

					consoleServiceConfig.OnTeamChat(data);
				};
			}

			if (consoleServiceConfig.OnGlobalChat is not null)
			{
				_consoleService.OnGlobalChatMessageReceived += data =>
				{
					if (IsProgrammedMessage(data.Message))
						return;

					if (consoleServiceConfig.IgnoreLocalUser && data.Username.Contains(localUsername, StringComparison.InvariantCulture))
						return;

					consoleServiceConfig.OnGlobalChat(data);
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
			ArgumentNullException.ThrowIfNull(message);
			
			string messageActual = message.Message.Length > 256 ? message.Message[..256] : message.Message;
			string messagePrefix = message.Channel == ChatChannel.Global ? "say " : "say_team ";

			_sentMessages.Enqueue((messageActual, DateTime.UtcNow));

			(string error, bool success) = await _consoleService.SendConsoleCommand($"{messagePrefix}{messageActual}", _consoleServiceConfig).ConfigureAwait(false);

			if (!success)
				throw new InvalidOperationException(error);
		}
		
		private void CheckCs2UserConfig()
		{
			long userId = SteamService.GetCurrentUserId();
			string steamPath = SteamService.GetSteamPath();
			string counterStrikeKeybindPath = Path.Combine(steamPath, "userdata", userId.ToString(CultureInfo.InvariantCulture), "730");

			if (!Directory.Exists(counterStrikeKeybindPath))
				throw new DirectoryNotFoundException($"Failed to find user consoleServiceConfig path: {counterStrikeKeybindPath}");

			string localConfigPath = Path.Combine(counterStrikeKeybindPath, "local", "cfg");

			(bool localConfigReady, string localKeybind) = CheckLocalCs2Config(localConfigPath);

			if (!localConfigReady)
				throw new InvalidOperationException("CS2 user keybind configuration not found. Please configure a keybind for Strike Link in CS2 settings.");

			if (localKeybind != NativeMethods.VirtualKeyToChar[_consoleServiceConfig.Keybind])
				throw new InvalidOperationException($"CS2 user keybind configuration does not match the configured keybind '{_consoleServiceConfig.Keybind}'. Please update your CS2 keybind configuration accordingly.");
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
				if (command == "exec strike_link.cfg") 
					return (true, property.Key);
			}

			return (false, "");
		}

		private static string GetLocalUsername()
		{
			long userId = SteamService.GetCurrentUserId();
			string steamPath = SteamService.GetSteamPath();
			string counterStrikeKeybindPath = Path.Combine(steamPath, "userdata", userId.ToString(CultureInfo.InvariantCulture), "730");

			if (!Directory.Exists(counterStrikeKeybindPath))
				throw new DirectoryNotFoundException($"Failed to find user consoleServiceConfig path: {counterStrikeKeybindPath}");

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
				if (now - entry.Timestamp > SentMessageTtl) { _ = _sentMessages.TryDequeue(out _); continue; }
				if (entry.Message.NormalizeForComparison().Contains(normalizedIncomingMessage, StringComparison.InvariantCulture)) { _ = _sentMessages.TryDequeue(out _); return true; }

				break;
			}

			return false;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
		/// </summary>
		/// <remarks>This method is called by public Dispose methods and the finalizer. When disposing is true, this
		/// method disposes all managed resources referenced by the object. Override this method to release additional
		/// resources.</remarks>

		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;
			_disposed = true;

			if (_ownsConsoleService)
				await _consoleService.DisposeAsync().ConfigureAwait(false);

			GC.SuppressFinalize(this);
		}
	}
}
