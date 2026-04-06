# DemoParser

Namespace: StrikeLink.DemoParser

```csharp
public class DemoParser : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoParser](./strikelink/demoparser/demoparser.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Constructors

### **DemoParser(FileInfo, DemoAuthorization)**

```csharp
public DemoParser(FileInfo demoFile, DemoAuthorization authData)
```

#### Parameters

`demoFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>

### **DemoParser(String, DemoAuthorization)**

```csharp
public DemoParser(string matchId, DemoAuthorization authData)
```

#### Parameters

`matchId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>

## Methods

### **ParseDemo()**

```csharp
public Task ParseDemo()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **Dispose()**

```csharp
public void Dispose()
```
