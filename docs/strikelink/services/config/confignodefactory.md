# ConfigNodeFactory

Namespace: StrikeLink.Services.Config

Provides factory methods for creating [ConfigNode](./strikelink/services/config/confignode.md) instances.

```csharp
public static class ConfigNodeFactory
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigNodeFactory](./strikelink/services/config/confignodefactory.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Methods

### **Object(IDictionary&lt;String, ConfigNode&gt;)**

Creates a configuration node that represents an object with named properties.

```csharp
public static ConfigNode Object(IDictionary<string, ConfigNode> properties)
```

#### Parameters

`properties` [IDictionary&lt;String, ConfigNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The dictionary of child property nodes.

#### Returns

[ConfigNode](./strikelink/services/config/confignode.md)<br>
A [ConfigNode](./strikelink/services/config/confignode.md) of type [ConfigNodeType.Object](./strikelink/services/config/confignodetype.md#object).

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `properties` is `null`.

### **Value(String)**

Creates a configuration node that represents a primitive value.

```csharp
public static ConfigNode Value(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string value of the node.

#### Returns

[ConfigNode](./strikelink/services/config/confignode.md)<br>
A [ConfigNode](./strikelink/services/config/confignode.md) of type [ConfigNodeType.Value](./strikelink/services/config/confignodetype.md#value).

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `value` is `null`.
