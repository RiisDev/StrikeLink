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
	internal static partial class CrosshairShareCode
	{
		private const string Dictionary = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";
		private const int Base = 57;
		private const int EncodedLength = 25;

		// Preset RGB values used to recover CrosshairColor from raw R/G/B on decode.
		// These are CS2's fixed preset colors; anything that doesn't match one of them is Custom.
		private static readonly (CrosshairColor Color, byte R, byte G, byte B)[] PresetColors =
		[
			(CrosshairColor.Red, 255, 0, 0),
			(CrosshairColor.Green, 0, 255, 0),
			(CrosshairColor.Yellow, 255, 255, 0),
			(CrosshairColor.Blue, 0, 0, 255),
			(CrosshairColor.Cyan, 0, 255, 255),
		];

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

			// BigInteger.ToByteArray() is little-endian and may drop a trailing 0x00 sign byte
			// or come up short; normalize to exactly 18 bytes.
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

		/// <remarks>
		/// Byte layout below is the real CS2/CS:GO crosshair share-code format (verified against
		/// a known-working reference decoder), NOT an invented scheme:
		///
		///   [3]  Gap               sbyte / 10.0
		///   [4]  OutlineThickness  byte / 2.0
		///   [5]  CustomColorR      byte
		///   [6]  CustomColorG      byte
		///   [7]  CustomColorB      byte
		///   [8]  Alpha             byte
		///   [9]  SplitDistance     byte   (dynamic-crosshair field, not exposed by CrosshairSettings — fixed default)
		///   [11] bit 0x8           DrawOutline
		///        high nibble       InnerSplitAlpha / 10.0 (not exposed — fixed default)
		///   [12] low nibble        OuterSplitAlpha / 10.0 (not exposed — fixed default)
		///        high nibble       SplitSizeRatio / 10.0 (not exposed — fixed default)
		///   [13] Thickness         byte / 10.0
		///   [14] low nibble >> 1   Style (not exposed — fixed to Classic Static)
		///        high nibble 0x1   Dot (center dot)
		///        high nibble 0x4   UseAlpha
		///        high nibble 0x8   TStyle
		///   [15] Size              byte / 10.0
		///
		/// Bytes [0..2], [10], [16], [17] are reserved/unconfirmed in every source available to us.
		/// Color (preset), SniperWidth, and UseWeaponGap are CS2-specific fields with no confirmed
		/// byte position, so they are packed into those spare bytes below as a best-effort scheme —
		/// this part is NOT verified against the real game and should be treated as provisional.
		/// Validate by round-tripping share codes generated in-game with known Sniper Width /
		/// "scale with weapon" / color-preset values and checking the decoded result matches.
		/// </remarks>
		private static byte[] Pack(CrosshairSettings s)
		{
			byte[] b = new byte[18];

			sbyte gapS = (sbyte)Math.Clamp((int)Math.Round(s.Gap * 10), sbyte.MinValue, sbyte.MaxValue);
			b[3] = unchecked((byte)gapS);

			b[4] = (byte)Math.Clamp((int)Math.Round(s.OutlineThickness * 2), 0, 255);

			b[5] = (byte)Math.Clamp(s.CustomColorR, 0, 255);
			b[6] = (byte)Math.Clamp(s.CustomColorG, 0, 255);
			b[7] = (byte)Math.Clamp(s.CustomColorB, 0, 255);
			b[8] = (byte)Math.Clamp(s.Alpha, 0, 255);

			b[9] = 7; // SplitDistance — not modeled; fixed to a reasonable CS2 default.

			b[11] = (byte)(
				(s.DrawOutline ? 0x8 : 0) |
				(10 << 4)); // InnerSplitAlpha fixed to 1.0 — not modeled.

			b[12] = (byte)(
				10 |             // OuterSplitAlpha fixed to 1.0 — not modeled.
				(3 << 4));       // SplitSizeRatio fixed to 0.3 — not modeled.

			b[13] = (byte)Math.Clamp((int)Math.Round(s.Thickness * 10), 0, 255);

			const int classicStatic = 4; // CrosshairStyle.ClassicStatic — fixed, not modeled.
			b[14] = (byte)(
				((classicStatic << 1) & 0xF) |
				((s.Dot ? 0x1 : 0) << 4) |
				((s.UseAlpha ? 0x4 : 0) << 4) |
				((s.TStyle ? 0x8 : 0) << 4));

			b[15] = (byte)Math.Clamp((int)Math.Round(s.Size * 10), 0, 255);

			// --- Provisional / unverified packing (see remarks above) ---
			b[10] = (byte)(
				(Math.Clamp((int)Math.Round(s.SniperWidth), 0, 15) & 0xF) |
				(s.UseWeaponGap ? 0x10 : 0));

			b[2] = (byte)((int)s.Color & 0xF);

			return b;
		}

		private static CrosshairSettings Unpack(byte[] b)
		{
			byte rawR = b[5];
			byte rawG = b[6];
			byte rawB = b[7];

			// Prefer the color actually encoded in byte[2] (see Pack remarks); fall back to
			// matching known preset RGB values, then Custom, if that byte looks unset.
			CrosshairColor color = (CrosshairColor)(b[2] & 0xF);
			if (!Enum.IsDefined(color))
			{
				color = CrosshairColor.Custom;
				foreach (var preset in PresetColors)
				{
					if (preset.R == rawR && preset.G == rawG && preset.B == rawB)
					{
						color = preset.Color;
						break;
					}
				}
			}

			return new CrosshairSettings
			{
				Gap = unchecked((sbyte)b[3]) / 10.0f,
				OutlineThickness = b[4] / 2.0f,
				CustomColorR = rawR,
				CustomColorG = rawG,
				CustomColorB = rawB,
				Alpha = b[8],
				DrawOutline = (b[11] & 0x8) != 0,
				Thickness = b[13] / 10.0f,
				Dot = ((b[14] >> 4) & 0x1) != 0,
				UseAlpha = ((b[14] >> 4) & 0x4) != 0,
				TStyle = ((b[14] >> 4) & 0x8) != 0,
				Size = b[15] / 10.0f,
				Color = color,

				// --- Provisional / unverified (see Pack remarks) ---
				SniperWidth = b[10] & 0xF,
				UseWeaponGap = (b[10] & 0x10) != 0,
			};
		}
	}
}