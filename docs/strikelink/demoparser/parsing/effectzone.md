# EffectZone

Namespace: StrikeLink.DemoParser.Parsing

A smoke cloud or fire area that lingered in the world.

```csharp
public sealed record class EffectZone : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.EffectZone, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [EffectZone](./strikelink/demoparser/parsing/effectzone.md)<br>
Implements [IEquatable&lt;EffectZone&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Type**

Smoke or fire.

```csharp
public EffectZoneType Type { get; init; }
```

#### Property Value

[EffectZoneType](./strikelink/demoparser/parsing/effectzonetype.md)<br>

### **StartTick**

Tick it appeared.

```csharp
public int StartTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EndTick**

Tick it ended, or the round end if never seen ending.

```csharp
public int EndTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Position**

Centre of the effect.

```csharp
public PlayerPosition Position { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

### **Radius**

Approximate radius in world units.

```csharp
public double Radius { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **EffectZone(EffectZoneType, Int32, Int32, PlayerPosition, Double)**

A smoke cloud or fire area that lingered in the world.

```csharp
public EffectZone(EffectZoneType Type, int StartTick, int EndTick, PlayerPosition Position, double Radius)
```

#### Parameters

`Type` [EffectZoneType](./strikelink/demoparser/parsing/effectzonetype.md)<br>
Smoke or fire.

`StartTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Tick it appeared.

`EndTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Tick it ended, or the round end if never seen ending.

`Position` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>
Centre of the effect.

`Radius` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Approximate radius in world units.
