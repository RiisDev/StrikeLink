# RoundImpactSnapshot

Namespace: StrikeLink.DemoParser.Parsing

Represents a snapshot of a player's impact and performance metrics for a single round in a match.

```csharp
public sealed class RoundImpactSnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundImpactSnapshot, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundImpactSnapshot](./strikelink/demoparser/parsing/roundimpactsnapshot.md)<br>
Implements [IEquatable&lt;RoundImpactSnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

The number of the round within the match. Must be a positive integer.

```csharp
public int RoundNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Side**

The team side the player was on during the round.

```csharp
public CsTeamSide Side { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **Won**

A value indicating whether the player's team won the round. Set to  if the round was won;
 otherwise, .

```csharp
public bool Won { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Kills**

The number of kills achieved by the player in the round. Must be zero or greater.

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The number of assists credited to the player in the round. Must be zero or greater.

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The number of times the player died in the round. Must be zero or greater.

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Damage**

The total amount of damage dealt by the player in the round. Must be zero or greater.

```csharp
public int Damage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WinProbability**

The probability, as a value between 0.0 and 1.0, that the player's team would win the round at the start of the
 round.

```csharp
public double WinProbability { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **RoundImpact**

A calculated value representing the player's overall impact on the round, based on various performance metrics.

```csharp
public double RoundImpact { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **RoundRating**

A rating value summarizing the player's performance in the round, typically normalized for comparison across
 rounds.

```csharp
public double RoundRating { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **RoundImpactSnapshot(Int32, CsTeamSide, Boolean, Int32, Int32, Int32, Int32, Double, Double, Double)**

Represents a snapshot of a player's impact and performance metrics for a single round in a match.

```csharp
public RoundImpactSnapshot(int RoundNumber, CsTeamSide Side, bool Won, int Kills, int Assists, int Deaths, int Damage, double WinProbability, double RoundImpact, double RoundRating)
```

#### Parameters

`RoundNumber` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of the round within the match. Must be a positive integer.

`Side` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
The team side the player was on during the round.

`Won` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A value indicating whether the player's team won the round. Set to  if the round was won;
 otherwise, .

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of kills achieved by the player in the round. Must be zero or greater.

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of assists credited to the player in the round. Must be zero or greater.

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player died in the round. Must be zero or greater.

`Damage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt by the player in the round. Must be zero or greater.

`WinProbability` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The probability, as a value between 0.0 and 1.0, that the player's team would win the round at the start of the
 round.

`RoundImpact` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
A calculated value representing the player's overall impact on the round, based on various performance metrics.

`RoundRating` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
A rating value summarizing the player's performance in the round, typically normalized for comparison across
 rounds.

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

### **Equals(RoundImpactSnapshot)**

```csharp
public bool Equals(RoundImpactSnapshot other)
```

#### Parameters

`other` [RoundImpactSnapshot](./strikelink/demoparser/parsing/roundimpactsnapshot.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundImpactSnapshot <Clone>$()
```

#### Returns

[RoundImpactSnapshot](./strikelink/demoparser/parsing/roundimpactsnapshot.md)<br>

### **Deconstruct(Int32&, CsTeamSide&, Boolean&, Int32&, Int32&, Int32&, Int32&, Double&, Double&, Double&)**

```csharp
public void Deconstruct(Int32& RoundNumber, CsTeamSide& Side, Boolean& Won, Int32& Kills, Int32& Assists, Int32& Deaths, Int32& Damage, Double& WinProbability, Double& RoundImpact, Double& RoundRating)
```

#### Parameters

`RoundNumber` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Side` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`Won` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`Kills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Assists` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Deaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Damage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`WinProbability` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`RoundImpact` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`RoundRating` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>
