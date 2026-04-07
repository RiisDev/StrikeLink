# WeaponStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for a specific weapon, including kills, deaths, assists, damage, shots fired, hits, and
 accuracy.

```csharp
public sealed class WeaponStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.WeaponStats, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [WeaponStats](./strikelink/demoparser/parsing/weaponstats.md)<br>
Implements [IEquatable&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Weapon**

The name of the weapon for which the statistics are recorded. Cannot be null or empty.

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Kills**

The total number of kills achieved with the weapon. Must be zero or greater.

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The total number of deaths incurred while using the weapon. Must be zero or greater.

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The total number of assists made with the weapon. Must be zero or greater.

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Damage**

The total amount of damage dealt using the weapon. Must be zero or greater.

```csharp
public int Damage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Shots**

The total number of shots fired with the weapon. Must be zero or greater.

```csharp
public int Shots { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Hits**

The total number of successful hits made with the weapon. Must be zero or greater.

```csharp
public int Hits { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Accuracy**

The accuracy percentage for the weapon, calculated as the ratio of hits to shots. Must be between 0.0 and 100.0.

```csharp
public double Accuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **WeaponStats(String, Int32, Int32, Int32, Int32, Int32, Int32, Double)**

Represents statistical data for a specific weapon, including kills, deaths, assists, damage, shots fired, hits, and
 accuracy.

```csharp
public WeaponStats(string Weapon, int Kills, int Deaths, int Assists, int Damage, int Shots, int Hits, double Accuracy)
```

#### Parameters

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the weapon for which the statistics are recorded. Cannot be null or empty.

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved with the weapon. Must be zero or greater.

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of deaths incurred while using the weapon. Must be zero or greater.

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of assists made with the weapon. Must be zero or greater.

`Damage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt using the weapon. Must be zero or greater.

`Shots` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of shots fired with the weapon. Must be zero or greater.

`Hits` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of successful hits made with the weapon. Must be zero or greater.

`Accuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The accuracy percentage for the weapon, calculated as the ratio of hits to shots. Must be between 0.0 and 100.0.

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

### **Equals(WeaponStats)**

```csharp
public bool Equals(WeaponStats other)
```

#### Parameters

`other` [WeaponStats](./strikelink/demoparser/parsing/weaponstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public WeaponStats <Clone>$()
```

#### Returns

[WeaponStats](./strikelink/demoparser/parsing/weaponstats.md)<br>

### **Deconstruct(String&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Double&)**

```csharp
public void Deconstruct(String& Weapon, Int32& Kills, Int32& Deaths, Int32& Assists, Int32& Damage, Int32& Shots, Int32& Hits, Double& Accuracy)
```

#### Parameters

`Weapon` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Kills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Deaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Assists` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Damage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Shots` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Hits` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Accuracy` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>
