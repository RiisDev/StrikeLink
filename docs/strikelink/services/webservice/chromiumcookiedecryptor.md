# ChromiumCookieDecryptor

Namespace: StrikeLink.Services.WebService

Decrypts Chromium cookie values using the encryption key stored in the Local State file.
 
 Supports:
 - AES-256-GCM (v10/v11 prefix) — Chrome 80+ on all platforms
 - Legacy DPAPI (Windows only, pre-Chrome 80)
 
 Usage:
 1. Read Local State JSON from:
 Windows : %LOCALAPPDATA%\Google\Chrome\User Data\Local State
 macOS : ~/Library/Application Support/Google/Chrome/Local State
 Linux : ~/.config/google-chrome/Local State
 2. Pass the raw JSON string to ChromiumCookieDecryptor.Create()
 3. Call Decrypt() on each encrypted_value blob

```csharp
public sealed class ChromiumCookieDecryptor : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ChromiumCookieDecryptor](./strikelink/services/webservice/chromiumcookiedecryptor.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Methods

### **CreateFromLocalState(String)**

Creates a decryptor by parsing the Local State JSON and extracting + unwrapping the AES key.
 On Windows the key is protected with DPAPI; on macOS/Linux pass the raw key bytes directly
 via [ChromiumCookieDecryptor.CreateFromRawKey(Byte[])](./strikelink/services/webservice/chromiumcookiedecryptor.md#createfromrawkeybyte) after obtaining them from the Keychain.

```csharp
public static ChromiumCookieDecryptor CreateFromLocalState(string localStateJson)
```

#### Parameters

`localStateJson` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Raw contents of the 'Local State' file (no path needed — just pass File.ReadAllText(...)).

#### Returns

[ChromiumCookieDecryptor](./strikelink/services/webservice/chromiumcookiedecryptor.md)<br>

### **CreateFromRawKey(Byte[])**

Creates a decryptor from a raw 32-byte AES key.
 Use this on macOS/Linux after retrieving the key from the OS Keychain.

```csharp
public static ChromiumCookieDecryptor CreateFromRawKey(Byte[] rawAesKey)
```

#### Parameters

`rawAesKey` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[ChromiumCookieDecryptor](./strikelink/services/webservice/chromiumcookiedecryptor.md)<br>

### **Decrypt(Byte[])**

Decrypts a single encrypted_value blob retrieved from the Chromium cookies database.
 Returns the plaintext string, or null if the blob is empty or unrecognised.

```csharp
public string Decrypt(Byte[] encryptedValue)
```

#### Parameters

`encryptedValue` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DecryptAll(Dictionary&lt;String, Byte[]&gt;)**

Decrypts all blobs in a dictionary (as returned by SqliteReader.GetEncryptedCookies),
 returning a name → plaintext mapping. Entries that fail decryption are omitted.

```csharp
public Dictionary<string, string> DecryptAll(Dictionary<string, Byte[]> encryptedCookies)
```

#### Parameters

`encryptedCookies` [Dictionary&lt;String, Byte[]&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

#### Returns

[Dictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Dispose()**



```csharp
public void Dispose()
```
