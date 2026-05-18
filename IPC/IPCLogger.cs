using StrikeLink.Services;
using System.Globalization;
using System.Text.RegularExpressions;
// ReSharper disable InconsistentlySynchronizedField
#pragma warning disable CA1003

namespace StrikeLink.IPC
{
	/// <summary>
	/// Represents a user used in inter-process communication, containing a Steam account identifier and a username.
	/// </summary>
	/// <remarks>Immutable record with value-based equality; intended as a compact data transfer object for
	/// IPC.</remarks>
	/// <param name="SteamId3">SteamId3 of the user</param>
	/// <param name="Username">Display name of the user.</param>
	public record IPCUser(int SteamId3, string Username);

	/// <summary>
	/// Configuration for IPC that specifies whether the Steam client should be started or restarted.
	/// </summary>
	/// <remarks>Immutable record used to convey Steam start and restart options over IPC.</remarks>
	/// <param name="StartSteam">Whether to start the Steam client if it is not running.</param>
	/// <param name="RestartSteam">Whether to restart the Steam client if it is already running.</param>
	public record IPCConfig(bool StartSteam = false, bool RestartSteam = false);

	/// <summary>
	/// Monitors Steam's ipc_SteamClient.log, raises OnLogReceived for each new log line, accumulates completed match
	/// segments, and exposes player lists parsed from the current segment or full session.
	/// </summary>
	/// <remarks>Starts a background reader task that watches the IPC log file and incrementally parses lines into
	/// match segments. Maintains an in-memory list of completed match segments and a mutable current match segment
	/// (accessed with internal synchronization). Can start or restart the Steam client when configured and requires IPC
	/// logging to be enabled. Implements IAsyncDisposable to cancel the background reader and release resources. Event
	/// handlers and public APIs may be invoked from background threads.</remarks>
	public partial class IPCLogger : IAsyncDisposable
	{
		/// <summary>
		/// Occurs when a new console log line is received.
		/// </summary>
		public event Action<string>? OnLogReceived;

		/// <summary>
		/// Gets a read-only list of match segments.
		/// </summary>
		/// <remarks>The returned IReadOnlyList is a read-only wrapper around the internal list; changes to the
		/// underlying collection are reflected in the returned view.</remarks>
		public IReadOnlyList<string> MatchSegments => _matchSegments.AsReadOnly();

		private readonly List<string> _matchSegments = [];
		private readonly CancellationTokenSource? _cancellationTokenSource = new();
		private readonly StringBuilder _currentMatchSegment = new();

		private int _lastLineIndex;

		private string? _lastLineText;
		private string _currentLogText = "";
		private readonly string _ipcLogPath;

		private bool _firstRun = true;
		private bool _inMatch;
		private bool _disposed;
		
		/// <summary>
		/// Initializes a new IPCLogger, ensures Steam is running with IPC logging enabled, removes any existing IPC log file,
		/// and begins asynchronous log reading.
		/// </summary>
		/// <remarks>Throws ArgumentNullException if config is null. May start or restart Steam synchronously based on
		/// config. Attempts to delete any existing IPC log file (errors ignored) and starts background log reading.</remarks>
		/// <param name="config">Configuration options that control whether to start or restart Steam and enable IPC logging.</param>
		/// <exception cref="InvalidOperationException">Thrown when Steam is not running and StartSteam is false, or when Steam is running without IPC enabled and
		/// RestartSteam is false.</exception>
		public IPCLogger(IPCConfig config)
		{
			ArgumentNullException.ThrowIfNull(config);

			Process? currentSteamProcess = GetCurrentSteamProcess();

			string steamPath = SteamService.GetSteamPath();
			_ipcLogPath = Path.Combine(steamPath, "logs", "ipc_SteamClient.log");

			if (File.Exists(_ipcLogPath)) try { File.Delete(_ipcLogPath); } catch {/**/}

			if (currentSteamProcess is null)
			{
				if (!config.StartSteam)
					throw new InvalidOperationException("Steam must be running, or StartSteam must be true");
				
				Task.Run(() => StartSteam()).GetAwaiter().GetResult();
			}
			
			if (currentSteamProcess is not null && !config.RestartSteam && !IsIPCLogEnabled())
			{
				if (!config.RestartSteam && !IsIPCLogEnabled())
					throw new InvalidOperationException("Steam is currently running without IPC enabled, to auto restart please configure RestartSteam = true");

				Task.Run(() => StartSteam(true)).GetAwaiter().GetResult();
			}
			
			_ = Task.Run(StartReading);
		}
		
