# Cs2DemoParser

Namespace: StrikeLink.DemoParser.Parsing

Provides functionality to parse and extract structured data from Counter-Strike 2 (CS2) demo files, producing
 match, player, and round statistics from the legacy Source 2 demo format.

```csharp
public sealed class Cs2DemoParser : System.IDisposable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoParser](./strikelink/demoparser/parsing/cs2demoparser.md)<br>
Implements [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

The parser supports only CS2 Source 2 demo files with the ".dem" extension. It combines game events
 and string tables with packet-entity decoding (send tables, class info and entity state) to resolve controller
 properties. If entity decoding fails fatally, entity tracking is disabled and parsing continues with event data
 only, so entity-derived statistics may be missing. The parser is designed for single-use per file and is not
 thread-safe. Dispose the instance after use to release file resources.

## Properties

### **PositionSampleIntervalTicks**

Ticks between the player position samples recorded into [RoundStats.Frames](./strikelink/demoparser/parsing/roundstats.md#frames) (default 32, about half a second at 64 tick).
 Lower values give a smoother replay track and a larger result; 0 turns sampling off.

```csharp
public int PositionSampleIntervalTicks { get; set; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **Cs2DemoParser(FileInfo, DemoAuthorization)**

Initializes a new instance of the Cs2DemoParser class for reading and parsing a demo file using the specified
 authorization data.

```csharp
public Cs2DemoParser(FileInfo demoFile, DemoAuthorization authData)
```

#### Parameters

`demoFile` [FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
The demo file to be parsed. The file must have a ".dem" extension and must not be null.

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization data used to access or decrypt the demo file. May be null if no authorization is required.

#### Exceptions

[FormatException](https://learn.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Thrown if demoFile does not have a ".dem" file extension.

### **Cs2DemoParser(Stream, DemoAuthorization)**

Initializes a new instance of the Cs2DemoParser class for reading and parsing a demo stream using the specified
 authorization data. Supports [MemoryStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.memorystream), [FileStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream), [BinaryReader](https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader) 
 underlying streams, or any live/network stream source.

```csharp
public Cs2DemoParser(Stream demoStream, DemoAuthorization authData)
```

#### Parameters

`demoStream` [Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
A readable stream containing demo data. Must be non-null and readable.
 For network/live streams, the stream does not need to be seekable.

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization data used to access or decrypt the demo. May be null if no authorization is required.

#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown if `demoStream` is null.

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
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

[InvalidDataException](https://learn.microsoft.com/en-us/dotnet/api/system.io.invaliddataexception)<br>
Thrown if the demo file format is not supported or does not match the expected CS2 Source 2 demo format.

**Remarks:**

The method reads and processes the demo file from the provided stream, extracting commands and
 payloads as defined by the CS2 demo format. The stream must be positioned at the start of a valid demo file. The
 method finalizes any open rounds before returning the result.
