using System.Security.Principal;
#pragma warning disable CA1031

namespace StrikeLink.Services.WebService
{
	/// <summary>
	/// Provides functionality to retrieve a secure login token for authentication on supported operating systems.
	/// </summary>
	/// <remarks>On Windows, administrator privileges are required to retrieve the secure login token. On Linux, no
	/// special privileges are needed. The service is not supported on other operating systems.</remarks>
	public class LoginSecureService : IDisposable
	{
		/// <summary>
		/// Gets or sets the secure token used for authentication during login operations.
		/// </summary>
		public string LoginSecureToken { get; private set; }
		private readonly string _tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		/// <summary>
		/// Initializes a new instance of the LoginSecureService class and retrieves a secure login token for the current
		/// operating system.
		/// </summary>
		/// <remarks>On Windows, the constructor requires the process to be running with administrator privileges to
		/// retrieve the secure login token. On Linux, no special privileges are required. The constructor will throw an
		/// exception if called on an unsupported operating system.</remarks>
		/// <exception cref="InvalidOperationException">Thrown if the current process does not have administrator privileges on Windows, or if the operating system is not
		/// supported.</exception>
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

			if (OperatingSystem.IsLinux())
			{
				LoginSecureToken = GetLoginSecureLinux();
				return;
			}

			throw new InvalidOperationException("Unknown operating system found, cannot grab token.");
		}
		
		/// <inheritdoc />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the object and, optionally, releases the managed resources.
		/// </summary>
		/// <remarks>This method is called by public Dispose methods and the finalizer. When disposing is true, this
		/// method disposes all managed and unmanaged resources; when false, only unmanaged resources are released. Override
		/// this method to release resources specific to the derived class.</remarks>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing) return;
			try { Directory.Delete(_tempPath, true); } catch {/**/}
		}

		private string GetLoginSecureLinux()
		{
			if (!OperatingSystem.IsLinux()) throw new InvalidOperationException("How did you possibly see this normally.");

			string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

			string htmlCache = Path.Combine(homeDirectory, ".local", "share", "Steam", "config", "htmlcache");

			if (!Directory.Exists(htmlCache))
				throw new InvalidOperationException("Failed to find steam html cache");

			Directory.CreateDirectory(_tempPath);

			string cookiesFile = Path.Combine(htmlCache, "Default", "Cookies");
			string localStateFile = Path.Combine(htmlCache, "Local State");
			
			using SqliteReader reader = SqliteReader.Open(cookiesFile);
			using ChromiumCookieDecryptor decryptor = ChromiumCookieDecryptor.CreateFromLocalState(localStateFile);

			Dictionary<string, byte[]> encrypted = reader.GetEncryptedCookies(new Uri("https://steamcommunity.com"));
			Dictionary<string, string> plaintext = decryptor.DecryptAll(encrypted);

			string? loginSecure = plaintext.Values.FirstOrDefault(x => x.Contains('|', StringComparison.InvariantCulture));

			return loginSecure.IsNullOrEmpty() ? throw new InvalidOperationException("Failed to find loginSecure token") : loginSecure;
		}

		private string GetLoginSecureWindows()
		{
			if (!OperatingSystem.IsWindows()) 
				throw new InvalidOperationException("How did you possibly see this normally."); 

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
		}
	}
}
