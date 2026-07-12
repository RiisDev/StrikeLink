using System.Numerics;
using System.Text.RegularExpressions;

namespace StrikeLink.Crosshair
{
	/// <summary>
	/// Encodes and decodes CS2 crosshair share codes in the <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c> format.
	/// </summary>
	/// <remarks>
	/// Share codes are Base57-encoded representations of 18 bytes of packed crosshair cvar data.
	/// The alphabet used is <c>ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789</c>
	/// (standard Base57 — no <c>I</c>, <c>O</c>, <c>g</c>, or <c>l</c>).
	/// </remarks>
	public static partial class CrosshairShareCode
	{
		private const string Dictionary = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";
		private const int Base = 57;
		private const int EncodedLength = 25;

		[GeneratedRegex(@"^CSGO(-[A-Za-z0-9]{5}){5}$", RegexOptions.Compiled)]
		private static partial Regex ShareCodePattern();

		/// <summary>
		/// Decodes a CS2 crosshair share code into a <see cref="CrosshairSettings"/> record.
		/// </summary>
		/// <param name="shareCode">
		/// A share code in the format <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>.
		/// </param>
		/// <returns>The decoded <see cref="CrosshairSettings"/>.</returns>
		/// <exception cref="ArgumentException">Thrown when the share code format is invalid or contains unknown characters.</exception>
		public static CrosshairSettings Decode(string shareCode)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(shareCode);

			if (!ShareCodePattern().IsMatch(shareCode))
				throw new ArgumentException($"Invalid crosshair share code format: {shareCode}", nameof(shareCode));

			string stripped = shareCode[5..].Replace("-", string.Empty, StringComparison.Ordinal);

			BigInteger value = BigInteger.Zero;
			for (int i = stripped.Length - 1; i >= 0; i--)
			{
				int charIndex = Dictionary.IndexOf(stripped[i], StringComparison.Ordinal);
				if (charIndex == -1)
					throw new ArgumentException($"Invalid character '{stripped[i]}' in share code.", nameof(shareCode));

				value = value * Base + charIndex;
			}

			byte[] rawBytes = value.ToByteArray();
			byte[] buffer = new byte[18];
			Buffer.BlockCopy(rawBytes, 0, buffer, 0, Math.Min(rawBytes.Length, 18));

			return Unpack(buffer);
		}

		/// <summary>
		/// Encodes a <see cref="CrosshairSettings"/> record into a CS2 crosshair share code.
		/// </summary>
		/// <param name="settings">The crosshair settings to encode.</param>
		/// <returns>A share code string in the format <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="settings"/> is <see langword="null"/>.</exception>
		public static string Encode(CrosshairSettings settings)
		{
			ArgumentNullException.ThrowIfNull(settings);

			byte[] bytes = Pack(settings);

			byte[] bigIntBytes = new byte[19];
			Buffer.BlockCopy(bytes, 0, bigIntBytes, 0, 18);
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

		/// <summary>
		/// Returns <see langword="true"/> if the given string is a syntactically valid share code.
		/// </summary>
		/// <param name="shareCode">The string to validate.</param>
		public static bool IsValid(string? shareCode) =>
			!string.IsNullOrWhiteSpace(shareCode) && ShareCodePattern().IsMatch(shareCode);

		private static byte[] Pack(CrosshairSettings s)
		{
			byte[] b = new byte[18];

			b[1] = (byte)(
				(s.UseAlpha ? 0x80 : 0) |
				(s.UseWeaponGap ? 0x40 : 0) |
				(s.TStyle ? 0x20 : 0) |
				(s.Dot ? 0x10 : 0) |
				((int)s.Color & 0xF));

			b[2] = (byte)Math.Clamp(s.Alpha, 0, 255);

			b[3] = (byte)(
				(Math.Clamp((int)Math.Round(s.SniperWidth), 0, 15) << 4) |
				Math.Clamp((int)Math.Round(s.OutlineThickness * 2), 0, 15));

			ushort sizeU = (ushort)Math.Clamp((int)Math.Round(s.Size * 10), 0, ushort.MaxValue);
			b[4] = (byte)(sizeU & 0xFF);
			b[5] = (byte)(sizeU >> 8);

			short gapS = (short)Math.Clamp((int)Math.Round(s.Gap * 10), short.MinValue, short.MaxValue);
			b[6] = (byte)(gapS & 0xFF);
			b[7] = (byte)((gapS >> 8) & 0xFF);

			ushort thickU = (ushort)Math.Clamp((int)Math.Round(s.Thickness * 10), 0, ushort.MaxValue);
			b[8] = (byte)(thickU & 0xFF);
			b[9] = (byte)(thickU >> 8);

			b[10] = (byte)(s.DrawOutline ? 1 : 0);
			b[11] = (byte)Math.Clamp(s.CustomColorR, 0, 255);
			b[12] = (byte)Math.Clamp(s.CustomColorG, 0, 255);
			b[13] = (byte)Math.Clamp(s.CustomColorB, 0, 255);

			return b;
		}

		private static CrosshairSettings Unpack(byte[] b) => new()
		{
			UseAlpha         = (b[1] & 0x80) != 0,
			UseWeaponGap     = (b[1] & 0x40) != 0,
			TStyle           = (b[1] & 0x20) != 0,
			Dot              = (b[1] & 0x10) != 0,
			Color            = (CrosshairColor)(b[1] & 0xF),
			Alpha            = b[2],
			SniperWidth      = (b[3] >> 4) & 0xF,
			OutlineThickness = (b[3] & 0xF) / 2.0f,
			Size             = BitConverter.ToUInt16(b, 4) / 10.0f,
			Gap              = BitConverter.ToInt16(b, 6) / 10.0f,
			Thickness        = BitConverter.ToUInt16(b, 8) / 10.0f,
			DrawOutline      = b[10] != 0,
			CustomColorR     = b[11],
			CustomColorG     = b[12],
			CustomColorB     = b[13]
		};
	}
}
