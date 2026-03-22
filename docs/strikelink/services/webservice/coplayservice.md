# CoPlayService

Namespace: StrikeLink.Services.WebService

```csharp
public class CoPlayService : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CoPlayService](./strikelink/services/webservice/coplayservice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Constructors

### **CoPlayService(String)**

```csharp
public CoPlayService(string loginSecure)
```

#### Parameters

`loginSecure` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetCoplayData()**

```csharp
public Task<List<CoPlaySession>> GetCoplayData()
```

#### Returns

[Task&lt;List&lt;CoPlaySession&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Dispose()**

```csharp
public void Dispose()
```
