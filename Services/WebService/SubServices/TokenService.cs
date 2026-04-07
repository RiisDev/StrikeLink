using System.Text.RegularExpressions;

namespace StrikeLink.Services.WebService.SubServices
{
	/// <summary>
	/// Provides static methods for retrieving authentication and API tokens related to Counter-Strike and Steam services.
	/// </summary>
	/// <remarks>This class is intended for use in scenarios where programmatic access to Counter-Strike
	/// authentication codes, match share codes, or Steam developer API keys is required. All methods require valid Steam
	/// session credentials and perform HTTP requests to official Steam endpoints. The class is static and cannot be
	/// instantiated.</remarks>
	public static partial class TokenService
	{
		[GeneratedRegex("authentication.+([a-z0-9]{4}-[a-z0-9]{5}-[a-z0-9]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private static partial Regex AuthCodeFormat();

		[GeneratedRegex("recent.+(CSGO-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5})", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private static partial Regex MatchShareCodeFormat();

		[GeneratedRegex("<p>.+([a-z0-9]{32})<\\/p>", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private static partial Regex DevKeyFormat();

		/// <summary>
		/// Retrieves the Counter-Strike authentication token for the specified Steam session.
		/// </summary>
		/// <remarks>This method sends an HTTP request to the Steam support site using the provided session
		/// credentials. The caller is responsible for ensuring that the credentials are valid and have not expired.</remarks>
		/// <param name="loginSecure">The value of the 'steamLoginSecure' cookie associated with the user's Steam account. Cannot be null or empty.</param>
		/// <param name="sessionId">The value of the 'sessionid' cookie for the current Steam session. Cannot be null or empty.</param>
		/// <returns>A string containing the Counter-Strike authentication token if found.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the authentication token cannot be found in the response.</exception>
		public static async Task<string> GetCounterStrikeAuthToken(string loginSecure, string sessionId)
		{
			using HttpRequestMessage request = new (HttpMethod.Get, "https://help.steampowered.com/en/wizard/HelpWithGameIssue?appid=730&issueid=128");
			request.Headers.TryAddWithoutValidation("Cookie", $"sessionid={sessionId};steamLoginSecure={loginSecure}");

			using HttpResponseMessage response = await SharedClient.SendAsync(request).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();

			string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			Match match = AuthCodeFormat().Match(content);

			return !match.Success ? throw new InvalidOperationException("Failed to find match for authentication code.") : match.Groups[1].Value;
		}

		/// <summary>
		/// Retrieves the Counter-Strike match token for the specified Steam account session.
		/// </summary>
		/// <remarks>This method sends an HTTP request to the Steam support site using the provided session
		/// credentials. The caller is responsible for ensuring that the credentials are valid and have the necessary
		/// permissions.</remarks>
		/// <param name="loginSecure">The value of the 'steamLoginSecure' cookie associated with the user's Steam account. Cannot be null or empty.</param>
		/// <param name="sessionId">The value of the 'sessionid' cookie for the current Steam session. Cannot be null or empty.</param>
		/// <returns>A string containing the Counter-Strike match token if found.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the match token cannot be found in the response content.</exception>
		public static async Task<string> GetCounterStrikeMatchToken(string loginSecure, string sessionId)
		{
			using HttpRequestMessage request = new(HttpMethod.Get, "https://help.steampowered.com/en/wizard/HelpWithGameIssue?appid=730&issueid=128");
			request.Headers.TryAddWithoutValidation("Cookie", $"sessionid={sessionId};steamLoginSecure={loginSecure}");

			using HttpResponseMessage response = await SharedClient.SendAsync(request).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();

			string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			Match match = MatchShareCodeFormat().Match(content);

			return !match.Success ? throw new InvalidOperationException("Failed to find match for match code.") : match.Groups[1].Value;
		}

		/// <summary>
		/// Retrieves the Steam Web API developer key associated with the specified session and login credentials.
		/// </summary>
		/// <remarks>The caller must provide valid authentication cookies for a Steam account that has access to the
		/// developer key page. This method performs an HTTP request to the Steam Community developer API key page and parses
		/// the response to extract the key.</remarks>
		/// <param name="loginSecure">The value of the 'steamLoginSecure' authentication cookie for the Steam account.</param>
		/// <param name="sessionId">The session ID associated with the authenticated Steam session.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the Steam Web API developer key as a
		/// string.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the developer API key cannot be found in the response content.</exception>
		public static async Task<string> GetSteamDevKey(string loginSecure, string sessionId)
		{
			using HttpRequestMessage request = new(HttpMethod.Get, "https://steamcommunity.com/dev/apikey");
			request.Headers.TryAddWithoutValidation("Cookie", $"sessionid={sessionId};steamLoginSecure={loginSecure}");

			using HttpResponseMessage response = await SharedClient.SendAsync(request).ConfigureAwait(false);

			response.EnsureSuccessStatusCode();

			string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			Match match = DevKeyFormat().Match(content);

			return !match.Success ? throw new InvalidOperationException("Failed to find match for dev api key pattern.") : match.Groups[1].Value;
		}
	}
}
