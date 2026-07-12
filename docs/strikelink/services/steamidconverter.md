# SteamIdConverter

Namespace: StrikeLink.Services

Provides methods for converting between different Steam ID formats, including Steam64, Steam2, and Steam3
 representations.

```csharp
public class SteamIdConverter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SteamIdConverter](./strikelink/services/steamidconverter.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class enables conversion and parsing of Steam user identifiers across multiple formats
 commonly used in Steam APIs and community tools. It supports creating instances from any supported format and
 retrieving the corresponding representations. All conversions are performed using standard algorithms based on
 Valve's documented Steam ID structure. This class is thread-safe for read-only operations.

## Properties

### **Steam64**

Gets the 64-bit Steam identifier associated with the user.

```csharp
public ulong Steam64 { get; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **AccountId**

Gets the unique identifier for the account.

```csharp
public uint AccountId { get; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **Steam2**

Gets the Steam2 (legacy) identifier for the account in the format used by older Steam systems.

```csharp
public string Steam2 { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The Steam2 ID is primarily used for compatibility with legacy systems and may not be suitable for
 modern Steam integrations. Prefer using Steam3 or SteamID64 formats for new development unless interoperability
 with older systems is required.

### **Steam3**

Gets the Steam3 identifier for the account in the format used by Steam services.

```csharp
public string Steam3 { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The Steam3 identifier is commonly used for referencing user accounts in Steam APIs and community
 features. The format is [U:1:AccountId], where AccountId is the unique numeric identifier for the
 account.

## Methods

### **FromSteam64(UInt64)**

Creates a new instance of the SteamIdConverter class from a 64-bit Steam ID.

```csharp
public static SteamIdConverter FromSteam64(ulong steam64)
```

#### Parameters

`steam64` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The 64-bit unsigned integer representing the Steam ID to convert.

#### Returns

[SteamIdConverter](./strikelink/services/steamidconverter.md)<br>
A SteamIdConverter instance initialized with the specified 64-bit Steam ID.

### **FromAccountId(UInt32)**

Creates a new instance of the SteamIdConverter class from the specified Steam account ID.

```csharp
public static SteamIdConverter FromAccountId(uint accountId)
```

#### Parameters

`accountId` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
The 32-bit unsigned integer representing the Steam account ID to convert.

#### Returns

[SteamIdConverter](./strikelink/services/steamidconverter.md)<br>
A SteamIdConverter instance initialized with the corresponding Steam64 identifier.

### **FromSteam3(String)**

Creates a new instance of the SteamIdConverter class from a Steam3-formatted identifier string.

```csharp
public static SteamIdConverter FromSteam3(string steam3)
```

#### Parameters

`steam3` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Steam3-formatted identifier string to convert. Must be in the expected format and not null.

#### Returns

[SteamIdConverter](./strikelink/services/steamidconverter.md)<br>
A SteamIdConverter instance representing the specified Steam3 identifier.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.

**Remarks:**

The input string must follow the standard Steam3 ID format (e.g., "[U:1:123456]"). An exception
 may be thrown if the format is invalid.

### **FromSteam2(String)**

Creates a new instance of the SteamIdConverter class from a Steam2-formatted identifier string.

```csharp
public static SteamIdConverter FromSteam2(string steam2)
```

#### Parameters

`steam2` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Steam2 identifier string in the format "STEAM_X:Y:Z" to convert.

#### Returns

[SteamIdConverter](./strikelink/services/steamidconverter.md)<br>
A SteamIdConverter instance representing the specified Steam2 identifier.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.

**Remarks:**

The input string must be in the standard Steam2 format. An exception may be thrown if the format
 is invalid or if parsing fails.

### **ToSteam2(String)**

Converts a Steam identifier in various supported formats to its legacy Steam2 format (e.g., "STEAM_X:Y:Z").

```csharp
public static string ToSteam2(string input)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Steam identifier to convert. Supported formats include Steam64, Steam3, Steam2, and account ID. Cannot be null
 or empty.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string containing the Steam2 representation of the specified identifier.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown if the input is null, empty, or not recognized as a valid Steam identifier format.

**Remarks:**

This method accepts multiple Steam identifier formats and returns the equivalent Steam2 format.
 If the input does not match any supported format, an exception is thrown.
