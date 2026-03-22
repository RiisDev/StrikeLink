
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
	// -------------------------------------------------------------------------
	// File header constants (all offsets from SQLite file format spec)
	// -------------------------------------------------------------------------
	private const int FileHeaderSize = 100;
	private const int PageSizeOffset = 16;      // u16 big-endian; value 1 means 65536
	private const int TextEncodingOffset = 56;      // u32: 1=UTF-8, 2=UTF-16le, 3=UTF-16be

	// B-tree page types
	private const byte TableInteriorPage = 0x05;
	private const byte TableLeafPage = 0x0D;

	// SQLite record serial type constants
	private const long SerialTypeNull = 0;
	private const long SerialTypeFloat64 = 7;
	private const long SerialTypeZero = 8;      // integer value 0
	private const long SerialTypeOne = 9;      // integer value 1
	private const long SerialTypeTextMin = 13;     // text: (N-13)/2 bytes, odd serial types
	private const long SerialTypeBlobMin = 12;     // blob: (N-12)/2 bytes, even serial types >= 12

	private readonly byte[] _data;
	private readonly int _pageSize;
	private readonly Encoding _textEncoding;

	// -------------------------------------------------------------------------
	// Construction
	// -------------------------------------------------------------------------

	private SqliteReader(byte[] data)
	{
		ValidateMagic(data);

		_data = data;
		_pageSize = ReadPageSize(data);
		_textEncoding = ReadTextEncoding(data);
	}

	/// <summary>Opens a SQLite database file and reads it fully into memory.</summary>
	public static SqliteReader Open(string filePath)
	{
		return new SqliteReader(File.ReadAllBytes(filePath));
	}

	// -------------------------------------------------------------------------
	// Public API
	// -------------------------------------------------------------------------

	/// <summary>
	/// Reads all rows from the named table and returns each row as an object?[] (column values).
	/// Column order matches the CREATE TABLE definition stored in sqlite_schema.
	/// </summary>
	public IEnumerable<object?[]> ReadTable(string tableName)
	{
		int rootPage = FindTableRootPage(tableName);
		if (rootPage == 0)
			yield break;

		foreach (object?[] row in WalkBTree(rootPage))
			yield return row;
	}

	// -------------------------------------------------------------------------
	// Schema resolution — reads sqlite_schema (always root page 1)
	// -------------------------------------------------------------------------

	private int FindTableRootPage(string tableName)
	{
		// sqlite_schema has columns: type, name, tbl_name, rootpage, sql
		// rootpage is column index 3
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

	// -------------------------------------------------------------------------
	// B-tree traversal
	// -------------------------------------------------------------------------

	private IEnumerable<object?[]> WalkBTree(int rootPage)
	{
		Stack<int> pageStack = new();
		pageStack.Push(rootPage);

		while (pageStack.Count > 0)
		{
			int pageNumber = pageStack.Pop();
			int pageOffset = (pageNumber - 1) * _pageSize;
			// Page 1 has the 100-byte file header before the b-tree page header
			int headerBase = pageNumber == 1 ? pageOffset + FileHeaderSize : pageOffset;

			byte pageType = _data[headerBase];

			if (pageType == TableLeafPage)
			{
				foreach (object?[] row in ReadLeafPage(pageOffset, headerBase))
					yield return row;
			}
			else if (pageType == TableInteriorPage)
			{
				// Interior page header is 12 bytes (leaf is 8 bytes)
				// Cell pointer array starts at headerBase + 12
				int numCells = ReadBigEndianU16(_data, headerBase + 3);
				int rightmostPtr = ReadBigEndianI32(_data, headerBase + 8);

				// Push rightmost child first so leftmost is processed first (LIFO)
				pageStack.Push(rightmostPtr);

				for (int i = 0; i < numCells; i++)
				{
					int ptrOffset = headerBase + 12 + (i * 2);
					int cellOffset = pageOffset + ReadBigEndianU16(_data, ptrOffset);
					int leftChild = ReadBigEndianI32(_data, cellOffset);
					pageStack.Push(leftChild);
				}
			}
			// Free/overflow pages are ignored — we never navigate to them directly
		}
	}

	private IEnumerable<object?[]> ReadLeafPage(int pageOffset, int headerBase)
	{
		// Leaf page header layout (8 bytes):
		//   0: page type (1 byte)
		//   1: first freeblock offset (2 bytes)
		//   3: number of cells (2 bytes)
		//   5: cell content area start (2 bytes)
		//   7: fragmented free bytes (1 byte)
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

	// -------------------------------------------------------------------------
	// Cell / record parsing
	// -------------------------------------------------------------------------

	private object?[]? TryReadCell(int cellOffset)
	{
		int pos = cellOffset;

		// Leaf cell layout:
		//   varint: total payload size in bytes
		//   varint: row id
		//   bytes:  payload (record)
		(long payloadSize, int varSize) = ReadVarint(_data, pos);
		pos += varSize;

		(_, varSize) = ReadVarint(_data, pos);  // row id — not needed
		pos += varSize;

		// Guard: skip cells whose payload spills into overflow pages.
		// Overflow threshold = floor((U - 12) * 32/255) + 23  where U = usable page size.
		// For simplicity we skip any cell that can't fit within the current page.
		int usable = _pageSize;  // reserved space ignored (almost always 0)
		long maxLocal = usable - 35;
		return payloadSize > maxLocal ? null : ParseRecord(_data, pos);
	}

	private object?[] ParseRecord(byte[] data, int recordStart)
	{
		int pos = recordStart;

		// Record header: leading varint is total header size (including itself)
		(long headerSize, int varLen) = ReadVarint(data, pos);
		int headerEnd = pos + (int)headerSize;
		pos += varLen;

		// Collect serial types from the header
		List<long> serialTypes = [];
		while (pos < headerEnd)
		{
			(long serialType, int sz) = ReadVarint(data, pos);
			serialTypes.Add(serialType);
			pos += sz;
		}

		// pos is now at the start of the body (immediately after the header)
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
		if (serialType == SerialTypeNull) return (null, 0);
		if (serialType == SerialTypeZero) return (0L, 0);
		if (serialType == SerialTypeOne) return (1L, 0);

		if (serialType == SerialTypeFloat64)
		{
			double value = ReadBigEndianDouble(data, pos);
			return (value, 8);
		}

		if (serialType >= 1 && serialType <= 6)
		{
			// Fixed-width signed integers: 1/2/3/4/6/8 bytes
			int[] byteCounts = [0, 1, 2, 3, 4, 6, 8];
			int byteCount = byteCounts[serialType];
			long value = ReadBigEndianSignedInt(data, pos, byteCount);
			return (value, byteCount);
		}

		if (serialType >= SerialTypeBlobMin && serialType % 2 == 0)
		{
			// BLOB: length = (serialType - 12) / 2
			int length = (int)((serialType - 12) / 2);
			byte[] blob = new byte[length];
			Buffer.BlockCopy(data, pos, blob, 0, length);
			return (blob, length);
		}

		if (serialType >= SerialTypeTextMin && serialType % 2 == 1)
		{
			// TEXT: length = (serialType - 13) / 2 bytes
			int length = (int)((serialType - 13) / 2);
			string text = _textEncoding.GetString(data, pos, length);
			return (text, length);
		}

		// Unknown / reserved serial type — skip
		return (null, 0);
	}

	// -------------------------------------------------------------------------
	// Chromium cookie query
	// -------------------------------------------------------------------------

	/// <summary>
	/// Returns all raw encrypted_value blobs for the given URL, keyed by cookie name.
	/// When multiple rows share the same name, the one with the latest expires_utc wins.
	/// Decrypt the returned bytes using DPAPI (Windows) or Keychain + AES-GCM (macOS/Linux).
	/// </summary>
	public Dictionary<string, byte[]> GetEncryptedCookies(Uri url)
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

		// Resolve encrypted_value index separately — it may not exist in older schemas
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

	/// <summary>
	/// Returns all cookies for the given URL, keyed by cookie name.
	/// When multiple rows share the same name, the one with the latest expires_utc wins.
	/// </summary>
	public Dictionary<string, string> GetCookies(Uri url)
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
	/// <summary>
	/// Reads the cookies table schema from sqlite_schema to resolve column indices by name,
	/// so the reader is robust against Chromium adding/reordering columns across versions.
	/// Returns [hostIndex, nameIndex, valueIndex, expiresIndex] or null on failure.
	/// </summary>
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

	/// <summary>
	/// Minimal CREATE TABLE SQL parser — extracts column names in declaration order.
	/// Handles quoted identifiers and is intentionally simple (not a full SQL parser).
	/// </summary>
	private static string[] ParseColumnNamesFromSql(string sql)
	{
		// Find the opening paren of the column list
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

			// Skip table constraints (PRIMARY KEY, UNIQUE, CHECK, FOREIGN KEY)
			if (trimmed.StartsWith("PRIMARY", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("UNIQUE", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("CHECK", StringComparison.OrdinalIgnoreCase) ||
			    trimmed.StartsWith("FOREIGN", StringComparison.OrdinalIgnoreCase))
				continue;

			// Column name is the first token; may be backtick/quote/bracket delimited
			string colName = trimmed.Split(' ', '\t')[0]
				.Trim('`', '"', '[', ']', '\'');

			if (!string.IsNullOrWhiteSpace(colName))
				names.Add(colName);
		}

		return [.. names];
	}

	// -------------------------------------------------------------------------
	// Binary helpers
	// -------------------------------------------------------------------------

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
		// Per spec: value of 1 in this field means page size is 65536
		return raw == 1 ? 65536 : raw;
	}

	private static Encoding ReadTextEncoding(byte[] data)
	{
		long encoding = ReadBigEndianU32(data, TextEncodingOffset);
		return encoding switch
		{
			2 => Encoding.Unicode,          // UTF-16 LE
			3 => Encoding.BigEndianUnicode, // UTF-16 BE
			_ => Encoding.UTF8,             // 1 = UTF-8 (default, and by far the most common)
		};
	}

	/// <summary>
	/// Reads a SQLite varint (1–9 bytes, big-endian, 7 bits per byte with MSB as continuation flag).
	/// Returns the decoded value and the number of bytes consumed.
	/// </summary>
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
				// Final byte: all 8 bits are data (no continuation bit)
				result = (result << 8) | b;
				break;
			}

			result = (result << 7) | (uint)(b & 0x7F);

			if ((b & 0x80) == 0)
				break;  // High bit clear = last byte of varint
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

		// Sign-extend based on the most significant bit of the last byte read
		int totalBits = byteCount * 8;
		long signBit = 1L << (totalBits - 1);
		return (raw & signBit) != 0
			? raw - (signBit << 1)   // two's complement sign extension
			: raw;
	}

	private static double ReadBigEndianDouble(byte[] data, int pos)
	{
		// Read 8 bytes big-endian into a long, then reinterpret as IEEE-754 double
		long bits = ReadBigEndianSignedInt(data, pos, byteCount: 8);
		return BitConverter.Int64BitsToDouble(bits);
	}

	// -------------------------------------------------------------------------
	// IDisposable
	// -------------------------------------------------------------------------

	/// <inheritdoc />
	public void Dispose() { /* data is managed memory; nothing to release */ }
}

