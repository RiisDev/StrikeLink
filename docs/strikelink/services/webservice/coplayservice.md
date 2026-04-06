# CoPlayService

Namespace: StrikeLink.Services.WebService

Provides methods for retrieving and parsing Steam Co-Play session data for a specific user.

```csharp
public class CoPlayService : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CoPlayService](./strikelink/services/webservice/coplayservice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This service manages HTTP communication with the Steam Community website to obtain co-play session
 information. It requires a valid Steam login token for authentication. The class is disposable and should be
 disposed when no longer needed to release network resources.

## Constructors

### **CoPlayService(String)**

Initializes a new instance of the CoPlayService class using the specified Steam login secure token.

```csharp
public CoPlayService(string loginSecure)
```

#### Parameters

`loginSecure` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Steam login secure token used to authenticate requests. Cannot be null or empty. Must contain a '|' character
 to separate the user ID.

**Remarks:**

The provided login secure token is used to set authentication cookies for HTTP requests. The user
 ID is extracted from the portion of the token before the '|' character.

## Methods

### **GetCoplayData()**

Retrieves a list of co-play sessions for the current Steam user.

```csharp
public Task<List<CoPlaySession>> GetCoplayData()
```

#### Returns

[Task&lt;List&lt;CoPlaySession&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task that represents the asynchronous operation. The task result contains a list of [CoPlaySession](./strikelink/services/webservice/coplaysession.md)
 objects representing the user's co-play sessions. The list is empty if no co-play data is available.

**Remarks:**

This method sends an HTTP request to the Steam Community service to obtain co-play session data
 for the specified user. The operation is performed asynchronously and requires network connectivity. An exception
 is thrown if the HTTP request fails.

### **Dispose()**

```csharp
public void Dispose()
```

### **Dispose(Boolean)**

Releases the unmanaged resources used by the class and optionally releases the managed resources.

```csharp
protected void Dispose(bool disposing)
```

#### Parameters

`disposing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true to release both managed and unmanaged resources; false to release only unmanaged resources.

**Remarks:**

This method is called by public Dispose methods and can be overridden to release additional
 resources. When disposing is true, managed resources can be disposed; when false, only unmanaged resources should
 be released.
