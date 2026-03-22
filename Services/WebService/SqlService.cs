/*
 *
 * AI Notice, I could not be fucked with to write this and didn't want to use another nuget package
 *
 */
using System.Security.Cryptography;
#pragma warning disable CA1031

namespace StrikeLink.Services.WebService;

internal sealed class SqliteReader : IDisposable
{
	private const int FileHeaderSize = 100;
	private const int PageSizeOffset = 16;
	private const int TextEncodingOffset = 56;

	private const byte TableInteriorPage = 0x05;
	private const byte TableLeafPage = 0x0D;

	private const long SerialTypeNull = 0;
	private const long SerialTypeFloat64 = 7;
	private const long SerialTypeZero = 8;
	private const long SerialTypeOne = 9;
	private const long SerialTypeTextMin = 13;
	private const long SerialTypeBlobMin = 12;

	private readonly byte[] _data;
	private readonly int _pageSize;
	private readonly Encoding _textEncoding;

	private SqliteReader(byte[] data)
	{
		ValidateMagic(data);

		_data = data;
		_pageSize = ReadPageSize(data);
		_textEncoding = ReadTextEncoding(data);
	}

	/// <summary>Opens a SQLite database file and reads it fully into memory.</summary>
	internal static SqliteReader Open(string filePath)
	{
		return new SqliteReader(File.ReadAllBytes(filePath));
	}

	/// <summary>
	/// Reads all rows from the named table and returns each row as an object?[] (column values).
	/// Column order matches the CREATE TABLE definition stored in sqlite_schema.
	/// </summary>
	internal IEnumerable<object?[]> ReadTable(string tableName)
	{
		int rootPage = FindTableRootPage(tableName);
		if (rootPage == 0)
			yield break;

		foreach (object?[] row in WalkBTree(rootPage))
			yield return row;
	}

	private int FindTableRootPage(string tableName)
	{
		foreach (object?[] row in WalkBTree(rootPage: 1))
		{
			if (row.Length < 4)
				continue;

			string? type = row[0] as string;
			string? name = row[1] as string;
			object? rootObj = row[3];

			bool isTable = string.Equals(type, "table", StringComparison.OrdinalIgnoreCase);
			bool nameMatches = string.Equals(name, tableName, StringComparison.OrdinalIgnoreCase);
			bool hasRootPage = rootObj is long rootLong && rootLong > 0;

			if (isTable && nameMatches && hasRootPage)
				return (int)(long)rootObj!;
		}

		return 0;
	}

	private IEnumerable<object?[]> WalkBTree(int rootPage)
	{
		Stack<int> pageStack = new();
		pageStack.Push(rootPage);

		while (pageStack.Count > 0)
		{
			int pageNumber = pageStack.Pop();
			int pageOffset = (pageNumber - 1) * _pageSize;
			int headerBase = pageNumber == 1 ? pageOffset + FileHeaderSize : pageOffset;

			byte pageType = _data[headerBase];

			if (pageType == TableLeafPage)
			{
				foreach (object?[] row in ReadLeafPage(pageOffset, headerBase))
					yield return row;
			}
			else if (pageType == TableInteriorPage)
			{
				int numCells = ReadBigEndianU16(_data, headerBase + 3);
				int rightmostPtr = ReadBigEndianI32(_data, headerBase + 8);

				pageStack.Push(rightmostPtr);

				for (int i = 0; i < numCells; i++)
				{
					int ptrOffset = headerBase + 12 + (i * 2);
					int cellOffset = pageOffset + ReadBigEndianU16(_data, ptrOffset);
					int leftChild = ReadBigEndianI32(_data, cellOffset);
					pageStack.Push(leftChild);
				}
			}
		}
	}

	private IEnumerable<object?[]> ReadLeafPage(int pageOffset, int headerBase)
	{
		int numCells = ReadBigEndianU16(_data, headerBase + 3);

		for (int i = 0; i < numCells; i++)
		{
			int ptrOffset = headerBase + 8 + (i * 2);
			int cellOffset = pageOffset + ReadBigEndianU16(_data, ptrOffset);

			object?[]? row = TryReadCell(cellOffset);
			if (row is not null)
				yield return row;
		}
	}

	private object?[]? TryReadCell(int cellOffset)
	{
		int pos = cellOffset;

		(long payloadSize, int varSize) = ReadVarint(_data, pos);
		pos += varSize;

		(_, varSize) = ReadVarint(_data, pos);
		pos += varSize;

		long maxLocal = _pageSize - 35;
		return payloadSize > maxLocal ? null : ParseRecord(_data, pos);
	}

