using StrikeLink.DemoParser.Parsing;
using StrikeLink.Extensions.BZip2;
#pragma warning disable CA2007

namespace StrikeLink.DemoParser
{
	/// <summary>
	/// Represents a downloaded CS2 replay payload.
	/// </summary>
	/// <param name="Stream">Readable stream positioned at the beginning of the decompressed .dem content.</param>
	/// <param name="FileName">Suggested filename for persistence (always .dem, never .dem.bz2).</param>
	/// <param name="TempFilePath">Local temp path when persisted to disk; otherwise null.</param>
	public sealed record DemoDownloadResult(Stream Stream, string FileName, string? TempFilePath) : IAsyncDisposable, IDisposable
	{
		private bool _disposed;

		/// <summary>
		/// Releases all resources used by the current instance, including the underlying stream and any associated temporary
		/// files.
		/// </summary>
		/// <remarks>Call this method when you are finished using the object to free unmanaged resources and delete
		/// any temporary files that may have been created. After calling this method, the object should not be
		/// used.</remarks>
		public void Dispose()
		{
			if (_disposed) return;
			_disposed = true;
			Stream.Dispose();
			if (TempFilePath is not null && File.Exists(TempFilePath))
				File.Delete(TempFilePath);
		}

		/// <summary>
		/// Asynchronously releases the unmanaged resources used by the object and deletes any associated temporary files.
		/// </summary>
		/// <remarks>Call this method to clean up resources when the object is no longer needed. After calling
		/// DisposeAsync, the object should not be used.</remarks>
		/// <returns>A ValueTask that represents the asynchronous dispose operation.</returns>
		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;
			_disposed = true;
			await Stream.DisposeAsync().ConfigureAwait(false);
			if (TempFilePath is not null && File.Exists(TempFilePath))
				File.Delete(TempFilePath);
		}
	}

	/// <summary>
	/// Downloads CS2 demo files directly from replay servers using a match share code.
	/// Bzip2-compressed payloads are transparently decompressed before being returned.
	/// </summary>
	public sealed class Cs2DemoDownloader
	{
		/// <summary>
		/// Builds the replay URL from a CS2 share code.
		/// </summary>
		public static async Task<Uri> BuildReplayUri(string shareCode)
		{
			if (string.IsNullOrWhiteSpace(shareCode))
				throw new ArgumentException("Share code cannot be empty.", nameof(shareCode));

			DemoShareCodeInfo decoded = DemoNetwork.DecodeShareCode(shareCode);
			string? demoUrl = await BoilerWriterRunner.GetDemoUrl(decoded.MatchId, decoded.ReservationId, decoded.TvPort).ConfigureAwait(false);
			return string.IsNullOrWhiteSpace(demoUrl)
				? throw new InvalidOperationException("Failed to get demoUrl")
				: new Uri(demoUrl, UriKind.Absolute);
		}

		/// <summary>
		/// Downloads the replay into memory, decompressing bzip2 if necessary, and returns a readable stream.
		/// </summary>
		public static async Task<DemoDownloadResult> DownloadToMemoryAsync(string shareCode, CancellationToken cancellationToken = default)
		{
			Uri replayUri = await BuildReplayUri(shareCode).ConfigureAwait(false);
			using HttpResponseMessage response = await SharedClient.GetAsync(replayUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
			await EnsureSuccessAsync(response).ConfigureAwait(false);

			bool isBzip2 = IsBzip2Replay(replayUri);
			string fileName = StripBzip2Extension(Path.GetFileName(replayUri.LocalPath));

			MemoryStream demStream = new();

			await using (Stream networkStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
			{
				if (isBzip2)
				{
					MemoryStream compressedBuffer = new();
					await networkStream.CopyToAsync(compressedBuffer, cancellationToken).ConfigureAwait(false);

					long downloadedBytes = compressedBuffer.Length;
					long? contentLength = response.Content.Headers.ContentLength;

					if (contentLength.HasValue && downloadedBytes != contentLength.Value)
					{
						throw new InvalidDataException($"Truncated download for {replayUri}: expected {contentLength.Value} bytes, got {downloadedBytes}.");
					}

					compressedBuffer.Position = 0;

					try
					{
						await Task.Run(() => BZip2.Decompress(compressedBuffer, demStream, false), cancellationToken).ConfigureAwait(false);
					}
					catch (EndOfStreamException ex)
					{
						throw new InvalidDataException(
							$"BZip2 decompression failed for {replayUri}. " +
							$"Downloaded {downloadedBytes} bytes" +
							(contentLength.HasValue ? $" of {contentLength.Value} expected" : " (no Content-Length header)") +
							$". First 4 bytes: {FormatHeader(compressedBuffer)}", ex);
					}
				}
				else
				{
					await networkStream.CopyToAsync(demStream, cancellationToken).ConfigureAwait(false);
				}
			}

			demStream.Position = 0;
			return new DemoDownloadResult(demStream, fileName, null);
		}

		private static string FormatHeader(MemoryStream ms)
		{
			byte[] buffer = ms.GetBuffer();
			int len = (int)Math.Min(ms.Length, 4);
			return len == 0 ? "<empty>" : BitConverter.ToString(buffer, 0, len);
		}

		/// <summary>
		/// Downloads the replay to a temp file, decompressing bzip2 if necessary, and returns a file stream.
		/// The <see cref="DemoDownloadResult"/> owns the temp file; dispose it to clean up.
		/// </summary>
		public static async Task<DemoDownloadResult> DownloadToTempFileAsync(string shareCode, CancellationToken cancellationToken = default)
		{
			Uri replayUri = await BuildReplayUri(shareCode).ConfigureAwait(false);
			using HttpResponseMessage response = await SharedClient.GetAsync(replayUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
			await EnsureSuccessAsync(response).ConfigureAwait(false);

			bool isBzip2 = IsBzip2Replay(replayUri);
			string fileName = StripBzip2Extension(Path.GetFileName(replayUri.LocalPath));

			string tempDemFile = Path.Combine(Path.GetTempPath(), $"cs2_{Guid.NewGuid():N}.dem");

			await using Stream networkStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

			if (isBzip2)
			{
				string tempBzip2File = Path.Combine(Path.GetTempPath(), $"cs2_{Guid.NewGuid():N}.dem.bz2");
				try
				{
					await using (FileStream bzip2WriteStream = new(tempBzip2File, FileMode.CreateNew, FileAccess.Write, FileShare.None))
					{
						await networkStream.CopyToAsync(bzip2WriteStream, cancellationToken).ConfigureAwait(false);
					} // flushed and closed before reading

					await using (FileStream bzip2ReadStream = new(tempBzip2File, FileMode.Open, FileAccess.Read, FileShare.Read))
					await using (FileStream demWriteStream = new(tempDemFile, FileMode.CreateNew, FileAccess.Write, FileShare.None))
					{
						await Task.Run(() => BZip2.Decompress(bzip2ReadStream, demWriteStream, false), cancellationToken).ConfigureAwait(false);
					} // both flushed and closed before handing off tempDemFile
				}
				finally
				{
					if (File.Exists(tempBzip2File))
						File.Delete(tempBzip2File);
				}
			}
			else
			{
				await using FileStream demWriteStream = new(tempDemFile, FileMode.CreateNew, FileAccess.Write, FileShare.None);
				await networkStream.CopyToAsync(demWriteStream, cancellationToken).ConfigureAwait(false);
			}

			FileStream readStream = new(tempDemFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None);
			return new DemoDownloadResult(readStream, fileName, tempDemFile);
		}

		private static bool IsBzip2Replay(Uri replayUri)
			=> replayUri.AbsolutePath.EndsWith(".dem.bz2", StringComparison.OrdinalIgnoreCase);

		private static string StripBzip2Extension(string fileName)
			=> fileName.EndsWith(".dem.bz2", StringComparison.OrdinalIgnoreCase)
				? fileName[..^4]
				: fileName;

		private static async Task EnsureSuccessAsync(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
				return;

			string body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			throw new HttpRequestException(
				$"Replay download failed with {(int)response.StatusCode} ({response.ReasonPhrase}). Body: {body}",
				null,
				response.StatusCode);
		}
	}
}