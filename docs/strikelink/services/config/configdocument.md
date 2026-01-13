# ConfigDocument

Namespace: StrikeLink.Services.Config

Represents a parsed configuration document.

```csharp
public sealed class ConfigDocument : System.IEquatable`1[[StrikeLink.Services.Config.ConfigDocument, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigDocument](./strikelink/services/config/configdocument.md)<br>
Implements [IEquatable&lt;ConfigDocument&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Node**

The root configuration node produced by the parser.

```csharp
public ConfigNode Node { get; set; }
```

#### Property Value

[ConfigNode](./strikelink/services/config/confignode.md)<br>

### **Root**

Gets the root object node of the configuration document.

```csharp
public ConfigNode Root { get; }
```

#### Property Value

[ConfigNode](./strikelink/services/config/confignode.md)<br>

## Constructors

### **ConfigDocument(ConfigNode)**

Represents a parsed configuration document.

```csharp
public ConfigDocument(ConfigNode Node)
```

#### Parameters

`Node` [ConfigNode](./strikelink/services/config/confignode.md)<br>
The root configuration node produced by the parser.

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

### **Equals(ConfigDocument)**

```csharp
public bool Equals(ConfigDocument other)
```

#### Parameters

`other` [ConfigDocument](./strikelink/services/config/configdocument.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public ConfigDocument <Clone>$()
```

#### Returns

[ConfigDocument](./strikelink/services/config/configdocument.md)<br>

### **Deconstruct(ConfigNode&)**

```csharp
public void Deconstruct(ConfigNode& Node)
```

#### Parameters

`Node` [ConfigNode&](./strikelink/services/config/confignode&.md)<br>
