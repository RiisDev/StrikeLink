# MapState

Namespace: StrikeLink.GSI.ObjectStates

Represents the current state of the map as reported by GSI.

```csharp
public class MapState : StrikeLink.GSI.IGsiPayload, System.IEquatable`1[[StrikeLink.GSI.ObjectStates.MapState, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MapState](./strikelink/gsi/objectstates/mapstate.md)<br>
Implements [IGsiPayload](./strikelink/gsi/igsipayload.md), [IEquatable&lt;MapState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Mode**

The current game mode. [MapMode](./strikelink/gsi/objectstates/mapmode.md)

```csharp
public MapMode Mode { get; set; }
```

#### Property Value

[MapMode](./strikelink/gsi/objectstates/mapmode.md)<br>

### **Name**

The internal map name.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Phase**

The current phase of the match. [MapPhase](./strikelink/gsi/objectstates/mapphase.md)

```csharp
public MapPhase Phase { get; set; }
```

#### Property Value

[MapPhase](./strikelink/gsi/objectstates/mapphase.md)<br>

### **Round**

The current round number.

```csharp
public int Round { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CounterTerroristStats**

Statistics for the Counter-Terrorist team. [Stats](./strikelink/gsi/objectstates/stats.md)

```csharp
public Stats CounterTerroristStats { get; set; }
```

#### Property Value

[Stats](./strikelink/gsi/objectstates/stats.md)<br>

### **TerroristsStats**

Statistics for the Terrorist team. [Stats](./strikelink/gsi/objectstates/stats.md)

```csharp
public Stats TerroristsStats { get; set; }
```

#### Property Value

[Stats](./strikelink/gsi/objectstates/stats.md)<br>

### **MatchesToWinSeries**

The number of matches required to win the series.

```csharp
public int MatchesToWinSeries { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RoundWins**

A mapping of round numbers to their respective win conditions. [WinState](./strikelink/gsi/objectstates/winstate.md)

```csharp
public Dictionary<int, WinState> RoundWins { get; set; }
```

#### Property Value

[Dictionary&lt;Int32, WinState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Constructors

### **MapState(MapMode, String, MapPhase, Int32, Stats, Stats, Int32, Dictionary&lt;Int32, WinState&gt;)**

Represents the current state of the map as reported by GSI.

```csharp
public MapState(MapMode Mode, string Name, MapPhase Phase, int Round, Stats CounterTerroristStats, Stats TerroristsStats, int MatchesToWinSeries, Dictionary<int, WinState> RoundWins)
```

#### Parameters

`Mode` [MapMode](./strikelink/gsi/objectstates/mapmode.md)<br>
The current game mode. [MapMode](./strikelink/gsi/objectstates/mapmode.md)

`Name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The internal map name.

`Phase` [MapPhase](./strikelink/gsi/objectstates/mapphase.md)<br>
The current phase of the match. [MapPhase](./strikelink/gsi/objectstates/mapphase.md)

`Round` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The current round number.

`CounterTerroristStats` [Stats](./strikelink/gsi/objectstates/stats.md)<br>
Statistics for the Counter-Terrorist team. [Stats](./strikelink/gsi/objectstates/stats.md)

`TerroristsStats` [Stats](./strikelink/gsi/objectstates/stats.md)<br>
Statistics for the Terrorist team. [Stats](./strikelink/gsi/objectstates/stats.md)

`MatchesToWinSeries` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of matches required to win the series.

`RoundWins` [Dictionary&lt;Int32, WinState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A mapping of round numbers to their respective win conditions. [WinState](./strikelink/gsi/objectstates/winstate.md)

### **MapState(MapState)**

```csharp
protected MapState(MapState original)
```

#### Parameters

`original` [MapState](./strikelink/gsi/objectstates/mapstate.md)<br>

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

### **Equals(MapState)**

```csharp
public bool Equals(MapState other)
```

#### Parameters

`other` [MapState](./strikelink/gsi/objectstates/mapstate.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public MapState <Clone>$()
```

#### Returns

[MapState](./strikelink/gsi/objectstates/mapstate.md)<br>

### **Deconstruct(MapMode&, String&, MapPhase&, Int32&, Stats&, Stats&, Int32&, Dictionary`2&)**

```csharp
public void Deconstruct(MapMode& Mode, String& Name, MapPhase& Phase, Int32& Round, Stats& CounterTerroristStats, Stats& TerroristsStats, Int32& MatchesToWinSeries, Dictionary`2& RoundWins)
```

#### Parameters

`Mode` [MapMode&](./strikelink/gsi/objectstates/mapmode&.md)<br>

`Name` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Phase` [MapPhase&](./strikelink/gsi/objectstates/mapphase&.md)<br>

`Round` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`CounterTerroristStats` [Stats&](./strikelink/gsi/objectstates/stats&.md)<br>

`TerroristsStats` [Stats&](./strikelink/gsi/objectstates/stats&.md)<br>

`MatchesToWinSeries` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`RoundWins` [Dictionary`2&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2&)<br>
