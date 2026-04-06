# UtilityStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class UtilityStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.UtilityStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>
Implements [IEquatable&lt;UtilityStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **FragKills**

```csharp
public int FragKills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FragDeaths**

```csharp
public int FragDeaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PlayersFlashed**

```csharp
public int PlayersFlashed { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TimesFlashed**

```csharp
public int TimesFlashed { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MollyKills**

```csharp
public int MollyKills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MollyDeaths**

```csharp
public int MollyDeaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UtilityDamage**

```csharp
public int UtilityDamage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FlashbangsThrown**

```csharp
public int FlashbangsThrown { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HeGrenadesThrown**

```csharp
public int HeGrenadesThrown { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MolotovsThrown**

```csharp
public int MolotovsThrown { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **UtilityStats(Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)**

```csharp
public UtilityStats(int FragKills, int FragDeaths, int PlayersFlashed, int TimesFlashed, int MollyKills, int MollyDeaths, int UtilityDamage, int FlashbangsThrown, int HeGrenadesThrown, int MolotovsThrown)
```

#### Parameters

`FragKills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`FragDeaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`PlayersFlashed` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`TimesFlashed` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`MollyKills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`MollyDeaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`UtilityDamage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`FlashbangsThrown` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`HeGrenadesThrown` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`MolotovsThrown` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **Equals(UtilityStats)**

```csharp
public bool Equals(UtilityStats other)
```

#### Parameters

`other` [UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public UtilityStats <Clone>$()
```

#### Returns

[UtilityStats](./strikelink/demoparser/parsing/utilitystats.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&)**

```csharp
public void Deconstruct(Int32& FragKills, Int32& FragDeaths, Int32& PlayersFlashed, Int32& TimesFlashed, Int32& MollyKills, Int32& MollyDeaths, Int32& UtilityDamage, Int32& FlashbangsThrown, Int32& HeGrenadesThrown, Int32& MolotovsThrown)
```

#### Parameters

`FragKills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`FragDeaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`PlayersFlashed` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TimesFlashed` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MollyKills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MollyDeaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`UtilityDamage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`FlashbangsThrown` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`HeGrenadesThrown` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MolotovsThrown` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
