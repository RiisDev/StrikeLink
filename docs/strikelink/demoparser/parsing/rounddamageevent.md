# RoundDamageEvent

Namespace: StrikeLink.DemoParser.Parsing

Represents a damage event that occurred during a round, including information about the attacker, victim, weapon
 used, and damage details.

```csharp
public sealed record class RoundDamageEvent : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundDamageEvent, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [RoundDamageEvent](./strikelink/demoparser/parsing/rounddamageevent.md)<br>
Implements [IEquatable&lt;RoundDamageEvent&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

The game tick at which the damage event occurred.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AttackerSteamId**

The Steam ID of the attacker, or null if the attacker is not identified.

```csharp
public ulong? AttackerSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AttackerName**

The display name of the attacker, or null if the attacker is not identified.

```csharp
public string? AttackerName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **VictimSteamId**

The Steam ID of the victim, or null if the victim is not identified.

```csharp
public ulong? VictimSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VictimName**

The display name of the victim, or null if the victim is not identified.

```csharp
public string? VictimName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Weapon**

The name of the weapon used to inflict the damage.

```csharp
public string Weapon { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Damage**

The total amount of damage dealt in the event.

```csharp
public int Damage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HealthDamage**

The amount of health damage inflicted on the victim.

```csharp
public int HealthDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ArmorDamage**

The amount of armor damage inflicted on the victim.

```csharp
public int ArmorDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsFriendlyFire**

true if the damage was caused by a teammate; otherwise, false.

```csharp
public bool IsFriendlyFire { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AttackerPosition**

The attacker's position at the hit, or null when unavailable.

```csharp
public PlayerPosition? AttackerPosition { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

### **VictimPosition**

The victim's position at the hit, or null when unavailable.

```csharp
public PlayerPosition? VictimPosition { get; init; }
```

#### Property Value

[PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

## Constructors

### **RoundDamageEvent(Int32, UInt64?, String, UInt64?, String, String, Int32, Int32, Int32, Boolean, PlayerPosition, PlayerPosition)**

Represents a damage event that occurred during a round, including information about the attacker, victim, weapon
 used, and damage details.

```csharp
public RoundDamageEvent(int Tick, ulong? AttackerSteamId, string? AttackerName, ulong? VictimSteamId, string? VictimName, string Weapon, int Damage, int HealthDamage, int ArmorDamage, bool IsFriendlyFire, PlayerPosition? AttackerPosition = null, PlayerPosition? VictimPosition = null)
```

#### Parameters

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The game tick at which the damage event occurred.

`AttackerSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the attacker, or null if the attacker is not identified.

`AttackerName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the attacker, or null if the attacker is not identified.

`VictimSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the victim, or null if the victim is not identified.

`VictimName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the victim, or null if the victim is not identified.

`Weapon` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the weapon used to inflict the damage.

`Damage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt in the event.

`HealthDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The amount of health damage inflicted on the victim.

`ArmorDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The amount of armor damage inflicted on the victim.

`IsFriendlyFire` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the damage was caused by a teammate; otherwise, false.

`AttackerPosition` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)?<br>
The attacker's position at the hit, or null when unavailable.

`VictimPosition` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)?<br>
The victim's position at the hit, or null when unavailable.
