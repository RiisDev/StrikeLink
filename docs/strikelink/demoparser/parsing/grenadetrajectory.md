# GrenadeTrajectory

Namespace: StrikeLink.DemoParser.Parsing

One thrown grenade from release to detonation.

```csharp
public sealed record class GrenadeTrajectory : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.GrenadeTrajectory, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GrenadeTrajectory](./strikelink/demoparser/parsing/grenadetrajectory.md)<br>
Implements [IEquatable&lt;GrenadeTrajectory&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Type**

Grenade kind.

```csharp
public GrenadeType Type { get; init; }
```

#### Property Value

[GrenadeType](./strikelink/demoparser/parsing/grenadetype.md)<br>

### **ThrowerSteamId**

Steam ID of the thrower, or null when unknown.

```csharp
public ulong? ThrowerSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **StartTick**

Tick the grenade appeared in the world.

```csharp
public int StartTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EndTick**

Tick it detonated or was removed.

```csharp
public int EndTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Path**

Sampled positions from release to the last known position (the detonation point).

```csharp
public IReadOnlyList<GrenadePathPoint> Path { get; init; }
```

#### Property Value

[IReadOnlyList&lt;GrenadePathPoint&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **GrenadeTrajectory(GrenadeType, UInt64?, Int32, Int32, IReadOnlyList&lt;GrenadePathPoint&gt;)**

One thrown grenade from release to detonation.

```csharp
public GrenadeTrajectory(GrenadeType Type, ulong? ThrowerSteamId, int StartTick, int EndTick, IReadOnlyList<GrenadePathPoint> Path)
```

#### Parameters

`Type` [GrenadeType](./strikelink/demoparser/parsing/grenadetype.md)<br>
Grenade kind.

`ThrowerSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Steam ID of the thrower, or null when unknown.

`StartTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Tick the grenade appeared in the world.

`EndTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Tick it detonated or was removed.

`Path` [IReadOnlyList&lt;GrenadePathPoint&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Sampled positions from release to the last known position (the detonation point).
