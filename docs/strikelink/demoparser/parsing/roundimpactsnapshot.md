# RoundImpactSnapshot

Namespace: StrikeLink.DemoParser.Parsing

Represents a snapshot of a player's impact and performance metrics for a single round in a match.

```csharp
public sealed record class RoundImpactSnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundImpactSnapshot, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [RoundImpactSnapshot](./strikelink/demoparser/parsing/roundimpactsnapshot.md)<br>
Implements [IEquatable&lt;RoundImpactSnapshot&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

The number of the round within the match. Must be a positive integer.

```csharp
public int RoundNumber { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Side**

The team side the player was on during the round.

```csharp
public CsTeamSide Side { get; init; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **Won**

A value indicating whether the player's team won the round. Set to  if the round was won;
 otherwise, .

```csharp
public bool Won { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Kills**

The number of kills achieved by the player in the round. Must be zero or greater.

```csharp
public int Kills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The number of assists credited to the player in the round. Must be zero or greater.

```csharp
public int Assists { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The number of times the player died in the round. Must be zero or greater.

```csharp
public int Deaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Damage**

The total amount of damage dealt by the player in the round. Must be zero or greater.

```csharp
public int Damage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WinProbability**

The player's side's chance of winning the round at round start, as a percentage, from the alive-count model (about 49 for T and 51 for CT).

```csharp
public double WinProbability { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **RoundImpact**

Win probability the player added or lost this round, in percentage points. Each kill adds the gain in the killing side's round win probability (alive counts, bomb state) and costs the victim the same; an assist earns a quarter of the gain. Can be negative.

```csharp
public double RoundImpact { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **RoundRating**

Round impact centred on 50 and clamped to 0 to 100.

```csharp
public double RoundRating { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **RoundImpactSnapshot(Int32, CsTeamSide, Boolean, Int32, Int32, Int32, Int32, Double, Double, Double)**

Represents a snapshot of a player's impact and performance metrics for a single round in a match.

```csharp
public RoundImpactSnapshot(int RoundNumber, CsTeamSide Side, bool Won, int Kills, int Assists, int Deaths, int Damage, double WinProbability, double RoundImpact, double RoundRating)
```

#### Parameters

`RoundNumber` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of the round within the match. Must be a positive integer.

`Side` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The team side the player was on during the round.

`Won` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A value indicating whether the player's team won the round. Set to  if the round was won;
 otherwise, .

`Kills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of kills achieved by the player in the round. Must be zero or greater.

`Assists` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of assists credited to the player in the round. Must be zero or greater.

`Deaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player died in the round. Must be zero or greater.

`Damage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt by the player in the round. Must be zero or greater.

`WinProbability` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The player's side's chance of winning the round at round start, as a percentage, from the alive-count model (about 49 for T and 51 for CT).

`RoundImpact` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Win probability the player added or lost this round, in percentage points. Each kill adds the gain in the killing side's round win probability (alive counts, bomb state) and costs the victim the same; an assist earns a quarter of the gain. Can be negative.

`RoundRating` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Round impact centred on 50 and clamped to 0 to 100.
