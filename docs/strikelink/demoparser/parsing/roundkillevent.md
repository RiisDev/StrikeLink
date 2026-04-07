# RoundKillEvent

Namespace: StrikeLink.DemoParser.Parsing

Represents a kill event that occurred during a round, including details about the participants, weapon used, and
 contextual information about the kill.

```csharp
public sealed class RoundKillEvent : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundKillEvent, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundKillEvent](./strikelink/demoparser/parsing/roundkillevent.md)<br>
Implements [IEquatable&lt;RoundKillEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

The game tick at which the kill event occurred.

```csharp
public int Tick { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **KillerSteamId**

The Steam ID of the player who performed the kill, or null if not available.

```csharp
public Nullable<ulong> KillerSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **KillerName**

The display name of the player who performed the kill, or null if not available.

```csharp
public string KillerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **VictimSteamId**

The Steam ID of the player who was killed, or null if not available.

```csharp
public Nullable<ulong> VictimSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VictimName**

The display name of the player who was killed, or null if not available.

```csharp
public string VictimName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AssisterSteamId**

The Steam ID of the player who assisted in the kill, or null if there was no assister.

```csharp
public Nullable<ulong> AssisterSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AssisterName**

The display name of the player who assisted in the kill, or null if there was no assister.

```csharp
public string AssisterName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Weapon**

The name of the weapon used to perform the kill.

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IsHeadshot**

true if the kill was a headshot; otherwise, false.

```csharp
public bool IsHeadshot { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsTeamKill**

true if the kill was a team kill; otherwise, false.

```csharp
public bool IsTeamKill { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsTrade**

true if the kill was a trade (i.e., occurred shortly after the killer's teammate was killed by the victim);
 otherwise, false.

```csharp
public bool IsTrade { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsWallBang**

true if the kill was performed through a wall or other penetrable surface; otherwise, false.

```csharp
public bool IsWallBang { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ThroughSmoke**

true if the kill occurred through smoke; otherwise, false.

```csharp
public bool ThroughSmoke { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AttackerBlind**

true if the attacker was blinded at the time of the kill; otherwise, false.

```csharp
public bool AttackerBlind { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **RoundKillEvent(Int32, Nullable&lt;UInt64&gt;, String, Nullable&lt;UInt64&gt;, String, Nullable&lt;UInt64&gt;, String, String, Boolean, Boolean, Boolean, Boolean, Boolean, Boolean)**

Represents a kill event that occurred during a round, including details about the participants, weapon used, and
 contextual information about the kill.

```csharp
public RoundKillEvent(int Tick, Nullable<ulong> KillerSteamId, string KillerName, Nullable<ulong> VictimSteamId, string VictimName, Nullable<ulong> AssisterSteamId, string AssisterName, string Weapon, bool IsHeadshot, bool IsTeamKill, bool IsTrade, bool IsWallBang, bool ThroughSmoke, bool AttackerBlind)
```

#### Parameters

`Tick` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The game tick at which the kill event occurred.

`KillerSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who performed the kill, or null if not available.

`KillerName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player who performed the kill, or null if not available.

`VictimSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who was killed, or null if not available.

`VictimName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player who was killed, or null if not available.

`AssisterSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player who assisted in the kill, or null if there was no assister.

`AssisterName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player who assisted in the kill, or null if there was no assister.

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the weapon used to perform the kill.

`IsHeadshot` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a headshot; otherwise, false.

`IsTeamKill` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a team kill; otherwise, false.

`IsTrade` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was a trade (i.e., occurred shortly after the killer's teammate was killed by the victim);
 otherwise, false.

`IsWallBang` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill was performed through a wall or other penetrable surface; otherwise, false.

`ThroughSmoke` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the kill occurred through smoke; otherwise, false.

`AttackerBlind` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the attacker was blinded at the time of the kill; otherwise, false.

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

### **Equals(RoundKillEvent)**

```csharp
public bool Equals(RoundKillEvent other)
```

#### Parameters

`other` [RoundKillEvent](./strikelink/demoparser/parsing/roundkillevent.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundKillEvent <Clone>$()
```

#### Returns

[RoundKillEvent](./strikelink/demoparser/parsing/roundkillevent.md)<br>

### **Deconstruct(Int32&, Nullable`1&, String&, Nullable`1&, String&, Nullable`1&, String&, String&, Boolean&, Boolean&, Boolean&, Boolean&, Boolean&, Boolean&)**

```csharp
public void Deconstruct(Int32& Tick, Nullable`1& KillerSteamId, String& KillerName, Nullable`1& VictimSteamId, String& VictimName, Nullable`1& AssisterSteamId, String& AssisterName, String& Weapon, Boolean& IsHeadshot, Boolean& IsTeamKill, Boolean& IsTrade, Boolean& IsWallBang, Boolean& ThroughSmoke, Boolean& AttackerBlind)
```

#### Parameters

`Tick` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`KillerSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`KillerName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`VictimSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`VictimName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`AssisterSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`AssisterName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Weapon` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`IsHeadshot` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`IsTeamKill` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`IsTrade` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`IsWallBang` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`ThroughSmoke` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`AttackerBlind` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
