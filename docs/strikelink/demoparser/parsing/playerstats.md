# PlayerStats

Namespace: StrikeLink.DemoParser.Parsing

Represents a comprehensive snapshot of a player's in-game statistics and performance metrics for a match or series.

```csharp
public sealed record class PlayerStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [PlayerStats](./strikelink/demoparser/parsing/playerstats.md)<br>
Implements [IEquatable&lt;PlayerStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This record aggregates a wide range of player performance data, enabling detailed analysis and
 comparison across matches. All values are captured at the time of record creation and are intended for read-only
 use.

## Properties

### **SteamId**

The unique Steam identifier for the player.

```csharp
public ulong SteamId { get; init; }
```

#### Property Value

[UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Name**

The display name of the player at the time the statistics were recorded.

```csharp
public string Name { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **UserId**

The in-game user ID assigned to the player during the match.

```csharp
public int UserId { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsBot**

Indicates whether the player is a bot. Set to  if the player is an AI-controlled bot;
 otherwise, .

```csharp
public bool IsBot { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Team**

The persistent lineup identity of the player in the match.

```csharp
public MatchTeam Team { get; init; }
```

#### Property Value

[MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>

### **RoundsWon**

The number of rounds won by the player's team while the player participated.

```csharp
public int RoundsWon { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsLost**

The number of rounds lost by the player's team while the player participated.

```csharp
public int RoundsLost { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsParticipated**

The total number of rounds in which the player actively participated.

```csharp
public int RoundsParticipated { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Kills**

The total number of kills achieved by the player.

```csharp
public int Kills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The total number of times the player died.

```csharp
public int Deaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The total number of assists credited to the player.

```csharp
public int Assists { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UtilityDamage**

The total amount of damage the player dealt with utility.

```csharp
public int UtilityDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MvpCount**

The number of Most Valuable Player (MVP) awards earned by the player.

```csharp
public int MvpCount { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Rank**

The player's rank snapshot at the time of the statistics capture.

```csharp
public RankSnapshot Rank { get; init; }
```

#### Property Value

[RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

### **Adr**

The average damage dealt per round by the player.

```csharp
public double Adr { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **MultiKills**

A summary of the player's multi-kill rounds.

```csharp
public MultiKillSummary MultiKills { get; init; }
```

#### Property Value

[MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

### **AimRating**

A rating representing the player's aiming performance.

```csharp
public double AimRating { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **UtilityRating**

A rating representing the player's effectiveness with utility usage.

```csharp
public double UtilityRating { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Trading**

Statistics related to the player's trading actions, such as trade kills.

```csharp
public TradingStats Trading { get; init; }
```

#### Property Value

[TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

### **Clutches**

Statistics summarizing the player's clutch performance.

```csharp
public ClutchStats Clutches { get; init; }
```

#### Property Value

[ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

### **HeadshotPercentage**

The percentage of kills made by the player that were headshots.

```csharp
public double HeadshotPercentage { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **TotalAccuracy**

The player's overall weapon accuracy percentage.

```csharp
public double TotalAccuracy { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SprayAccuracy**

The player's accuracy percentage during spray firing.

```csharp
public double SprayAccuracy { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Utility**

Statistics related to the player's use of grenades and other utility items.

```csharp
public UtilityStats Utility { get; init; }
```

#### Property Value

[UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

### **TeamDamage**

Statistics summarizing damage dealt to teammates by the player.

```csharp
public TeamDamageStats TeamDamage { get; init; }
```

#### Property Value

[TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

### **Weapons**

A read-only list of weapon-specific statistics for the player.

```csharp
public IReadOnlyList<WeaponStats> Weapons { get; init; }
```

#### Property Value

[IReadOnlyList&lt;WeaponStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Impact**

Statistics measuring the player's impact on the match, such as entry kills or opening duels.

```csharp
public PlayerImpactStats Impact { get; init; }
```

#### Property Value

[PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>

### **BombPlants**

The number of times the player planted the bomb.

```csharp
public int BombPlants { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BombDefuses**

The number of times the player defused the bomb.

```csharp
public int BombDefuses { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **KastPercentage**

Percentage of rounds in which the player got a kill, an assist, survived, or was traded (avenged within 4 seconds).

```csharp
public double KastPercentage { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Score**

Scoreboard score. Taken from the game's own `m_iScore` when the demo carries entity state; otherwise estimated from the contribution values (kill 2, assist 1, plant 2, defuse 3/1 plus 1 per living teammate, explosion 1, team kill and suicide -2; objective-kill bonuses are not visible, so the estimate can run slightly low).

```csharp
public int Score { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ControllerProperties**

Raw values of the player's controller entity (`CCSPlayerController`) as last seen in the demo, keyed by dotted property name, for example `m_iMVPs`, `m_pActionTrackingServices.m_iDamage`, `m_pActionTrackingServices.m_perRoundStats.0003.m_iKills` or `m_iCompetitiveRanking`. Values are [Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64), [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64), [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean), [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single) (or `float[]`) and [String](https://learn.microsoft.com/en-us/dotnet/api/system.string). Empty when the demo has no entity state. The set of names depends on the game build; prefer `Controller` for the common stats.

```csharp
public IReadOnlyDictionary<string, object?> ControllerProperties { get; init; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **Controller**

Typed, build-tolerant view of the controller (see [ControllerStats](./strikelink/demoparser/parsing/controllerstats.md)); null when the demo has no entity state.

```csharp
public ControllerStats? Controller { get; init; }
```

#### Property Value

[ControllerStats](./strikelink/demoparser/parsing/controllerstats.md)<br>

### **OpeningDuels**

```csharp
public OpeningDuelStats OpeningDuels { get; init; }
```

#### Property Value

[OpeningDuelStats](./strikelink/demoparser/parsing/openingduelstats.md)<br>

### **Economy**

```csharp
public EconomyStats Economy { get; init; }
```

#### Property Value

[EconomyStats](./strikelink/demoparser/parsing/economystats.md)<br>

### **Role**

```csharp
public PlayerRole Role { get; init; }
```

#### Property Value

[PlayerRole](./strikelink/demoparser/parsing/playerrole.md)<br>

### **Aim**

```csharp
public AimStats Aim { get; init; }
```

#### Property Value

[AimStats](./strikelink/demoparser/parsing/aimstats.md)<br>

## Constructors

### **PlayerStats(UInt64, String, Int32, Boolean, MatchTeam, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, RankSnapshot, Double, MultiKillSummary, Double, Double, TradingStats, ClutchStats, Double, Double, Double, UtilityStats, TeamDamageStats, IReadOnlyList&lt;WeaponStats&gt;, PlayerImpactStats, Int32, Int32, Double, Int32, IReadOnlyDictionary&lt;String, Object&gt;, ControllerStats, OpeningDuelStats, EconomyStats, PlayerRole, AimStats)**

Represents a comprehensive snapshot of a player's in-game statistics and performance metrics for a match or series.

```csharp
public PlayerStats(ulong SteamId, string Name, int UserId, bool IsBot, MatchTeam Team, int RoundsWon, int RoundsLost, int RoundsParticipated, int Kills, int Deaths, int Assists, int UtilityDamage, int MvpCount, RankSnapshot Rank, double Adr, MultiKillSummary MultiKills, double AimRating, double UtilityRating, TradingStats Trading, ClutchStats Clutches, double HeadshotPercentage, double TotalAccuracy, double SprayAccuracy, UtilityStats Utility, TeamDamageStats TeamDamage, IReadOnlyList<WeaponStats> Weapons, PlayerImpactStats Impact, int BombPlants, int BombDefuses, double KastPercentage, int Score, IReadOnlyDictionary<string, object?> ControllerProperties, ControllerStats? Controller, OpeningDuelStats OpeningDuels, EconomyStats Economy, PlayerRole Role, AimStats Aim)
```

#### Parameters

`SteamId` [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique Steam identifier for the player.

`Name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player at the time the statistics were recorded.

`UserId` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The in-game user ID assigned to the player during the match.

`IsBot` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Indicates whether the player is a bot. Set to  if the player is an AI-controlled bot;
 otherwise, .

`Team` [MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>
The persistent lineup identity of the player in the match.

`RoundsWon` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of rounds won by the player's team while the player participated.

`RoundsLost` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of rounds lost by the player's team while the player participated.

`RoundsParticipated` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which the player actively participated.

`Kills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved by the player.

`Deaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of times the player died.

`Assists` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of assists credited to the player.

`UtilityDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage the player dealt with utility.

`MvpCount` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of Most Valuable Player (MVP) awards earned by the player.

`Rank` [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>
The player's rank snapshot at the time of the statistics capture.

`Adr` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The average damage dealt per round by the player.

`MultiKills` [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>
A summary of the player's multi-kill rounds.

`AimRating` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
A rating representing the player's aiming performance.

`UtilityRating` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
A rating representing the player's effectiveness with utility usage.

`Trading` [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>
Statistics related to the player's trading actions, such as trade kills.

`Clutches` [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>
Statistics summarizing the player's clutch performance.

`HeadshotPercentage` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The percentage of kills made by the player that were headshots.

`TotalAccuracy` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The player's overall weapon accuracy percentage.

`SprayAccuracy` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The player's accuracy percentage during spray firing.

`Utility` [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>
Statistics related to the player's use of grenades and other utility items.

`TeamDamage` [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>
Statistics summarizing damage dealt to teammates by the player.

`Weapons` [IReadOnlyList&lt;WeaponStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of weapon-specific statistics for the player.

`Impact` [PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>
Statistics measuring the player's impact on the match, such as entry kills or opening duels.

`BombPlants` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player planted the bomb.

`BombDefuses` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player defused the bomb.

`KastPercentage` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Percentage of rounds in which the player got a kill, an assist, survived, or was traded (avenged within 4 seconds).

`Score` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Scoreboard score. Taken from the game's own `m_iScore` when the demo carries entity state; otherwise estimated from the contribution values (kill 2, assist 1, plant 2, defuse 3/1 plus 1 per living teammate, explosion 1, team kill and suicide -2; objective-kill bonuses are not visible, so the estimate can run slightly low).

`ControllerProperties` [IReadOnlyDictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>
Raw values of the player's controller entity (`CCSPlayerController`) as last seen in the demo, keyed by dotted property name, for example `m_iMVPs`, `m_pActionTrackingServices.m_iDamage`, `m_pActionTrackingServices.m_perRoundStats.0003.m_iKills` or `m_iCompetitiveRanking`. Values are [Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64), [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64), [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean), [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single) (or `float[]`) and [String](https://learn.microsoft.com/en-us/dotnet/api/system.string). Empty when the demo has no entity state. The set of names depends on the game build; prefer `Controller` for the common stats.

`Controller` [ControllerStats](./strikelink/demoparser/parsing/controllerstats.md)?<br>
Typed, build-tolerant view of the controller (see [ControllerStats](./strikelink/demoparser/parsing/controllerstats.md)); null when the demo has no entity state.

`OpeningDuels` [OpeningDuelStats](./strikelink/demoparser/parsing/openingduelstats.md)<br>

`Economy` [EconomyStats](./strikelink/demoparser/parsing/economystats.md)<br>

`Role` [PlayerRole](./strikelink/demoparser/parsing/playerrole.md)<br>

`Aim` [AimStats](./strikelink/demoparser/parsing/aimstats.md)<br>

**Remarks:**

This record aggregates a wide range of player performance data, enabling detailed analysis and
 comparison across matches. All values are captured at the time of record creation and are intended for read-only
 use.
