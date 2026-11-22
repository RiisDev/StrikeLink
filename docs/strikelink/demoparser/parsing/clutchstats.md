# ClutchStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for clutch scenarios, including total attempts, wins, and a breakdown of wins by the
 number of opponents.

```csharp
public sealed record class ClutchStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.ClutchStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ClutchStats](./strikelink/demoparser/parsing/clutchstats.md)<br>
Implements [IEquatable&lt;ClutchStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Attempts**

The total number of clutch attempts recorded.

```csharp
public int Attempts { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Wins**

The total number of successful clutch wins.

```csharp
public int Wins { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WinsByOpponentCount**

A read-only dictionary mapping the number of opponents faced to the number of wins achieved against that count.

```csharp
public IReadOnlyDictionary<int, int> WinsByOpponentCount { get; init; }
```

#### Property Value

[IReadOnlyDictionary&lt;Int32, Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **ClutchStats(Int32, Int32, IReadOnlyDictionary&lt;Int32, Int32&gt;)**

Represents statistical data for clutch scenarios, including total attempts, wins, and a breakdown of wins by the
 number of opponents.

```csharp
public ClutchStats(int Attempts, int Wins, IReadOnlyDictionary<int, int> WinsByOpponentCount)
```

#### Parameters

`Attempts` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of clutch attempts recorded.

`Wins` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of successful clutch wins.

`WinsByOpponentCount` [IReadOnlyDictionary&lt;Int32, Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>
A read-only dictionary mapping the number of opponents faced to the number of wins achieved against that count.
