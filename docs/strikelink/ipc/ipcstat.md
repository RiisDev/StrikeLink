# IPCStat

Namespace: StrikeLink.IPC

Represents a single career stat key–value pair read from the Steam client.

```csharp
public class IPCStat : System.IEquatable`1[[StrikeLink.IPC.IPCStat, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCStat](./strikelink/ipc/ipcstat.md)<br>
Implements [IEquatable&lt;IPCStat&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Stat**

The name of the stat (for example, `total_kills`).

```csharp
public string Stat { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Value**

The raw string value of the stat.

```csharp
public string Value { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCStat(String, String)**

Represents a single career stat key–value pair read from the Steam client.

```csharp
public IPCStat(string Stat, string Value)
```

#### Parameters

`Stat` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the stat (for example, `total_kills`).

`Value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The raw string value of the stat.

### **IPCStat(IPCStat)**

```csharp
protected IPCStat(IPCStat original)
```

#### Parameters

`original` [IPCStat](./strikelink/ipc/ipcstat.md)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PrintMembers(StringBuilder)**

```csharp
protected bool PrintMembers(StringBuilder builder)
```

#### Parameters

`builder` [StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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

### **Equals(IPCStat)**

```csharp
public bool Equals(IPCStat other)
```

#### Parameters

`other` [IPCStat](./strikelink/ipc/ipcstat.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCStat <Clone>$()
```

#### Returns

[IPCStat](./strikelink/ipc/ipcstat.md)<br>

### **Deconstruct(String&, String&)**

```csharp
public void Deconstruct(String& Stat, String& Value)
```

#### Parameters

`Stat` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Value` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
