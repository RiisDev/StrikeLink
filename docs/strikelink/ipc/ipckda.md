# IPCKda

Namespace: StrikeLink.IPC

Represents a kill/death/assist score snapshot.

```csharp
public class IPCKda : System.IEquatable`1[[StrikeLink.IPC.IPCKda, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCKda](./strikelink/ipc/ipckda.md)<br>
Implements [IEquatable&lt;IPCKda&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Kills**

Number of kills.

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

Number of deaths.

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

Number of assists.

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **IPCKda(Int32, Int32, Int32)**

Represents a kill/death/assist score snapshot.

```csharp
public IPCKda(int Kills, int Deaths, int Assists)
```

#### Parameters

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of kills.

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of deaths.

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of assists.

### **IPCKda(IPCKda)**

```csharp
protected IPCKda(IPCKda original)
```

#### Parameters

`original` [IPCKda](./strikelink/ipc/ipckda.md)<br>

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

### **Equals(IPCKda)**

```csharp
public bool Equals(IPCKda other)
```

#### Parameters

`other` [IPCKda](./strikelink/ipc/ipckda.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCKda <Clone>$()
```

#### Returns

[IPCKda](./strikelink/ipc/ipckda.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&)**

```csharp
public void Deconstruct(Int32& Kills, Int32& Deaths, Int32& Assists)
```

#### Parameters

`Kills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Deaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Assists` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
