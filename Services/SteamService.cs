using Microsoft.Win32;
using StrikeLink.Services.Config;
using System.Globalization;
using System.Runtime.InteropServices;

// ReSharper disable InvertIf
#pragma warning disable IDE0046

namespace StrikeLink.Services
{
	/// <summary>
	/// Provides utility methods for interacting with the local Steam installation,
	/// including resolving install paths, game locations, user configuration, and launch options.
	/// </summary>
	public static class SteamService
	{
		private const string FlatPackLinux = "~/.var/app/com.valvesoftware.Steam/.local/share/Steam/";
		private const string BaseLinuxInstall = "~/.local/share/Steam/";
		private const string SteamSubKey = @"Software\Valve\Steam";

		/// <summary>
		/// Gets the root installation path of Steam for the current operating system.
		/// </summary>
		/// <returns>
		/// The absolute path to the Steam installation directory.
		/// </returns>
		/// <exception cref="DirectoryNotFoundException">
		/// Thrown when Steam cannot be located.
		/// </exception>
		/// <exception cref="KeyNotFoundException">
		/// Thrown when the Steam registry key or path value cannot be found on Windows.
		/// </exception>
		/// <exception cref="FileNotFoundException">
		/// Thrown when the operating system is unsupported.
		/// </exception>
		public static string GetSteamPath()	
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				if (Directory.Exists(FlatPackLinux))
					return FlatPackLinux;

				if (Directory.Exists(BaseLinuxInstall))
					return BaseLinuxInstall;

				throw new DirectoryNotFoundException("Failed to find steam location");
			}

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
			{
				using RegistryKey? key = Registry.CurrentUser.OpenSubKey(SteamSubKey);
				if (key == null) throw new KeyNotFoundException($"{key} could not be found.");

				object? value = key.GetValue("SteamPath");
				return value as string ?? throw new KeyNotFoundException("Steam path key could not be found.");
			}

