# DemoNetwork

Namespace: StrikeLink.DemoParser

Provides network operations for retrieving and managing CS:GO match share codes using the specified authorization
 credentials.

```csharp
public class DemoNetwork : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoNetwork](./strikelink/demoparser/demonetwork.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
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

### **Dispose()**

Releases all resources used by the [DemoNetwork](./strikelink/demoparser/demonetwork.md).

```csharp
public void Dispose()
```

### **Dispose(Boolean)**

Releases the unmanaged resources used by the [DemoNetwork](./strikelink/demoparser/demonetwork.md) and optionally releases the managed resources.

```csharp
protected void Dispose(bool disposing)
```

#### Parameters

`disposing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true to release both managed and unmanaged resources; false to release only unmanaged resources.
