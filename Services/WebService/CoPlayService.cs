
using System.Text.RegularExpressions;

namespace StrikeLink.Services.WebService
{
	/// <summary>
	/// Represents a Steam user with display name, unique Steam ID, and profile URL.
	/// </summary>
	/// <param name="DisplayName">The display name of the Steam user as shown on their profile. Cannot be null.</param>
	/// <param name="SteamId">The unique Steam identifier for the user. Cannot be null.</param>
	/// <param name="ProfileUrl">The URL to the user's Steam profile. Cannot be null.</param>
	public record SteamPlayer(
		string DisplayName,
		string SteamId,
		string ProfileUrl
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

	public partial class CoPlayService : IDisposable
	{
		private readonly HttpClient _httpClient;
		private readonly string _userId;

		public CoPlayService(string loginSecure)
		{
			_userId = loginSecure[..loginSecure.IndexOf('|', StringComparison.InvariantCulture)];

			_httpClient = new HttpClient(new HttpClientHandler()
			{
				AllowAutoRedirect = true,
				UseCookies = true
			});

			_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", $"steamLoginSecure={loginSecure}");
		}

		public async Task<List<CoPlaySession>> GetCoplayData()
		{
			HttpRequestMessage request = new(HttpMethod.Get, $"https://steamcommunity.com/profiles/{_userId}/friends/coplay?ajax=1");
			HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return Parse(responseBody);
		}

		internal List<CoPlaySession> Parse(string html)
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
				playersData.AddRange(from playerCard in players let steamId = GetValue(GetSteamIdRegex().Match(playerCard)) let displayName = GetValue(GetSteamDisplayNameRegex().Match(playerCard)) let steamUrl = GetValue(GetSteamUrlRegex().Match(playerCard)) select new SteamPlayer(displayName, steamId, steamUrl));

				sessions.Add(new CoPlaySession(gameName, playedOn, duration, playersData));
			}

			return sessions;
		}

		[GeneratedRegex("<h4>(.*)<\\/h4>", RegexOptions.Compiled)]
		private static partial Regex GameNameRegex();

		[GeneratedRegex("Played on (.+).+for (.+).+<br", RegexOptions.Compiled)]
		private static partial Regex GetPlayRegex();

		[GeneratedRegex("data-steamid=\"(\\d+)\"", RegexOptions.Compiled)]
		private static partial Regex GetSteamIdRegex();

		[GeneratedRegex("friend_block_content\">(.+)<br>", RegexOptions.Compiled)]
		private static partial Regex GetSteamDisplayNameRegex();

		[GeneratedRegex("href=\"(.+)\"", RegexOptions.Compiled)]
		private static partial Regex GetSteamUrlRegex();

		private static string GetValue(Match match, int group = 1)
		{
			return match.Groups[group].Value.Trim();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_httpClient.Dispose();
		}
	}
}
