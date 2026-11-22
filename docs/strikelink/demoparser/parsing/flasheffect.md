# FlashEffect

Namespace: StrikeLink.DemoParser.Parsing

One player being flashed.

```csharp
public sealed record class FlashEffect : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.FlashEffect, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [FlashEffect](./strikelink/demoparser/parsing/flasheffect.md)<br>
Implements [IEquatable&lt;FlashEffect&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

Tick of the flash.

```csharp
public int Tick { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ThrowerSteamId**

Steam ID of the thrower.

```csharp
public ulong? ThrowerSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VictimSteamId**

Steam ID of the blinded player.

```csharp
public ulong? VictimSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **DurationSeconds**

How long the player was blinded.

```csharp
public double DurationSeconds { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **IsTeamFlash**

true when thrower and victim are on the same team.

```csharp
public bool IsTeamFlash { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **FlashEffect(Int32, UInt64?, UInt64?, Double, Boolean)**

One player being flashed.

```csharp
public FlashEffect(int Tick, ulong? ThrowerSteamId, ulong? VictimSteamId, double DurationSeconds, bool IsTeamFlash)
```

#### Parameters

`Tick` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Tick of the flash.

`ThrowerSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Steam ID of the thrower.

`VictimSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Steam ID of the blinded player.

`DurationSeconds` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
How long the player was blinded.

`IsTeamFlash` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true when thrower and victim are on the same team.
