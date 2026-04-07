# TokenService

Namespace: StrikeLink.Services.WebService.SubServices

Provides static methods for retrieving authentication and API tokens related to Counter-Strike and Steam services.

```csharp
public static class TokenService
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TokenService](./strikelink/services/webservice/subservices/tokenservice.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class is intended for use in scenarios where programmatic access to Counter-Strike
 authentication codes, match share codes, or Steam developer API keys is required. All methods require valid Steam
 session credentials and perform HTTP requests to official Steam endpoints. The class is static and cannot be
 instantiated.

## Methods

### **GetCounterStrikeAuthToken(String, String)**

Retrieves the Counter-Strike authentication token for the specified Steam session.

```csharp
public static Task<string> GetCounterStrikeAuthToken(string loginSecure, string sessionId)
```

#### Parameters

`loginSecure` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value of the 'steamLoginSecure' cookie associated with the user's Steam account. Cannot be null or empty.

`sessionId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value of the 'sessionid' cookie for the current Steam session. Cannot be null or empty.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A string containing the Counter-Strike authentication token if found.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown if the authentication token cannot be found in the response.

**Remarks:**

This method sends an HTTP request to the Steam support site using the provided session
 credentials. The caller is responsible for ensuring that the credentials are valid and have not expired.

### **GetCounterStrikeMatchToken(String, String)**

Retrieves the Counter-Strike match token for the specified Steam account session.

```csharp
public static Task<string> GetCounterStrikeMatchToken(string loginSecure, string sessionId)
```

#### Parameters

`loginSecure` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value of the 'steamLoginSecure' cookie associated with the user's Steam account. Cannot be null or empty.

`sessionId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value of the 'sessionid' cookie for the current Steam session. Cannot be null or empty.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A string containing the Counter-Strike match token if found.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown if the match token cannot be found in the response content.

**Remarks:**

This method sends an HTTP request to the Steam support site using the provided session
 credentials. The caller is responsible for ensuring that the credentials are valid and have the necessary
 permissions.

### **GetSteamDevKey(String, String)**

Retrieves the Steam Web API developer key associated with the specified session and login credentials.

```csharp
public static Task<string> GetSteamDevKey(string loginSecure, string sessionId)
```

#### Parameters

`loginSecure` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value of the 'steamLoginSecure' authentication cookie for the Steam account.

`sessionId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The session ID associated with the authenticated Steam session.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task that represents the asynchronous operation. The task result contains the Steam Web API developer key as a
 string.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown if the developer API key cannot be found in the response content.

**Remarks:**

The caller must provide valid authentication cookies for a Steam account that has access to the
 developer key page. This method performs an HTTP request to the Steam Community developer API key page and parses
 the response to extract the key.
