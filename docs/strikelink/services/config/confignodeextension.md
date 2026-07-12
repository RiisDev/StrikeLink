# ConfigNodeExtension

Namespace: StrikeLink.Services.Config

Provides extension methods for navigating [ConfigNode](./strikelink/services/config/confignode.md) hierarchies.

```csharp
public static class ConfigNodeExtension
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigNodeExtension](./strikelink/services/config/confignodeextension.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **GetPath(ConfigNode, String)**

Gets a nested configuration node using a dot-separated path.

```csharp
public static ConfigNode GetPath(ConfigNode node, string path)
```

#### Parameters

`node` [ConfigNode](./strikelink/services/config/confignode.md)<br>

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A dot-separated property path (for example, `"Software.Valve.Steam"`).

#### Returns

[ConfigNode](./strikelink/services/config/confignode.md)<br>
The configuration node located at the specified path.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when `path` is `null`, empty, or whitespace.

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when an intermediate node is not an object.

[KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keynotfoundexception)<br>
Thrown when a path segment does not exist.

### **TryGetPath(ConfigNode, String, ConfigNode&)**

```csharp
public static bool TryGetPath(ConfigNode node, string path, ConfigNode& result)
```

#### Parameters

`node` [ConfigNode](./strikelink/services/config/confignode.md)<br>

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`result` [ConfigNode&](./strikelink/services/config/confignode&.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
