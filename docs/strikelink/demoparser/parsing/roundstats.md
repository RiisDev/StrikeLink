# RoundStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public sealed class RoundStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>
Implements [IEquatable&lt;RoundStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

The zero-based index of the round within the match. Must be non-negative.

```csharp
public int RoundNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StartTick**

Beginning Tick of the round.

```csharp
public int StartTick { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EndTick**

End tick of the round.

```csharp
public int EndTick { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Duration**

The total elapsed time of the round, or null if the duration is not available.

```csharp
public Nullable<TimeSpan> Duration { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **WinnerTeam**

The lineup identity that won the round.

```csharp
public MatchTeam WinnerTeam { get; set; }
```

#### Property Value

[MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>

### **WinnerSide**

The side (T/CT) that won the round.

```csharp
public CsTeamSide WinnerSide { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **TeamASide**

The side Team A played on for this round.

```csharp
public CsTeamSide TeamASide { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **TeamBSide**

The side Team B played on for this round.

```csharp
public CsTeamSide TeamBSide { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **Kills**

A read-only list of all kill events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundKillEvent> Kills { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Damage**

A read-only list of all damage events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundDamageEvent> Damage { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **RoundStats(Int32, Int32, Int32, Nullable&lt;TimeSpan&gt;, MatchTeam, CsTeamSide, CsTeamSide, CsTeamSide, IReadOnlyList&lt;RoundKillEvent&gt;, IReadOnlyList&lt;RoundDamageEvent&gt;)**

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public RoundStats(int RoundNumber, int StartTick, int EndTick, Nullable<TimeSpan> Duration, MatchTeam WinnerTeam, CsTeamSide WinnerSide, CsTeamSide TeamASide, CsTeamSide TeamBSide, IReadOnlyList<RoundKillEvent> Kills, IReadOnlyList<RoundDamageEvent> Damage)
```

#### Parameters

`RoundNumber` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The zero-based index of the round within the match. Must be non-negative.

`StartTick` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Beginning Tick of the round.

`EndTick` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
End tick of the round.

`Duration` [Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The total elapsed time of the round, or null if the duration is not available.

`WinnerTeam` [MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>
The lineup identity that won the round.

`WinnerSide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side (T/CT) that won the round.

`TeamASide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side Team A played on for this round.

`TeamBSide` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The side Team B played on for this round.

`Kills` [IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all kill events that occurred during the round. Never null.

`Damage` [IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all damage events that occurred during the round. Never null.

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

### **Equals(RoundStats)**

```csharp
public bool Equals(RoundStats other)
```

#### Parameters

`other` [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundStats <Clone>$()
```

#### Returns

[RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Nullable`1&, MatchTeam&, CsTeamSide&, CsTeamSide&, CsTeamSide&, IReadOnlyList`1&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Int32& RoundNumber, Int32& StartTick, Int32& EndTick, Nullable`1& Duration, MatchTeam& WinnerTeam, CsTeamSide& WinnerSide, CsTeamSide& TeamASide, CsTeamSide& TeamBSide, IReadOnlyList`1& Kills, IReadOnlyList`1& Damage)
```

#### Parameters

`RoundNumber` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`StartTick` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`EndTick` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Duration` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`WinnerTeam` [MatchTeam&](./strikelink/demoparser/parsing/matchteam&.md)<br>

`WinnerSide` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`TeamASide` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`TeamBSide` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`Kills` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Damage` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
