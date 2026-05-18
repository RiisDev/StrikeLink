using StrikeLink.ChatBot;
using StrikeLink.Services.Config;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Channels;
// ReSharper disable AccessToDisposedClosure
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
	public partial class ConsoleService : IAsyncDisposable
	{
		private sealed record CommandRequest(string Command, ConsoleServiceConfig ConsoleServiceConfig, TaskCompletionSource<(string Message, bool Success)> Tcs);

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


		private const int MaxQueueDepth = 100;
		private const int RetryLimit = 10;
		private const int DelayAfterWrite = 250;
		private const int DelayPerRetry = 250;

		private readonly CancellationTokenSource? _cancellationTokenSource = new();

		private readonly Channel<CommandRequest> _commandChannel;
		private readonly Task _commandWorker;

		private readonly StatusData _statusData = new();
		private readonly ConsoleServiceConfig? _execCfg;
		private readonly string _consoleLogPath;
		private readonly string _chatCfgLocation;
		private readonly string _counterStrikePath;
		private string _downloadingUgcData = string.Empty;

		private int _lastLineIndex;
		private string? _lastLineText;
		private bool _firstRun = true;
		private bool _disposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleService"/> class.
		/// </summary>
		/// <param name="config">
		/// ConsoleServiceConfig record, only needs to be set if intending to use <see cref="SendConsoleCommand"/>.
		/// </param>
		/// <exception cref="DirectoryNotFoundException">
		/// Thrown when the Counter-Strike 2 installation directory cannot be located.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the game was not launched with the <c>-condebug</c> launch option.
		/// </exception>
		public ConsoleService(ConsoleServiceConfig? config = null)
		{
			if (!SteamService.TryGetGamePath(730, out string? counterStrikePath) || counterStrikePath.IsNullOrEmpty())
				throw new DirectoryNotFoundException("Failed to find CS:2 game directory.");
			
			_counterStrikePath = counterStrikePath;

			bool conDebug = SteamService.GetGameLaunchOptions(730).Any(x => x == "-condebug");
			if (!conDebug) throw new InvalidOperationException("CS:2 was not launched with -condebug, console log will not be available.");

			_consoleLogPath = Path.Combine(_counterStrikePath, "game", "csgo", "console.log");
			_chatCfgLocation = Path.Combine(_counterStrikePath, "game", "csgo", "cfg", "strike_link.cfg");
			_execCfg = config;

			BoundedChannelOptions channelOptions = new(MaxQueueDepth)
			{
				FullMode = BoundedChannelFullMode.Wait,
				SingleReader = true,
				SingleWriter = false,
			};

			_commandChannel = Channel.CreateBounded<CommandRequest>(channelOptions);
			_commandWorker = Task.Run(CommandWorkerLoop);

			StartListening();

			Thread.Sleep(1500);
		}

		/// <summary>
		/// Sends a console command by enqueuing a command request and asynchronously returns the response message and a
		/// success flag.
		/// </summary>
		/// <remarks>The command is enqueued to an internal channel and the returned task completes when the command
		/// processing signals its result.</remarks>
		/// <param name="command">Console command text to execute; must not be null, empty, or whitespace. Leading and trailing whitespace are
		/// trimmed.</param>
		/// <param name="config">Optional execution configuration; when null the instance configuration is used.</param>
		/// <param name="ct">Cancellation token to cancel the asynchronous operation.</param>
		/// <returns>A task that completes with a (Message, Success) tuple containing the response message and a boolean indicating
		/// success.</returns>
		/// <exception cref="InvalidOperationException">Thrown when neither the provided consoleServiceConfig nor the instance configuration is available.</exception>
		public async Task<(string Message, bool Success)> SendConsoleCommand(string command, ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			ConsoleServiceConfig requiredConsoleServiceConfig = config ?? _execCfg ?? throw new InvalidOperationException("SendConsoleCommand requires either constructor consoleServiceConfig or a parameter consoleServiceConfig.");

			CheckCs2UserConfig(requiredConsoleServiceConfig.Keybind);

			ArgumentException.ThrowIfNullOrWhiteSpace(command);

			TaskCompletionSource<(string Message, bool Success)> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

			CommandRequest request = new(command.Trim(), requiredConsoleServiceConfig, tcs);

			await _commandChannel.Writer.WriteAsync(request, ct).ConfigureAwait(false);

			return await tcs.Task.ConfigureAwait(false);
		}

		/// <summary>
		/// Sends the "status" console command and returns the next Cs2StatusMessage received in response.
		/// </summary>
		/// <remarks>Registers a temporary event handler, sends the "status" console command via SendConsoleCommand,
		/// awaits the response or cancellation, and unsubscribes the handler. The response wait is subject to a 5-second
		/// timeout.</remarks>
		/// <param name="config">Execution configuration to use; if null the instance configuration is used. Throws InvalidOperationException if
		/// neither is available.</param>
		/// <param name="ct">Cancellation token that can cancel the operation. The operation is also subject to a 5-second timeout combined
		/// with this token.</param>
		/// <returns>A task that completes with the next Cs2StatusMessage received in response to the sent command.</returns>
		/// <exception cref="InvalidOperationException">Thrown when no configuration is provided and no instance configuration is available.</exception>
		public async Task<Cs2StatusMessage> GetStatus(ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			using CancellationTokenSource timeoutCts = new(TimeSpan.FromSeconds(5));
			using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);

			TaskCompletionSource<Cs2StatusMessage> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

			CancellationTokenRegistration ctr = linkedCts.Token.Register(() => tcs.TrySetCanceled(linkedCts.Token));
			await using ConfiguredAsyncDisposable _ = ctr.ConfigureAwait(false);

			OnStatusMessage += OnOnStatusMessage;

			try
			{
				await SendConsoleCommand("status", config, ct).ConfigureAwait(false);
				return await tcs.Task.WaitAsync(linkedCts.Token).ConfigureAwait(false);
			}
			finally { OnStatusMessage -= OnOnStatusMessage; }

			void OnOnStatusMessage(Cs2StatusMessage msg) => tcs.TrySetResult(msg);
		}

		/// <summary>
		/// Executes a CS2 configuration file by validating its presence and issuing an exec
		/// console command.
		/// </summary>
		/// <remarks>Checks that the cfg file exists under game/csgo/cfg before sending the console command.</remarks>
		/// <param name="cfgName">Name of the configuration file to execute (including extension) located in the game's cfg directory.</param>
		/// <param name="config">Optional ConsoleServiceConfig to use; when null, the configured default is used.</param>
		/// <param name="ct">Cancellation token that can cancel the operation. The operation is also subject to a 5-second timeout combined
		/// with this token.</param>
		/// <returns>A Task that represents the asynchronous operation.</returns>
		/// <exception cref="InvalidOperationException">Thrown when no ConsoleServiceConfig is available from either the parameter or the configured default.</exception>
		/// <exception cref="FileNotFoundException">Thrown when the specified configuration file does not exist in the game's cfg directory.</exception>
		public async Task ExecuteCfgFile(string cfgName, ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			string desiredCfgPath = Path.Combine(_counterStrikePath, "game", "csgo", "cfg", cfgName);

			if (!File.Exists(desiredCfgPath))
				throw new FileNotFoundException($"{desiredCfgPath} does not exist.");

			await SendConsoleCommand($"exec {cfgName}", config, ct).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the name of the currently loaded map from the console status.
		/// </summary>
		/// <remarks>Retrieves status via GetStatus and selects the first spawn group with LoadType "mapload".
		/// Exceptions from status retrieval propagate to the caller; the operation honors the provided cancellation
		/// token.</remarks>
		/// <param name="config">Optional console service configuration used to retrieve status; when null, the default configuration is used.</param>
		/// <param name="ct">Cancellation token to observe while awaiting the operation.</param>
		/// <returns>A task that resolves to the current map name, or "N/A" if no map is found.</returns>
		public async Task<string> GetCurrentMap(ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			Cs2StatusMessage statusDump = await GetStatus(config, ct).ConfigureAwait(false);
			return statusDump.SpawnGroups.FirstOrDefault(x=> x.LoadType == "mapload")?.MapName ?? "N/A";
		}

		/// <summary>
		/// Gets the current local Steam user's ping from the CS2 status.
		/// </summary>
		/// <remarks>Identifies the local player via SteamService.GetLocalUsername and queries GetStatus to obtain the
		/// Players list.</remarks>
		/// <param name="config">Console service configuration used when fetching status, or null to use defaults.</param>
		/// <param name="ct">Cancellation token to cancel the asynchronous operation.</param>
		/// <returns>The local user's ping in milliseconds, or -1 if the user is not present in the status.</returns>
		public async Task<int> GetPing(ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			string currentUsername = SteamService.GetLocalUsername();
			Cs2StatusMessage statusDump = await GetStatus(config, ct).ConfigureAwait(false);
			return statusDump.Players.FirstOrDefault(x => x.Name == currentUsername)?.Ping ?? -1;
		}

		// Doesn't work with console log file
		private async Task<string> GetCvarValue(string name, ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name);

			if (!ValidCvarNameRegex().IsMatch(name))
				throw new InvalidOperationException("Invalid CVAR type");

			string consoleDetection = $"[Console] {name} =";
			using CancellationTokenSource timeoutCts = new(TimeSpan.FromSeconds(5));
			using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);

			TaskCompletionSource<string> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

			CancellationTokenRegistration ctr = linkedCts.Token.Register(() => tcs.TrySetCanceled(linkedCts.Token));
			await using ConfiguredAsyncDisposable _ = ctr.ConfigureAwait(false);

			OnLogReceived += OnLogData;

			try
			{
				await SendConsoleCommand(name, config, ct).ConfigureAwait(false);
				return await tcs.Task.WaitAsync(linkedCts.Token).ConfigureAwait(false);
			}
			finally { OnLogReceived -= OnLogData; }

			void OnLogData(string msg)
			{
				Debug.WriteLine($"CAUGHT_MSG: {msg}");
				if (msg.StartsWith(consoleDetection, StringComparison.InvariantCulture))
				{
					tcs.TrySetResult(msg[consoleDetection.Length..].Trim());
				}
			}
		}

		private async Task CommandWorkerLoop()
		{
			await foreach (CommandRequest req in _commandChannel.Reader.ReadAllAsync().ConfigureAwait(false))
			{
				(string Message, bool Success) result = await ExecuteCommand(req).ConfigureAwait(false);
				req.Tcs.TrySetResult(result);
			}
		}

		private async Task<(string Message, bool Success)> ExecuteCommand(CommandRequest req)
		{
			Process[] csProcesses = Process.GetProcessesByName("cs2");

			if (csProcesses.Length == 0)
			{
				Log("CS2 process not found, skipping call.");
				return ("CS2 process not found, skipping call.", false);
			}
			
			Process csProcess = csProcesses[0];

			if (csProcesses.Length > 1)
				Log($"Multiple CS2 processes found, targeting first found: {csProcess.Id}");
			
			await File.WriteAllTextAsync(_chatCfgLocation, req.Command).ConfigureAwait(false);
			await Task.Delay(DelayAfterWrite).ConfigureAwait(false);

			for (int retry = 0; retry < RetryLimit; retry++)
			{
				if (!NativeMethods.IsProcessActivated(csProcess))
				{
					if (req.ConsoleServiceConfig.ForceWindowActivation.HasValue && req.ConsoleServiceConfig.ForceWindowActivation.Value)
						NativeMethods.SetForegroundWindow(csProcess.MainWindowHandle);

					await Task.Delay(DelayPerRetry).ConfigureAwait(false);
					continue;
				}

				NativeMethods.PressKey(req.ConsoleServiceConfig.Keybind);
				Log($"Sent command '{req.Command}' after {retry} retries.");
				return ("", true);
			}

			Log("Failed to send command: CS2 window not focused.");
			return ("Failed to send command: CS2 window not focused.", false);
		}

		private void StartListening()
		{
			_ = Task.Run(async () =>
			{
				try
				{
					while (!_cancellationTokenSource?.IsCancellationRequested ?? false)
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
					return await FileReader.ReadFileAsync(_consoleLogPath).ConfigureAwait(false);
				}
				catch (IOException) { await Task.Delay(delayMilliseconds, _cancellationTokenSource?.Token ?? CancellationToken.None).ConfigureAwait(false); }
			}

			return string.Empty;
		}

		private void ParseLineData(string lineText)
		{
			if (_firstRun) return;

			OnLogReceived?.Invoke(lineText);

			lock (_statusData)
			{
				if (_statusData.IsStatusScanning)
					_ = _statusData.StatusBuilder.AppendLine(lineText);
			}

			switch (lineText)
			{
				case var _ when lineText.Contains("----- Status -----", StringComparison.InvariantCultureIgnoreCase):
					lock (_statusData)
					{
						_statusData.IsStatusScanning = true;
						_ = _statusData.StatusBuilder.AppendLine(lineText);
					}
					break;
				case var _ when lineText.Contains("[Client] #end", StringComparison.InvariantCultureIgnoreCase):
					lock (_statusData)
					{
						_statusData.IsStatusScanning = false;
						_ = _statusData.StatusBuilder.AppendLine(lineText);
					}
					ParseStatusData();
					break;
				case var _ when lineText.Contains("match_id=", StringComparison.InvariantCultureIgnoreCase):
					OnMatchPrompt?.Invoke();
					break;
				case var _ when lineText.Contains(" [ALL] ", StringComparison.InvariantCulture) && lineText.Contains(':', StringComparison.InvariantCulture):
					ParseChatLine(lineText, false);
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

					GameUiState oldState = Enum.TryParse(beforeState[(beforeState.LastIndexOf('_') + 1)..], true, out GameUiState state) ? state : GameUiState.Invalid;
					GameUiState newState = Enum.TryParse(afterState[(afterState.LastIndexOf('_') + 1)..], true, out state) ? state : GameUiState.Invalid;

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
				_ = _statusData.StatusBuilder.Clear();
				Log("Running Invoke");
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

		private sealed record Cs2ConfigCache(NativeMethods.VirtualKey Keybind, string ResolvedKey, string ConfigPath);
		private static Cs2ConfigCache? _cs2ConfigCache;

		internal static void CheckCs2UserConfig(NativeMethods.VirtualKey keybind)
		{
			Cs2ConfigCache? existing = Volatile.Read(ref _cs2ConfigCache);
			if (existing?.Keybind == keybind)
				return;

			long userId = SteamService.GetCurrentUserId();
			string steamPath = SteamService.GetSteamPath();
			string counterStrikeKeybindPath = Path.Combine(
				steamPath, "userdata", userId.ToString(CultureInfo.InvariantCulture), "730");

			if (!Directory.Exists(counterStrikeKeybindPath))
				throw new DirectoryNotFoundException(
					$"Failed to find user consoleServiceConfig path: {counterStrikeKeybindPath}");

			string localConfigPath = Path.Combine(counterStrikeKeybindPath, "local", "cfg");

			(bool localConfigReady, string localKeybind) = CheckLocalCs2Config(localConfigPath);

			if (!localConfigReady)
				throw new InvalidOperationException(
					"CS2 user keybind configuration not found. Please configure a keybind for Strike Link in CS2 settings.");

			if (localKeybind != NativeMethods.VirtualKeyToChar[keybind])
				throw new InvalidOperationException(
					$"CS2 user keybind configuration does not match the configured keybind '{keybind}'. " +
					$"Please update your CS2 keybind configuration accordingly.");

			Volatile.Write(ref _cs2ConfigCache, new Cs2ConfigCache(keybind, localKeybind, localConfigPath));
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

		/// <summary>
		/// Completes the command queue (no new commands accepted), waits for any
		/// in-flight commands to finish, then cancels the log listener.
		/// </summary>
		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;
			_disposed = true;

			_commandChannel.Writer.Complete();
			await _commandWorker.ConfigureAwait(false);

			try { if (_cancellationTokenSource is not null) await _cancellationTokenSource.CancelAsync().ConfigureAwait(false); }
			catch { /**/ }

			_cancellationTokenSource?.Dispose();

			GC.SuppressFinalize(this);
		}

		[GeneratedRegex(@"^\[([^\]]+)\]\s+([^:]+):\s+(.+)$", RegexOptions.Singleline)]
		private static partial Regex ChatRegex();


		[GeneratedRegex(@"^[a-z0-9_.]+$")]
		private static partial Regex ValidCvarNameRegex();
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
			string version = string.Empty;
			string build = string.Empty;
			string security = string.Empty;
			string visibility = string.Empty;
			string steamId = string.Empty;

			foreach (string line in stripped)
			{
				if (CurrentStateRegex().Match(line) is { Success: true } csMatch)
				{ currentState = csMatch.Groups[1].Value; continue; }

				if (SlotRegex().Match(line) is { Success: true } slotMatch)
				{ slot = int.Parse(slotMatch.Groups[1].Value, CultureInfo.InvariantCulture); continue; }

				if (VersionRegex().Match(line) is { Success: true } verMatch)
				{
					version = verMatch.Groups[1].Value;
					build = verMatch.Groups[2].Value;
					security = verMatch.Groups[3].Value;
					visibility = verMatch.Groups[4].Value;
					continue;
				}

				if (SteamIdRegex().Match(line) is { Success: true } steamMatch)
				{ steamId = steamMatch.Groups[1].Value; continue; }

				if (PlayersRegex().Match(line) is { Success: true } playersMatch)
				{
					int humans = int.Parse(playersMatch.Groups[1].Value, CultureInfo.InvariantCulture);
					int bots = int.Parse(playersMatch.Groups[2].Value, CultureInfo.InvariantCulture);
					int maxPlayers = int.Parse(playersMatch.Groups[3].Value, CultureInfo.InvariantCulture);
					bool hibernating = !playersMatch.Groups[4].Success;
					string reservation = playersMatch.Groups[5].Value;

					serverInfo = new ServerInfo(slot, version, build, security, visibility,
						steamId, humans, bots, maxPlayers, hibernating, reservation);
					continue;
				}

				if (line.Contains("spawngroups", StringComparison.OrdinalIgnoreCase))
				{ inSpawnGroups = true; inPlayers = false; continue; }

				if (line.Contains("---------players--------", StringComparison.OrdinalIgnoreCase))
				{ inPlayers = true; inSpawnGroups = false; continue; }

				if (inPlayers && line.TrimStart().StartsWith("id ", StringComparison.OrdinalIgnoreCase)) continue;

				if (line == "Official Valve Server") { serverTag = line; continue; }
				if (line == "#end") break;

				if (inSpawnGroups && SpawnGroupRegex().Match(line) is { Success: true } sgMatch)
				{
					List<string> flags = [];
					for (int i = 3; i <= 5; i++)
					{
						string flag = sgMatch.Groups[i].Value.Trim();
						if (!string.IsNullOrWhiteSpace(flag)) flags.Add(flag);
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
						time = new TimeSpan(0,
							int.Parse(parts[0], CultureInfo.InvariantCulture),
							int.Parse(parts[1], CultureInfo.InvariantCulture));
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
					players.Remove(player);
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