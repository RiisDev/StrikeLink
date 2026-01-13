# MatchStats

Namespace: StrikeLink.GSI.ObjectStates

Represents aggregate player statistics for the current match.

```csharp
public class MatchStats : System.IEquatable`1[[StrikeLink.GSI.ObjectStates.MatchStats, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>
Implements [IEquatable&lt;MatchStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Kills**

The total number of kills.

```csharp
public int Kills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Assists**

The total number of assists.

```csharp
public int Assists { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Deaths**

The total number of deaths.

```csharp
public int Deaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MVPs**

The number of MVP awards earned.

```csharp
public int MVPs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Score**

The player's current score.

```csharp
public int Score { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **MatchStats(Int32, Int32, Int32, Int32, Int32)**

Represents aggregate player statistics for the current match.

```csharp
public MatchStats(int Kills, int Assists, int Deaths, int MVPs, int Score)
```

#### Parameters

`Kills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills.

`Assists` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of assists.

`Deaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of deaths.

`MVPs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of MVP awards earned.

`Score` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The player's current score.

### **MatchStats(MatchStats)**

```csharp
protected MatchStats(MatchStats original)
```

#### Parameters

`original` [MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>

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

### **Equals(MatchStats)**

```csharp
public bool Equals(MatchStats other)
```

#### Parameters

`other` [MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public MatchStats <Clone>$()
```

#### Returns

[MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Int32&, Int32&)**

```csharp
public void Deconstruct(Int32& Kills, Int32& Assists, Int32& Deaths, Int32& MVPs, Int32& Score)
```

#### Parameters

`Kills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Assists` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Deaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MVPs` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Score` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
