# Cs2DemoParseResult

Namespace: StrikeLink.DemoParser.Parsing

Represents the result of parsing a Counter-Strike 2 demo, including match statistics, player statistics, round
 details, and any warnings encountered during parsing.

```csharp
public sealed record class Cs2DemoParseResult : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.Cs2DemoParseResult, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoParseResult](./strikelink/demoparser/parsing/cs2demoparseresult.md)<br>
Implements [IEquatable&lt;Cs2DemoParseResult&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This record aggregates all relevant data extracted from a demo file. Consumers should inspect the
 Warnings property to determine if any non-fatal issues occurred during parsing.

## Properties

### **Match**

The overall statistics for the parsed match.

```csharp
public MatchStats Match { get; init; }
```

#### Property Value

[MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>

### **Players**

A read-only list containing statistics for each player in the match.

```csharp
public IReadOnlyList<PlayerStats> Players { get; init; }
```

#### Property Value

[IReadOnlyList&lt;PlayerStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Rounds**

A read-only list containing statistics for each round in the match.

```csharp
public IReadOnlyList<RoundStats> Rounds { get; init; }
```

#### Property Value

[IReadOnlyList&lt;RoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Warnings**

A read-only list of warning messages generated during parsing. The list will be empty if no warnings were
 encountered.

```csharp
public IReadOnlyList<string> Warnings { get; init; }
```

#### Property Value

[IReadOnlyList&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ControllerSchema**

Every controller property this demo's game build declares (see [ControllerPropertySchema](./strikelink/demoparser/parsing/controllerpropertyschema.md)); empty when the demo has no entity state.
 Compare it across builds to see which raw names changed.

```csharp
public IReadOnlyList<ControllerPropertySchema> ControllerSchema { get; init; }
```

#### Property Value

[IReadOnlyList&lt;ControllerPropertySchema&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **Cs2DemoParseResult(MatchStats, IReadOnlyList&lt;PlayerStats&gt;, IReadOnlyList&lt;RoundStats&gt;, IReadOnlyList&lt;String&gt;, IReadOnlyList&lt;ControllerPropertySchema&gt;)**

Represents the result of parsing a Counter-Strike 2 demo, including match statistics, player statistics, round
 details, and any warnings encountered during parsing.

```csharp
public Cs2DemoParseResult(MatchStats Match, IReadOnlyList<PlayerStats> Players, IReadOnlyList<RoundStats> Rounds, IReadOnlyList<string> Warnings, IReadOnlyList<ControllerPropertySchema> ControllerSchema)
```

#### Parameters

`Match` [MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>
The overall statistics for the parsed match.

`Players` [IReadOnlyList&lt;PlayerStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing statistics for each player in the match.

`Rounds` [IReadOnlyList&lt;RoundStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing statistics for each round in the match.

`Warnings` [IReadOnlyList&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of warning messages generated during parsing. The list will be empty if no warnings were
 encountered.

`ControllerSchema` [IReadOnlyList&lt;ControllerPropertySchema&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
Every controller property this demo's game build declares (see [ControllerPropertySchema](./strikelink/demoparser/parsing/controllerpropertyschema.md)); empty when the demo has no entity state.
 Compare it across builds to see which raw names changed.

**Remarks:**

This record aggregates all relevant data extracted from a demo file. Consumers should inspect the
 Warnings property to determine if any non-fatal issues occurred during parsing.
