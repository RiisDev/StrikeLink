# RoundState

Namespace: StrikeLink.GSI.ObjectStates

Represents the current state of a round as reported by GSI.

```csharp
public class RoundState : StrikeLink.GSI.IGsiPayload, System.IEquatable`1[[StrikeLink.GSI.ObjectStates.RoundState, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundState](./strikelink/gsi/objectstates/roundstate.md)<br>
Implements [IGsiPayload](./strikelink/gsi/igsipayload.md), [IEquatable&lt;RoundState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Phase**

The current phase of the round. [RoundPhase](./strikelink/gsi/objectstates/roundphase.md)

```csharp
public RoundPhase Phase { get; set; }
```

#### Property Value

[RoundPhase](./strikelink/gsi/objectstates/roundphase.md)<br>

### **WinTeam**

The team that won the round, if the round has ended; otherwise `null`. [Team](./strikelink/gsi/objectstates/team.md)

```csharp
public Nullable<Team> WinTeam { get; set; }
```

#### Property Value

[Nullable&lt;Team&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **BombState**

The current state of the bomb, if applicable; otherwise `null`. [RoundState.BombState](./strikelink/gsi/objectstates/roundstate.md#bombstate)

```csharp
public Nullable<BombState> BombState { get; set; }
```

#### Property Value

[Nullable&lt;BombState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **RoundState(RoundPhase, Nullable&lt;Team&gt;, Nullable&lt;BombState&gt;)**

Represents the current state of a round as reported by GSI.

```csharp
public RoundState(RoundPhase Phase, Nullable<Team> WinTeam, Nullable<BombState> BombState)
```

#### Parameters

`Phase` [RoundPhase](./strikelink/gsi/objectstates/roundphase.md)<br>
The current phase of the round. [RoundPhase](./strikelink/gsi/objectstates/roundphase.md)

`WinTeam` [Nullable&lt;Team&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The team that won the round, if the round has ended; otherwise `null`. [Team](./strikelink/gsi/objectstates/team.md)

`BombState` [Nullable&lt;BombState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The current state of the bomb, if applicable; otherwise `null`. [RoundState.BombState](./strikelink/gsi/objectstates/roundstate.md#bombstate)

### **RoundState(RoundState)**

```csharp
protected RoundState(RoundState original)
```

#### Parameters

`original` [RoundState](./strikelink/gsi/objectstates/roundstate.md)<br>

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

### **Equals(RoundState)**

```csharp
public bool Equals(RoundState other)
```

#### Parameters

`other` [RoundState](./strikelink/gsi/objectstates/roundstate.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundState <Clone>$()
```

#### Returns

[RoundState](./strikelink/gsi/objectstates/roundstate.md)<br>

### **Deconstruct(RoundPhase&, Nullable`1&, Nullable`1&)**

```csharp
public void Deconstruct(RoundPhase& Phase, Nullable`1& WinTeam, Nullable`1& BombState)
```

#### Parameters

`Phase` [RoundPhase&](./strikelink/gsi/objectstates/roundphase&.md)<br>

`WinTeam` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`BombState` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
