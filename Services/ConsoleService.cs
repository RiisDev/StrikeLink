using System.Runtime.CompilerServices;

namespace StrikeLink.Services
{
	public class ConsoleService : IDisposable
	{
		private readonly CancellationTokenSource _cancellationTokenSource = new();

		public event Action<string>? OnLogReceived;

		private readonly string _consoleLogPath;
		private readonly string _strikeConsoleTmp;

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

		private void ParseLineData(string lineText)
		{
			OnLogReceived?.Invoke(lineText);
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

						for (int lineIndex = _lastLineIndex; lineIndex < logLines.Length; lineIndex++)
						{
							string lineText = logLines[lineIndex];
							if (lineText == _lastLineText) continue;
							ParseLineData(lineText);
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
