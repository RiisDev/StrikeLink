
using System.Security.Principal;

namespace StrikeLink.Services.WebService
{
	public class LoginSecureService : IDisposable
	{
		public string LoginSecureToken;
		private readonly string _tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		public LoginSecureService()
		{
			if (OperatingSystem.IsWindows())
			{
				using WindowsIdentity identity = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new (identity);
				if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
				{
					throw new InvalidOperationException("To grab LoginSecure you must be running as admin.");
				}

				LoginSecureToken = GetLoginSecureWindows();
				return;
			}

			LoginSecureToken = "";
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			try { Directory.Delete(_tempPath, true); } catch {/**/}
		}

		private string GetLoginSecureWindows()
		{
			if (!OperatingSystem.IsWindows()) throw new InvalidOperationException("How did you possibly see this normally."); ;

#if WINDOWS

			Directory.CreateDirectory(_tempPath);

			string cookies = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Steam\htmlcache\Default\Network\Cookies");
			string cookiesFile = Path.Combine(_tempPath, Path.GetFileName(cookies));
			string localState = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Steam\htmlcache\Local State");
			string localStateFile = Path.Combine(_tempPath, Path.GetFileName(localState));

			ShadowCopyService.CopyFilesViaShadowCopy([cookies, localState], _tempPath);

			using SqliteReader reader = SqliteReader.Open(cookiesFile);
			using ChromiumCookieDecryptor decryptor = ChromiumCookieDecryptor.CreateFromLocalState(localStateFile);

			Dictionary<string, byte[]> encrypted = reader.GetEncryptedCookies(new Uri("https://steamcommunity.com"));
			Dictionary<string, string> plaintext = decryptor.DecryptAll(encrypted);

			string? loginSecure = plaintext.Values.FirstOrDefault(x => x.Contains('|', StringComparison.InvariantCulture));
			
			return loginSecure.IsNullOrEmpty() ? throw new InvalidOperationException("Failed to find loginSecure token") : loginSecure;
#else
			throw new InvalidOperationException("How did you possibly see this normally.");
#endif
		}
	}
}