/// <summary>
/// Decrypts Chromium cookie values using the encryption key stored in the Local State file.
///
/// Supports:
///   - AES-256-GCM (v10/v11 prefix) — Chrome 80+ on all platforms
///   - Legacy DPAPI (Windows only, pre-Chrome 80)
///
/// Usage:
///   1. Read Local State JSON from:
///        Windows : %LOCALAPPDATA%\Google\Chrome\User Data\Local State
///        macOS   : ~/Library/Application Support/Google/Chrome/Local State
///        Linux   : ~/.config/google-chrome/Local State
///   2. Pass the raw JSON string to ChromiumCookieDecryptor.Create()
///   3. Call Decrypt() on each encrypted_value blob
/// </summary>
public sealed class ChromiumCookieDecryptor : IDisposable
{
	private const string AesGcmV10Prefix = "v10";
	private const string AesGcmV11Prefix = "v11";
	private const int PrefixLength = 3;
	private const int NonceLengthBytes = 12;
	private const int TagLengthBytes = 16;

	// The decrypted AES-256 key (32 bytes) extracted from Local State
	private readonly byte[] _aesKey;

	private ChromiumCookieDecryptor(byte[] aesKey)
	{
		_aesKey = aesKey;
	}

	// -------------------------------------------------------------------------
	// Factory
	// -------------------------------------------------------------------------

