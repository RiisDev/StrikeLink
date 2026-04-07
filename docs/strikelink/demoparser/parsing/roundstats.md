# RoundStats

Namespace: StrikeLink.DemoParser.Parsing

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public sealed class RoundStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundStats, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>
Implements [IEquatable&lt;RoundStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

The zero-based index of the round within the match. Must be non-negative.

```csharp
public int RoundNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Duration**

The total elapsed time of the round, or null if the duration is not available.

```csharp
public Nullable<TimeSpan> Duration { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Winner**

The team that won the round, or null if the round did not have a winner.

```csharp
public Nullable<CsTeamSide> Winner { get; set; }
```

#### Property Value

[Nullable&lt;CsTeamSide&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Kills**

A read-only list of all kill events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundKillEvent> Kills { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Damage**

A read-only list of all damage events that occurred during the round. Never null.

```csharp
public IReadOnlyList<RoundDamageEvent> Damage { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **RoundStats(Int32, Nullable&lt;TimeSpan&gt;, Nullable&lt;CsTeamSide&gt;, IReadOnlyList&lt;RoundKillEvent&gt;, IReadOnlyList&lt;RoundDamageEvent&gt;)**

Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
 events.

```csharp
public RoundStats(int RoundNumber, Nullable<TimeSpan> Duration, Nullable<CsTeamSide> Winner, IReadOnlyList<RoundKillEvent> Kills, IReadOnlyList<RoundDamageEvent> Damage)
```

#### Parameters

`RoundNumber` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The zero-based index of the round within the match. Must be non-negative.

`Duration` [Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The total elapsed time of the round, or null if the duration is not available.

`Winner` [Nullable&lt;CsTeamSide&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The team that won the round, or null if the round did not have a winner.

`Kills` [IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all kill events that occurred during the round. Never null.

`Damage` [IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of all damage events that occurred during the round. Never null.

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

### **Equals(RoundStats)**

```csharp
public bool Equals(RoundStats other)
```

#### Parameters

`other` [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundStats <Clone>$()
```

#### Returns

[RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>

### **Deconstruct(Int32&, Nullable`1&, Nullable`1&, IReadOnlyList`1&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Int32& RoundNumber, Nullable`1& Duration, Nullable`1& Winner, IReadOnlyList`1& Kills, IReadOnlyList`1& Damage)
```

#### Parameters

`RoundNumber` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Duration` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Winner` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Kills` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Damage` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
