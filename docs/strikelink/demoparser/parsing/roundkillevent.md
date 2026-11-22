# RoundKillEvent

Namespace: StrikeLink.DemoParser.Parsing

Represents a kill event that occurred during a round, including details about the participants, weapon used, and
 contextual information about the kill.

```csharp
public sealed record class RoundKillEvent : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundKillEvent, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [RoundKillEvent](./strikelink/demoparser/parsing/roundkillevent.md)<br>
Implements [IEquatable&lt;RoundKillEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

The game tick at which the kill event occurred.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **KillerSteamId**

The Steam ID of the player who performed the kill, or null if not available.

```csharp
public ulong? KillerSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **KillerName**

The display name of the player who performed the kill, or null if not available.

```csharp
public string? KillerName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **VictimSteamId**

The Steam ID of the player who was killed, or null if not available.

```csharp
public ulong? VictimSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VictimName**

The display name of the player who was killed, or null if not available.

```csharp
public string? VictimName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AssisterSteamId**

The Steam ID of the player who assisted in the kill, or null if there was no assister.

```csharp
public ulong? AssisterSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AssisterName**

The display name of the player who assisted in the kill, or null if there was no assister.

```csharp
public string? AssisterName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Weapon**

The name of the weapon used to perform the kill.

```csharp
public string Weapon { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IsHeadshot**

true if the kill was a headshot; otherwise, false.

```csharp
public bool IsHeadshot { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsTeamKill**

true if the kill was a team kill; otherwise, false.

```csharp
public bool IsTeamKill { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsTrade**

true if the kill was a trade (i.e., occurred shortly after the killer's teammate was killed by the victim);
 otherwise, false.

```csharp
public bool IsTrade { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsWallBang**

true if the kill was performed through a wall or other penetrable surface; otherwise, false.

```csharp
public bool IsWallBang { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ThroughSmoke**

true if the kill occurred through smoke; otherwise, false.

```csharp
public bool ThroughSmoke { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AttackerBlind**

true if the attacker was blinded at the time of the kill; otherwise, false.

```csharp
public bool AttackerBlind { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **KillerPosition**

The killer's position at the kill, or null when unavailable.

```csharp
public PlayerPosition? KillerPosition { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

### **VictimPosition**

The victim's position at the kill, or null when unavailable.

```csharp
public PlayerPosition? VictimPosition { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

## Constructors

### **RoundKillEvent(Int32, UInt64?, String, UInt64?, String, UInt64?, String, String, Boolean, Boolean, Boolean, Boolean, Boolean, Boolean, PlayerPosition, PlayerPosition)**

Represents a kill event that occurred during a round, including details about the participants, weapon used, and
 contextual information about the kill.

```csharp
public RoundKillEvent(int Tick, ulong? KillerSteamId, string? KillerName, ulong? VictimSteamId, string? VictimName, ulong? AssisterSteamId, string? AssisterName, string Weapon, bool IsHeadshot, bool IsTeamKill, bool IsTrade, bool IsWallBang, bool ThroughSmoke, bool AttackerBlind, PlayerPosition? KillerPosition = null, PlayerPosition? VictimPosition = null)
```

#### Parameters

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The game tick at which the kill event occurred.

`KillerSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who performed the kill, or null if not available.

`KillerName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the player who performed the kill, or null if not available.

`VictimSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who was killed, or null if not available.

`VictimName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the player who was killed, or null if not available.

`AssisterSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who assisted in the kill, or null if there was no assister.

`AssisterName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the player who assisted in the kill, or null if there was no assister.

`Weapon` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the weapon used to perform the kill.

`IsHeadshot` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a headshot; otherwise, false.

`IsTeamKill` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a team kill; otherwise, false.

`IsTrade` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a trade (i.e., occurred shortly after the killer's teammate was killed by the victim);
 otherwise, false.

`IsWallBang` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was performed through a wall or other penetrable surface; otherwise, false.

`ThroughSmoke` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill occurred through smoke; otherwise, false.

`AttackerBlind` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the attacker was blinded at the time of the kill; otherwise, false.

`KillerPosition` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)?<br>
The killer's position at the kill, or null when unavailable.

`VictimPosition` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)?<br>
The victim's position at the kill, or null when unavailable.
