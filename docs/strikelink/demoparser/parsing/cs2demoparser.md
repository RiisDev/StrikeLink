# Cs2DemoParser

Namespace: StrikeLink.DemoParser.Parsing

Provides functionality to parse and extract structured data from Counter-Strike 2 (CS2) demo files, producing
 match, player, and round statistics from the legacy Source 2 demo format.

```csharp
public sealed class Cs2DemoParser : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoParser](./strikelink/demoparser/parsing/cs2demoparser.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

The parser supports only CS2 Source 2 demo files with the ".dem" extension. It processes the demo
 file using legacy event and string table data, without relying on packet-entity or send-table decoding. Some
 advanced statistics are not available due to these
 limitations. The parser is designed for single-use per file and is not thread-safe. Dispose the instance after use
 to release file resources.

## Constructors

### **Cs2DemoParser(FileInfo, DemoAuthorization)**

Initializes a new instance of the Cs2DemoParser class for reading and parsing a demo file using the specified
 authorization data.

```csharp
public Cs2DemoParser(FileInfo demoFile, DemoAuthorization authData)
```

#### Parameters

`demoFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
The demo file to be parsed. The file must have a ".dem" extension and must not be null.

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization data used to access or decrypt the demo file. May be null if no authorization is required.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Thrown if demoFile does not have a ".dem" file extension.

### **Cs2DemoParser(Stream, DemoAuthorization)**

Initializes a new instance of the Cs2DemoParser class for reading and parsing a demo stream using the specified
 authorization data. Supports [MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream), [FileStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream), [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader) 
 underlying streams, or any live/network stream source.

```csharp
public Cs2DemoParser(Stream demoStream, DemoAuthorization authData)
```

#### Parameters

`demoStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
A readable stream containing demo data. Must be non-null and readable.
 For network/live streams, the stream does not need to be seekable.

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization data used to access or decrypt the demo. May be null if no authorization is required.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown if `demoStream` is null.

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown if `demoStream` is not readable.

## Methods

### **Dispose()**

Releases all resources used by the [Cs2DemoParser](./strikelink/demoparser/parsing/cs2demoparser.md).

```csharp
public void Dispose()
```

### **ParseDemo()**

Parses the current CS2 demo file stream and returns the parsed result.

```csharp
public Cs2DemoParseResult ParseDemo()
```

#### Returns

[Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md)<br>
A [Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md) containing the parsed data from the demo file.

#### Exceptions

[InvalidDataException](https://docs.microsoft.com/en-us/dotnet/api/system.io.invaliddataexception)<br>
Thrown if the demo file format is not supported or does not match the expected CS2 Source 2 demo format.

**Remarks:**

The method reads and processes the demo file from the provided stream, extracting commands and
 payloads as defined by the CS2 demo format. The stream must be positioned at the start of a valid demo file. The
 method finalizes any open rounds before returning the result.
