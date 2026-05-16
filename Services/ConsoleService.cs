using StrikeLink.ChatBot;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
#pragma warning disable CA1031
#pragma warning disable CA1308
#pragma warning disable CA1003
#pragma warning disable CA1034

namespace StrikeLink.Services
{
	internal class StatusData
	{
		public bool IsStatusScanning { get; set; }
		public StringBuilder StatusBuilder { get; set; } = new();
	}

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
		/// Occurs when a status message is available.
		/// </summary>
		/// <remarks>Subscribers receive an object containing status information. The specific type and content of the
		/// object depend on the context in which the event is raised. Handlers should verify the type of the event argument
		/// before use.</remarks>
		public event Action<Cs2StatusMessage>? OnStatusMessage;

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

		/// <summary>
		/// Occurs when the client receives 'Match Found' screen.
		/// </summary>
		public event Action? OnMatchPrompt;

		// Fields
		private readonly CancellationTokenSource _cancellationTokenSource = new();

		private readonly StatusData _statusData = new();
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
		
		private void ParseLineData(string lineText)
		{
			if (_firstRun) return;

			//Debug.WriteLine(lineText);
			OnLogReceived?.Invoke(lineText);

			lock (_statusData)
			{
				if (_statusData.IsStatusScanning)
				{
					_statusData.StatusBuilder.AppendLine(lineText);
				}
			}

			switch (lineText)
			{
				case var _ when lineText.Contains("----- Status -----", StringComparison.InvariantCultureIgnoreCase):
					lock (_statusData)
					{
						_statusData.IsStatusScanning = true;
						_statusData.StatusBuilder.AppendLine(lineText);
					}
					break;
				case var _ when lineText.Contains("[Client] #end", StringComparison.InvariantCultureIgnoreCase):
					lock (_statusData)
					{
						_statusData.IsStatusScanning = false;
						_statusData.StatusBuilder.AppendLine(lineText);
					}
					ParseStatusData();
					break;
				case var _ when lineText.Contains("match_id=", StringComparison.InvariantCultureIgnoreCase):
					OnMatchPrompt?.Invoke();
					break;
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

		private void ParseStatusData()
		{
			lock (_statusData)
			{
				string statusText = _statusData.StatusBuilder.ToString();
				_statusData.StatusBuilder.Clear();
				OnStatusMessage?.Invoke(Cs2StatusParser.Parse(statusText));
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
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases all resources used by the <see cref="ChatService"/>.
		/// </summary>
		/// <remarks>
		/// This method suppresses finalization and disposes managed resources.
		/// </remarks>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing) return;
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
		}


		[GeneratedRegex(@"^\[([^\]]+)\]\s+([^:]+):\s+(.+)$", RegexOptions.Singleline)]
		private static partial Regex ChatRegex();

	}

	/// <summary>
	/// Represents a snapshot of the current status of a CS2 server, including server information, player data, and spawn
	/// group details.
	/// </summary>
	/// <param name="Timestamp">The date and time when the status message was generated.</param>
	/// <param name="CurrentState">The current operational state of the server, such as running, paused, or stopped.</param>
	/// <param name="Server">Information about the server, including its configuration and status.</param>
	/// <param name="SpawnGroups">A read-only list of spawn group details present on the server at the time of the status message.</param>
	/// <param name="Players">A read-only list of player entries representing all players currently connected to the server.</param>
	/// <param name="ServerTag">A tag or identifier associated with the server, used for categorization or filtering.</param>
	public record Cs2StatusMessage(
		DateTimeOffset Timestamp,
		string CurrentState,
		ServerInfo Server,
		IReadOnlyList<SpawnGroup> SpawnGroups,
		IReadOnlyList<PlayerEntry> Players,
		string ServerTag
	);

	/// <summary>
	/// Represents information about a game server, including its version, player counts, status, and reservation details.
	/// </summary>
	/// <param name="Slot">The slot number assigned to the server instance.</param>
	/// <param name="Version">The version string of the server software.</param>
	/// <param name="BuildNumber">The build number identifying the specific server build.</param>
	/// <param name="SecurityMode">The security mode in which the server is operating.</param>
	/// <param name="Visibility">The visibility status of the server, indicating whether it is public or private.</param>
	/// <param name="SteamId">The unique Steam identifier associated with the server.</param>
	/// <param name="HumanCount">The number of human players currently connected to the server.</param>
	/// <param name="BotCount">The number of bot players currently present on the server.</param>
	/// <param name="MaxPlayers">The maximum number of players that the server supports.</param>
	/// <param name="IsHibernating">A value indicating whether the server is currently in a hibernating state.</param>
	/// <param name="ReservationId">The reservation identifier associated with the server session, if any.</param>
	public record ServerInfo(
		int Slot,
		string Version,
		string BuildNumber,
		string SecurityMode,
		string Visibility,
		string SteamId,
		int HumanCount,
		int BotCount,
		int MaxPlayers,
		bool IsHibernating,
		string ReservationId
	);

	/// <summary>
	/// Represents a group of spawn entities with associated metadata for a specific map and lump type.
	/// </summary>
	/// <param name="Id">The unique identifier for the spawn group.</param>
	/// <param name="MapName">The name of the map to which this spawn group belongs. Cannot be null.</param>
	/// <param name="LumpType">The type of lump associated with this spawn group. Cannot be null.</param>
	/// <param name="LoadType">The load type that determines how the spawn group is processed. Cannot be null.</param>
	/// <param name="Flags">A read-only list of flags that define additional properties or behaviors for the spawn group. Cannot be null.</param>
	public record SpawnGroup(
		int Id,
		string MapName,
		string LumpType,
		string LoadType,
		IReadOnlyList<string> Flags
	);

	/// <summary>
	/// Represents a player entry containing connection and status information for a player in a session.
	/// </summary>
	/// <param name="Id">The unique identifier for the player entry.</param>
	/// <param name="Channel">The name of the channel to which the player is connected, or null if not assigned.</param>
	/// <param name="TimeConnected">The duration for which the player has been connected.</param>
	/// <param name="Ping">The current network ping for the player, measured in milliseconds.</param>
	/// <param name="Loss">The percentage of packet loss experienced by the player.</param>
	/// <param name="State">The current connection or activity state of the player.</param>
	/// <param name="Rate">The data transfer rate allocated to the player, in bytes per second.</param>
	/// <param name="Name">The display name of the player.</param>
	/// <param name="IsBot">true if the player is a bot; otherwise, false.</param>
	public record PlayerEntry(
		int Id,
		string? Channel,
		TimeSpan TimeConnected,
		int Ping,
		int Loss,
		string State,
		int Rate,
		string Name,
		bool IsBot
	);


	internal static partial class Cs2StatusParser
	{
		[GeneratedRegex(@"^\d{2}/\d{2} \d{2}:\d{2}:\d{2} \[\w+\] ", RegexOptions.Compiled)]
		private static partial Regex LogPrefixRegex();

		[GeneratedRegex(@"@\s+Current\s+:\s+(\S+)", RegexOptions.Compiled)]
		private static partial Regex CurrentStateRegex();

		[GeneratedRegex(@"source\s+:\s+slot\s+(\d+)", RegexOptions.Compiled)]
		private static partial Regex SlotRegex();

		[GeneratedRegex(@"version\s+:\s+([\d.]+/\d+)\s+(\d+)\s+(\w+)\s+(\w+)", RegexOptions.Compiled)]
		private static partial Regex VersionRegex();

		[GeneratedRegex(@"steamid\s+:\s+(\[[^\]]+\])", RegexOptions.Compiled)]
		private static partial Regex SteamIdRegex();

		[GeneratedRegex(@"players\s+:\s+(\d+)\s+humans,\s+(\d+)\s+bots\s+\((\d+)\s+max\)\s+\((not )?hibernating\)(?:\s+\(reserved\s+([0-9a-f]+)\))?", RegexOptions.Compiled)]
		private static partial Regex PlayersRegex();

		[GeneratedRegex(@"loaded spawngroup\(\s*(\d+)\)\s+:\s+SV:\s+\[\d+:\s+([^|]+?)\s*\|\s*([^|]+?)\s*\|\s*([^|\]]+?)(?:\s*\|\s*([^\]]+))?\]", RegexOptions.Compiled)]
		private static partial Regex SpawnGroupRegex();

		[GeneratedRegex(@"^\s*(\d+)\s+(?:\[(\w+)\]|(\d+:\d+)|BOT)\s+(\d+)\s+(\d+)\s+(\w+)\s+(\d+)\s+'([^']*)'", RegexOptions.Compiled)]
		private static partial Regex PlayerRegex();

		public static Cs2StatusMessage Parse(string rawLog)
		{
			string[] lines = rawLog.Split('\n', StringSplitOptions.RemoveEmptyEntries);

			string[] stripped = lines
				.Select(l => LogPrefixRegex().Replace(l.Trim(), string.Empty))
				.ToArray();

			DateTimeOffset timestamp = ParseTimestamp(lines[0]);
			string currentState = string.Empty;
			ServerInfo? serverInfo = null;
			List<SpawnGroup> spawnGroups = [];
			List<PlayerEntry> players = [];
			string serverTag = string.Empty;

			bool inPlayers = false;
			bool inSpawnGroups = false;

			int slot = 0;
			string version = string.Empty, build = string.Empty, security = string.Empty, visibility = string.Empty;
			string steamId = string.Empty;

			foreach (string line in stripped)
			{
				if (CurrentStateRegex().Match(line) is { Success: true } csMatch)
				{
					currentState = csMatch.Groups[1].Value;
					continue;
				}

				if (SlotRegex().Match(line) is { Success: true } slotMatch)
				{
					slot = int.Parse(slotMatch.Groups[1].Value, CultureInfo.InvariantCulture);
					continue;
				}

				if (VersionRegex().Match(line) is { Success: true } verMatch)
				{
					version = verMatch.Groups[1].Value;
					build = verMatch.Groups[2].Value;
					security = verMatch.Groups[3].Value;
					visibility = verMatch.Groups[4].Value;
					continue;
				}

				if (SteamIdRegex().Match(line) is { Success: true } steamMatch)
				{
					steamId = steamMatch.Groups[1].Value;
					continue;
				}

				if (PlayersRegex().Match(line) is { Success: true } playersMatch)
				{
					int humans = int.Parse(playersMatch.Groups[1].Value, CultureInfo.InvariantCulture);
					int bots = int.Parse(playersMatch.Groups[2].Value, CultureInfo.InvariantCulture);
					int maxPlayers = int.Parse(playersMatch.Groups[3].Value, CultureInfo.InvariantCulture);
					bool hibernating = !playersMatch.Groups[4].Success;
					string reservation = playersMatch.Groups[5].Value;

					serverInfo = new ServerInfo(
						slot, version, build, security, visibility,
						steamId, humans, bots, maxPlayers, hibernating, reservation
					);
					continue;
				}

				if (line.Contains("spawngroups", StringComparison.OrdinalIgnoreCase))
				{
					inSpawnGroups = true;
					inPlayers = false;
					continue;
				}

				if (line.Contains("---------players--------", StringComparison.OrdinalIgnoreCase))
				{
					inPlayers = true;
					inSpawnGroups = false;
					continue;
				}

				if (inPlayers && line.TrimStart().StartsWith("id ", StringComparison.OrdinalIgnoreCase))
					continue;

				if (line == "Official Valve Server")
				{
					serverTag = line;
					continue;
				}

				if (line == "#end")
					break;

				if (inSpawnGroups && SpawnGroupRegex().Match(line) is { Success: true } sgMatch)
				{
					List<string> flags = [];

					for (int i = 3; i <= 5; i++)
					{
						string flag = sgMatch.Groups[i].Value.Trim();
						if (!string.IsNullOrWhiteSpace(flag))
							flags.Add(flag);
					}

					spawnGroups.Add(new SpawnGroup(
						Id: int.Parse(sgMatch.Groups[1].Value, CultureInfo.InvariantCulture),
						MapName: sgMatch.Groups[2].Value.Trim(),
						LumpType: flags.Count > 0 ? flags[0] : string.Empty,
						LoadType: flags.Count > 1 ? flags[1] : string.Empty,
						Flags: flags.Count > 2 ? flags[2..] : []
					));
					continue;
				}

				if (inPlayers && PlayerRegex().Match(line) is { Success: true } pMatch)
				{
					int id = int.Parse(pMatch.Groups[1].Value, CultureInfo.InvariantCulture);
					string? channel = pMatch.Groups[2].Success ? pMatch.Groups[2].Value : null;
					bool isBot = line.Contains("BOT", StringComparison.Ordinal) && !pMatch.Groups[3].Success;
					TimeSpan time = TimeSpan.Zero;

					if (pMatch.Groups[3].Success)
					{
						string[] parts = pMatch.Groups[3].Value.Split(':');
						time = new TimeSpan(0, int.Parse(parts[0], CultureInfo.InvariantCulture), int.Parse(parts[1], CultureInfo.InvariantCulture));
					}

					players.Add(new PlayerEntry(
						Id: id,
						Channel: channel,
						TimeConnected: time,
						Ping: int.Parse(pMatch.Groups[4].Value, CultureInfo.InvariantCulture),
						Loss: int.Parse(pMatch.Groups[5].Value, CultureInfo.InvariantCulture),
						State: pMatch.Groups[6].Value,
						Rate: int.Parse(pMatch.Groups[7].Value, CultureInfo.InvariantCulture),
						Name: pMatch.Groups[8].Value,
						IsBot: isBot
					));
				}
			}

			foreach (PlayerEntry player in players.ToList())
			{
				if (player is { Channel: "NoChan", State: "challenging" })
				{
					players.Remove(player);
				}
			}

			return new Cs2StatusMessage(
				Timestamp: timestamp,
				CurrentState: currentState,
				Server: serverInfo ?? throw new FormatException("Server info block not found."),
				SpawnGroups: spawnGroups.AsReadOnly(),
				Players: players.AsReadOnly(),
				ServerTag: serverTag
			);
		}

		private static DateTimeOffset ParseTimestamp(string firstLine)
		{
			ReadOnlySpan<char> span = firstLine.AsSpan(0, 14);
			int month = int.Parse(span[..2], CultureInfo.InvariantCulture);
			int day = int.Parse(span[3..5], CultureInfo.InvariantCulture);
			int hour = int.Parse(span[6..8], CultureInfo.InvariantCulture);
			int minute = int.Parse(span[9..11], CultureInfo.InvariantCulture);
			int second = int.Parse(span[12..14], CultureInfo.InvariantCulture);
			return new DateTimeOffset(DateTimeOffset.UtcNow.Year, month, day, hour, minute, second, TimeSpan.Zero);
		}
	}
}
