using System.Globalization;
using System.Net;
using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Local

namespace StrikeLink.DemoParser
{
	/// <summary>
	/// Provides network operations for retrieving and managing CS:GO match share codes using the specified authorization
	/// credentials.
	/// </summary>
	/// <remarks>This class encapsulates HTTP communication with the Steam Web API to obtain match share codes. It
	/// manages request retries and handles rate limiting automatically. Instances of this class are not
	/// thread-safe.</remarks>
	/// <param name="demoAuth">The authorization credentials used to authenticate requests to the CS:GO match sharing API. Cannot be null.</param>
	public class DemoNetwork(DemoAuthorization demoAuth) : IDisposable
	{
		private record Result([property: JsonPropertyName("nextcode")] string Nextcode);
		private record ShareCodeRoot([property: JsonPropertyName("result")] Result Result);


		private readonly HttpClient? _client = new(new HttpClientHandler
		{
			AllowAutoRedirect = true,
			AutomaticDecompression = DecompressionMethods.All,
			UseCookies = true
		})
		{
			DefaultRequestHeaders =
			{
				{
					"User-Agent",
					"StrikeLink/1.0 (Windows NT 10.0; Win64; x64; rv:141.0)"
				}
			},
			Timeout = TimeSpan.FromSeconds(60)
		};

		/// <summary>
		/// Retrieves the most recent valid match share code available for the authenticated user.
		/// </summary>
		/// <remarks>This method iteratively queries for the next available match share code until a valid code is
		/// found. The returned share code can be used to access match details or share match information with
		/// others.</remarks>
		/// <returns>A string containing the latest valid match share code, or null if no valid code is found.</returns>
		public async Task<string?> GetMatchShareCode()
		{
			string demoCode = demoAuth.MatchShareCode;

		doCode:
			ShareCodeRoot? shareData = await GetNextAvailableMatchCode(demoCode).ConfigureAwait(false);

			if (!(shareData?.Result.Nextcode.Contains("csgo-", StringComparison.InvariantCultureIgnoreCase) ?? false))
				return demoCode;
			
			demoCode = shareData.Result.Nextcode;
			goto doCode;
		}

		private async Task<ShareCodeRoot?> GetNextAvailableMatchCode(string input) =>
			await GetAsync<ShareCodeRoot>(
				"https://api.steampowered.com",
				"/ICSGOPlayers_730/GetNextMatchSharingCode/v1",
				new Dictionary<string, string>
				{
					{"key", demoAuth.ApiKey},
					{"steamid", demoAuth.SteamId.ToString(CultureInfo.InvariantCulture)},
					{"steamidkey", demoAuth.AuthCode},
					{"knowncode", input}
				}
			).ConfigureAwait(false);

		private async Task<T?> GetAsync<T>(string baseUrl, string endPoint, Dictionary<string, string>? parameters = null)
			=> await SendAndConvertAsync<T>(HttpMethod.Get, baseUrl, endPoint, null, parameters).ConfigureAwait(false);

		private async Task GetAsync(string baseUrl, string endPoint, Dictionary<string, string>? parameters = null)
			=> await CreateRequest(HttpMethod.Get, baseUrl, endPoint, null, parameters).ConfigureAwait(false);

		private async Task<T?> SendAndConvertAsync<T>(HttpMethod method, string baseUrl, string endPoint, HttpContent? httpContent = null, Dictionary<string, string>? parameters = null)
			=> ConvertResponse<T>(await CreateRequest(method, baseUrl, endPoint, httpContent, parameters).ConfigureAwait(false));

		private async Task<string?> CreateRequest(HttpMethod httpMethod, string baseUrl, string endPoint, HttpContent? content = null, Dictionary<string, string>? parameters = null)
		{
			if (baseUrl.IsNullOrEmpty()) return string.Empty;
			if (!endPoint.IsNullOrEmpty())
			{
				if (baseUrl[^1] == '/' && endPoint[0] == '/') endPoint = endPoint[1..]; // Make sure the slash isn't duplicated
				if (baseUrl[^1] != '/' && endPoint[0] != '/') baseUrl += "/"; // Make sure it actually contains a slash
			}

			string queryString = parameters is not null && parameters.Count > 0
				? "?" + string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))
				: string.Empty;

