using StrikeLink.DemoParser.Parsing;

namespace StrikeLink.DemoParser
{
	/// <summary>
	/// Represents a downloaded CS2 replay payload.
	/// </summary>
	/// <param name="Stream">Readable stream positioned at the beginning of the downloaded content.</param>
	/// <param name="FileName">Suggested filename for persistence.</param>
	/// <param name="TempFilePath">Local temp path when persisted to disk; otherwise null.</param>
	/// <param name="IsCompressedBzip2">True when the payload is a .dem.bz2 archive.</param>
	public sealed record DemoDownloadResult(Stream Stream, string FileName, string? TempFilePath, bool IsCompressedBzip2);

	/// <summary>
	/// Downloads CS2 demo files directly from replay servers using a match share code.
	/// </summary>
	/// <remarks>
	/// This class does not use SteamKit or any external package. It uses the replay URL shape:
	/// https://replay{tvPort}.valve.net/730/{matchId}_{reservationId}.dem.bz2
	/// </remarks>
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

			return string.IsNullOrWhiteSpace(demoUrl) ? throw new InvalidOperationException("Failed to get demoUrl") : new Uri(demoUrl, UriKind.Absolute);
		}

		/// <summary>
		/// Downloads the replay into memory and returns a readable stream.
		/// </summary>
		public static async Task<DemoDownloadResult> DownloadToMemoryAsync(string shareCode, CancellationToken cancellationToken = default)
		{
			Uri replayUri = await BuildReplayUri(shareCode).ConfigureAwait(false);
			using HttpResponseMessage response = await SharedClient.GetAsync(replayUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
			await EnsureSuccessAsync(response).ConfigureAwait(false);

			MemoryStream memory = new();
			await using Stream networkStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
			await networkStream.CopyToAsync(memory, cancellationToken).ConfigureAwait(false);
			memory.Position = 0;

			return new DemoDownloadResult(
				memory,
				Path.GetFileName(replayUri.LocalPath),
				null,
				IsBzip2Replay(replayUri));
		}

		/// <summary>
		/// Downloads the replay into a temp file and returns a file stream.
		/// </summary>
		/// <remarks>
		/// Set <paramref name="deleteFileOnClose"/> to true when you only need ephemeral storage.
		/// </remarks>
		public static async Task<DemoDownloadResult> DownloadToTempFileAsync(string shareCode, bool deleteFileOnClose = false, CancellationToken cancellationToken = default)
		{
			Uri replayUri = await BuildReplayUri(shareCode).ConfigureAwait(false);
			using HttpResponseMessage response = await SharedClient.GetAsync(replayUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
			await EnsureSuccessAsync(response).ConfigureAwait(false);

			string extension = IsBzip2Replay(replayUri) ? ".dem.bz2" : ".dem";
			string tempFile = Path.Combine(Path.GetTempPath(), $"cs2_{Guid.NewGuid():N}{extension}");

			await using FileStream writeStream = new(tempFile, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
			await using Stream networkStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
			await networkStream.CopyToAsync(writeStream, cancellationToken).ConfigureAwait(false);

			FileOptions options = deleteFileOnClose ? FileOptions.DeleteOnClose : FileOptions.None;
			FileStream readStream = new(tempFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, options);
			Console.WriteLine($"TempFile:{tempFile}");
			return new DemoDownloadResult(readStream, Path.GetFileName(tempFile), tempFile, IsBzip2Replay(replayUri));
		}

		private static bool IsBzip2Replay(Uri replayUri)
			=> replayUri.AbsolutePath.EndsWith(".dem.bz2", StringComparison.OrdinalIgnoreCase);

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
