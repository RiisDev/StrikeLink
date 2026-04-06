# WeaponStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class WeaponStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.WeaponStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [WeaponStats](./strikelink/demoparser/parsing/weaponstats.md)<br>
Implements [IEquatable&lt;WeaponStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Weapon**

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **Damage**

```csharp
public int Damage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Shots**

```csharp
public int Shots { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Hits**

```csharp
public int Hits { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Accuracy**

```csharp
public double Accuracy { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **WeaponStats(String, Int32, Int32, Int32, Int32, Int32, Int32, Double)**

```csharp
public WeaponStats(string Weapon, int Kills, int Deaths, int Assists, int Damage, int Shots, int Hits, double Accuracy)
```

#### Parameters

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Damage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Shots` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Hits` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Accuracy` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

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
