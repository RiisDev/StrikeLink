# RankSnapshot

Namespace: StrikeLink.DemoParser.Parsing

Represents a snapshot of a player's rank and related statistics at a specific point in time.

```csharp
public sealed class RankSnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RankSnapshot, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>
Implements [IEquatable&lt;RankSnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **RankBefore**

The player's previous rank, or null if not available.

```csharp
public Nullable<int> RankBefore { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Rank**

The player's current rank, or null if not available.

```csharp
public Nullable<int> Rank { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankChange**

The change in rank since the previous snapshot. A positive value indicates an increase in rank; a negative value
 indicates a decrease. Null if not available.

```csharp
public Nullable<double> RankChange { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Wins**

The number of wins associated with this snapshot, or null if not available.

```csharp
public Nullable<int> Wins { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankTypeId**

The identifier for the type of rank, or null if not specified.

```csharp
public Nullable<int> RankTypeId { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VisibleSkill**

The visible skill value associated with the player at the time of the snapshot, or null if not available.

```csharp
public Nullable<int> VisibleSkill { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RankBeforeName**

Gets the display name of the rank that precedes the current rank.

```csharp
public string RankBeforeName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RankName**

Gets the display name of the current rank.

```csharp
public string RankName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **RankSnapshot(Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;, Nullable&lt;Double&gt;, Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;)**

Represents a snapshot of a player's rank and related statistics at a specific point in time.

```csharp
public RankSnapshot(Nullable<int> RankBefore, Nullable<int> Rank, Nullable<double> RankChange, Nullable<int> Wins, Nullable<int> RankTypeId, Nullable<int> VisibleSkill)
```

#### Parameters

`RankBefore` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The player's previous rank, or null if not available.

`Rank` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The player's current rank, or null if not available.

`RankChange` [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The change in rank since the previous snapshot. A positive value indicates an increase in rank; a negative value
 indicates a decrease. Null if not available.

`Wins` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The number of wins associated with this snapshot, or null if not available.

`RankTypeId` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The identifier for the type of rank, or null if not specified.

`VisibleSkill` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The visible skill value associated with the player at the time of the snapshot, or null if not available.

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

### **Equals(RankSnapshot)**

```csharp
public bool Equals(RankSnapshot other)
```

#### Parameters

`other` [RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RankSnapshot <Clone>$()
```

#### Returns

[RankSnapshot](./strikelink/demoparser/parsing/ranksnapshot.md)<br>

### **Deconstruct(Nullable`1&, Nullable`1&, Nullable`1&, Nullable`1&, Nullable`1&, Nullable`1&)**

```csharp
public void Deconstruct(Nullable`1& RankBefore, Nullable`1& Rank, Nullable`1& RankChange, Nullable`1& Wins, Nullable`1& RankTypeId, Nullable`1& VisibleSkill)
```

#### Parameters

`RankBefore` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Rank` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`RankChange` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Wins` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`RankTypeId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`VisibleSkill` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
