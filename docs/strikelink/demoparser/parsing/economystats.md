# EconomyStats

Namespace: StrikeLink.DemoParser.Parsing

Results split by how the player's team and the opposition bought.

```csharp
public sealed record class EconomyStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.EconomyStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [EconomyStats](./strikelink/demoparser/parsing/economystats.md)<br>
Implements [IEquatable&lt;EconomyStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **PistolRounds**

Pistol rounds played.

```csharp
public int PistolRounds { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PistolWins**

Pistol rounds won.

```csharp
public int PistolWins { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EcoRounds**

Rounds where the player's team bought Eco or Force.

```csharp
public int EcoRounds { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EcoWins**

Eco or Force rounds won.

```csharp
public int EcoWins { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FullBuyRounds**

Rounds where the player's team bought Half or Full.

```csharp
public int FullBuyRounds { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FullBuyWins**

Half or Full rounds won.

```csharp
public int FullBuyWins { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EcoKills**

Kills made while the player's team was on an Eco or Force buy against a Half or Full buy.

```csharp
public int EcoKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AntiEcoKills**

Kills made on a Half or Full buy against an Eco or Force buy.

```csharp
public int AntiEcoKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **EconomyStats(Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)**

Results split by how the player's team and the opposition bought.

```csharp
public EconomyStats(int PistolRounds, int PistolWins, int EcoRounds, int EcoWins, int FullBuyRounds, int FullBuyWins, int EcoKills, int AntiEcoKills)
```

#### Parameters

`PistolRounds` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Pistol rounds played.

`PistolWins` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Pistol rounds won.

`EcoRounds` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Rounds where the player's team bought Eco or Force.

`EcoWins` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Eco or Force rounds won.

`FullBuyRounds` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Rounds where the player's team bought Half or Full.

`FullBuyWins` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Half or Full rounds won.

`EcoKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Kills made while the player's team was on an Eco or Force buy against a Half or Full buy.

`AntiEcoKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Kills made on a Half or Full buy against an Eco or Force buy.
