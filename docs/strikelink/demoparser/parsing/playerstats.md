# PlayerStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class PlayerStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerStats](./strikelink/demoparser/parsing/playerstats.md)<br>
Implements [IEquatable&lt;PlayerStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **SteamId**

```csharp
public ulong SteamId { get; set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Name**

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **UserId**

```csharp
public int UserId { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsBot**

```csharp
public bool IsBot { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Team**

```csharp
public CsTeamSide Team { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **RoundsWon**

```csharp
public int RoundsWon { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsLost**

```csharp
public int RoundsLost { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundsParticipated**

```csharp
public int RoundsParticipated { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Kills**

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Rank**

```csharp
public RankSnapshot Rank { get; set; }
```

#### Property Value

[RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

### **Adr**

```csharp
public double Adr { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **MultiKills**

```csharp
public MultiKillSummary MultiKills { get; set; }
```

#### Property Value

[MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

### **AimRating**

```csharp
public double AimRating { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **UtilityRating**

```csharp
public double UtilityRating { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Trading**

```csharp
public TradingStats Trading { get; set; }
```

#### Property Value

[TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

### **Clutches**

```csharp
public ClutchStats Clutches { get; set; }
```

#### Property Value

[ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

### **HeadshotPercentage**

```csharp
public double HeadshotPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **TotalAccuracy**

```csharp
public double TotalAccuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SprayAccuracy**

```csharp
public double SprayAccuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **CrosshairAngle**

```csharp
public Nullable<double> CrosshairAngle { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Utility**

```csharp
public UtilityStats Utility { get; set; }
```

#### Property Value

[UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

### **TeamDamage**

```csharp
public TeamDamageStats TeamDamage { get; set; }
```

#### Property Value

[TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

### **Weapons**

```csharp
public IReadOnlyList<WeaponStats> Weapons { get; set; }
```

#### Property Value

[IReadOnlyList&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **BombPlants**

```csharp
public int BombPlants { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BombDefuses**

```csharp
public int BombDefuses { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **PlayerStats(UInt64, String, Int32, Boolean, CsTeamSide, Int32, Int32, Int32, Int32, Int32, Int32, RankSnapshot, Double, MultiKillSummary, Double, Double, TradingStats, ClutchStats, Double, Double, Double, Nullable&lt;Double&gt;, UtilityStats, TeamDamageStats, IReadOnlyList&lt;WeaponStats&gt;, Int32, Int32)**

```csharp
public PlayerStats(ulong SteamId, string Name, int UserId, bool IsBot, CsTeamSide Team, int RoundsWon, int RoundsLost, int RoundsParticipated, int Kills, int Deaths, int Assists, RankSnapshot Rank, double Adr, MultiKillSummary MultiKills, double AimRating, double UtilityRating, TradingStats Trading, ClutchStats Clutches, double HeadshotPercentage, double TotalAccuracy, double SprayAccuracy, Nullable<double> CrosshairAngle, UtilityStats Utility, TeamDamageStats TeamDamage, IReadOnlyList<WeaponStats> Weapons, int BombPlants, int BombDefuses)
```

#### Parameters

`SteamId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`Name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`UserId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`IsBot` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`Team` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

`RoundsWon` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`RoundsLost` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`RoundsParticipated` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Rank` [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

`Adr` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`MultiKills` [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

`AimRating` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`UtilityRating` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`Trading` [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

`Clutches` [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

`HeadshotPercentage` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`TotalAccuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`SprayAccuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`CrosshairAngle` [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`Utility` [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

`TeamDamage` [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

`Weapons` [IReadOnlyList&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`BombPlants` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`BombDefuses` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **Deconstruct(UInt64&, String&, Int32&, Boolean&, CsTeamSide&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, RankSnapshot&, Double&, MultiKillSummary&, Double&, Double&, TradingStats&, ClutchStats&, Double&, Double&, Double&, Nullable`1&, UtilityStats&, TeamDamageStats&, IReadOnlyList`1&, Int32&, Int32&)**

```csharp
public void Deconstruct(UInt64& SteamId, String& Name, Int32& UserId, Boolean& IsBot, CsTeamSide& Team, Int32& RoundsWon, Int32& RoundsLost, Int32& RoundsParticipated, Int32& Kills, Int32& Deaths, Int32& Assists, RankSnapshot& Rank, Double& Adr, MultiKillSummary& MultiKills, Double& AimRating, Double& UtilityRating, TradingStats& Trading, ClutchStats& Clutches, Double& HeadshotPercentage, Double& TotalAccuracy, Double& SprayAccuracy, Nullable`1& CrosshairAngle, UtilityStats& Utility, TeamDamageStats& TeamDamage, IReadOnlyList`1& Weapons, Int32& BombPlants, Int32& BombDefuses)
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

`CrosshairAngle` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Utility` [UtilityStats&](./strikelink/demoparser/parsing/utilitystats&.md)<br>

`TeamDamage` [TeamDamageStats&](./strikelink/demoparser/parsing/teamdamagestats&.md)<br>

`Weapons` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`BombPlants` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`BombDefuses` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
