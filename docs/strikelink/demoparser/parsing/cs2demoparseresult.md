# Cs2DemoParseResult

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class Cs2DemoParseResult : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.Cs2DemoParseResult, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md)<br>
Implements [IEquatable&lt;Cs2DemoParseResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Match**

```csharp
public MatchStats Match { get; set; }
```

#### Property Value

[MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>

### **Players**

```csharp
public IReadOnlyList<PlayerStats> Players { get; set; }
```

#### Property Value

[IReadOnlyList&lt;PlayerStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Rounds**

```csharp
public IReadOnlyList<RoundStats> Rounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Warnings**

```csharp
public IReadOnlyList<string> Warnings { get; set; }
```

#### Property Value

[IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **Cs2DemoParseResult(MatchStats, IReadOnlyList&lt;PlayerStats&gt;, IReadOnlyList&lt;RoundStats&gt;, IReadOnlyList&lt;String&gt;)**

```csharp
public Cs2DemoParseResult(MatchStats Match, IReadOnlyList<PlayerStats> Players, IReadOnlyList<RoundStats> Rounds, IReadOnlyList<string> Warnings)
```

#### Parameters

`Match` [MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>

`Players` [IReadOnlyList&lt;PlayerStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`Rounds` [IReadOnlyList&lt;RoundStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`Warnings` [IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

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

### **Equals(Cs2DemoParseResult)**

```csharp
public bool Equals(Cs2DemoParseResult other)
```

#### Parameters

`other` [Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Cs2DemoParseResult <Clone>$()
```

#### Returns

[Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md)<br>

### **Deconstruct(MatchStats&, IReadOnlyList`1&, IReadOnlyList`1&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(MatchStats& Match, IReadOnlyList`1& Players, IReadOnlyList`1& Rounds, IReadOnlyList`1& Warnings)
```

#### Parameters

`Match` [MatchStats&](./strikelink/demoparser/parsing/matchstats&.md)<br>

`Players` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Rounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Warnings` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