	/// <summary>
	/// Creates a decryptor by parsing the Local State JSON and extracting + unwrapping the AES key.
	/// On Windows the key is protected with DPAPI; on macOS/Linux pass the raw key bytes directly
	/// via <see cref="CreateFromRawKey"/> after obtaining them from the Keychain.
	/// </summary>
	/// <param name="localStateJson">
	///     Raw contents of the 'Local State' file (no path needed — just pass File.ReadAllText(...)).
	/// </param>
	public static ChromiumCookieDecryptor CreateFromLocalState(string localStateJson)
	{
		byte[] encryptedKey = ExtractEncryptedKeyFromLocalState(localStateJson);
		byte[] aesKey = UnwrapKey(encryptedKey);
		return new ChromiumCookieDecryptor(aesKey);
	}

	/// <summary>
	/// Creates a decryptor from a raw 32-byte AES key.
	/// Use this on macOS/Linux after retrieving the key from the OS Keychain.
	/// </summary>
	public static ChromiumCookieDecryptor CreateFromRawKey(byte[] rawAesKey)
	{
		ArgumentNullException.ThrowIfNull(rawAesKey);
		return rawAesKey.Length != 32 ? throw new ArgumentException("AES key must be exactly 32 bytes (AES-256).", nameof(rawAesKey)) : new ChromiumCookieDecryptor((byte[])rawAesKey.Clone());
	}

	// -------------------------------------------------------------------------
	// Public decryption API
	// -------------------------------------------------------------------------