	private object?[] ParseRecord(byte[] data, int recordStart)
	{
		int pos = recordStart;

		(long headerSize, int varLen) = ReadVarint(data, pos);
		int headerEnd = pos + (int)headerSize;
		pos += varLen;

		List<long> serialTypes = [];
		while (pos < headerEnd)
		{
			(long serialType, int sz) = ReadVarint(data, pos);
			serialTypes.Add(serialType);
			pos += sz;
		}

		int bodyPos = recordStart + (int)headerSize;
		object?[] values = new object?[serialTypes.Count];

		for (int col = 0; col < serialTypes.Count; col++)
		{
			(object? value, int bytesRead) = ReadSerialValue(data, bodyPos, serialTypes[col]);
			values[col] = value;
			bodyPos += bytesRead;
		}

		return values;
	}

	private (object? value, int bytesRead) ReadSerialValue(byte[] data, int pos, long serialType)
	{
		switch (serialType)
		{
			case SerialTypeNull:
				return (null, 0);
			case SerialTypeZero:
				return (0L, 0);
			case SerialTypeOne:
				return (1L, 0);
			case SerialTypeFloat64:
			{
				double value = ReadBigEndianDouble(data, pos);
				return (value, 8);
			}
			case >= 1 and <= 6:
			{
				int[] byteCounts = [0, 1, 2, 3, 4, 6, 8];
				int byteCount = byteCounts[serialType];
				long value = ReadBigEndianSignedInt(data, pos, byteCount);
				return (value, byteCount);
			}
			case >= SerialTypeBlobMin when serialType % 2 == 0:
			{
				int length = (int)((serialType - 12) / 2);
				byte[] blob = new byte[length];
				Buffer.BlockCopy(data, pos, blob, 0, length);
				return (blob, length);
			}
			case >= SerialTypeTextMin when serialType % 2 == 1:
			{
				int length = (int)((serialType - 13) / 2);
				string text = _textEncoding.GetString(data, pos, length);
				return (text, length);
			}
			default:
				return (null, 0);
		}
	}

