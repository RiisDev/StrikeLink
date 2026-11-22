# DemoShareCodeInfo

Namespace: StrikeLink.DemoParser

Represents information about a demo match, including its unique identifier, reservation, and TV port.

```csharp
public record class DemoShareCodeInfo : System.IEquatable`1[[StrikeLink.DemoParser.DemoShareCodeInfo, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>
Implements [IEquatable&lt;DemoShareCodeInfo&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **MatchId**

The unique identifier for the match.

```csharp
public ulong MatchId { get; init; }
```

#### Property Value

[UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **ReservationId**

The unique identifier for the reservation associated with the match.

```csharp
public ulong ReservationId { get; init; }
```

#### Property Value

[UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **TvPort**

The port number used for TV or spectator access to the match.

```csharp
public uint TvPort { get; init; }
```

#### Property Value

[UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32)<br>

## Constructors

### **DemoShareCodeInfo(UInt64, UInt64, UInt32)**

Represents information about a demo match, including its unique identifier, reservation, and TV port.

```csharp
public DemoShareCodeInfo(ulong MatchId, ulong ReservationId, uint TvPort)
```

#### Parameters

`MatchId` [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique identifier for the match.

`ReservationId` [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique identifier for the reservation associated with the match.

`TvPort` [UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32)<br>
The port number used for TV or spectator access to the match.

### **DemoShareCodeInfo(DemoShareCodeInfo)**

```csharp
protected DemoShareCodeInfo(DemoShareCodeInfo original)
```

#### Parameters

`original` [DemoShareCodeInfo](./strikelink/demoparser/demosharecodeinfo.md)<br>
