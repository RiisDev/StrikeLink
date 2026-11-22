# WeaponStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for a specific weapon, including kills, deaths, assists, damage, shots fired, hits, and
 accuracy.

```csharp
public sealed record class WeaponStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.WeaponStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [WeaponStats](./strikelink/demoparser/parsing/weaponstats.md)<br>
Implements [IEquatable&lt;WeaponStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Weapon**

The name of the weapon for which the statistics are recorded. Cannot be null or empty.

```csharp
public string Weapon { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Kills**

The total number of kills achieved with the weapon. Must be zero or greater.

```csharp
public int Kills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The total number of deaths incurred while using the weapon. Must be zero or greater.

```csharp
public int Deaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The total number of assists made with the weapon. Must be zero or greater.

```csharp
public int Assists { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Damage**

The total amount of damage dealt using the weapon. Must be zero or greater.

```csharp
public int Damage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Shots**

The total number of shots fired with the weapon. Must be zero or greater.

```csharp
public int Shots { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Hits**

The total number of successful hits made with the weapon. Must be zero or greater.

```csharp
public int Hits { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Accuracy**

The accuracy percentage for the weapon, calculated as the ratio of hits to shots. Must be between 0.0 and 100.0.

```csharp
public double Accuracy { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **WeaponStats(String, Int32, Int32, Int32, Int32, Int32, Int32, Double)**

Represents statistical data for a specific weapon, including kills, deaths, assists, damage, shots fired, hits, and
 accuracy.

```csharp
public WeaponStats(string Weapon, int Kills, int Deaths, int Assists, int Damage, int Shots, int Hits, double Accuracy)
```

#### Parameters

`Weapon` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the weapon for which the statistics are recorded. Cannot be null or empty.

`Kills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved with the weapon. Must be zero or greater.

`Deaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of deaths incurred while using the weapon. Must be zero or greater.

`Assists` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of assists made with the weapon. Must be zero or greater.

`Damage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt using the weapon. Must be zero or greater.

`Shots` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of shots fired with the weapon. Must be zero or greater.

`Hits` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of successful hits made with the weapon. Must be zero or greater.

`Accuracy` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The accuracy percentage for the weapon, calculated as the ratio of hits to shots. Must be between 0.0 and 100.0.
