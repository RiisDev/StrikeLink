# BombEvent

Namespace: StrikeLink.DemoParser.Parsing

A bomb plant, defuse or explosion.

```csharp
public sealed record class BombEvent : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.BombEvent, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [BombEvent](./strikelink/demoparser/parsing/bombevent.md)<br>
Implements [IEquatable&lt;BombEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Type**

What happened.

```csharp
public BombEventType Type { get; init; }
```

#### Property Value

[BombEventType](./strikelink/demoparser/parsing/bombeventtype.md)<br>

### **Tick**

Demo tick.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **SteamId**

The planter or defuser; null for an explosion.

```csharp
public ulong? SteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Position**

Where the acting player stood, or null when unavailable.

```csharp
public PlayerPosition? Position { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

## Constructors

### **BombEvent(BombEventType, Int32, UInt64?, PlayerPosition)**

A bomb plant, defuse or explosion.

```csharp
public BombEvent(BombEventType Type, int Tick, ulong? SteamId, PlayerPosition? Position)
```

#### Parameters

`Type` [BombEventType](./strikelink/demoparser/parsing/bombeventtype.md)<br>
What happened.

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Demo tick.

`SteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The planter or defuser; null for an explosion.

`Position` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)?<br>
Where the acting player stood, or null when unavailable.
