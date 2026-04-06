using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
#pragma warning disable IDE0051

// ReSharper disable ClassNeverInstantiated.Local

namespace StrikeLink.DemoParser
{
	/// <summary>
	/// Represents information about a demo match, including its unique identifier, reservation, and TV port.
	/// </summary>
	/// <param name="MatchId">The unique identifier for the match.</param>
	/// <param name="ReservationId">The unique identifier for the reservation associated with the match.</param>
	/// <param name="TvPort">The port number used for TV or spectator access to the match.</param>
	public record DemoShareCodeInfo(ulong MatchId, ulong ReservationId, uint TvPort);

	/// <summary>
	/// Provides network operations for retrieving and managing CS:GO match share codes using the specified authorization
	/// credentials.
	/// </summary>
	/// <remarks>This class encapsulates HTTP communication with the Steam Web API to obtain match share codes. It
	/// manages request retries and handles rate limiting automatically. Instances of this class are not
	/// thread-safe.</remarks>
	/// <param name="demoAuth">The authorization credentials used to authenticate requests to the CS:GO match sharing API. Cannot be null.</param>
	public partial class DemoNetwork(DemoAuthorization demoAuth) : IDisposable
	{
		private const string Dictionary = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";
		private const int Base = 57;
		private const int EncodedLength = 25;

		[GeneratedRegex("^CSGO-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}-[a-z0-9]{5}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
		private static partial Regex MatchShareCodeFormat();

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

		/// <summary>
		/// Decodes a Counter-Strike: Global Offensive match share code into its corresponding match information.
		/// </summary>
		/// <param name="shareCode">The share code string to decode. Must be in the standard CS:GO share code format and contain only valid
		/// characters.</param>
		/// <returns>A DemoMatchInfo object containing the match ID, reservation ID, and TV port extracted from the share code.</returns>
		/// <exception cref="ArgumentException">Thrown if shareCode is not in a valid format or contains invalid characters.</exception>
		public static DemoShareCodeInfo DecodeShareCode(string shareCode)
		{
			if (!MatchShareCodeFormat().IsMatch(shareCode))
				throw new ArgumentException($"Invalid share code: {shareCode}", nameof(shareCode));

			string stripped = shareCode[5..].Replace("-", string.Empty, StringComparison.InvariantCulture);

			BigInteger value = BigInteger.Zero;
			for (int i = stripped.Length - 1; i >= 0; i--)
			{
				int charIndex = Dictionary.IndexOf(stripped[i], StringComparison.InvariantCulture);
				if (charIndex == -1)
					throw new ArgumentException($"Invalid character '{stripped[i]}'.", nameof(shareCode));

				value = value * Base + charIndex;
			}

			byte[] rawBytes = value.ToByteArray();
			byte[] buffer = new byte[18];
			Buffer.BlockCopy(rawBytes, 0, buffer, 0, Math.Min(rawBytes.Length, 18));

			uint tvPort = (uint)((buffer[0] << 8) | buffer[1]);
			ulong reservationId = ReadUInt64BigEndian(buffer, 2);
			ulong matchId = ReadUInt64BigEndian(buffer, 10);

			return new DemoShareCodeInfo(matchId, reservationId, tvPort);
		}

		/// <summary>
		/// Encodes the specified match information into a shareable code string compatible with CS:GO share code format.
		/// </summary>
		/// <param name="matchInfo">The match information to encode. Cannot be null.</param>
		/// <returns>A string containing the encoded share code representing the provided match information.</returns>
		public static string EncodeShareCode(DemoShareCodeInfo matchInfo)
		{
			ArgumentNullException.ThrowIfNull(matchInfo);

			ulong matchId = matchInfo.MatchId;
			ulong reservationId = matchInfo.ReservationId;

			ushort tvPortU16 = (ushort)matchInfo.TvPort;
			byte[] buffer = new byte[18];
			buffer[0] = (byte)(tvPortU16 >> 8);
			buffer[1] = (byte)(tvPortU16 & 0xFF);
			WriteUInt64BigEndian(buffer, 2, reservationId);
			WriteUInt64BigEndian(buffer, 10, matchId);

			byte[] bigIntBytes = new byte[19];
			Buffer.BlockCopy(buffer, 0, bigIntBytes, 0, 18);
			bigIntBytes[18] = 0x00;

			BigInteger value = new(bigIntBytes);

			char[] encoded = new char[EncodedLength];
			for (int i = 0; i < EncodedLength; i++)
			{
				value = BigInteger.DivRem(value, Base, out BigInteger remainder);
				encoded[i] = Dictionary[(int)remainder];
			}

			string chars = new(encoded);
			StringBuilder sb = new(32);

			sb.Append("CSGO");
			for (int i = 0; i < 5; i++) { sb.Append('-'); sb.Append(chars, i * 5, 5); }

			return sb.ToString();
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

		#region Networking

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

		#endregion
		
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

		private static ulong ReadUInt64BigEndian(byte[] buf, int offset) =>
			((ulong)buf[offset] << 56) | ((ulong)buf[offset + 1] << 48) |
			((ulong)buf[offset + 2] << 40) | ((ulong)buf[offset + 3] << 32) |
			((ulong)buf[offset + 4] << 24) | ((ulong)buf[offset + 5] << 16) |
			((ulong)buf[offset + 6] << 8) | buf[offset + 7];

		private static void WriteUInt64BigEndian(byte[] buf, int offset, ulong value)
		{
			buf[offset] = (byte)(value >> 56);
			buf[offset + 1] = (byte)(value >> 48);
			buf[offset + 2] = (byte)(value >> 40);
			buf[offset + 3] = (byte)(value >> 32);
			buf[offset + 4] = (byte)(value >> 24);
			buf[offset + 5] = (byte)(value >> 16);
			buf[offset + 6] = (byte)(value >> 8);
			buf[offset + 7] = (byte)value;
		}
	}
}
