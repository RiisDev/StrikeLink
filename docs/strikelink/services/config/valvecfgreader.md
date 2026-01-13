# ValveCfgReader

Namespace: StrikeLink.Services.Config

Reads and parses Valve configuration files (`.cfg`, `.vdf`, `.vcfg`, `.acf`)
 into a structured [ConfigDocument](./strikelink/services/config/configdocument.md).

```csharp
public class ValveCfgReader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValveCfgReader](./strikelink/services/config/valvecfgreader.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **ConfigName**

Gets the name of the configuration root object.

```csharp
public string ConfigName { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FileName**

Gets the file name (without extension) of the parsed configuration file.

```csharp
public string FileName { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Document**

Gets the parsed configuration document.

```csharp
public ConfigDocument Document { get; private set; }
```

#### Property Value

[ConfigDocument](./strikelink/services/config/configdocument.md)<br>

## Constructors

### **ValveCfgReader(String)**

Initializes a new instance of the [ValveCfgReader](./strikelink/services/config/valvecfgreader.md) class
 and parses the specified configuration file.

```csharp
public ValveCfgReader(string filePath)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the configuration file.

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the specified file does not exist.

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when the file format or structure is invalid.
