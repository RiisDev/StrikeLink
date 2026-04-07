# PlayerStats

Namespace: StrikeLink.DemoParser.Parsing

Represents a comprehensive snapshot of a player's in-game statistics and performance metrics for a match or series.

```csharp
public sealed class PlayerStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerStats, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerStats](./strikelink/demoparser/parsing/playerstats.md)<br>
Implements [IEquatable&lt;PlayerStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This record aggregates a wide range of player performance data, enabling detailed analysis and
 comparison across matches. All values are captured at the time of record creation and are intended for read-only
 use.

## Properties

### **SteamId**

The unique Steam identifier for the player.

```csharp
public ulong SteamId { get; set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Name**

The display name of the player at the time the statistics were recorded.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **UserId**

The in-game user ID assigned to the player during the match.

```csharp
public int UserId { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsBot**

Indicates whether the player is a bot. Set to  if the player is an AI-controlled bot;
 otherwise, .

```csharp
public bool IsBot { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Team**

The team side the player was assigned to during the match.

```csharp
public CsTeamSide Team { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **RoundsWon**

The number of rounds won by the player's team while the player participated.

```csharp
public int RoundsWon { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsLost**

The number of rounds lost by the player's team while the player participated.

```csharp
public int RoundsLost { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsParticipated**

The total number of rounds in which the player actively participated.

```csharp
public int RoundsParticipated { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Kills**

The total number of kills achieved by the player.

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The total number of times the player died.

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The total number of assists credited to the player.

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UtilityDamage**

The total amount of damage the player dealt with utility.

```csharp
public int UtilityDamage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MvpCount**

The number of Most Valuable Player (MVP) awards earned by the player.

```csharp
public int MvpCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Rank**

The player's rank snapshot at the time of the statistics capture.

```csharp
public RankSnapshot Rank { get; set; }
```

#### Property Value

[RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

### **Adr**

The average damage dealt per round by the player.

```csharp
public double Adr { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **MultiKills**

A summary of the player's multi-kill rounds.

```csharp
public MultiKillSummary MultiKills { get; set; }
```

#### Property Value

[MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

### **AimRating**

A rating representing the player's aiming performance.

```csharp
public double AimRating { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **UtilityRating**

A rating representing the player's effectiveness with utility usage.

```csharp
public double UtilityRating { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Trading**

Statistics related to the player's trading actions, such as trade kills.

```csharp
public TradingStats Trading { get; set; }
```

#### Property Value

[TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

### **Clutches**

Statistics summarizing the player's clutch performance.

```csharp
public ClutchStats Clutches { get; set; }
```

#### Property Value

[ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

### **HeadshotPercentage**

The percentage of kills made by the player that were headshots.

```csharp
public double HeadshotPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **TotalAccuracy**

The player's overall weapon accuracy percentage.

```csharp
public double TotalAccuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SprayAccuracy**

The player's accuracy percentage during spray firing.

```csharp
public double SprayAccuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Utility**

Statistics related to the player's use of grenades and other utility items.

```csharp
public UtilityStats Utility { get; set; }
```

#### Property Value

[UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

### **TeamDamage**

Statistics summarizing damage dealt to teammates by the player.

```csharp
public TeamDamageStats TeamDamage { get; set; }
```

#### Property Value

[TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

### **Weapons**

A read-only list of weapon-specific statistics for the player.

```csharp
public IReadOnlyList<WeaponStats> Weapons { get; set; }
```

#### Property Value

[IReadOnlyList&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Impact**

Statistics measuring the player's impact on the match, such as entry kills or opening duels.

```csharp
public PlayerImpactStats Impact { get; set; }
```

#### Property Value

[PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>

### **BombPlants**

The number of times the player planted the bomb.

```csharp
public int BombPlants { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BombDefuses**

The number of times the player defused the bomb.

```csharp
public int BombDefuses { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **PlayerStats(UInt64, String, Int32, Boolean, CsTeamSide, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, RankSnapshot, Double, MultiKillSummary, Double, Double, TradingStats, ClutchStats, Double, Double, Double, UtilityStats, TeamDamageStats, IReadOnlyList&lt;WeaponStats&gt;, PlayerImpactStats, Int32, Int32)**

Represents a comprehensive snapshot of a player's in-game statistics and performance metrics for a match or series.

```csharp
public PlayerStats(ulong SteamId, string Name, int UserId, bool IsBot, CsTeamSide Team, int RoundsWon, int RoundsLost, int RoundsParticipated, int Kills, int Deaths, int Assists, int UtilityDamage, int MvpCount, RankSnapshot Rank, double Adr, MultiKillSummary MultiKills, double AimRating, double UtilityRating, TradingStats Trading, ClutchStats Clutches, double HeadshotPercentage, double TotalAccuracy, double SprayAccuracy, UtilityStats Utility, TeamDamageStats TeamDamage, IReadOnlyList<WeaponStats> Weapons, PlayerImpactStats Impact, int BombPlants, int BombDefuses)
```

#### Parameters

`SteamId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The unique Steam identifier for the player.

`Name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player at the time the statistics were recorded.

`UserId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The in-game user ID assigned to the player during the match.

`IsBot` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Indicates whether the player is a bot. Set to  if the player is an AI-controlled bot;
 otherwise, .

`Team` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The team side the player was assigned to during the match.

`RoundsWon` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of rounds won by the player's team while the player participated.

`RoundsLost` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of rounds lost by the player's team while the player participated.

`RoundsParticipated` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which the player actively participated.

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved by the player.

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of times the player died.

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of assists credited to the player.

`UtilityDamage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage the player dealt with utility.

`MvpCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of Most Valuable Player (MVP) awards earned by the player.

`Rank` [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>
The player's rank snapshot at the time of the statistics capture.

`Adr` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The average damage dealt per round by the player.

`MultiKills` [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>
A summary of the player's multi-kill rounds.

`AimRating` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
A rating representing the player's aiming performance.

`UtilityRating` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
A rating representing the player's effectiveness with utility usage.

`Trading` [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>
Statistics related to the player's trading actions, such as trade kills.

`Clutches` [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>
Statistics summarizing the player's clutch performance.

`HeadshotPercentage` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The percentage of kills made by the player that were headshots.

`TotalAccuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The player's overall weapon accuracy percentage.

`SprayAccuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The player's accuracy percentage during spray firing.

`Utility` [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>
Statistics related to the player's use of grenades and other utility items.

`TeamDamage` [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>
Statistics summarizing damage dealt to teammates by the player.

`Weapons` [IReadOnlyList&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of weapon-specific statistics for the player.

`Impact` [PlayerImpactStats](./strikelink/demoparser/parsing/playerimpactstats.md)<br>
Statistics measuring the player's impact on the match, such as entry kills or opening duels.

`BombPlants` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player planted the bomb.

`BombDefuses` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player defused the bomb.

**Remarks:**

This record aggregates a wide range of player performance data, enabling detailed analysis and
 comparison across matches. All values are captured at the time of record creation and are intended for read-only
 use.

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

### **Equals(PlayerStats)**

```csharp
public bool Equals(PlayerStats other)
```

#### Parameters

`other` [PlayerStats](./strikelink/demoparser/parsing/playerstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public PlayerStats <Clone>$()
```

#### Returns

[PlayerStats](./strikelink/demoparser/parsing/playerstats.md)<br>

### **Deconstruct(UInt64&, String&, Int32&, Boolean&, CsTeamSide&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, RankSnapshot&, Double&, MultiKillSummary&, Double&, Double&, TradingStats&, ClutchStats&, Double&, Double&, Double&, UtilityStats&, TeamDamageStats&, IReadOnlyList`1&, PlayerImpactStats&, Int32&, Int32&)**

```csharp
public void Deconstruct(UInt64& SteamId, String& Name, Int32& UserId, Boolean& IsBot, CsTeamSide& Team, Int32& RoundsWon, Int32& RoundsLost, Int32& RoundsParticipated, Int32& Kills, Int32& Deaths, Int32& Assists, Int32& UtilityDamage, Int32& MvpCount, RankSnapshot& Rank, Double& Adr, MultiKillSummary& MultiKills, Double& AimRating, Double& UtilityRating, TradingStats& Trading, ClutchStats& Clutches, Double& HeadshotPercentage, Double& TotalAccuracy, Double& SprayAccuracy, UtilityStats& Utility, TeamDamageStats& TeamDamage, IReadOnlyList`1& Weapons, PlayerImpactStats& Impact, Int32& BombPlants, Int32& BombDefuses)
```

#### Parameters

`SteamId` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

`Name` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`UserId` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`IsBot` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`Team` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`RoundsWon` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`RoundsLost` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`RoundsParticipated` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Kills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Deaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Assists` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`UtilityDamage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MvpCount` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Rank` [RankSnapshot&](./strikelink/demoparser/parsing/ranksnapshot&.md)<br>

`Adr` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`MultiKills` [MultiKillSummary&](./strikelink/demoparser/parsing/multikillsummary&.md)<br>

`AimRating` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`UtilityRating` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`Trading` [TradingStats&](./strikelink/demoparser/parsing/tradingstats&.md)<br>

`Clutches` [ClutchStats&](./strikelink/demoparser/parsing/clutchstats&.md)<br>

`HeadshotPercentage` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`TotalAccuracy` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`SprayAccuracy` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`Utility` [UtilityStats&](./strikelink/demoparser/parsing/utilitystats&.md)<br>

`TeamDamage` [TeamDamageStats&](./strikelink/demoparser/parsing/teamdamagestats&.md)<br>

`Weapons` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Impact` [PlayerImpactStats&](./strikelink/demoparser/parsing/playerimpactstats&.md)<br>

`BombPlants` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`BombDefuses` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
