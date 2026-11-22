# PlayerFrame

Namespace: StrikeLink.DemoParser.Parsing

One sampled moment of one player: where they were, where they looked, and their health.

```csharp
public sealed record class PlayerFrame : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerFrame, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [PlayerFrame](./strikelink/demoparser/parsing/playerframe.md)<br>
Implements [IEquatable&lt;PlayerFrame&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

Demo tick of the sample.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **SteamId**

The player's Steam ID.

```csharp
public ulong SteamId { get; init; }
```

#### Property Value

[UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Position**

World position.

```csharp
public PlayerPosition Position { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

### **Yaw**

Horizontal view angle in degrees.

```csharp
public float Yaw { get; init; }
```

#### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Pitch**

Vertical view angle in degrees.

```csharp
public float Pitch { get; init; }
```

#### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Health**

Health; 0 once dead.

```csharp
public int Health { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Armor**

Armor value.

```csharp
public int Armor { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Zone**

Coarse A, B or Mid region of the map, or none.

```csharp
public MapZone Zone { get; init; }
```

#### Property Value

[MapZone](./strikelink/demoparser/parsing/mapzone.md)<br>

## Constructors

### **PlayerFrame(Int32, UInt64, PlayerPosition, Single, Single, Int32, Int32, MapZone)**

One sampled moment of one player: where they were, where they looked, and their health.

```csharp
public PlayerFrame(int Tick, ulong SteamId, PlayerPosition Position, float Yaw, float Pitch, int Health, int Armor, MapZone Zone = MapZone.None)
```

#### Parameters

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Demo tick of the sample.

`SteamId` [UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)<br>
The player's Steam ID.

`Position` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>
World position.

`Yaw` [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>
Horizontal view angle in degrees.

`Pitch` [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>
Vertical view angle in degrees.

`Health` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Health; 0 once dead.

`Armor` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Armor value.

`Zone` [MapZone](./strikelink/demoparser/parsing/mapzone.md)<br>
Coarse A, B or Mid region of the map, or none.