		/// <summary>
		/// Parses the current match segment and returns a read-only list of IPCUser instances representing the players found.
		/// </summary>
		/// <remarks>Snapshots the segment under a lock and uses FilterTextRegex() to extract 'steamId' and
		/// 'playerName' groups. Steam IDs are parsed with CultureInfo.InvariantCulture and must be valid integers.</remarks>
		/// <returns>A read-only list of IPCUser instances parsed from the current match segment; an empty list when not currently in a
		/// match.</returns>
		public IReadOnlyList<IPCUser> GetPlayerListFromCurrentSegment()
		{
			List<IPCUser> ipcUsers = [];

			string currentSegment;
			lock (_currentMatchSegment) { currentSegment = _currentMatchSegment.ToString(); }

			if (!_inMatch) return [];

			MatchCollection regexResult = FilterTextRegex().Matches(currentSegment);
			foreach (Match match in regexResult)
			{
				if (!match.Success) continue;
				string steamId = match.Groups["steamId"].Value;
				string playerName = match.Groups["playerName"].Value;

				ipcUsers.Add(new IPCUser(int.Parse(steamId, CultureInfo.InvariantCulture), playerName));
			}

			return ipcUsers.AsReadOnly();
		}

		/// <summary>
		/// Retrieves a read-only list of IPCUser parsed from the current session log text.
		/// </summary>
		/// <remarks>Parses _currentLogText using FilterTextRegex(), expecting named capture groups "steamId" and
		/// "playerName". Steam IDs are parsed to int using CultureInfo.InvariantCulture. May throw FormatException or
		/// OverflowException if a steamId value is not a valid integer.</remarks>
		/// <returns>A read-only list of IPCUser parsed from the session; empty if no players are found.</returns>
		public IReadOnlyList<IPCUser> GetPlayerListFromSession()
		{
			List<IPCUser> ipcUsers = [];

			MatchCollection regexResult = FilterTextRegex().Matches(_currentLogText);
			
			foreach (Match match in regexResult)
			{
				if (!match.Success) continue;
				string steamId = match.Groups["steamId"].Value;
				string playerName = match.Groups["playerName"].Value;

				ipcUsers.Add(new IPCUser(int.Parse(steamId, CultureInfo.InvariantCulture), playerName));
			}

			return ipcUsers.AsReadOnly();
		}

		/// <summary>
		/// Asynchronously releases managed resources by canceling and disposing the internal CancellationTokenSource and
		/// suppressing finalization.
		/// </summary>
		/// <remarks>Idempotent: subsequent calls have no effect. Exceptions thrown during cancellation are
		/// ignored.</remarks>
		/// <returns>A ValueTask that represents the asynchronous disposal operation.</returns>
		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;
			_disposed = true;
			
			try { if (_cancellationTokenSource is not null) await _cancellationTokenSource.CancelAsync().ConfigureAwait(false); }
			catch { /**/ }

			_cancellationTokenSource?.Dispose();

			GC.SuppressFinalize(this);
		}


		private async Task StartSteam(bool killProcesses = false)
		{
			if (killProcesses)
			{
				Process.GetProcessesByName("steam").ToList().ForEach(x => x.Kill(true));
				await Task.Delay(250).ConfigureAwait(false);

				if (File.Exists(_ipcLogPath)) try { File.Delete(_ipcLogPath); } catch {/**/}
			}

			ProcessStartInfo psi = new()
			{
				WorkingDirectory = SteamService.GetSteamPath(),
				FileName = "steam.exe",
				ArgumentList = { "+ipc_log 730", "-console" }
			};

			Process.Start(psi);

			await Task.Delay(500).ConfigureAwait(false);
		}