			throw new FileNotFoundException("Unable to automatically find steam, (OS_NOT_WINDOWS_LINUX)");
		}

		/// <summary>
		/// Attempts to locate the installation directory of a Steam game.
		/// </summary>
		/// <param name="gameId">
		/// The Steam application ID of the game.
		/// </param>
		/// <param name="gamePath">
		/// When this method returns, contains the absolute path to the game directory if found;
		/// otherwise, <c>null</c>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the game was found in a Steam library folder; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="FileNotFoundException">
		/// Thrown when the Steam library configuration file cannot be found.
		/// </exception>
		public static bool TryGetGamePath(int gameId, out string? gamePath)
		{
			string vdfPath = Path.Combine(GetSteamPath(), "steamapps", "libraryfolders.vdf");

			if (!File.Exists(vdfPath)) throw new FileNotFoundException($"Failed to find libraryfolders.vdf at {vdfPath}");

			ValveCfgReader reader = new(vdfPath);

			foreach (KeyValuePair<string, ConfigNode> node in reader.Document.Root.EnumerateObject())
			{
				bool appsFound = node.Value.TryGetProperty("apps", out ConfigNode appsNode);
				if (!appsFound) continue;

				bool hasGameId = appsNode.TryGetProperty(gameId.ToString(CultureInfo.InvariantCulture), out ConfigNode _);
				if (!hasGameId) continue;

				string path = node.Value.GetProperty("path").GetString();
				path = path.Replace(@"\\", @"\", StringComparison.InvariantCulture);
				
				ValveCfgReader subReader = new(Path.Combine(path, "steamapps", $"appmanifest_{gameId}.acf"));

				string installDir = subReader.Document.Root.GetProperty("installdir").GetString();
				string gameInstallDirectory = Path.Combine(path, "steamapps", "common", installDir);

				gamePath = gameInstallDirectory;
				return Directory.Exists(gameInstallDirectory);
			}

			gamePath = null;
			return false;
		}

		/// <summary>
		/// Gets the installation directory of a Steam game.
		/// </summary>
		/// <param name="gameId">
		/// The Steam application ID of the game.
		/// </param>
		/// <returns>
		/// The absolute path to the game's installation directory.
		/// </returns>
		/// <exception cref="DirectoryNotFoundException">
		/// Thrown when the game cannot be found in any Steam library folder.
		/// </exception>
		public static string GetGamePath(int gameId) => TryGetGamePath(gameId, out string? path) ? path! : throw new DirectoryNotFoundException($"Could not find game with ID {gameId} in any Steam library folder.");
		
		/// <summary>
		/// Gets the Steam user configuration for the specified or currently active user.
		/// </summary>
		/// <param name="userId">
		/// Optional Steam user ID. If <c>null</c>, the currently active user is inferred.
		/// </param>
		/// <returns>
		/// A <see cref="ValveCfgReader"/> for the user's local Steam configuration file.
		/// </returns>
		/// <exception cref="FileNotFoundException">
		/// Thrown when the user configuration file cannot be found.
		/// </exception>
		public static ValveCfgReader GetUserConfig(long? userId = null)
		{
			string steamPath = GetSteamPath();
			long id = userId ?? GetCurrentUserId();

			string localUserConfig = Path.Combine(steamPath, "userdata", id.ToString(CultureInfo.InvariantCulture), "config", "localconfig.vdf");

			return new ValveCfgReader(localUserConfig);
		}

		/// <summary>
		/// Gets the configured launch options for a Steam game.
		/// </summary>
		/// <param name="gameId">
		/// The Steam application ID of the game.
		/// </param>
		/// <returns>
		/// An array of individual launch options, each prefixed with a dash.
		/// Returns an empty array if no launch options are configured.
		/// </returns>
		/// <exception cref="KeyNotFoundException">
		/// Thrown when the game entry or launch options cannot be found in the user configuration.
		/// </exception>
		public static string[] GetGameLaunchOptions(int gameId)
		{
			ValveCfgReader reader = GetUserConfig();

			ConfigNode launchOptionsNode = reader.Document.Root
				.GetProperty("Software")
				.GetProperty("Valve")
				.GetProperty("Steam")
				.GetProperty("Apps")
				.GetProperty(gameId.ToString(CultureInfo.InvariantCulture));

			string launchOptionsString = launchOptionsNode.GetProperty("LaunchOptions").GetString();

			if (string.IsNullOrWhiteSpace(launchOptionsString))
				return [];

			string[] splitOptions = launchOptionsString.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			return splitOptions.Select(option => $"-{option}").ToArray();
		}

		/// <summary>
		/// Attempts to determine the currently logged-in Steam user ID by reading the Steam connection log.
		/// </summary>
		/// <returns>
		/// The Steam ID of the currently active user.
		/// </returns>
		/// <exception cref="FileNotFoundException">
		/// Thrown when the Steam connection log cannot be found.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when no valid Steam ID can be extracted from the log file.
		/// </exception>
		public static long GetCurrentUserId()
		{
			string steamPath = GetSteamPath();
			string steamConnectionLog = Path.Combine(steamPath, "logs", "connection_log.txt");

			if (!File.Exists(steamConnectionLog))
			{
				throw new FileNotFoundException($"Could not find Steam connection log at {steamConnectionLog}");
			}

			const int bufferSize = 4096;

			using FileStream fileStream = new(steamConnectionLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

			long position = fileStream.Length;
			byte[] buffer = new byte[bufferSize];
			StringBuilder lineBuilder = new();

			while (position > 0)
			{
				int bytesToRead = (int)Math.Min(bufferSize, position);
				position -= bytesToRead;

				fileStream.Seek(position, SeekOrigin.Begin);
				fileStream.ReadExactly(buffer.AsSpan(0, bytesToRead));

				for (int i = bytesToRead - 1; i >= 0; i--)
				{
					char character = (char)buffer[i];

					if (character == '\n')
					{
						if (lineBuilder.Length == 0) continue;

						string line = Reverse(lineBuilder);
						lineBuilder.Clear();

						if (TryExtractUserId(line, out long userId)) return userId;
					}
					else lineBuilder.Append(character);
				}
			}

			if (lineBuilder.Length > 0)
			{
				string line = Reverse(lineBuilder);
				if (TryExtractUserId(line, out long userId)) return userId;
			}

			throw new InvalidOperationException("Failed to gather SteamId from log");
		}

		private static bool TryExtractUserId(string line, out long userId)
		{
			userId = 0;

			if (!line.Contains("[Logged On", StringComparison.Ordinal)) return false;

			int startIndex = line.IndexOf("[U:1:", StringComparison.Ordinal);
			if (startIndex == -1) return false;

			startIndex += 5;

			int endIndex = line.IndexOf(']', startIndex);
			if (endIndex == -1) return false;

			return long.TryParse(line.AsSpan(startIndex, endIndex - startIndex), out userId);
		}

		private static string Reverse(StringBuilder stringBuilder)
		{
			int length = stringBuilder.Length;
			char[] characters = new char[length];

			for (int index = 0; index < length; index++)
				characters[index] = stringBuilder[length - index - 1];

			return new string(characters);
		}
	}
}
