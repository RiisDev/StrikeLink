global using StrikeLink.Extensions;
global using System;
global using System.Diagnostics;
global using System.Text;
global using System.Text.Json;
global using static StrikeLink.Extensions.Logging;
global using static StrikeLink.Extensions.Shared;
using System.Globalization;
using System.Net;

// ReSharper disable ArrangeNamespaceBody
namespace StrikeLink.Extensions;

internal static class Shared
{
	public static HttpClient SharedClient => LazyClient.Value;

	private static readonly Lazy<HttpClient> LazyClient = new(() => new HttpClient(new HttpClientHandler
	{
		AllowAutoRedirect = true,
		UseCookies = true,
		AutomaticDecompression = DecompressionMethods.All
	})
	{
		Timeout = TimeSpan.FromSeconds(90),
		DefaultRequestHeaders = { { "User-Agent", "RiisDev/StrikeLink 1.0" } }
	});

	internal static async Task<T?> ClientGetAsync<T>(string baseUrl, string endPoint, Dictionary<string, string>? parameters = null)
			=> await ClientSendAndConvertAsync<T>(HttpMethod.Get, baseUrl, endPoint, null, parameters).ConfigureAwait(false);

	internal static async Task ClientGetAsync(string baseUrl, string endPoint, Dictionary<string, string>? parameters = null)
		=> await ClientCreateRequest(HttpMethod.Get, baseUrl, endPoint, null, parameters).ConfigureAwait(false);

	internal static async Task<T?> ClientSendAndConvertAsync<T>(HttpMethod method, string baseUrl, string endPoint, HttpContent? httpContent = null, Dictionary<string, string>? parameters = null)
		=> ConvertResponse<T>(await ClientCreateRequest(method, baseUrl, endPoint, httpContent, parameters).ConfigureAwait(false));

	internal static async Task<string?> ClientCreateRequest(HttpMethod httpMethod, string baseUrl, string endPoint, HttpContent? content = null, Dictionary<string, string>? parameters = null)
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

			if (SharedClient is null)
				throw new InvalidOperationException("Client is somehow null.");

			using HttpResponseMessage responseMessage = await SharedClient.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			string responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);


			if (retryCount == 0)
				Debug.WriteLine($"[StringLink Log] Uri:{baseUrl}{endPoint}{queryString}\n[StringLink Log] Request Headers:{JsonSerializer.Serialize(SharedClient.DefaultRequestHeaders.ToDictionary())}\n[StringLink Log] Request Content: {JsonSerializer.Serialize(content)}\n[StringLink Log] Response Content:{responseContent}\n[StringLink Log] Response Data: {responseMessage}");

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
					throw new InvalidOperationException($"\n[StringLink Log] Uri:{baseUrl}{endPoint}{queryString}\n[StringLink Log] Request Headers:{JsonSerializer.Serialize(SharedClient.DefaultRequestHeaders.ToDictionary())}\n[StringLink Log] Request Content: {JsonSerializer.Serialize(content)}\n[StringLink Log] Response Content:{responseContent}\n[StringLink Log] Response Data: {responseMessage}");
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
}