# ValveCfgWriter

Namespace: StrikeLink.Services.Config

Provides methods for serializing [ConfigDocument](./strikelink/services/config/configdocument.md) instances
 into Valve configuration file format.

```csharp
public static class ValveCfgWriter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValveCfgWriter](./strikelink/services/config/valvecfgwriter.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Methods

### **Write(ConfigDocument, ConfigWriterOptions)**

Serializes a configuration document into its textual representation.

```csharp
public static string Write(ConfigDocument document, ConfigWriterOptions options)
```

#### Parameters

`document` [ConfigDocument](./strikelink/services/config/configdocument.md)<br>
The configuration document to write.

`options` [ConfigWriterOptions](./strikelink/services/config/configwriteroptions.md)<br>
Optional writer options controlling formatting behavior.
 If `null`, [ConfigWriterOptions.Default](./strikelink/services/config/configwriteroptions.md#default) is used.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string containing the serialized configuration document.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `document` is `null`.
