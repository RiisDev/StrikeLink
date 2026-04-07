# DemoDownloadResult

Namespace: StrikeLink.DemoParser

Represents a downloaded CS2 replay payload.

```csharp
public sealed class DemoDownloadResult : System.IEquatable`1[[StrikeLink.DemoParser.DemoDownloadResult, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoDownloadResult](./strikelink/demoparser/demodownloadresult.md)<br>
Implements [IEquatable&lt;DemoDownloadResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Stream**

Readable stream positioned at the beginning of the downloaded content.

```csharp
public Stream Stream { get; set; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

### **FileName**

Suggested filename for persistence.

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

### **IsCompressedBzip2**

True when the payload is a .dem.bz2 archive.

```csharp
public bool IsCompressedBzip2 { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **DemoDownloadResult(Stream, String, String, Boolean)**

Represents a downloaded CS2 replay payload.

```csharp
public DemoDownloadResult(Stream Stream, string FileName, string TempFilePath, bool IsCompressedBzip2)
```

#### Parameters

`Stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Readable stream positioned at the beginning of the downloaded content.

`FileName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Suggested filename for persistence.

`TempFilePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Local temp path when persisted to disk; otherwise null.

`IsCompressedBzip2` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True when the payload is a .dem.bz2 archive.

## Methods

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

### **Deconstruct(Stream&, String&, String&, Boolean&)**

```csharp
public void Deconstruct(Stream& Stream, String& FileName, String& TempFilePath, Boolean& IsCompressedBzip2)
```

#### Parameters

`Stream` [Stream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream&)<br>

`FileName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`TempFilePath` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`IsCompressedBzip2` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
