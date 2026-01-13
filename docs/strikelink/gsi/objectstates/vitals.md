# Vitals

Namespace: StrikeLink.GSI.ObjectStates

Represents the player's current vital statistics and round-specific metrics.

```csharp
public class Vitals : System.IEquatable`1[[StrikeLink.GSI.ObjectStates.Vitals, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Vitals](./strikelink/gsi/objectstates/vitals.md)<br>
Implements [IEquatable&lt;Vitals&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Health**

The player's current health.

```csharp
public int Health { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Armor**

The player's current armor value.

```csharp
public int Armor { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Helmet**

Indicates whether the player has a helmet equipped.

```csharp
public bool Helmet { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Flashed**

The flashbang effect duration (in milliseconds).

```csharp
public int Flashed { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Smoked**

The smoke effect duration (in milliseconds).

```csharp
public int Smoked { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Burning**

The burn effect duration (in milliseconds).

```csharp
public int Burning { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Money**

The player's current money amount.

```csharp
public int Money { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundKills**

The number of kills in the current round.

```csharp
public int RoundKills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundHeadshotKills**

The number of headshot kills in the current round.

```csharp
public int RoundHeadshotKills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **EquippedSlot**

The currently equipped weapon slot, if available.

```csharp
public Nullable<int> EquippedSlot { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **EquippedValue**

The value of the currently equipped weapon, if available.

```csharp
public Nullable<int> EquippedValue { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **Vitals(Int32, Int32, Boolean, Int32, Int32, Int32, Int32, Int32, Int32, Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;)**

Represents the player's current vital statistics and round-specific metrics.

```csharp
public Vitals(int Health, int Armor, bool Helmet, int Flashed, int Smoked, int Burning, int Money, int RoundKills, int RoundHeadshotKills, Nullable<int> EquippedSlot, Nullable<int> EquippedValue)
```

#### Parameters

`Health` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The player's current health.

`Armor` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The player's current armor value.

`Helmet` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Indicates whether the player has a helmet equipped.

`Flashed` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The flashbang effect duration (in milliseconds).

`Smoked` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The smoke effect duration (in milliseconds).

`Burning` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The burn effect duration (in milliseconds).

`Money` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The player's current money amount.

`RoundKills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of kills in the current round.

`RoundHeadshotKills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of headshot kills in the current round.

`EquippedSlot` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The currently equipped weapon slot, if available.

`EquippedValue` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The value of the currently equipped weapon, if available.

### **Vitals(Vitals)**

```csharp
protected Vitals(Vitals original)
```

#### Parameters

`original` [Vitals](./strikelink/gsi/objectstates/vitals.md)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PrintMembers(StringBuilder)**

```csharp
protected bool PrintMembers(StringBuilder builder)
```

#### Parameters

`builder` [StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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

### **Equals(Vitals)**

```csharp
public bool Equals(Vitals other)
```

#### Parameters

`other` [Vitals](./strikelink/gsi/objectstates/vitals.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Vitals <Clone>$()
```

#### Returns

[Vitals](./strikelink/gsi/objectstates/vitals.md)<br>

### **Deconstruct(Int32&, Int32&, Boolean&, Int32&, Int32&, Int32&, Int32&, Int32&, Int32&, Nullable`1&, Nullable`1&)**

```csharp
public void Deconstruct(Int32& Health, Int32& Armor, Boolean& Helmet, Int32& Flashed, Int32& Smoked, Int32& Burning, Int32& Money, Int32& RoundKills, Int32& RoundHeadshotKills, Nullable`1& EquippedSlot, Nullable`1& EquippedValue)
```

#### Parameters

`Health` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Armor` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Helmet` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`Flashed` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Smoked` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Burning` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Money` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`RoundKills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`RoundHeadshotKills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`EquippedSlot` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`EquippedValue` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
