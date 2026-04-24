using System.Text.RegularExpressions;

namespace StrikeLink.Services.WebService.SubServices
{
	/// <summary>
	/// Represents a Steam user with display name, unique Steam ID, and profile URL.
	/// </summary>
	/// <param name="DisplayName">The display name of the Steam user as shown on their profile. Cannot be null.</param>
	/// <param name="SteamId">The unique Steam identifier for the user. Cannot be null.</param>
	/// <param name="SteamMiniId">The unique Steam Mini identifier for the user. Cannot be null.</param>
	/// <param name="ProfileUrl">The URL to the user's Steam profile. Cannot be null.</param>
	public record SteamPlayer(
		string DisplayName,
		string SteamId,
		string SteamMiniId,
		Uri ProfileUrl
	);

	/// <summary>
	/// Represents a record of a co-play gaming session, including game details, session timing, and participating players.
	/// </summary>
	/// <param name="GameName">The name of the game played during the session. Cannot be null.</param>
	/// <param name="PlayedOn">The date or platform on which the session was played. The format and meaning depend on the application's context.
	/// Cannot be null.</param>
	/// <param name="Duration">The duration of the session, typically represented as a formatted string (for example, "2h 15m"). Cannot be null.</param>
	/// <param name="Players">A read-only list of players who participated in the session. Cannot be null and may be empty if no players are
	/// recorded.</param>
	public record CoPlaySession(
		string GameName,
		string PlayedOn,
		string Duration,
		IReadOnlyList<SteamPlayer> Players
	);

	/// <summary>
	/// Provides methods for retrieving and parsing Steam Co-Play session data for a specific user.
	/// </summary>
	/// <remarks>This service manages HTTP communication with the Steam Community website to obtain co-play session
	/// information. It requires a valid Steam login token for authentication.</remarks>
	public partial class CoPlayService
	{
		private readonly string _userId;
		private readonly string _loginSecure;

		/// <summary>
		/// Initializes a new instance of the CoPlayService class using the specified Steam login secure token.
		/// </summary>
		/// <remarks>The provided login secure token is used to set authentication cookies for HTTP requests. The user
		/// ID is extracted from the portion of the token before the '|' character.</remarks>
		/// <param name="loginSecure">The Steam login secure token used to authenticate requests. Cannot be null or empty. Must contain a '|' character
		/// to separate the user ID.</param>
		public CoPlayService(string loginSecure)
		{
			ArgumentException.ThrowIfNullOrEmpty(loginSecure);
			_loginSecure = loginSecure;
			_userId = loginSecure[..loginSecure.IndexOf('|', StringComparison.InvariantCulture)];
		}

		/// <summary>
		/// Retrieves a list of co-play sessions for the current Steam user.
		/// </summary>
		/// <remarks>This method sends an HTTP request to the Steam Community service to obtain co-play session data
		/// for the specified user. The operation is performed asynchronously and requires network connectivity. An exception
		/// is thrown if the HTTP request fails.</remarks>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="CoPlaySession"/>
		/// objects representing the user's co-play sessions. The list is empty if no co-play data is available.</returns>
		public async Task<List<CoPlaySession>> GetCoplayData()
		{
			using HttpRequestMessage request = new(HttpMethod.Get, $"https://steamcommunity.com/profiles/{_userId}/friends/coplay?ajax=1");
			request.Headers.TryAddWithoutValidation("Cookie", $"steamLoginSecure={_loginSecure}");

			using HttpResponseMessage response = await SharedClient.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return Parse(responseBody);
		}

		internal static List<CoPlaySession> Parse(string html)
		{
			List<CoPlaySession> sessions = [];

			IEnumerable<string> coplayGroup = html.Split("coplayGroup").Skip(1);

			foreach (string sessionHtml in coplayGroup)
			{
				Match playMatch = GetPlayRegex().Match(sessionHtml);

				string gameName = GetValue(GameNameRegex().Match(sessionHtml));
				string playedOn = GetValue(playMatch);
				string duration = GetValue(playMatch, 2);

				IEnumerable<string> players = sessionHtml.Split("data-panel").Skip(1);

				List<SteamPlayer> playersData = [];
				playersData.AddRange(
					from playerCard in players 
					let steamId = GetValue(GetSteamIdRegex().Match(playerCard))
					let displayName = GetValue(GetSteamDisplayNameRegex().Match(playerCard)) 
					let steamUrl = GetValue(GetSteamUrlRegex().Match(playerCard)) 
					let steamMini = GetValue(GetSteamMiniIdRegex().Match(playerCard))
					select new SteamPlayer(displayName, steamId, steamMini, new Uri(steamUrl, UriKind.RelativeOrAbsolute))
				);

				sessions.Add(new CoPlaySession(gameName, playedOn, duration, playersData));
			}

			return sessions;
		}

		[GeneratedRegex("<h4>(.*)<\\/h4>", RegexOptions.Compiled)]
		private static partial Regex GameNameRegex();

		[GeneratedRegex("Played (?:on )?(.+?)\\s+for (.+?)\\s*<br", RegexOptions.Compiled)]
		private static partial Regex GetPlayRegex();

		[GeneratedRegex("data-steamid=\"(\\d+)\"", RegexOptions.Compiled)]
		private static partial Regex GetSteamIdRegex();

		[GeneratedRegex("data-miniprofile=\"(\\d+)\"", RegexOptions.Compiled)]
		private static partial Regex GetSteamMiniIdRegex();

		[GeneratedRegex("friend_block_content\">(.+)<br>", RegexOptions.Compiled)]
		private static partial Regex GetSteamDisplayNameRegex();

		[GeneratedRegex("href=\"(.+)\"", RegexOptions.Compiled)]
		private static partial Regex GetSteamUrlRegex();

		private static string GetValue(Match match, int group = 1)
		{
			return match.Groups[group].Value.Trim();
		}

	}
}
