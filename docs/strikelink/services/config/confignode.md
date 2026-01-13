# ConfigNode

Namespace: StrikeLink.Services.Config

Represents a node within a hierarchical configuration structure.

```csharp
public struct ConfigNode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [ConfigNode](./strikelink/services/config/confignode.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [IsReadOnlyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.isreadonlyattribute)

**Remarks:**

A node may represent either an object with named properties or a single value.

## Properties

### **NodeType**

Gets the type of this configuration node.

```csharp
public ConfigNodeType NodeType { get; }
```

#### Property Value

[ConfigNodeType](./strikelink/services/config/confignodetype.md)<br>

## Methods

### **GetProperty(String)**

Gets a named child property from this node.

```csharp
ConfigNode GetProperty(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the property.

#### Returns

[ConfigNode](./strikelink/services/config/confignode.md)<br>
The requested child [ConfigNode](./strikelink/services/config/confignode.md).

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this node is not an object.

[KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keynotfoundexception)<br>
Thrown when the specified property does not exist.

### **TryGetProperty(String, ConfigNode&)**

Attempts to get a named child property from this node.

```csharp
bool TryGetProperty(string name, ConfigNode& node)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the property.

`node` [ConfigNode&](./strikelink/services/config/confignode&.md)<br>
When this method returns, contains the child node if found; otherwise, `default`. [ConfigNode](./strikelink/services/config/confignode.md)

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if the property exists; otherwise, `false`.

### **EnumerateObject()**

Enumerates all child properties of this node.

```csharp
IEnumerable<KeyValuePair<string, ConfigNode>> EnumerateObject()
```

#### Returns

[IEnumerable&lt;KeyValuePair&lt;String, ConfigNode&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
A sequence of property name and node pairs.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this node is not an object.

### **GetString()**

Gets the string value of this node.

```csharp
string GetString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string value represented by this node.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this node is not a value node.

### **GetInt32()**

Gets the value of this node as a 32-bit integer.

```csharp
int GetInt32()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The parsed 32-bit integer value.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this node is not a value node or the value cannot be parsed.

### **GetInt64()**

Gets the value of this node as a 64-bit integer.

```csharp
long GetInt64()
```

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The parsed 64-bit integer value.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this node is not a value node or the value cannot be parsed.
