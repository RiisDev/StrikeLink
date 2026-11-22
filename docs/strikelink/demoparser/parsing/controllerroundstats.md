# ControllerRoundStats

Namespace: StrikeLink.DemoParser.Parsing

One round of a controller's per-round stats.

```csharp
public sealed record class ControllerRoundStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.ControllerRoundStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ControllerRoundStats](./strikelink/demoparser/parsing/controllerroundstats.md)<br>
Implements [IEquatable&lt;ControllerRoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Round**

The round number (1-based).

```csharp
public int Round { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Kills**

Kills this round.

```csharp
public int Kills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

Deaths this round.

```csharp
public int Deaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

Assists this round.

```csharp
public int Assists { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Damage**

Damage dealt this round.

```csharp
public int Damage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HeadshotKills**

Headshot kills this round.

```csharp
public int HeadshotKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **LiveTime**

Seconds alive this round.

```csharp
public int LiveTime { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Objective**

Objective points this round.

```csharp
public int Objective { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UtilityDamage**

Utility damage this round.

```csharp
public int UtilityDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EnemiesFlashed**

Enemies flashed this round.

```csharp
public int EnemiesFlashed { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EquipmentValue**

Equipment value at the start of the round.

```csharp
public int EquipmentValue { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MoneySaved**

Money saved into the next round.

```csharp
public int MoneySaved { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **KillReward**

Money earned from kills this round.

```csharp
public int KillReward { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CashEarned**

Money earned this round.

```csharp
public int CashEarned { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **ControllerRoundStats(Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)**

One round of a controller's per-round stats.

```csharp
public ControllerRoundStats(int Round, int Kills, int Deaths, int Assists, int Damage, int HeadshotKills, int LiveTime, int Objective, int UtilityDamage, int EnemiesFlashed, int EquipmentValue, int MoneySaved, int KillReward, int CashEarned)
```

#### Parameters

`Round` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The round number (1-based).

`Kills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Kills this round.

`Deaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Deaths this round.

`Assists` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Assists this round.

`Damage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Damage dealt this round.

`HeadshotKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Headshot kills this round.

`LiveTime` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Seconds alive this round.

`Objective` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Objective points this round.

`UtilityDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Utility damage this round.

`EnemiesFlashed` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Enemies flashed this round.

`EquipmentValue` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Equipment value at the start of the round.

`MoneySaved` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Money saved into the next round.

`KillReward` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Money earned from kills this round.

`CashEarned` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Money earned this round.
