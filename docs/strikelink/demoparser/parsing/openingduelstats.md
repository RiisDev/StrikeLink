# OpeningDuelStats

Namespace: StrikeLink.DemoParser.Parsing

Opening duels: the first kill of each round (team kills and suicides excluded).

```csharp
public sealed record class OpeningDuelStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.OpeningDuelStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [OpeningDuelStats](./strikelink/demoparser/parsing/openingduelstats.md)<br>
Implements [IEquatable&lt;OpeningDuelStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Kills**

Rounds where the player got the opening kill.

```csharp
public int Kills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

Rounds where the player was the opening victim.

```csharp
public int Deaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WinRate**

Kills as a percentage of all opening duels the player was in.

```csharp
public double WinRate { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **KillsTraded**

Opening kills where the player was then killed by an enemy of the victim's team within the trade window (the opening kill was traded back).

```csharp
public int KillsTraded { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **DeathsTraded**

Opening deaths that a teammate avenged within the trade window.

```csharp
public int DeathsTraded { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **OpeningDuelStats(Int32, Int32, Double, Int32, Int32)**

Opening duels: the first kill of each round (team kills and suicides excluded).

```csharp
public OpeningDuelStats(int Kills, int Deaths, double WinRate, int KillsTraded, int DeathsTraded)
```

#### Parameters

`Kills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Rounds where the player got the opening kill.

`Deaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Rounds where the player was the opening victim.

`WinRate` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Kills as a percentage of all opening duels the player was in.

`KillsTraded` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Opening kills where the player was then killed by an enemy of the victim's team within the trade window (the opening kill was traded back).

`DeathsTraded` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Opening deaths that a teammate avenged within the trade window.
