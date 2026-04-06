# DemoParser

Namespace: StrikeLink.DemoParser.Parsing

Provides functionality to parse and extract structured data from Counter-Strike 2 (CS2) demo files, producing
 match, player, and round statistics from the legacy Source 2 demo format.

```csharp
public sealed class DemoParser : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoParser](./strikelink/demoparser/parsing/demoparser.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

The parser supports only CS2 Source 2 demo files with the ".dem" extension. It processes the demo
 file using legacy event and string table data, without relying on packet-entity or send-table decoding. Some
 advanced statistics, such as per-round player economy and crosshair angle, are not available due to these
 limitations. The parser is designed for single-use per file and is not thread-safe. Dispose the instance after use
 to release file resources.

## Fields

### **EventData**

Provides a mapping of event names to their associated data collections.

```csharp
public static Dictionary<string, Dictionary<string, object>> EventData;
```

**Remarks:**

Each key in the outer dictionary represents an event name. The corresponding value is a
 dictionary that maps string keys to dynamic values, allowing storage of arbitrary event-related data. This
 collection is static and read-only; its contents should be initialized at application startup and not modified at
 runtime.

## Constructors

### **DemoParser(FileInfo, DemoAuthorization)**

Initializes a new instance of the DemoParser class for reading and parsing a demo file using the specified
 authorization data.

```csharp
public DemoParser(FileInfo demoFile, DemoAuthorization authData)
```

#### Parameters

`demoFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
The demo file to be parsed. The file must have a ".dem" extension and must not be null.

`authData` [DemoAuthorization](./strikelink/demoparser/demoauthorization.md)<br>
The authorization data used to access or decrypt the demo file. May be null if no authorization is required.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Thrown if demoFile does not have a ".dem" file extension.

## Methods

### **Dispose()**

Releases all resources used by the [DemoParser](./strikelink/demoparser/parsing/demoparser.md).

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