		private static bool IsIPCLogEnabled()
		{
			string steamPath = SteamService.GetSteamPath();
			string bootstrapPath = Path.Combine(steamPath, "logs", "bootstrap_log.txt");

			if (!File.Exists(bootstrapPath))
				throw new FileNotFoundException($"Failed to find bootstrap_log.txt at {bootstrapPath}");

			string fileText = FileReader.ReadFile(bootstrapPath);
			string[] segments = fileText.Split("Startup - updater", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			return segments[^1].Contains("+log_ipc cs2", StringComparison.InvariantCulture);
		}

		private static Process? GetCurrentSteamProcess()
		{
			Process[] processes = Process.GetProcessesByName("steam");
			return processes.Length > 1 ? throw new InvalidOperationException("Only one steam client can be running at a time.") : processes.FirstOrDefault();
		}

		private async Task StartReading()
		{
			try
			{
				while (!_cancellationTokenSource?.IsCancellationRequested ?? false)
				{
					if (!File.Exists(_ipcLogPath)) continue;

					_currentLogText = await FileReader.ReadFileAsync(_ipcLogPath).ConfigureAwait(false);
					if (_currentLogText.IsNullOrEmpty()) continue;

					string[] logLines = _currentLogText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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

					if (_firstRun)
					{
						lock (_currentMatchSegment)
						{
							ExtractCurrentSegment(logLines);
						}
						_firstRun = false;
					}
				}
			}
			catch (OperationCanceledException) { }
		}


		private void ExtractCurrentSegment(string[] logLines)
		{
			int segmentStartIndex = -1;
			for (int lineIndex = logLines.Length - 1; lineIndex >= 0; lineIndex--)
			{
				if (!logLines[lineIndex].Contains("IClientTimeline::SetTimelineGameMode( 1, )", StringComparison.InvariantCulture)) continue;
				segmentStartIndex = lineIndex;
				break;
			}

			if (segmentStartIndex == -1) return;

			// Check if the match was already ended after the segment started
			bool matchEnded = false;
			for (int lineIndex = segmentStartIndex + 1; lineIndex < logLines.Length; lineIndex++)
			{
				if (!logLines[lineIndex].Contains("IClientTimeline::SetTimelineGameMode( 3, )", StringComparison.InvariantCulture)) continue;
				matchEnded = true;
				break;
			}

			_currentMatchSegment.Clear();

			if (matchEnded)
			{
				_inMatch = false;
				return;
			}

			for (int lineIndex = segmentStartIndex; lineIndex < logLines.Length; lineIndex++)
				_currentMatchSegment.AppendLine(logLines[lineIndex]);

			_inMatch = true;
		}

		private void ParseLineData(string lineText)
		{
			if (_firstRun) return;

			OnLogReceived?.Invoke(lineText);

			switch (lineText)
			{
				case var _ when lineText.Contains("IClientTimeline::SetTimelineGameMode( 1, )", StringComparison.InvariantCulture):
					_currentMatchSegment.Clear();
					_inMatch = true;
					_currentMatchSegment.AppendLine(lineText);
					break;
				case var _ when lineText.Contains("IClientTimeline::SetTimelineGameMode( 3, )", StringComparison.InvariantCulture):
					_currentMatchSegment.AppendLine(lineText);
					if (!_currentMatchSegment.ToString().IsNullOrEmpty())
						_matchSegments.Add(_currentMatchSegment.ToString());
					_currentMatchSegment.Clear();
					_inMatch = false;
					break;
				default:
					if (_inMatch)
						_currentMatchSegment.AppendLine(lineText);
					break;
			}
		}

		[GeneratedRegex("FilterText\\( 730, 3, \\[U:1:(?<steamId>\\d+)\\], \"(?<playerName>[^\"]+)\", \\d+, \\)", RegexOptions.Compiled)]
		private partial Regex FilterTextRegex();
	}
}