	internal Dictionary<string, byte[]> GetEncryptedCookies(Uri url)
	{
		ArgumentNullException.ThrowIfNull(url);

		string host = url.Host;
		string hostWithDot = $".{host}";

		int[]? columnIndices = ResolveCookieColumnIndices();
		if (columnIndices is null)
			return [];

		(int idxHost, int idxName, int idxExpires) = (
			columnIndices[0], columnIndices[1], columnIndices[3]
		);

		int idxEncrypted = ResolveSingleColumnIndex("cookies", "encrypted_value");
		if (idxEncrypted < 0)
			return [];

		Dictionary<string, (byte[] blob, long expires)> best = [];

		foreach (object?[] row in ReadTable("cookies"))
		{
			if (row.Length <= Math.Max(idxHost, Math.Max(idxName, Math.Max(idxExpires, idxEncrypted))))
				continue;

			string? hostKey = row[idxHost] as string;
			string? name = row[idxName] as string;
			byte[]? encryptedValue = row[idxEncrypted] as byte[];
			long expiresUtc = row[idxExpires] is long e ? e : long.MinValue;

			bool hostMatches = string.Equals(hostKey, host, StringComparison.OrdinalIgnoreCase)
			                   || string.Equals(hostKey, hostWithDot, StringComparison.OrdinalIgnoreCase);

			if (!hostMatches || name is null || encryptedValue is null || encryptedValue.Length == 0)
				continue;

			bool alreadySeen = best.TryGetValue(name, out (byte[] blob, long expires) existing);
			bool isLaterExpiry = !alreadySeen || expiresUtc > existing.expires;

			if (isLaterExpiry)
				best[name] = (encryptedValue, expiresUtc);
		}

		return best.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.blob);
	}
	private int ResolveSingleColumnIndex(string tableName, string columnName)
	{
		foreach (object?[] row in WalkBTree(rootPage: 1))
		{
			if (row.Length < 5)
				continue;

			string? type = row[0] as string;
			string? name = row[1] as string;
			string? sql = row[4] as string;

			bool isTable = string.Equals(type, "table", StringComparison.OrdinalIgnoreCase);
			bool nameMatches = string.Equals(name, tableName, StringComparison.OrdinalIgnoreCase);

			if (!isTable || !nameMatches || sql is null)
				continue;

			string[] columns = ParseColumnNamesFromSql(sql);
			return Array.FindIndex(columns, c => string.Equals(c, columnName, StringComparison.OrdinalIgnoreCase));
		}

		return -1;
	}


	internal Dictionary<string, string> GetCookies(Uri url)
	{
		ArgumentNullException.ThrowIfNull(url);

		string host = url.Host;
		string hostWithDot = $".{host}";

		int[]? columnIndices = ResolveCookieColumnIndices();
		if (columnIndices is null)
			return [];

		(int idxHost, int idxName, int idxValue, int idxExpires) = (
			columnIndices[0], columnIndices[1], columnIndices[2], columnIndices[3]
		);

		Dictionary<string, (string value, long expires)> best = [];

		foreach (object?[] row in ReadTable("cookies"))
		{
			if (row.Length <= Math.Max(idxHost, Math.Max(idxName, Math.Max(idxValue, idxExpires))))
				continue;

			string? hostKey = row[idxHost] as string;
			long expiresUtc = row[idxExpires] is long e ? e : long.MinValue;

			bool hostMatches = string.Equals(hostKey, host, StringComparison.OrdinalIgnoreCase)
			                   || string.Equals(hostKey, hostWithDot, StringComparison.OrdinalIgnoreCase);

			if (!hostMatches || row[idxName] is not string name || row[idxValue] is not string value)
				continue;

			bool alreadySeen = best.TryGetValue(name, out (string value, long expires) existing);
			bool isLaterExpiry = !alreadySeen || expiresUtc > existing.expires;

			if (isLaterExpiry)
				best[name] = (value, expiresUtc);
		}

		return best.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.value);
	}

	private int[]? ResolveCookieColumnIndices()
	{
		foreach (object?[] row in WalkBTree(rootPage: 1))
		{
			if (row.Length < 5)
				continue;

			string? type = row[0] as string;
			string? name = row[1] as string;
			string? sql = row[4] as string;

			bool isTable = string.Equals(type, "table", StringComparison.OrdinalIgnoreCase);
			bool isCookies = string.Equals(name, "cookies", StringComparison.OrdinalIgnoreCase);

			if (!isTable || !isCookies || sql is null)
				continue;

			string[] columns = ParseColumnNamesFromSql(sql);

			int idxHost = Array.FindIndex(columns, c => c == "host_key");
			int idxName = Array.FindIndex(columns, c => c == "name");
			int idxValue = Array.FindIndex(columns, c => c == "value");
			int idxExpires = Array.FindIndex(columns, c => c == "expires_utc");

			if (idxHost < 0 || idxName < 0 || idxValue < 0 || idxExpires < 0)
				return null;

			return [idxHost, idxName, idxValue, idxExpires];
		}

		return null;
	}

	private static string[] ParseColumnNamesFromSql(string sql)
	{
		int start = sql.IndexOf('(', StringComparison.InvariantCulture);
		int end = sql.LastIndexOf(')');
		if (start < 0 || end < 0 || end <= start)
			return [];

		string columnBlock = sql[(start + 1)..end];
		string[] parts = columnBlock.Split(',');
		List<string> names = [];

		foreach (string part in parts)
		{
			string trimmed = part.Trim();

			if (trimmed.StartsWith("PRIMARY", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("UNIQUE", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("CHECK", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("FOREIGN", StringComparison.OrdinalIgnoreCase))
				continue;

			string colName = trimmed.Split(' ', '\t')[0]
				.Trim('`', '"', '[', ']', '\'');

			if (!string.IsNullOrWhiteSpace(colName))
				names.Add(colName);
		}

		return [.. names];
	}

	private static void ValidateMagic(byte[] data)
	{
		byte[] expectedMagic = "SQLite format 3\0"u8.ToArray();
		for (int i = 0; i < expectedMagic.Length; i++)
		{
			if (data[i] != expectedMagic[i])
				throw new InvalidDataException("File is not a valid SQLite database.");
		}
	}

	private static int ReadPageSize(byte[] data)
	{
		int raw = ReadBigEndianU16(data, PageSizeOffset);
		return raw == 1 ? 65536 : raw;
	}

	private static Encoding ReadTextEncoding(byte[] data)
	{
		long encoding = ReadBigEndianU32(data, TextEncodingOffset);
		return encoding switch
		{
			2 => Encoding.Unicode,
			3 => Encoding.BigEndianUnicode,
			_ => Encoding.UTF8,
		};
	}

	private static (long value, int bytesRead) ReadVarint(byte[] data, int pos)
	{
		long result = 0;
		int bytesRead = 0;

		for (int i = 0; i < 9; i++)
		{
			byte b = data[pos + i];
			bytesRead++;

			if (i == 8)
			{
				result = (result << 8) | b;
				break;
			}

			result = (result << 7) | (uint)(b & 0x7F);

			if ((b & 0x80) == 0)
				break;
		}

		return (result, bytesRead);
	}

	private static int ReadBigEndianU16(byte[] data, int offset)
	{
		return (data[offset] << 8) | data[offset + 1];
	}

	private static long ReadBigEndianU32(byte[] data, int offset)
	{
		return ((long)data[offset] << 24)
		       | ((long)data[offset + 1] << 16)
		       | ((long)data[offset + 2] << 8)
		       | data[offset + 3];
	}

	private static int ReadBigEndianI32(byte[] data, int offset)
	{
		return (data[offset] << 24)
		       | (data[offset + 1] << 16)
		       | (data[offset + 2] << 8)
		       | data[offset + 3];
	}

	private static long ReadBigEndianSignedInt(byte[] data, int pos, int byteCount)
	{
		long raw = 0;
		for (int i = 0; i < byteCount; i++)
			raw = (raw << 8) | data[pos + i];

		int totalBits = byteCount * 8;
		long signBit = 1L << (totalBits - 1);
		return (raw & signBit) != 0
			? raw - (signBit << 1)
			: raw;
	}

	private static double ReadBigEndianDouble(byte[] data, int pos)
	{
		long bits = ReadBigEndianSignedInt(data, pos, byteCount: 8);
		return BitConverter.Int64BitsToDouble(bits);
	}

	/// <inheritdoc />
	public void Dispose() { }
}

internal sealed class ChromiumCookieDecryptor : IDisposable
{
	private const string AesGcmV10Prefix = "v10";
	private const string AesGcmV11Prefix = "v11";
	private const int PrefixLength = 3;
	private const int NonceLengthBytes = 12;
	private const int TagLengthBytes = 16;

	private const int CbcIvLength = 16;
	private static readonly byte[] CbcIv = Enumerable.Repeat((byte)' ', CbcIvLength).ToArray();

	private readonly byte[] _aesKey;

	private ChromiumCookieDecryptor(byte[] aesKey)
	{
		_aesKey = aesKey;
	}

	internal static ChromiumCookieDecryptor CreateFromLocalState(string localStateJson)
	{
		byte[] encryptedKey;

		if (OperatingSystem.IsWindows())
		{
			encryptedKey = ExtractEncryptedKeyFromLocalState(localStateJson);
		}
		else if (OperatingSystem.IsLinux())
		{
			LinuxChromiumKeyProvider.DecryptionKeyResult result = LinuxChromiumKeyProvider.GetKeyAsync("Chromium").Result;
			encryptedKey = result.Key;
		}
		else
		{
			throw new InvalidOperationException("Failed to find key, unable to determine operating system.");
		}

		byte[] aesKey = UnwrapKey(encryptedKey);
		return new ChromiumCookieDecryptor(aesKey);
	}

	internal static ChromiumCookieDecryptor CreateFromRawKey(byte[] rawAesKey)
	{
		ArgumentNullException.ThrowIfNull(rawAesKey);
		return rawAesKey.Length != 32 ? throw new ArgumentException("AES key must be exactly 32 bytes (AES-256).", nameof(rawAesKey)) : new ChromiumCookieDecryptor((byte[])rawAesKey.Clone());
	}

	internal string? Decrypt(byte[]? encryptedValue)
	{
		if (encryptedValue is null || encryptedValue.Length == 0) return null;

		string prefix = Encoding.ASCII.GetString(encryptedValue, 0, Math.Min(PrefixLength, encryptedValue.Length));

		if (prefix is not (AesGcmV10Prefix or AesGcmV11Prefix))
			return DecryptLegacyDpapi(encryptedValue);

		return OperatingSystem.IsWindows() ? DecryptAesGcm(encryptedValue) : DecryptAesCbc(encryptedValue);
	}

	internal Dictionary<string, string> DecryptAll(Dictionary<string, byte[]> encryptedCookies)
	{
		ArgumentNullException.ThrowIfNull(encryptedCookies);
		Dictionary<string, string> result = [];

		foreach ((string name, byte[] blob) in encryptedCookies)
		{
			string? plaintext = null;

			try
			{
				plaintext = Decrypt(blob);
			}
			catch (Exception ex)
			{
				Log($"[ChromiumCookieDecryptor] Failed to decrypt '{name}': {ex.Message}");
			}

			if (plaintext is not null)
				result[name] = plaintext;
		}

		return result;
	}

	private string DecryptAesGcm(byte[] blob)
	{
		ReadOnlySpan<byte> payload = blob.AsSpan(PrefixLength);
		ReadOnlySpan<byte> nonce = payload[..NonceLengthBytes];
		ReadOnlySpan<byte> ciphertext = payload[NonceLengthBytes..^TagLengthBytes];
		ReadOnlySpan<byte> tag = payload[^TagLengthBytes..];

		byte[] plaintext = new byte[ciphertext.Length];

		using AesGcm aesGcm = new(_aesKey, TagLengthBytes);
		aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);

		return Encoding.UTF8.GetString(plaintext);
	}

	private string DecryptAesCbc(byte[] blob)
	{
		ReadOnlySpan<byte> afterPrefix = blob.AsSpan(PrefixLength);
		string prefix = Encoding.ASCII.GetString(blob, 0, PrefixLength);

		Log($"[DecryptAesCbc] Prefix: '{prefix}', Total blob: {blob.Length}, After prefix: {afterPrefix.Length}");

		ReadOnlySpan<byte> iv;
		ReadOnlySpan<byte> ciphertext;
		byte[] key;

		if (prefix == AesGcmV11Prefix)
		{
			iv = afterPrefix[..16];
			ciphertext = afterPrefix[16..];
			key = _aesKey;
			Log($"[DecryptAesCbc] v11 - IV: {Convert.ToHexString(iv)}, Ciphertext length: {ciphertext.Length}");
		}
		else
		{
			iv = CbcIv;
			ciphertext = afterPrefix;
			key = LinuxChromiumKeyProvider.DeriveKey("peanuts"u8.ToArray());
			Log($"[DecryptAesCbc] v10 - IV: spaces, Ciphertext length: {ciphertext.Length}");
		}

		using Aes aes = Aes.Create();
		aes.Key = key;
		aes.IV = iv.ToArray();
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.PKCS7;

		using ICryptoTransform decryptor = aes.CreateDecryptor();
		byte[] plaintext = decryptor.TransformFinalBlock(
			ciphertext.ToArray(), 0, ciphertext.Length
		);

		return Encoding.UTF8.GetString(plaintext);
	}

	private static string DecryptLegacyDpapi(byte[] blob)
	{
		if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException(
			"Legacy DPAPI decryption is only supported on Windows. " +
			"Non-Windows Chromium builds always use AES-GCM.");
#if WINDOWS
		byte[] plaintext = ProtectedData.Unprotect(blob, optionalEntropy: null, DataProtectionScope.CurrentUser);
#else
		byte[] plaintext = [];
#endif
		return Encoding.UTF8.GetString(plaintext);
	}

	private static byte[] ExtractEncryptedKeyFromLocalState(string localStateJson)
	{
		string jsonData = File.ReadAllText(localStateJson);
		using JsonDocument doc = JsonDocument.Parse(jsonData);

		bool hasOsCrypt = doc.RootElement.TryGetProperty("os_crypt", out JsonElement osCrypt);
		if (!hasOsCrypt)
			throw new InvalidDataException("Local State JSON is missing the 'os_crypt' section.");

		bool hasKey = osCrypt.TryGetProperty("encrypted_key", out JsonElement encryptedKeyElement);
		if (!hasKey)
			throw new InvalidDataException("Local State JSON is missing 'os_crypt.encrypted_key'.");

		string? base64Key = encryptedKeyElement.GetString();
		if (string.IsNullOrEmpty(base64Key))
			throw new InvalidDataException("'os_crypt.encrypted_key' is empty.");

		byte[] keyWithPrefix = Convert.FromBase64String(base64Key);

		const string dpapiMarker = "DPAPI";
		byte[] expectedPrefix = Encoding.ASCII.GetBytes(dpapiMarker);
		bool hasExpectedPrefix = keyWithPrefix.AsSpan(0, expectedPrefix.Length)
			.SequenceEqual(expectedPrefix);

		return !hasExpectedPrefix ? throw new InvalidDataException(
			"Encrypted key does not start with expected 'DPAPI' marker. " +
			"The Local State file may be from an unsupported Chromium version.") : keyWithPrefix[expectedPrefix.Length..];
	}

	private static byte[] UnwrapKey(byte[] encryptedKey)
	{
		if (OperatingSystem.IsWindows())
		{
#if WINDOWS
			return ProtectedData.Unprotect(
				encryptedKey, optionalEntropy: null, DataProtectionScope.CurrentUser);
#endif
		}

		return encryptedKey;
	}

	/// <summary>
	/// 
	/// </summary>
	public void Dispose()
	{
		CryptographicOperations.ZeroMemory(_aesKey);
	}
}