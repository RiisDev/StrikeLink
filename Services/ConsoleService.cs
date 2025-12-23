using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StrikeLink.Services
{
	public class ConsoleService : IDisposable
	{
		public enum GameUiState
		{
			LoadingScreen,
			InGame,
			PauseMenu,
			MainMenu,
			Invalid
		}

		public enum Size
		{
			Byte,
			Kilobyte,
			Megabyte,
			Gigabyte
		}

		public record ChatMessage(string Username, string Message, bool Dead);
		public record StateChanged(GameUiState OldState, GameUiState NewState);

		public record Progress(double Downloaded, Size SizeType);
		public record AddonProgress(long AddonId, Progress Downloaded, Progress Total);

		// Events
		public event Action<string>? OnLogReceived;
		public event Action<string>? OnPlayerConnected;
		public event Action<string>? OnMapJoined;

		public event Action<ChatMessage>? OnGlobalChatMessageReceived;
		public event Action<ChatMessage>? OnTeamChatMessageReceived;

		public event Action<StateChanged>? OnUiStateChanged;

		public event Action<AddonProgress>? OnAddonProgress;

		public event Action? OnAddonFinished;
		public event Action? OnServerJoining;
		public event Action? OnServerConnected;
		public event Action? OnServerDisconnected;

		// Fields
		private readonly CancellationTokenSource _cancellationTokenSource = new();

		private readonly string _consoleLogPath;
		private readonly string _strikeConsoleTmp;
		private string _downloadingUgcData = string.Empty;

		private int _lastLineIndex;
		private string? _lastLineText;

		public ConsoleService()
		{
			if (!SteamService.TryGetGamePath(730, out string? counterStrikePath) || counterStrikePath.IsNullOrEmpty())
				throw new DirectoryNotFoundException("Failed to find CS:2 game directory.");

			bool conDebug = SteamService.GetGameLaunchOptions(730).Any(x=> x == "-condebug");
			if (!conDebug) throw new InvalidOperationException("CS:2 was not launched with -condebug, console log will not be available.");

			_consoleLogPath = Path.Combine(counterStrikePath, "game", "csgo", "console.log");
			_strikeConsoleTmp = Path.Combine(Path.GetTempPath(), "console_tmp_strikelink.log");

			OnLogReceived?.Invoke("");
		}
		/*
		 *
		 * Note for future self:
		 * Can parse the status output by first checking if the lineText contains status, and then waiting for the last line that contains "end of status".
		 *
		 */
		private void ParseLineData(string lineText)
		{
			OnLogReceived?.Invoke(lineText);

			switch (lineText)
			{
				case var _ when lineText.Contains("[All]", StringComparison.InvariantCulture) && lineText.Contains(';', StringComparison.InvariantCulture):
					ParseChatLine(lineText,false);
					break;
				case var _ when lineText.Contains("[Team]", StringComparison.InvariantCulture) && lineText.Contains(';', StringComparison.InvariantCulture):
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
			string splitTypeData = team ? "Team" : "All";

			string[] splitData = lineText.Split([$"[{splitTypeData}]", ":"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			if (splitData.Length < 2) return;
			string username = splitData[0];
			string message = splitData[1];
			bool dead = false;

			if (username.Contains("[DEAD]", StringComparison.InvariantCulture))
			{
				dead = true;
				username = username.Replace("[DEAD]", string.Empty, StringComparison.InvariantCulture).Trim();
			}

			if (team) OnTeamChatMessageReceived?.Invoke(new ChatMessage(username, message, dead));
			else OnGlobalChatMessageReceived?.Invoke(new ChatMessage(username, message, dead));
		}

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


		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
		}
	}
}