			const int maxRetries = 3;
			int retryCount = 0;
			int backoffDelayMs = 5000;

			while (retryCount < maxRetries)
			{

				using HttpRequestMessage httpRequest = new();
				httpRequest.Method = httpMethod;
				httpRequest.RequestUri = new Uri($"{baseUrl}{endPoint}{queryString}");
				httpRequest.Content = content;

				if (_client is null)
					throw new InvalidOperationException("Client is somehow null.");

				using HttpResponseMessage responseMessage = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
				string responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);


				if (retryCount == 0)
					Debug.WriteLine($"[StringLink Log] Uri:{baseUrl}{endPoint}{queryString}\n[StringLink Log] Request Headers:{JsonSerializer.Serialize(_client.DefaultRequestHeaders.ToDictionary())}\n[StringLink Log] Request Content: {JsonSerializer.Serialize(content)}\n[StringLink Log] Response Content:{responseContent}\n[StringLink Log] Response Data: {responseMessage}");

				HttpStatusCode statusCode = responseMessage.StatusCode;

				switch ((int)statusCode)
				{
					case 200:
						content?.Dispose();
						return responseContent;
					case 429:
						Debug.WriteLine($"[StringLink Log] Rate limited, waiting {backoffDelayMs / 1000} seconds before retrying.");
						await Task.Delay(backoffDelayMs).ConfigureAwait(false);
						backoffDelayMs *= 2;
						break;
					case 404:
						content?.Dispose();
						return null;
					default:
						throw new InvalidOperationException($"\n[StringLink Log] Uri:{baseUrl}{endPoint}{queryString}\n[StringLink Log] Request Headers:{JsonSerializer.Serialize(_client.DefaultRequestHeaders.ToDictionary())}\n[StringLink Log] Request Content: {JsonSerializer.Serialize(content)}\n[StringLink Log] Response Content:{responseContent}\n[StringLink Log] Response Data: {responseMessage}");
				}

				retryCount++;

				await Task.Delay(backoffDelayMs).ConfigureAwait(false);

				Debug.WriteLine($"[StringLink Log] Retrying... Attempt {retryCount} of {maxRetries}");
			}

			throw new InvalidOperationException($"[StringLink Log] Failed after {maxRetries} retries. Uri:{baseUrl}{endPoint}{queryString}");

		}

#pragma warning disable IDE0046 // Convert if possible
		private static T? ConvertResponse<T>(string? jsonData)
		{
			try
			{
				if (jsonData.IsNullOrEmpty() ||
					string.Equals(jsonData.Trim(), "null", StringComparison.OrdinalIgnoreCase))
					return default;

				Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

				if (targetType == typeof(string))
					return (T)(object)jsonData;

				if (targetType == typeof(int) && int.TryParse(jsonData, out int intValue))
					return (T)Convert.ChangeType(intValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(long) && long.TryParse(jsonData, out long longValue))
					return (T)Convert.ChangeType(longValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(bool) && bool.TryParse(jsonData, out bool boolValue))
					return (T)Convert.ChangeType(boolValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(double) && double.TryParse(jsonData, out double doubleValue))
					return (T)Convert.ChangeType(doubleValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(decimal) && decimal.TryParse(jsonData, out decimal decimalValue))
					return (T)Convert.ChangeType(decimalValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(float) && float.TryParse(jsonData, out float floatValue))
					return (T)Convert.ChangeType(floatValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(DateTime) && DateTime.TryParse(jsonData, out DateTime dateValue))
					return (T)Convert.ChangeType(dateValue, targetType, CultureInfo.InvariantCulture);

				if (targetType == typeof(Guid) && Guid.TryParse(jsonData, out Guid guidValue))
					return (T)Convert.ChangeType(guidValue, targetType, CultureInfo.InvariantCulture);

				if (targetType.IsEnum && Enum.TryParse(targetType, jsonData, out object? enumValue))
					return (T)enumValue;

				return JsonSerializer.Deserialize<T>(jsonData);
			}
			catch
			{
				throw new InvalidOperationException($"Failed to parse datatype: {typeof(T)}, given data: {jsonData}");
			}
		}

		/// <summary>
		/// Releases all resources used by the <see cref="DemoNetwork"/>.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="DemoNetwork"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing) return;
			_client?.Dispose();
		}
	}
}
