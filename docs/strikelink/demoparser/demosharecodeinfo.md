# DemoShareCodeInfo

Namespace: StrikeLink.DemoParser

Represents information about a demo match, including its unique identifier, reservation, and TV port.

```csharp
public class DemoShareCodeInfo : System.IEquatable`1[[StrikeLink.DemoParser.DemoShareCodeInfo, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>
Implements [IEquatable&lt;DemoShareCodeInfo&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **MatchId**

The unique identifier for the match.

```csharp
public ulong MatchId { get; set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **ReservationId**

The unique identifier for the reservation associated with the match.

```csharp
public ulong ReservationId { get; set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **TvPort**

The port number used for TV or spectator access to the match.

```csharp
public uint TvPort { get; set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

## Constructors

### **DemoShareCodeInfo(UInt64, UInt64, UInt32)**

Represents information about a demo match, including its unique identifier, reservation, and TV port.

```csharp
public DemoShareCodeInfo(ulong MatchId, ulong ReservationId, uint TvPort)
```

#### Parameters

`MatchId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique identifier for the match.

`ReservationId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique identifier for the reservation associated with the match.

`TvPort` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
The port number used for TV or spectator access to the match.

### **DemoShareCodeInfo(DemoShareCodeInfo)**

```csharp
protected DemoShareCodeInfo(DemoShareCodeInfo original)
```

#### Parameters

`original` [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>

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

### **Equals(DemoShareCodeInfo)**

```csharp
public bool Equals(DemoShareCodeInfo other)
```

#### Parameters

`other` [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public DemoShareCodeInfo <Clone>$()
```

#### Returns

[DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>

### **Deconstruct(UInt64&, UInt64&, UInt32&)**

```csharp
public void Deconstruct(UInt64& MatchId, UInt64& ReservationId, UInt32& TvPort)
```

#### Parameters

`MatchId` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

`ReservationId` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

`TvPort` [UInt32&](https://docs.microsoft.com/en-us/dotnet/api/system.uint32&)<br>
