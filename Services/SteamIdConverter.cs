using System.Globalization;
#pragma warning disable IDE0046

namespace StrikeLink.Services
{
	/// <summary>
	/// Provides methods for converting between different Steam ID formats, including Steam64, Steam2, and Steam3
	/// representations.
	/// </summary>
	/// <remarks>This class enables conversion and parsing of Steam user identifiers across multiple formats
	/// commonly used in Steam APIs and community tools. It supports creating instances from any supported format and
	/// retrieving the corresponding representations. All conversions are performed using standard algorithms based on
	/// Valve's documented Steam ID structure. This class is thread-safe for read-only operations.</remarks>
	public class SteamIdConverter
	{
		private const ulong Steam64Base = 76561197960265728UL;

		/// <summary>
		/// Gets the 64-bit Steam identifier associated with the user.
		/// </summary>
		public ulong Steam64 { get; }

		/// <summary>
		/// Gets the unique identifier for the account.
		/// </summary>
		public uint AccountId { get; }

		/// <summary>
		/// Gets the Steam2 (legacy) identifier for the account in the format used by older Steam systems.
		/// </summary>
		/// <remarks>The Steam2 ID is primarily used for compatibility with legacy systems and may not be suitable for
		/// modern Steam integrations. Prefer using Steam3 or SteamID64 formats for new development unless interoperability
		/// with older systems is required.</remarks>
		public string Steam2 => $"STEAM_1:{AccountId & 1}:{AccountId >> 1}";

		/// <summary>
		/// Gets the Steam3 identifier for the account in the format used by Steam services.
		/// </summary>
		/// <remarks>The Steam3 identifier is commonly used for referencing user accounts in Steam APIs and community
		/// features. The format is [U:1:AccountId], where AccountId is the unique numeric identifier for the
		/// account.</remarks>
		public string Steam3 => $"[U:1:{AccountId}]";

		private SteamIdConverter(ulong steam64)
		{
			Steam64 = steam64;
			AccountId = (uint)(steam64 - Steam64Base);
		}

		/// <summary>
		/// Creates a new instance of the SteamIdConverter class from a 64-bit Steam ID.
		/// </summary>
		/// <param name="steam64">The 64-bit unsigned integer representing the Steam ID to convert.</param>
		/// <returns>A SteamIdConverter instance initialized with the specified 64-bit Steam ID.</returns>
		public static SteamIdConverter FromSteam64(ulong steam64) => new(steam64);

		/// <summary>
		/// Creates a new instance of the SteamIdConverter class from the specified Steam account ID.
		/// </summary>
		/// <param name="accountId">The 32-bit unsigned integer representing the Steam account ID to convert.</param>
		/// <returns>A SteamIdConverter instance initialized with the corresponding Steam64 identifier.</returns>
		public static SteamIdConverter FromAccountId(uint accountId) => new(Steam64Base + accountId);

		/// <summary>
		/// Creates a new instance of the SteamIdConverter class from a Steam3-formatted identifier string.
		/// </summary>
		/// <remarks>The input string must follow the standard Steam3 ID format (e.g., "[U:1:123456]"). An exception
		/// may be thrown if the format is invalid.</remarks>
		/// <param name="steam3">The Steam3-formatted identifier string to convert. Must be in the expected format and not null.</param>
		/// <returns>A SteamIdConverter instance representing the specified Steam3 identifier.</returns>
		/// <exception cref="ArgumentException">Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.</exception>
		public static SteamIdConverter FromSteam3(string steam3)
		{
			ArgumentException.ThrowIfNullOrEmpty(steam3);
			uint accountId = uint.Parse(steam3[4..^1], CultureInfo.InvariantCulture);
			return FromAccountId(accountId);
		}

		/// <summary>
		/// Creates a new instance of the SteamIdConverter class from a Steam2-formatted identifier string.
		/// </summary>
		/// <remarks>The input string must be in the standard Steam2 format. An exception may be thrown if the format
		/// is invalid or if parsing fails.</remarks>
		/// <param name="steam2">The Steam2 identifier string in the format "STEAM_X:Y:Z" to convert.</param>
		/// <returns>A SteamIdConverter instance representing the specified Steam2 identifier.</returns>
		/// <exception cref="ArgumentException">Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.</exception>
		public static SteamIdConverter FromSteam2(string steam2)
		{
			ArgumentException.ThrowIfNullOrEmpty(steam2);
			string[] parts = steam2.Split(':');
			uint w = uint.Parse(parts[1], CultureInfo.InvariantCulture);
			uint z = uint.Parse(parts[2], CultureInfo.InvariantCulture);
			return FromAccountId((z << 1) | w);
		}

		/// <summary>
		/// Converts a Steam identifier in various supported formats to its legacy Steam2 format (e.g., "STEAM_X:Y:Z").
		/// </summary>
		/// <remarks>This method accepts multiple Steam identifier formats and returns the equivalent Steam2 format.
		/// If the input does not match any supported format, an exception is thrown.</remarks>
		/// <param name="input">The Steam identifier to convert. Supported formats include Steam64, Steam3, Steam2, and account ID. Cannot be null
		/// or empty.</param>
		/// <returns>A string containing the Steam2 representation of the specified identifier.</returns>
		/// <exception cref="ArgumentException">Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.</exception>
		public static string ToSteam2(string input)
		{
			ArgumentException.ThrowIfNullOrEmpty(input);

			input = input.ToUpperInvariant();

			if (ulong.TryParse(input, out ulong steam64) && steam64 > Steam64Base)
				return FromSteam64(steam64).Steam2;

			if (input.StartsWith("[U:1:", StringComparison.Ordinal) && input.EndsWith(']'))
				return FromSteam3(input).Steam2;

			if (input.StartsWith("STEAM_", StringComparison.Ordinal) && input.Count(c => c == ':') == 2)
				return FromSteam2(input).Steam2;

			if (uint.TryParse(input, out uint accountId))
				return FromAccountId(accountId).Steam2;

			throw new ArgumentException($"Unrecognized SteamID format: {input}");
		}
	}
}
