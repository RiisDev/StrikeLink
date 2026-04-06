# DemoAuthorization

Namespace: StrikeLink.DemoParser

Represents the authorization credentials required to access match data using a Steam account, including the Steam
 ID, authentication code, and match share code.

```csharp
public class DemoAuthorization
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class encapsulates the information necessary for authenticating and retrieving match details
 from the Steam platform. All properties must be set to valid values that conform to the expected formats. Invalid
 values will result in a FormatException being thrown when setting the properties.

## Properties

### **SteamId**

Gets or sets the Steam user identifier associated with this instance.

```csharp
public long SteamId { get; set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

**Remarks:**

The Steam ID must be a 17-digit number within the valid range for Steam user accounts. Assigning
 a value outside this range will result in a FormatException.

### **AuthCode**

Gets or sets the authentication code used for verifying user identity.

```csharp
public string AuthCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The authentication code must be non-empty and match the required format. The value is
 automatically converted to uppercase when set. An exception is thrown if the value does not meet the format
 requirements.

### **MatchShareCode**

Gets or sets the match share code used to identify a specific match.

```csharp
public string MatchShareCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The match share code must be a non-empty string that matches the required format. The value is
 automatically converted to uppercase when set.

### **ApiKey**

Gets or sets the API key used for authentication with the service.

```csharp
public string ApiKey { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The API key must be in the correct format as defined by the service. Setting an invalid value
 will result in a FormatException being thrown. The value is automatically converted to uppercase when
 set.

## Constructors

### **DemoAuthorization(Int64, String, String, String)**

Initializes a new instance of the DemoAuthorization class with the specified Steam ID, authorization code, and
 match share code.

```csharp
public DemoAuthorization(long steamId, string authCode, string matchShareCode, string apiKey)
```

#### Parameters

`steamId` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The unique 64-bit Steam identifier associated with the user.

`authCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The authorization code used to authenticate the user or session. Cannot be null.

`matchShareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The share code representing the specific match to authorize. Cannot be null.

`apiKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The steam developer API key. Cannot be null,
