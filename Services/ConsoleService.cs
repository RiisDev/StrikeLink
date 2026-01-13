using StrikeLink.ChatBot;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StrikeLink.Services
{
	/// <summary>
	/// Provides access to the game console output and emits high-level events
	/// for game state changes, chat messages, server activity, and addon progress.
	/// </summary>
	public partial class ConsoleService : IDisposable
	{
		/// <summary>
		/// Represents the current UI state of the game.
		/// </summary>
		public enum GameUiState
		{
			/// <summary>
			/// The game is currently loading.
			/// </summary>
			LoadingScreen,

			/// <summary>
			/// The player is actively in-game.
			/// </summary>
			InGame,

			/// <summary>
			/// The pause menu is open.
			/// </summary>
			PauseMenu,

			/// <summary>
			/// The main menu is displayed.
			/// </summary>
			MainMenu,

			/// <summary>
			/// The UI state could not be determined.
			/// </summary>
			Invalid
		}

		/// <summary>
		/// Represents a unit of data size.
		/// </summary>
		public enum Size
		{
			/// <summary>
			/// Bytes.
			/// </summary>
			Byte,

			/// <summary>
			/// Kilobytes.
			/// </summary>
			Kilobyte,

			/// <summary>
			/// Megabytes.
			/// </summary>
			Megabyte,

			/// <summary>
			/// Gigabytes.
			/// </summary>
			Gigabyte
		}

		/// <summary>
		/// Represents a transition between two game UI states.
		/// </summary>
		/// <param name="OldState">
		/// The previous UI state.
		/// </param>
		/// <param name="NewState">
		/// The new UI state.
		/// </param>
		public record StateChanged(GameUiState OldState, GameUiState NewState);

		/// <summary>
		/// Represents a progress value with an associated size unit.
		/// </summary>
		/// <param name="Downloaded">
		/// The amount of data transferred.
		/// </param>
		/// <param name="SizeType">
		/// The unit of measurement for <paramref name="Downloaded"/>.
		/// </param>
		public record Progress(double Downloaded, Size SizeType);

		/// <summary>
		/// Represents download progress for a specific addon.
		/// </summary>
		/// <param name="AddonId">
		/// The unique identifier of the addon.
		/// </param>
		/// <param name="Downloaded">
		/// The amount of data downloaded so far. <see cref="Progress"/>
		/// </param>
		/// <param name="Total">
		/// The total size of the addon. <see cref="Progress"/>
		/// </param>
		public record AddonProgress(long AddonId, Progress Downloaded, Progress Total);

		/// <summary>
		/// Occurs when a new console log line is received.
		/// </summary>
		public event Action<string>? OnLogReceived;

		/// <summary>
		/// Occurs when a player connects to the server.
		/// </summary>
		public event Action<string>? OnPlayerConnected;

		/// <summary>
		/// Occurs when the local player joins a map.
		/// </summary>
		public event Action<string>? OnMapJoined;

		/// <summary>
		/// Occurs when a global chat message is received.
		/// </summary>
		public event Action<ChatMessage>? OnGlobalChatMessageReceived;

		/// <summary>
		/// Occurs when a team chat message is received.
		/// </summary>
		public event Action<ChatMessage>? OnTeamChatMessageReceived;

		/// <summary>
		/// Occurs when the game UI state changes.
		/// </summary>
		public event Action<StateChanged>? OnUiStateChanged;

		/// <summary>
		/// Occurs when addon download progress is updated.
		/// </summary>
		public event Action<AddonProgress>? OnAddonProgress;

		/// <summary>
		/// Occurs when an addon has finished downloading.
		/// </summary>
		public event Action? OnAddonFinished;

		/// <summary>
		/// Occurs when the client begins joining a server.
		/// </summary>
		public event Action? OnServerJoining;

		/// <summary>
		/// Occurs when the client successfully connects to a server.
		/// </summary>
		public event Action? OnServerConnected;

		/// <summary>
		/// Occurs when the client disconnects from a server.
		/// </summary>
		public event Action? OnServerDisconnected;

		// Fields
		private readonly CancellationTokenSource _cancellationTokenSource = new();

		private readonly string _consoleLogPath;
		private readonly string _strikeConsoleTmp;
		private string _downloadingUgcData = string.Empty;

		private int _lastLineIndex;
		private string? _lastLineText;
		private bool _firstRun = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleService"/> class.
		/// </summary>
		/// <exception cref="DirectoryNotFoundException">
		/// Thrown when the Counter-Strike 2 installation directory cannot be located.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the game was not launched with the <c>-condebug</c> launch option.
		/// </exception>
		public ConsoleService()
		{
			if (!SteamService.TryGetGamePath(730, out string? counterStrikePath) || counterStrikePath.IsNullOrEmpty())
				throw new DirectoryNotFoundException("Failed to find CS:2 game directory.");

			bool conDebug = SteamService.GetGameLaunchOptions(730).Any(x=> x == "-condebug");
			if (!conDebug) throw new InvalidOperationException("CS:2 was not launched with -condebug, console log will not be available.");

			_consoleLogPath = Path.Combine(counterStrikePath, "game", "csgo", "console.log");
			_strikeConsoleTmp = Path.Combine(Path.GetTempPath(), "console_tmp_strikelink.log");
		}

		/*
		 *
		 * Note for future self:
		 * Can parse the status output by first checking if the lineText contains status, and then waiting for the last line that contains "end of status".
		 *
		 */
		private void ParseLineData(string lineText)
		{
			if (_firstRun) return;

			Debug.WriteLine(lineText);
			OnLogReceived?.Invoke(lineText);

			switch (lineText)
			{
				case var _ when lineText.Contains(" [ALL] ", StringComparison.InvariantCulture) && lineText.Contains(':', StringComparison.InvariantCulture):
					ParseChatLine(lineText,false);
					break;
				case var _ when lineText.Contains(" [T] ", StringComparison.InvariantCulture) && lineText.Contains(':', StringComparison.InvariantCulture):
				case var _ when lineText.Contains(" [CT] ", StringComparison.InvariantCulture) && lineText.Contains(':', StringComparison.InvariantCulture):
					ParseChatLine(lineText, true);
					break;
				case var _ when lineText.Contains("GameChangeUIState", StringComparison.InvariantCulture):
					string stateChangePart = lineText.Split(':', 2)[1];
					string[] states = stateChangePart.Split("->", StringSplitOptions.TrimEntries);
					string beforeState = states[0];
					string afterState = states[1];
					
					GameUiState oldState = Enum.TryParse(beforeState[(beforeState.LastIndexOf('_')+1)..], true, out GameUiState state) ? state : GameUiState.Invalid;
					GameUiState newState = Enum.TryParse(afterState[(afterState.LastIndexOf('_')+1)..], true, out state) ? state : GameUiState.Invalid;

					OnUiStateChanged?.Invoke(new StateChanged(oldState, newState));
					break;
				case var _ when lineText.Contains("[Client] CL:  Connected to", StringComparison.InvariantCulture):
					OnServerConnected?.Invoke();
					break;
				case var _ when lineText.Contains("[Client] CL:  disconnect", StringComparison.InvariantCulture):
					OnServerDisconnected?.Invoke();
					break;
				case var _ when lineText.Contains("connected", StringComparison.InvariantCulture):
					int connectedIndex = lineText.IndexOf("connected", StringComparison.InvariantCulture);
					if (connectedIndex <= 0) return;
					// 15 is the length of the timestamp and space: "12/20 22:08:54 "
					string username = lineText[15..connectedIndex].Trim();
					OnPlayerConnected?.Invoke(username);
					break;
				case var _ when lineText.Contains("[Workshop] Downloading ugc", StringComparison.InvariantCulture):
					string actualData = lineText[15..];

					if (actualData == _downloadingUgcData) return;
					_downloadingUgcData = actualData;

					Match dataMatch = Regex.Match(actualData, "ugc.+\"(\\d+)\".+(\\d+).+\\/([\\d|.]+)");

					if (!dataMatch.Success) return;

					long addonId = long.Parse(dataMatch.Groups[1].Value, CultureInfo.InvariantCulture);

					double downloadedBytes = double.Parse(dataMatch.Groups[2].Value, CultureInfo.InvariantCulture);
					string downloadSizeStr = dataMatch.Groups[3].Value;

					double totalBytes = double.Parse(dataMatch.Groups[4].Value, CultureInfo.InvariantCulture);
					string totalSizeStr = dataMatch.Groups[5].Value;

					Size downloadSizeType = downloadSizeStr.ToLowerInvariant() switch
					{
						"kb" => Size.Kilobyte,
						"mb" => Size.Megabyte,
						"gb" => Size.Gigabyte,
						_ => Size.Byte,
					};

					Size totalSizeType = totalSizeStr.ToLowerInvariant() switch
					{
						"kb" => Size.Kilobyte,
						"mb" => Size.Megabyte,
						"gb" => Size.Gigabyte,
						_ => Size.Byte,
					};

					OnAddonProgress?.Invoke(new AddonProgress(
						addonId,
						new Progress(downloadedBytes, downloadSizeType),
						new Progress(totalBytes, totalSizeType)
					));

					if (Math.Abs(downloadedBytes - totalBytes) < 0.05) OnAddonFinished?.Invoke();

					break;
				case var _ when lineText.Contains("[Client] Sending connect to", StringComparison.InvariantCulture):
				case var _ when lineText.Contains(" Connecting to ", StringComparison.InvariantCulture):
					OnServerJoining?.Invoke();
					break;
				case var _ when lineText.Contains("[Client] Map: ", StringComparison.InvariantCulture):
					string mapName = lineText.Split(": ", 2)[1].Trim().Trim('"');
					OnMapJoined?.Invoke(mapName);
					break;

			}
		}

		private void ParseChatLine(string lineText, bool team)
		{
			string actualText = lineText[15..].Trim();
			Match chatMatch = ChatRegex().Match(actualText);

			string username = chatMatch.Groups[2].Value;
			string message = chatMatch.Groups[3].Value;

			bool dead = false;

			if (username.Contains("[DEAD]", StringComparison.InvariantCulture))
			{
				dead = true;
				username = username.Replace("[DEAD]", string.Empty, StringComparison.InvariantCulture).Trim();
			}
			if (team && !dead && username.Contains('﹫', StringComparison.InvariantCulture))
			{
				username = username[..username.LastIndexOf('﹫')];
			}
			
			if (team) OnTeamChatMessageReceived?.Invoke(new ChatMessage(username, message, dead));
			else OnGlobalChatMessageReceived?.Invoke(new ChatMessage(username, message, dead));
		}

		/// <summary>
		/// Starts monitoring the game console log and begins emitting console events.
		/// </summary>
		/// <remarks>
		/// This method must be called before any console-related events will fire.
		/// </remarks>
		public void StartListening()
		{
			_ = Task.Run(async () =>
			{
				try
				{
					while (!_cancellationTokenSource.IsCancellationRequested)
					{
						if (!File.Exists(_consoleLogPath)) continue;

						string logText = await ReadLogTextAsync().ConfigureAwait(false);
						if (logText.IsNullOrEmpty()) continue;

						string[] logLines = logText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

						if (_lastLineIndex > logLines.Length)
							_lastLineIndex = 0;

						for (int lineIndex = _lastLineIndex; lineIndex < logLines.Length; lineIndex++)
						{
							string lineText = logLines[lineIndex];
							if (lineText == _lastLineText) continue;

							try { ParseLineData(lineText); }
							catch { /**/ }
							
							_lastLineText = lineText;
						}

						_lastLineIndex = logLines.Length;
						_firstRun = false;
					}
				}
				catch (OperationCanceledException) { }
			});
		}

		private async Task<string> ReadLogTextAsync()
		{
			const int maxAttempts = 5;
			const int delayMilliseconds = 150;

			for (int attempt = 0; attempt < maxAttempts; attempt++)
			{
				try
				{
					File.Copy(_consoleLogPath, _strikeConsoleTmp, overwrite: true);

					FileStream fileStream = new(_strikeConsoleTmp, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, options: FileOptions.Asynchronous | FileOptions.SequentialScan);
					await using ConfiguredAsyncDisposable stream = fileStream.ConfigureAwait(false);
					using StreamReader streamReader = new (fileStream);
					string content = await streamReader.ReadToEndAsync().ConfigureAwait(false);

					return content;
				}
				catch (IOException) { await Task.Delay(delayMilliseconds, _cancellationTokenSource.Token).ConfigureAwait(false); }
				finally { try { File.Delete(_strikeConsoleTmp); } catch (IOException) {} }
			}

			return string.Empty;
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
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
		}

		[GeneratedRegex(@"^\[([^\]]+)\]\s+([^:]+):\s+(.+)$", RegexOptions.Singleline)]
		private static partial Regex ChatRegex();

	}
}
