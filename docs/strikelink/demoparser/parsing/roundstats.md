# RoundStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public sealed record class RoundStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>
Implements [IEquatable&lt;RoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

The zero-based index of the round within the match. Must be non-negative.

```csharp
public int RoundNumber { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StartTick**

Beginning Tick of the round.

```csharp
public int StartTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EndTick**

End tick of the round.

```csharp
public int EndTick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Duration**

The total elapsed time of the round, or null if the duration is not available.

```csharp
public TimeSpan? Duration { get; init; }
```

#### Property Value

[TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **WinnerTeam**

The lineup identity that won the round.

```csharp
public MatchTeam WinnerTeam { get; init; }
```

#### Property Value

[MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>

### **WinnerSide**

The side (T/CT) that won the round.

```csharp
public CsTeamSide WinnerSide { get; init; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **TeamASide**

The side Team A played on for this round.

```csharp
public CsTeamSide TeamASide { get; init; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **TeamBSide**

The side Team B played on for this round.

```csharp
public CsTeamSide TeamBSide { get; init; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **Kills**

A read-only list of all kill events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundKillEvent> Kills { get; init; }
```

#### Property Value

[IReadOnlyList&lt;RoundKillEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Damage**

A read-only list of all damage events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundDamageEvent> Damage { get; init; }
```

#### Property Value

[IReadOnlyList&lt;RoundDamageEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **TeamAEquipmentValue**

Average equipment value per Team A player at round start, or 0 when unavailable.

```csharp
public int TeamAEquipmentValue { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamBEquipmentValue**

Average equipment value per Team B player at round start, or 0 when unavailable.

```csharp
public int TeamBEquipmentValue { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamABuy**

How Team A bought this round.

```csharp
public BuyType TeamABuy { get; init; }
```

#### Property Value

[BuyType](./strikelink/demoparser/parsing/buytype.md)<br>

### **TeamBBuy**

How Team B bought this round.

```csharp
public BuyType TeamBBuy { get; init; }
```

#### Property Value

[BuyType](./strikelink/demoparser/parsing/buytype.md)<br>

### **Frames**

Player positions sampled through the round (see [Cs2DemoParser.PositionSampleIntervalTicks](./strikelink/demoparser/parsing/cs2demoparser.md#positionsampleintervalticks)); empty when the demo has no entity state or sampling is off.

```csharp
public IReadOnlyList<PlayerFrame> Frames { get; init; }
```

#### Property Value

[IReadOnlyList&lt;PlayerFrame&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Grenades**

Every grenade thrown this round with its flight path (empty without entity state).

```csharp
public IReadOnlyList<GrenadeTrajectory> Grenades { get; init; }
```

#### Property Value

[IReadOnlyList&lt;GrenadeTrajectory&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Flashes**

Every player blinded this round.

```csharp
public IReadOnlyList<FlashEffect> Flashes { get; init; }
```

#### Property Value

[IReadOnlyList&lt;FlashEffect&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **EffectZones**

Smoke clouds and fire areas this round.

```csharp
public IReadOnlyList<EffectZone> EffectZones { get; init; }
```

#### Property Value

[IReadOnlyList&lt;EffectZone&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **BombEvents**

Plant, defuse and explosion events this round.

```csharp
public IReadOnlyList<BombEvent> BombEvents { get; init; }
```

#### Property Value

[IReadOnlyList&lt;BombEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **RoundStats(Int32, Int32, Int32, TimeSpan?, MatchTeam, CsTeamSide, CsTeamSide, CsTeamSide, IReadOnlyList&lt;RoundKillEvent&gt;, IReadOnlyList&lt;RoundDamageEvent&gt;, Int32, Int32, BuyType, BuyType, IReadOnlyList&lt;PlayerFrame&gt;, IReadOnlyList&lt;GrenadeTrajectory&gt;, IReadOnlyList&lt;FlashEffect&gt;, IReadOnlyList&lt;EffectZone&gt;, IReadOnlyList&lt;BombEvent&gt;)**

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public RoundStats(int RoundNumber, int StartTick, int EndTick, TimeSpan? Duration, MatchTeam WinnerTeam, CsTeamSide WinnerSide, CsTeamSide TeamASide, CsTeamSide TeamBSide, IReadOnlyList<RoundKillEvent> Kills, IReadOnlyList<RoundDamageEvent> Damage, int TeamAEquipmentValue, int TeamBEquipmentValue, BuyType TeamABuy, BuyType TeamBBuy, IReadOnlyList<PlayerFrame> Frames, IReadOnlyList<GrenadeTrajectory> Grenades, IReadOnlyList<FlashEffect> Flashes, IReadOnlyList<EffectZone> EffectZones, IReadOnlyList<BombEvent> BombEvents)
```

#### Parameters

`RoundNumber` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The zero-based index of the round within the match. Must be non-negative.

`StartTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Beginning Tick of the round.

`EndTick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
End tick of the round.

`Duration` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The total elapsed time of the round, or null if the duration is not available.

`WinnerTeam` [MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>
The lineup identity that won the round.

`WinnerSide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side (T/CT) that won the round.

`TeamASide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side Team A played on for this round.

`TeamBSide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side Team B played on for this round.

`Kills` [IReadOnlyList&lt;RoundKillEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all kill events that occurred during the round. Never null.

`Damage` [IReadOnlyList&lt;RoundDamageEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all damage events that occurred during the round. Never null.

`TeamAEquipmentValue` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Average equipment value per Team A player at round start, or 0 when unavailable.

`TeamBEquipmentValue` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Average equipment value per Team B player at round start, or 0 when unavailable.

`TeamABuy` [BuyType](./strikelink/demoparser/parsing/buytype.md)<br>
How Team A bought this round.

`TeamBBuy` [BuyType](./strikelink/demoparser/parsing/buytype.md)<br>
How Team B bought this round.

`Frames` [IReadOnlyList&lt;PlayerFrame&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Player positions sampled through the round (see [Cs2DemoParser.PositionSampleIntervalTicks](./strikelink/demoparser/parsing/cs2demoparser.md#positionsampleintervalticks)); empty when the demo has no entity state or sampling is off.

`Grenades` [IReadOnlyList&lt;GrenadeTrajectory&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Every grenade thrown this round with its flight path (empty without entity state).

`Flashes` [IReadOnlyList&lt;FlashEffect&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Every player blinded this round.

`EffectZones` [IReadOnlyList&lt;EffectZone&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Smoke clouds and fire areas this round.

`BombEvents` [IReadOnlyList&lt;BombEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Plant, defuse and explosion events this round.
