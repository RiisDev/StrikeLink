# RankSnapshot

Namespace: StrikeLink.DemoParser.Parsing

Represents a snapshot of a player's rank and related statistics at a specific point in time.

```csharp
public sealed record class RankSnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RankSnapshot, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>
Implements [IEquatable&lt;RankSnapshot&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RankBefore**

The player's previous rank, or null if not available.

```csharp
public int? RankBefore { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Rank**

The player's current rank, or null if not available.

```csharp
public int? Rank { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankChange**

The change in rank since the previous snapshot. A positive value indicates an increase in rank; a negative value
 indicates a decrease. Null if not available.

```csharp
public double? RankChange { get; init; }
```

#### Property Value

[Double?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Wins**

The number of wins associated with this snapshot, or null if not available.

```csharp
public int? Wins { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankTypeId**

The identifier for the type of rank, or null if not specified.

```csharp
public int? RankTypeId { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VisibleSkill**

The visible skill value associated with the player at the time of the snapshot, or null if not available.

```csharp
public int? VisibleSkill { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankBeforeName**

Gets the display name of the rank that precedes the current rank.

```csharp
public string RankBeforeName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RankName**

Gets the display name of the current rank.

```csharp
public string RankName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **RankSnapshot(Int32?, Int32?, Double?, Int32?, Int32?, Int32?)**

Represents a snapshot of a player's rank and related statistics at a specific point in time.

```csharp
public RankSnapshot(int? RankBefore, int? Rank, double? RankChange, int? Wins, int? RankTypeId, int? VisibleSkill)
```

#### Parameters

`RankBefore` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The player's previous rank, or null if not available.

`Rank` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The player's current rank, or null if not available.

`RankChange` [Double?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The change in rank since the previous snapshot. A positive value indicates an increase in rank; a negative value
 indicates a decrease. Null if not available.

`Wins` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The number of wins associated with this snapshot, or null if not available.

`RankTypeId` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The identifier for the type of rank, or null if not specified.

`VisibleSkill` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The visible skill value associated with the player at the time of the snapshot, or null if not available.
