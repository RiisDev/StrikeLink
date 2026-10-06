# DemoDownloadResult

Namespace: StrikeLink.DemoParser

Represents a downloaded CS2 replay payload.

```csharp
public sealed class DemoDownloadResult : System.IAsyncDisposable, System.IDisposable, System.IEquatable`1[[StrikeLink.DemoParser.DemoDownloadResult, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoDownloadResult](./strikelink/demoparser/demodownloadresult.md)<br>
Implements [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable), [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IEquatable&lt;DemoDownloadResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Stream**

Readable stream positioned at the beginning of the decompressed .dem content.

```csharp
public Stream Stream { get; set; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

### **FileName**

Suggested filename for persistence (always .dem, never .dem.bz2).

```csharp
public string FileName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TempFilePath**

Local temp path when persisted to disk; otherwise null.

```csharp
public string TempFilePath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **DemoDownloadResult(Stream, String, String)**

Represents a downloaded CS2 replay payload.

```csharp
public DemoDownloadResult(Stream Stream, string FileName, string TempFilePath)
```

#### Parameters

`Stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Readable stream positioned at the beginning of the decompressed .dem content.

`FileName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Suggested filename for persistence (always .dem, never .dem.bz2).

`TempFilePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Local temp path when persisted to disk; otherwise null.

## Methods

### **Dispose()**

Releases all resources used by the current instance, including the underlying stream and any associated temporary
 files.

```csharp
public void Dispose()
```

**Remarks:**

Call this method when you are finished using the object to free unmanaged resources and delete
 any temporary files that may have been created. After calling this method, the object should not be
 used.

### **DisposeAsync()**

Asynchronously releases the unmanaged resources used by the object and deletes any associated temporary files.

```csharp
public ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>
A ValueTask that represents the asynchronous dispose operation.

**Remarks:**

Call this method to clean up resources when the object is no longer needed. After calling
 DisposeAsync, the object should not be used.

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Equals(DemoDownloadResult)**

```csharp
public bool Equals(DemoDownloadResult other)
```

#### Parameters

`other` [DemoDownloadResult](./strikelink/demoparser/demodownloadresult.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public DemoDownloadResult <Clone>$()
```

#### Returns

[DemoDownloadResult](./strikelink/demoparser/demodownloadresult.md)<br>

### **Deconstruct(Stream&, String&, String&)**

```csharp
public void Deconstruct(Stream& Stream, String& FileName, String& TempFilePath)
```

#### Parameters

`Stream` [Stream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream&)<br>

`FileName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`TempFilePath` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
