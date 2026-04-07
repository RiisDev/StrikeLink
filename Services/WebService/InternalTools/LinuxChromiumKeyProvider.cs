using System.Security.Cryptography;

namespace StrikeLink.Services.WebService.InternalTools
{
	internal sealed class LinuxChromiumKeyProvider
	{
		private static readonly byte[] PbkdfSalt = "saltysalt"u8.ToArray();
		private const int PbkdfIterations = 1;
		private const int KeyLengthBytes = 16;

		internal static byte[] DeriveKey(byte[] passwordBytes) => Rfc2898DeriveBytes.Pbkdf2(passwordBytes, PbkdfSalt, PbkdfIterations, HashAlgorithmName.SHA1, KeyLengthBytes);

		internal enum KeyringBackend { GnomeKeyring, KWallet, Fallback }

		internal sealed record DecryptionKeyResult(
			byte[] Key,
			KeyringBackend Backend,
			string BrowserName
		);

		internal static async Task<DecryptionKeyResult> GetKeyAsync(string browserName, CancellationToken cancellationToken = default)
		{
			string? gnomePassword = await TryGetGnomeKeyringPasswordAsync(browserName, cancellationToken).ConfigureAwait(false);
			if (!string.IsNullOrWhiteSpace(gnomePassword))
			{
				byte[] gnomeKey = DeriveKey(Encoding.UTF8.GetBytes(gnomePassword));
				return new DecryptionKeyResult(gnomeKey, KeyringBackend.GnomeKeyring, browserName);
			}

			string? kwalletRaw = await TryGetKWalletPasswordAsync(browserName, cancellationToken).ConfigureAwait(false);
			if (!string.IsNullOrWhiteSpace(kwalletRaw))
			{
				Log($"Raw Key: {kwalletRaw}");
				Log($"B64Decode: {kwalletRaw.FromBase64()}");
				byte[] kwalletKey = DeriveKey(Encoding.UTF8.GetBytes(kwalletRaw.FromBase64()));
				return new DecryptionKeyResult(kwalletKey, KeyringBackend.KWallet, browserName);
			}

			byte[] fallbackKey = DeriveKey("peanuts"u8.ToArray());
			return new DecryptionKeyResult(fallbackKey, KeyringBackend.Fallback, browserName);
		}
	
		private static async Task<string?> TryGetKWalletPasswordAsync(string browserName, CancellationToken cancellationToken) =>
			await RunProcessAsync(
				"kwallet-query",
				$"-r \"{browserName} Safe Storage\" -f \"Chromium Keys\" kdewallet",
				cancellationToken
			).ConfigureAwait(false);

		private static async Task<string?> TryGetGnomeKeyringPasswordAsync(string browserName, CancellationToken cancellationToken)
		{
			string? result = await RunProcessAsync(
				"secret-tool",
				$"lookup application \"{browserName} Keys\"",
				cancellationToken
			).ConfigureAwait(false);

			return !string.IsNullOrWhiteSpace(result) ? result : await RunProcessAsync(
				"secret-tool",
				$"lookup application \"{browserName} Safe Storage\"",
				cancellationToken
			).ConfigureAwait(false);
		}


		private static async Task<string?> RunProcessAsync(string executable, string arguments, CancellationToken cancellationToken)
		{
			try
			{
				ProcessStartInfo psi = new(executable, arguments)
				{
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true
				};

				using Process process = new();
				process.StartInfo = psi;
				process.Start();

				string output = await process.StandardOutput.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
				await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);

				string trimmed = output.Trim();
				return string.IsNullOrEmpty(trimmed) ? null : trimmed;
			}
			catch (Exception ex) when (
				ex is FileNotFoundException or UnauthorizedAccessException or OperationCanceledException)
			{
				return null;
			}
		}
	}
}