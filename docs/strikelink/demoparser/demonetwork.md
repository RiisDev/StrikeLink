# DemoNetwork

Namespace: StrikeLink.DemoParser

Provides network operations for retrieving and managing CS:GO match share codes using the specified authorization
 credentials.

```csharp
public class DemoNetwork
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoNetwork](./strikelink/demoparser/demonetwork.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class encapsulates HTTP communication with the Steam Web API to obtain match share codes. It
 manages request retries and handles rate limiting automatically. Instances of this class are not
 thread-safe.

## Constructors

### **DemoNetwork(DemoAuthorization)**

Provides network operations for retrieving and managing CS:GO match share codes using the specified authorization
 credentials.

```csharp
public DemoNetwork(DemoAuthorization demoAuth)
```

#### Parameters

`demoAuth` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization credentials used to authenticate requests to the CS:GO match sharing API. Cannot be null.

**Remarks:**

This class encapsulates HTTP communication with the Steam Web API to obtain match share codes. It
 manages request retries and handles rate limiting automatically. Instances of this class are not
 thread-safe.

## Methods

### **GetMatchShareCode()**

Retrieves the most recent valid match share code available for the authenticated user.

```csharp
public Task<string> GetMatchShareCode()
```

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A string containing the latest valid match share code, or null if no valid code is found.

**Remarks:**

This method iteratively queries for the next available match share code until a valid code is
 found. The returned share code can be used to access match details or share match information with
 others.

### **DecodeShareCode(String)**

Decodes a Counter-Strike: Global Offensive match share code into its corresponding match information.

```csharp
public static DemoShareCodeInfo DecodeShareCode(string shareCode)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The share code string to decode. Must be in the standard CS:GO share code format and contain only valid
 characters.

#### Returns

[DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>
A DemoMatchInfo object containing the match ID, reservation ID, and TV port extracted from the share code.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown if shareCode is not in a valid format or contains invalid characters.

### **EncodeShareCode(DemoShareCodeInfo)**

Encodes the specified match information into a shareable code string compatible with CS:GO share code format.

```csharp
public static string EncodeShareCode(DemoShareCodeInfo matchInfo)
```

#### Parameters

`matchInfo` [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>
The match information to encode. Cannot be null.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string containing the encoded share code representing the provided match information.
