# PlayerImpactStats

Namespace: StrikeLink.DemoParser.Parsing

Represents a set of impact statistics for a player, including overall match impact and per-side performance
 metrics.

```csharp
public sealed class PlayerImpactStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerImpactStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>
Implements [IEquatable&lt;PlayerImpactStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **MatchImpactPercentage**

The percentage value indicating the player's overall impact on the match. Typically ranges from 0 to 100.

```csharp
public double MatchImpactPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **KillsPerRound**

The player's average number of kills per round, split by side.

```csharp
public SideSplitMetrics KillsPerRound { get; set; }
```

#### Property Value

[SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>

### **RoundImpact**

The player's impact per round, split by side. This metric may include factors such as multi-kills or key actions.

```csharp
public SideSplitMetrics RoundImpact { get; set; }
```

#### Property Value

[SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>

### **WinProbability**

The player's contribution to round win probability, split by side.

```csharp
public SideSplitMetrics WinProbability { get; set; }
```

#### Property Value

[SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>

### **Rounds**

A read-only list of snapshots detailing the player's impact in each round.

```csharp
public IReadOnlyList<RoundImpactSnapshot> Rounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundImpactSnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **PlayerImpactStats(Double, SideSplitMetrics, SideSplitMetrics, SideSplitMetrics, IReadOnlyList&lt;RoundImpactSnapshot&gt;)**

Represents a set of impact statistics for a player, including overall match impact and per-side performance
 metrics.

```csharp
public PlayerImpactStats(double MatchImpactPercentage, SideSplitMetrics KillsPerRound, SideSplitMetrics RoundImpact, SideSplitMetrics WinProbability, IReadOnlyList<RoundImpactSnapshot> Rounds)
```

#### Parameters

`MatchImpactPercentage` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The percentage value indicating the player's overall impact on the match. Typically ranges from 0 to 100.

`KillsPerRound` [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>
The player's average number of kills per round, split by side.

`RoundImpact` [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>
The player's impact per round, split by side. This metric may include factors such as multi-kills or key actions.

`WinProbability` [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>
The player's contribution to round win probability, split by side.

`Rounds` [IReadOnlyList&lt;RoundImpactSnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of snapshots detailing the player's impact in each round.

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

### **Equals(PlayerImpactStats)**

```csharp
public bool Equals(PlayerImpactStats other)
```

#### Parameters

`other` [PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public PlayerImpactStats <Clone>$()
```

#### Returns

[PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>

### **Deconstruct(Double&, SideSplitMetrics&, SideSplitMetrics&, SideSplitMetrics&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Double& MatchImpactPercentage, SideSplitMetrics& KillsPerRound, SideSplitMetrics& RoundImpact, SideSplitMetrics& WinProbability, IReadOnlyList`1& Rounds)
```

#### Parameters

`MatchImpactPercentage` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`KillsPerRound` [SideSplitMetrics&](./strikelink/demoparser/parsing/sidesplitmetrics&.md)<br>

`RoundImpact` [SideSplitMetrics&](./strikelink/demoparser/parsing/sidesplitmetrics&.md)<br>

`WinProbability` [SideSplitMetrics&](./strikelink/demoparser/parsing/sidesplitmetrics&.md)<br>

`Rounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
