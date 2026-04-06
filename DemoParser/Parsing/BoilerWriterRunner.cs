using System.Reflection;
#pragma warning disable CA2007
#pragma warning disable CA1031

namespace StrikeLink.DemoParser.Parsing
{
	internal sealed class BoilerWriterRunner
	{
		public static async Task<string?> GetDemoUrl(ulong matchId, ulong outcomeId, long tokenId)
		{
			string tempDirPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}");
			string tempFileName = Path.Combine(tempDirPath, $"{Guid.NewGuid():N}");
			Directory.CreateDirectory(tempDirPath);

			try
			{
				ExtractResource("StrikeLink.DemoParser.EmbeddedResources.boiler.exe", "boiler.exe", tempDirPath);
				ExtractResource("StrikeLink.DemoParser.EmbeddedResources.steam_api64.dll", "steam_api64.dll", tempDirPath);
				await File.WriteAllTextAsync(Path.Combine(tempDirPath, "steam_appid.txt"), "730");

				using Process process = new();
				process.StartInfo = new ProcessStartInfo
				{
					FileName = Path.Combine(tempDirPath, "boiler.exe"),
					Arguments = $"\"{tempFileName}\" {matchId} {outcomeId} {tokenId}",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,
				};

				process.Start();

				Task<string> stdoutTask = process.StandardOutput.ReadToEndAsync();
				Task<string> stderrTask = process.StandardError.ReadToEndAsync();

				await process.WaitForExitAsync().ConfigureAwait(false);
				await Task.WhenAll(stdoutTask, stderrTask).ConfigureAwait(false);

				if (process.ExitCode != 0)
				{
					string stderr = await stderrTask.ConfigureAwait(false);
					throw new InvalidOperationException(
						$"boiler-writter exited with code {process.ExitCode}: {stderr}");
				}

				if (!File.Exists(tempFileName))
					throw new FileNotFoundException("boiler did not produce an output file.");

				byte[] protoBytes = await File.ReadAllBytesAsync(tempFileName).ConfigureAwait(false);

				return Encoding.ASCII.GetString(FindDemoUrl(protoBytes) ?? []);
			}
			finally { try { Directory.Delete(tempDirPath, true); } catch { /**/ } }
		}

		private static void ExtractResource(string resourceName, string fileName, string tempDir)
		{
			using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException($"Embedded resource not found: {resourceName}");
			using FileStream fileStream = File.Create(Path.Combine(tempDir, fileName));
			stream.CopyTo(fileStream);
		}

		private static byte[]? FindDemoUrl(byte[] data)
		{
			byte[] startPattern = "http"u8.ToArray();
			byte[] endPattern = ".dem.bz2"u8.ToArray();

			for (int i = 0; i <= data.Length - startPattern.Length; i++)
			{
				bool startMatch = !startPattern.Where((t, j) => data[i + j] != t).Any();
				if (!startMatch) continue;

				for (int k = i + startPattern.Length; k <= data.Length - endPattern.Length; k++)
				{
					bool endMatch = !endPattern.Where((t, j) => data[k + j] != t).Any();
					if (!endMatch) continue;
					int length = (k + endPattern.Length) - i;
					byte[] result = new byte[length];
					Array.Copy(data, i, result, 0, length);
					return result;
				}
			}

			return null;
		}
	}
}
