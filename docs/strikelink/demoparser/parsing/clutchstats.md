# ClutchStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for clutch scenarios, including total attempts, wins, and a breakdown of wins by the
 number of opponents.

```csharp
public sealed class ClutchStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.ClutchStats, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>
Implements [IEquatable&lt;ClutchStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Attempts**

The total number of clutch attempts recorded.

```csharp
public int Attempts { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Wins**

The total number of successful clutch wins.

```csharp
public int Wins { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WinsByOpponentCount**

A read-only dictionary mapping the number of opponents faced to the number of wins achieved against that count.

```csharp
public IReadOnlyDictionary<int, int> WinsByOpponentCount { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;Int32, Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **ClutchStats(Int32, Int32, IReadOnlyDictionary&lt;Int32, Int32&gt;)**

Represents statistical data for clutch scenarios, including total attempts, wins, and a breakdown of wins by the
 number of opponents.

```csharp
public ClutchStats(int Attempts, int Wins, IReadOnlyDictionary<int, int> WinsByOpponentCount)
```

#### Parameters

`Attempts` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of clutch attempts recorded.

`Wins` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of successful clutch wins.

`WinsByOpponentCount` [IReadOnlyDictionary&lt;Int32, Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>
A read-only dictionary mapping the number of opponents faced to the number of wins achieved against that count.

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

### **Equals(ClutchStats)**

```csharp
public bool Equals(ClutchStats other)
```

#### Parameters

`other` [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public ClutchStats <Clone>$()
```

#### Returns

[ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>

### **Deconstruct(Int32&, Int32&, IReadOnlyDictionary`2&)**

```csharp
public void Deconstruct(Int32& Attempts, Int32& Wins, IReadOnlyDictionary`2& WinsByOpponentCount)
```

#### Parameters

`Attempts` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Wins` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`WinsByOpponentCount` [IReadOnlyDictionary`2&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2&)<br>