	/// <summary>
	/// Decrypts a single encrypted_value blob retrieved from the Chromium cookies database.
	/// Returns the plaintext string, or null if the blob is empty or unrecognised.
	/// </summary>
	public string? Decrypt(byte[]? encryptedValue)
	{
		if (encryptedValue is null || encryptedValue.Length == 0) return null;

		string prefix = Encoding.ASCII.GetString(encryptedValue, 0, Math.Min(PrefixLength, encryptedValue.Length));

		return prefix is AesGcmV10Prefix or AesGcmV11Prefix ? DecryptAesGcm(encryptedValue) : DecryptLegacyDpapi(encryptedValue);
	}

	/// <summary>
	/// Decrypts all blobs in a dictionary (as returned by SqliteReader.GetEncryptedCookies),
	/// returning a name → plaintext mapping. Entries that fail decryption are omitted.
	/// </summary>
	public Dictionary<string, string> DecryptAll(Dictionary<string, byte[]> encryptedCookies)
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
				// Individual cookie decryption failure should not abort the whole batch
				Log($"[ChromiumCookieDecryptor] Failed to decrypt '{name}': {ex.Message}");
			}

			if (plaintext is not null)
				result[name] = plaintext;
		}

		return result;
	}

	// -------------------------------------------------------------------------
	// Decryption internals
	// -------------------------------------------------------------------------

	/// <summary>
	/// AES-256-GCM decryption (Chrome 80+).
	///
	/// Blob layout after stripping the 3-byte version prefix:
	///   [0  .. 11] : 12-byte nonce
	///   [12 .. N-17] : ciphertext
	///   [N-16 .. N-1] : 16-byte GCM authentication tag
	/// </summary>
	private string DecryptAesGcm(byte[] blob)
	{
		ReadOnlySpan<byte> payload = blob.AsSpan(PrefixLength);
		ReadOnlySpan<byte> nonce = payload[..NonceLengthBytes];
		// Ciphertext sits between the nonce and the trailing auth tag
		ReadOnlySpan<byte> ciphertext = payload[NonceLengthBytes..^TagLengthBytes];
		ReadOnlySpan<byte> tag = payload[^TagLengthBytes..];

		byte[] plaintext = new byte[ciphertext.Length];

		using AesGcm aesGcm = new(_aesKey, TagLengthBytes);
		aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);

		return Encoding.UTF8.GetString(plaintext);
	}

	/// <summary>
	/// Legacy DPAPI decryption (Windows only, Chrome pre-80).
	/// The blob has no prefix — the entire byte array is a raw DPAPI blob.
	/// </summary>
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

	// -------------------------------------------------------------------------
	// Key extraction + unwrapping
	// -------------------------------------------------------------------------

	/// <summary>
	/// Parses the Local State JSON and returns the raw base64-decoded encrypted key bytes.
	/// The key lives at: os_crypt → encrypted_key
	/// Chromium prepends "DPAPI" (5 bytes) to the base64 payload as a marker — we strip it.
	/// </summary>
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

		// Validate and strip the "DPAPI" prefix marker (5 bytes)
		const string dpapiMarker = "DPAPI";
		byte[] expectedPrefix = Encoding.ASCII.GetBytes(dpapiMarker);
		bool hasExpectedPrefix = keyWithPrefix.AsSpan(0, expectedPrefix.Length)
			.SequenceEqual(expectedPrefix);

		return !hasExpectedPrefix ? throw new InvalidDataException(
			"Encrypted key does not start with expected 'DPAPI' marker. " +
			"The Local State file may be from an unsupported Chromium version.") : keyWithPrefix[expectedPrefix.Length..];
	}

	/// <summary>
	/// Unwraps the encrypted AES key.
	/// On Windows this uses DPAPI; on other platforms the key bytes are used directly
	/// (the caller is expected to have already retrieved the raw key from the OS Keychain).
	/// </summary>
	private static byte[] UnwrapKey(byte[] encryptedKey)
	{
		if (OperatingSystem.IsWindows())
		{
#if WINDOWS
			return ProtectedData.Unprotect(
				encryptedKey, optionalEntropy: null, DataProtectionScope.CurrentUser);
#endif
		}

		// macOS / Linux: Local State still contains a base64 key but it is not DPAPI-wrapped.
		// On these platforms CreateFromLocalState() will hand us the raw key bytes after
		// stripping the "DPAPI" marker, so no further unwrapping is needed.
		return encryptedKey;
	}

	// -------------------------------------------------------------------------
	// IDisposable — zero out the key from memory when done
	// -------------------------------------------------------------------------

	/// <summary>
	/// 
	/// </summary>
	public void Dispose()
	{
		CryptographicOperations.ZeroMemory(_aesKey);
	}
}