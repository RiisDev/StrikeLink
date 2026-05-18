using System.Runtime.CompilerServices;
namespace StrikeLink.Services
{
	internal class FileReader
	{
		internal static async Task<string> ReadFileAsync(string path)
		{
			const int maxAttempts = 5;

			if (!File.Exists(path))
				throw new FileNotFoundException($"{path} does not exist.");

			string tempFileName = Path.Combine(Path.GetTempPath(), $"strikelink_{Path.GetRandomFileName()}_{Path.GetFileName(path)}");

			for (int retry = 0; retry < maxAttempts; retry++)
			{
				try
				{
					File.Copy(path, tempFileName);
					FileStream fileStream = new(
						tempFileName,
						FileMode.Open,
						FileAccess.Read,
						FileShare.None,
						bufferSize: 4096,
						options: FileOptions.Asynchronous | FileOptions.SequentialScan | FileOptions.DeleteOnClose);

					await using ConfiguredAsyncDisposable stream = fileStream.ConfigureAwait(false);
					using StreamReader streamReader = new(fileStream, leaveOpen: true);
					string content = await streamReader.ReadToEndAsync().ConfigureAwait(false);
					return content;
				}
				catch { await Task.Delay(150).ConfigureAwait(false); }
			}

			throw new IOException($"Failed to read {path} after {maxAttempts} attempts.");
		}

		internal static string ReadFile(string path) => Task.Run(() => ReadFileAsync(path)).GetAwaiter().GetResult();
	}
}
