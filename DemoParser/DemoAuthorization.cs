using System.Text.RegularExpressions;

namespace StrikeLink.DemoParser
{
	/// <summary>
	/// Represents the authorization credentials required to access match data using a Steam account, including the Steam
	/// ID, authentication code, and match share code.
	/// </summary>
	/// <remarks>This class encapsulates the information necessary for authenticating and retrieving match details
	/// from the Steam platform. All properties must be set to valid values that conform to the expected formats. Invalid
	/// values will result in a FormatException being thrown when setting the properties.</remarks>
	public partial class DemoAuthorization
	{
		/// <summary>
		/// Initializes a new instance of the DemoAuthorization class with the specified Steam ID, authorization code, and
		/// match share code.
		/// </summary>
		/// <param name="steamId">The unique 64-bit Steam identifier associated with the user.</param>
		/// <param name="authCode">The authorization code used to authenticate the user or session. Cannot be null.</param>
		/// <param name="matchShareCode">The share code representing the specific match to authorize. Cannot be null.</param>
		/// <param name="apiKey"> The steam developer API key. Cannot be null,</param>
		public DemoAuthorization(long steamId, string authCode, string matchShareCode, string apiKey)
		{
			SteamId = steamId;
			AuthCode = authCode;
			MatchShareCode = matchShareCode;
			ApiKey = apiKey;
		}

		[GeneratedRegex("^[a-z0-9]{4}-[a-z0-9]{5}-[a-z0-9]{4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private partial Regex AuthCodeFormat();

		[GeneratedRegex("^CSGO-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private partial Regex MatchShareCodeFormat();

		[GeneratedRegex("^[a-z0-9]{32}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private partial Regex DevKeyFormat();

		/// <summary>
		/// Gets or sets the Steam user identifier associated with this instance.
		/// </summary>
		/// <remarks>The Steam ID must be a 17-digit number within the valid range for Steam user accounts. Assigning
		/// a value outside this range will result in a FormatException.</remarks>
		public long SteamId
		{
			get;
			set
			{
				if (value is < 76561100000000000 or > 76561199999999999)
					throw new FormatException("Invalid SteamId.");
				field = value;
			}
		}

		/// <summary>
		/// Gets or sets the authentication code used for verifying user identity.
		/// </summary>
		/// <remarks>The authentication code must be non-empty and match the required format. The value is
		/// automatically converted to uppercase when set. An exception is thrown if the value does not meet the format
		/// requirements.</remarks>
		public string AuthCode
		{
			get;
			set
			{
				if (string.IsNullOrWhiteSpace(value) || !AuthCodeFormat().IsMatch(value))
					throw new FormatException("Invalid authentication code format.");
				field = value;
			}
		}

		/// <summary>
		/// Gets or sets the match share code used to identify a specific match.
		/// </summary>
		/// <remarks>The match share code must be a non-empty string that matches the required format. The value is
		/// automatically converted to uppercase when set.</remarks>
		public string MatchShareCode
		{
			get;
			set
			{
				if (string.IsNullOrWhiteSpace(value) || !MatchShareCodeFormat().IsMatch(value))
					throw new FormatException("Invalid match share code format.");
				field = value;
			}
		}

		/// <summary>
		/// Gets or sets the API key used for authentication with the service.
		/// </summary>
		/// <remarks>The API key must be in the correct format as defined by the service. Setting an invalid value
		/// will result in a FormatException being thrown. The value is automatically converted to uppercase when
		/// set.</remarks>
		public string ApiKey
		{
			get;
			set
			{
				if (string.IsNullOrWhiteSpace(value) || !DevKeyFormat().IsMatch(value))
					throw new FormatException("Invalid developer api key format.");
				field = value;
			}
		}
	}
}
