# UtilityStats

Namespace: StrikeLink.DemoParser.Parsing

Represents a set of statistics related to utility usage and effects in a match, including kills, deaths, and
 actions involving grenades and other utility items.

```csharp
public sealed record class UtilityStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.UtilityStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>
Implements [IEquatable&lt;UtilityStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This record is typically used to aggregate and analyze a player's effectiveness with utility items
 during a match. All values are non-negative and represent cumulative statistics for a given period or
 match.

## Properties

### **FragKills**

The number of kills achieved with utility grenades, such as HE grenades or molotovs.

```csharp
public int FragKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FragDeaths**

The number of deaths caused by enemy utility grenades.

```csharp
public int FragDeaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PlayersFlashed**

The number of opposing players blinded by the player's flashbangs.

```csharp
public int PlayersFlashed { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TimesFlashed**

The number of times the player was blinded by enemy flashbangs.

```csharp
public int TimesFlashed { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MollyKills**

The number of kills achieved specifically with molotovs or incendiary grenades.

```csharp
public int MollyKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MollyDeaths**

The number of times the player was killed by enemy molotovs or incendiary grenades.

```csharp
public int MollyDeaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UtilityDamage**

The total amount of damage dealt to opponents using utility grenades.

```csharp
public int UtilityDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FlashbangsThrown**

The number of flashbang grenades thrown by the player.

```csharp
public int FlashbangsThrown { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HeGrenadesThrown**

The number of high-explosive (HE) grenades thrown by the player.

```csharp
public int HeGrenadesThrown { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MolotovsThrown**

The number of molotov or incendiary grenades thrown by the player.

```csharp
public int MolotovsThrown { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **UtilityStats(Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)**

Represents a set of statistics related to utility usage and effects in a match, including kills, deaths, and
 actions involving grenades and other utility items.

```csharp
public UtilityStats(int FragKills, int FragDeaths, int PlayersFlashed, int TimesFlashed, int MollyKills, int MollyDeaths, int UtilityDamage, int FlashbangsThrown, int HeGrenadesThrown, int MolotovsThrown)
```

#### Parameters

`FragKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of kills achieved with utility grenades, such as HE grenades or molotovs.

`FragDeaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of deaths caused by enemy utility grenades.

`PlayersFlashed` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of opposing players blinded by the player's flashbangs.

`TimesFlashed` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player was blinded by enemy flashbangs.

`MollyKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of kills achieved specifically with molotovs or incendiary grenades.

`MollyDeaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times the player was killed by enemy molotovs or incendiary grenades.

`UtilityDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt to opponents using utility grenades.

`FlashbangsThrown` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of flashbang grenades thrown by the player.

`HeGrenadesThrown` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of high-explosive (HE) grenades thrown by the player.

`MolotovsThrown` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of molotov or incendiary grenades thrown by the player.

**Remarks:**

This record is typically used to aggregate and analyze a player's effectiveness with utility items
 during a match. All values are non-negative and represent cumulative statistics for a given period or
 match.
