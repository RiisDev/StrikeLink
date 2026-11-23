using System.Numerics;
using static System.Math;

namespace StrikeLink.Crosshair
{
	/// <summary>
	/// Encodes and decodes CS2 crosshair share codes: <c>CS</c> followed by 44 Base57 characters
	/// (the format introduced by the September 2026 update; the old <c>CSGO-XXXXX-...</c> codes are rejected by the game).
	/// </summary>
	/// <remarks>
	/// The 32 byte payload is read as a big-endian integer and written as Base57 digits, least significant first.
	/// <code>
	/// [0]      sum of bytes 1..31, modulo 256
	/// [1]      version, always 1
	/// [2..3]   screen height, little-endian
	/// [4]      bits 0-4 style, 5 follow recoil, 6 dot, 7 T style
	/// [5..8]   crosshair R G B A
	/// [9..12]  outline R G B A
	/// [13]     bits 0-5 thickness, 6-7 outline mode
	/// [14..15] gap, signed little-endian
	/// [16]     length
	/// [17]     spread limit
	/// [18..21] little-endian uint: bits 0-6 split distance, 7-13 inner split alpha x100,
	///          14-20 (outer split alpha - 0.3) x100, 21-27 split size ratio x100, 28 scope dot uses crosshair color
	/// [22]     (scope dot scale - 0.1) x100
	/// [23..31] unused
	/// </code>
	/// </remarks>
	internal static class CrosshairShareCode
	{
		private const string Dictionary = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";
		private const int Base = 57;
		private const int PayloadLength = 32;
		private const int EncodedLength = 44;

		public static bool IsValid(string? shareCode) => TryDecode(shareCode, out _);

		public static CrosshairSettings Decode(string shareCode) =>
			TryDecode(shareCode, out CrosshairSettings? settings)
				? settings
				: throw new ArgumentException($"Invalid crosshair share code: {shareCode}", nameof(shareCode));

		public static bool TryDecode(string? shareCode, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out CrosshairSettings? settings)
		{
			settings = null;
			if (shareCode is not { Length: EncodedLength + 2 } || !shareCode.StartsWith("CS", StringComparison.Ordinal))
				return false;

			BigInteger value = BigInteger.Zero;
			for (int i = EncodedLength - 1; i >= 0; i--)
			{
				int digit = Dictionary.IndexOf(shareCode[i + 2]);
				if (digit < 0) return false;
				value = value * Base + digit;
			}

			byte[] raw = value.ToByteArray(isUnsigned: true, isBigEndian: true);
			if (raw.Length > PayloadLength) return false;

			byte[] b = new byte[PayloadLength];
			raw.CopyTo(b, PayloadLength - raw.Length);

			if (b[0] != Checksum(b) || b[1] != 1) return false;

			uint packed = BitConverter.ToUInt32(b, 18);
			settings = new CrosshairSettings
			{
				ScreenHeight = b[2] | b[3] << 8,
				Style = (CrosshairStyle)(b[4] & 31),
				FollowRecoil = (b[4] & 32) != 0,
				Dot = (b[4] & 64) != 0,
				TStyle = (b[4] & 128) != 0,
				Red = b[5],
				Green = b[6],
				Blue = b[7],
				Alpha = b[8],
				OutlineRed = b[9],
				OutlineGreen = b[10],
				OutlineBlue = b[11],
				OutlineAlpha = b[12],
				Thickness = b[13] & 63,
				Outline = (CrosshairOutline)(b[13] >> 6),
				Gap = BitConverter.ToInt16(b, 14),
				Length = b[16],
				SpreadLimit = b[17],
				SplitDistance = (int)(packed & 127),
				InnerSplitAlpha = (packed >> 7 & 127) / 100.0,
				OuterSplitAlpha = Round((packed >> 14 & 127) / 100.0 + 0.3, 2),
				SplitSizeRatio = (packed >> 21 & 127) / 100.0,
				ScopeDotUsesCrosshairColor = (packed >> 28 & 1) != 0,
				ScopeDotScale = Round(b[22] / 100.0 + 0.1, 2)
			};
			return true;
		}

		public static string Encode(CrosshairSettings s)
		{
			ArgumentNullException.ThrowIfNull(s);

			int gap = Clamp(s.Gap, -3840, 3840) & 0xFFFF;
			int height = Clamp(s.ScreenHeight, 240, 65535);
			uint packed = (uint)Clamp(s.SplitDistance, 0, 127)
				| (uint)Hundredths(s.InnerSplitAlpha, 0, 1) << 7
				| (uint)Hundredths(s.OuterSplitAlpha, 0.3, 1) << 14
				| (uint)Hundredths(s.SplitSizeRatio, 0, 1) << 21
				| (s.ScopeDotUsesCrosshairColor ? 1u << 28 : 0);

			byte[] b = new byte[PayloadLength];
			b[1] = 1;
			b[2] = (byte)height;
			b[3] = (byte)(height >> 8);
			b[4] = (byte)(((int)s.Style & 31) | (s.FollowRecoil ? 32 : 0) | (s.Dot ? 64 : 0) | (s.TStyle ? 128 : 0));
			b[5] = (byte)Clamp(s.Red, 0, 255);
			b[6] = (byte)Clamp(s.Green, 0, 255);
			b[7] = (byte)Clamp(s.Blue, 0, 255);
			b[8] = (byte)Clamp(s.Alpha, 0, 255);
			b[9] = (byte)Clamp(s.OutlineRed, 0, 255);
			b[10] = (byte)Clamp(s.OutlineGreen, 0, 255);
			b[11] = (byte)Clamp(s.OutlineBlue, 0, 255);
			b[12] = (byte)Clamp(s.OutlineAlpha, 0, 255);
			b[13] = (byte)(Clamp(s.Thickness, 0, 32) | Clamp((int)s.Outline, 0, 2) << 6);
			b[14] = (byte)gap;
			b[15] = (byte)(gap >> 8);
			b[16] = (byte)Clamp(s.Length, 0, 255);
			b[17] = (byte)Clamp(s.SpreadLimit, 0, 255);
			BitConverter.TryWriteBytes(b.AsSpan(18), packed);
			b[22] = (byte)Hundredths(s.ScopeDotScale, 0.1, 2);
			b[0] = Checksum(b);

			BigInteger value = new(b, isUnsigned: true, isBigEndian: true);
			char[] chars = new char[EncodedLength + 2];
			chars[0] = 'C';
			chars[1] = 'S';
			for (int i = 0; i < EncodedLength; i++)
			{
				value = BigInteger.DivRem(value, Base, out BigInteger digit);
				chars[i + 2] = Dictionary[(int)digit];
			}
			return new string(chars);
		}

		private static byte Checksum(byte[] b)
		{
			int sum = 0;
			for (int i = 1; i < b.Length; i++) sum += b[i];
			return (byte)sum;
		}

		// Floor(x + 0.5) rather than Math.Round: the site's reference encoder uses JS Math.round (half rounds up).
		private static int Hundredths(double value, double min, double max) =>
			(int)Floor(100 * (Clamp(value, min, max) - min) + 0.5);
	}
}
