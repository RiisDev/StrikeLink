# Stats

Namespace: StrikeLink.GSI.ObjectStates

Represents team statistics for the current map.

```csharp
public class Stats : System.IEquatable`1[[StrikeLink.GSI.ObjectStates.Stats, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Stats](./strikelink/gsi/objectstates/stats.md)<br>
Implements [IEquatable&lt;Stats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Score**

The current team score.

```csharp
public int Score { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ConsecutiveLosses**

The number of consecutive rounds lost.

```csharp
public int ConsecutiveLosses { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TimeoutsRemaining**

The number of tactical timeouts remaining.

```csharp
public int TimeoutsRemaining { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MatchesWonThisSeries**

The number of maps won in the current series.

```csharp
public int MatchesWonThisSeries { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **Stats(Int32, Int32, Int32, Int32)**

Represents team statistics for the current map.

```csharp
public Stats(int Score, int ConsecutiveLosses, int TimeoutsRemaining, int MatchesWonThisSeries)
```

#### Parameters

`Score` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The current team score.

`ConsecutiveLosses` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of consecutive rounds lost.

`TimeoutsRemaining` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of tactical timeouts remaining.

`MatchesWonThisSeries` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of maps won in the current series.

### **Stats(Stats)**

```csharp
protected Stats(Stats original)
```

#### Parameters

`original` [Stats](./strikelink/gsi/objectstates/stats.md)<br>

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

### **Equals(Stats)**

```csharp
public bool Equals(Stats other)
```

#### Parameters

`other` [Stats](./strikelink/gsi/objectstates/stats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Stats <Clone>$()
```

#### Returns

[Stats](./strikelink/gsi/objectstates/stats.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Int32&)**

```csharp
public void Deconstruct(Int32& Score, Int32& ConsecutiveLosses, Int32& TimeoutsRemaining, Int32& MatchesWonThisSeries)
```

#### Parameters

`Score` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`ConsecutiveLosses` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TimeoutsRemaining` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MatchesWonThisSeries` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
