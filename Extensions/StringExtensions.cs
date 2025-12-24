using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace StrikeLink.Extensions
{
	internal static class StringExtensions
	{
		extension(string haystack)
		{
			internal string FromBase64()
			{
				if (string.IsNullOrEmpty(haystack)) return string.Empty;

				try
				{
					if (haystack.Contains('<', StringComparison.InvariantCultureIgnoreCase)) haystack = haystack[..haystack.IndexOf('<', StringComparison.InvariantCultureIgnoreCase)];

					int paddingNeeded = haystack.Length % 4;
					if (paddingNeeded > 0) haystack = haystack.PadRight(haystack.Length + (4 - paddingNeeded), '=');

					byte[] buffer = Convert.FromBase64String(haystack);
					return Encoding.UTF8.GetString(buffer);
				}
				catch { return string.Empty; }
			}

			internal string ToBase64() => Convert.ToBase64String(Encoding.UTF8.GetBytes(haystack));

			internal int IntParse() => int.Parse(haystack, CultureInfo.InvariantCulture);

			internal bool IntTryParse(out int value) => int.TryParse(haystack, CultureInfo.InvariantCulture, out value);

			internal string NormalizeForComparison()
			{
				if (string.IsNullOrEmpty(haystack))
				{
					return string.Empty;
				}

				string normalized = haystack.Normalize(NormalizationForm.FormKC);

				StringBuilder builder = new(normalized.Length);

				foreach (char character in from character in normalized let category = char.GetUnicodeCategory(character) where category != UnicodeCategory.Format && category != UnicodeCategory.Control select character)
				{
					builder.Append(character);
				}

				return builder.ToString();
			}
		}

		internal static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);
	}
}
