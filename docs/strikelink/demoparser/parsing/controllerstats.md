# ControllerStats

Namespace: StrikeLink.DemoParser.Parsing

Typed view of a player's controller entity (`CCSPlayerController`) at the end of the match. Each value is looked up through
 `ControllerStatResolver`, which tolerates property renames between game builds. A value is null when this build's schema
 doesn't have the property at all; a property the schema has but the demo never sent is 0.

```csharp
public sealed record class ControllerStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.ControllerStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ControllerStats](./strikelink/demoparser/parsing/controllerstats.md)<br>
Implements [IEquatable&lt;ControllerStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Score**

The scoreboard score.

```csharp
public int? Score { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Mvps**

Round MVP stars.

```csharp
public int? Mvps { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Kills**

Kills as counted by the game (team kills excluded).

```csharp
public int? Kills { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Deaths**

Deaths as counted by the game.

```csharp
public int? Deaths { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Assists**

Assists as counted by the game.

```csharp
public int? Assists { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Damage**

Total damage dealt, as counted by the game.

```csharp
public int? Damage { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **UtilityDamage**

Damage dealt with grenades and fire.

```csharp
public int? UtilityDamage { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **EnemiesFlashed**

Enemies flashed.

```csharp
public int? EnemiesFlashed { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **HeadshotKills**

Kills that were headshots.

```csharp
public int? HeadshotKills { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Objective**

Objective points (plants/defuses).

```csharp
public int? Objective { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **LiveTime**

Time alive, in seconds.

```csharp
public int? LiveTime { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Enemy3Ks**

Rounds with exactly three kills.

```csharp
public int? Enemy3Ks { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Enemy4Ks**

Rounds with exactly four kills.

```csharp
public int? Enemy4Ks { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Enemy5Ks**

Rounds with five kills.

```csharp
public int? Enemy5Ks { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **EnemyKnifeKills**

Knife kills.

```csharp
public int? EnemyKnifeKills { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **EnemyTaserKills**

Zeus kills.

```csharp
public int? EnemyTaserKills { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **EquipmentValue**

Summed equipment value at round starts.

```csharp
public int? EquipmentValue { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MoneySaved**

Summed money saved across rounds.

```csharp
public int? MoneySaved { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **KillReward**

Money earned from kills.

```csharp
public int? KillReward { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **CashEarned**

Total money earned.

```csharp
public int? CashEarned { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Account**

Money at the end of the match.

```csharp
public int? Account { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalCashSpent**

Total money spent.

```csharp
public int? TotalCashSpent { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Ping**

Last reported ping.

```csharp
public int? Ping { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **CompetitiveRanking**

Competitive rank value shown on the scoreboard.

```csharp
public int? CompetitiveRanking { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **CompetitiveWins**

Competitive wins shown on the scoreboard.

```csharp
public int? CompetitiveWins { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **CompetitiveRankType**

The rank type (for example Premier or Competitive).

```csharp
public int? CompetitiveRankType { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Clan**

Clan tag.

```csharp
public string? Clan { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CrosshairCode**

The crosshair share code.

```csharp
public string? CrosshairCode { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RoundStats**

Per-round stats kept by the game, one entry per round the controller reported.

```csharp
public IReadOnlyList<ControllerRoundStats> RoundStats { get; init; }
```

#### Property Value

[IReadOnlyList&lt;ControllerRoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **ControllerStats(Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, Int32?, String, String, IReadOnlyList&lt;ControllerRoundStats&gt;)**

Typed view of a player's controller entity (`CCSPlayerController`) at the end of the match. Each value is looked up through
 `ControllerStatResolver`, which tolerates property renames between game builds. A value is null when this build's schema
 doesn't have the property at all; a property the schema has but the demo never sent is 0.

```csharp
public ControllerStats(int? Score, int? Mvps, int? Kills, int? Deaths, int? Assists, int? Damage, int? UtilityDamage, int? EnemiesFlashed, int? HeadshotKills, int? Objective, int? LiveTime, int? Enemy3Ks, int? Enemy4Ks, int? Enemy5Ks, int? EnemyKnifeKills, int? EnemyTaserKills, int? EquipmentValue, int? MoneySaved, int? KillReward, int? CashEarned, int? Account, int? TotalCashSpent, int? Ping, int? CompetitiveRanking, int? CompetitiveWins, int? CompetitiveRankType, string? Clan, string? CrosshairCode, IReadOnlyList<ControllerRoundStats> RoundStats)
```

#### Parameters

`Score` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The scoreboard score.

`Mvps` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Round MVP stars.

`Kills` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Kills as counted by the game (team kills excluded).

`Deaths` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Deaths as counted by the game.

`Assists` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Assists as counted by the game.

`Damage` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Total damage dealt, as counted by the game.

`UtilityDamage` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Damage dealt with grenades and fire.

`EnemiesFlashed` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Enemies flashed.

`HeadshotKills` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Kills that were headshots.

`Objective` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Objective points (plants/defuses).

`LiveTime` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time alive, in seconds.

`Enemy3Ks` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Rounds with exactly three kills.

`Enemy4Ks` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Rounds with exactly four kills.

`Enemy5Ks` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Rounds with five kills.

`EnemyKnifeKills` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Knife kills.

`EnemyTaserKills` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Zeus kills.

`EquipmentValue` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Summed equipment value at round starts.

`MoneySaved` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Summed money saved across rounds.

`KillReward` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Money earned from kills.

`CashEarned` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Total money earned.

`Account` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Money at the end of the match.

`TotalCashSpent` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Total money spent.

`Ping` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Last reported ping.

`CompetitiveRanking` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Competitive rank value shown on the scoreboard.

`CompetitiveWins` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Competitive wins shown on the scoreboard.

`CompetitiveRankType` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The rank type (for example Premier or Competitive).

`Clan` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
Clan tag.

`CrosshairCode` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The crosshair share code.

`RoundStats` [IReadOnlyList&lt;ControllerRoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Per-round stats kept by the game, one entry per round the controller reported.
