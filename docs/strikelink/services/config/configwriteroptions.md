# ConfigWriterOptions

Namespace: StrikeLink.Services.Config

Represents configuration options for writing Valve configuration documents.

```csharp
public sealed class ConfigWriterOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigWriterOptions](./strikelink/services/config/configwriteroptions.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Default**

Gets the default writer options.

```csharp
public static ConfigWriterOptions Default { get; }
```

#### Property Value

[ConfigWriterOptions](./strikelink/services/config/configwriteroptions.md)<br>

### **IndentSize**

Gets the number of spaces used for indentation.

```csharp
public int IndentSize { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

**Remarks:**

The default value is `4`.

### **WriteTrailingNewline**

Gets a value indicating whether a trailing newline is written at the end of the document.

```csharp
public bool WriteTrailingNewline { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

**Remarks:**

The default value is `true`.

## Constructors

### **ConfigWriterOptions()**

```csharp
public ConfigWriterOptions()
```
