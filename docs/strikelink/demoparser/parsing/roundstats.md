# RoundStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class RoundStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundStats](./strikelink/demoparser/parsing/roundstats.md)<br>
Implements [IEquatable&lt;RoundStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RoundNumber**

```csharp
public int RoundNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Duration**

```csharp
public Nullable<TimeSpan> Duration { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Winner**

```csharp
public Nullable<CsTeamSide> Winner { get; set; }
```

#### Property Value

[Nullable&lt;CsTeamSide&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Kills**

```csharp
public IReadOnlyList<RoundKillEvent> Kills { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Damage**

```csharp
public IReadOnlyList<RoundDamageEvent> Damage { get; set; }
```

#### Property Value

[IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **PlayerEconomy**

```csharp
public IReadOnlyList<PlayerEconomySnapshot> PlayerEconomy { get; set; }
```

#### Property Value

[IReadOnlyList&lt;PlayerEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **TeamEconomy**

```csharp
public IReadOnlyList<TeamEconomySnapshot> TeamEconomy { get; set; }
```

#### Property Value

[IReadOnlyList&lt;TeamEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **RoundStats(Int32, Nullable&lt;TimeSpan&gt;, Nullable&lt;CsTeamSide&gt;, IReadOnlyList&lt;RoundKillEvent&gt;, IReadOnlyList&lt;RoundDamageEvent&gt;, IReadOnlyList&lt;PlayerEconomySnapshot&gt;, IReadOnlyList&lt;TeamEconomySnapshot&gt;)**

```csharp
public RoundStats(int RoundNumber, Nullable<TimeSpan> Duration, Nullable<CsTeamSide> Winner, IReadOnlyList<RoundKillEvent> Kills, IReadOnlyList<RoundDamageEvent> Damage, IReadOnlyList<PlayerEconomySnapshot> PlayerEconomy, IReadOnlyList<TeamEconomySnapshot> TeamEconomy)
```

#### Parameters

`RoundNumber` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Duration` [Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`Winner` [Nullable&lt;CsTeamSide&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`Kills` [IReadOnlyList&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`Damage` [IReadOnlyList&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`PlayerEconomy` [IReadOnlyList&lt;PlayerEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`TeamEconomy` [IReadOnlyList&lt;TeamEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

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

### **Deconstruct(Int32&, Nullable`1&, Nullable`1&, IReadOnlyList`1&, IReadOnlyList`1&, IReadOnlyList`1&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Int32& RoundNumber, Nullable`1& Duration, Nullable`1& Winner, IReadOnlyList`1& Kills, IReadOnlyList`1& Damage, IReadOnlyList`1& PlayerEconomy, IReadOnlyList`1& TeamEconomy)
```

#### Parameters

`RoundNumber` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Duration` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Winner` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Kills` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Damage` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`PlayerEconomy` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`TeamEconomy` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
