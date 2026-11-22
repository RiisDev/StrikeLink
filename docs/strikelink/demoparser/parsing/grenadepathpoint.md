# GrenadePathPoint

Namespace: StrikeLink.DemoParser.Parsing

A point on a grenade's flight path.

```csharp
public sealed record class GrenadePathPoint : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.GrenadePathPoint, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GrenadePathPoint](./strikelink/demoparser/parsing/grenadepathpoint.md)<br>
Implements [IEquatable&lt;GrenadePathPoint&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

Demo tick.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Position**

World position.

```csharp
public PlayerPosition Position { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

## Constructors

### **GrenadePathPoint(Int32, PlayerPosition)**

A point on a grenade's flight path.

```csharp
public GrenadePathPoint(int Tick, PlayerPosition Position)
```

#### Parameters

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Demo tick.

`Position` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>
World position.
