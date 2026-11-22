# AimStats

Namespace: StrikeLink.DemoParser.Parsing

Aim measurements taken from shots and the shooter's movement.

```csharp
public sealed record class AimStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.AimStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [AimStats](./strikelink/demoparser/parsing/aimstats.md)<br>
Implements [IEquatable&lt;AimStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **CounterStrafeAttempts**

Gun shots fired right after moving fast (counter-strafe situations).

```csharp
public int CounterStrafeAttempts { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CounterStrafePercentage**

Share of those shots fired while nearly stationary (under 34 units per second).

```csharp
public double CounterStrafePercentage { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **AverageSpeedAtShot**

Average horizontal speed at the moment of a gun shot, in units per second.

```csharp
public double AverageSpeedAtShot { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **EngagementsWithDamage**

Bursts of fire that landed at least one hit.

```csharp
public int EngagementsWithDamage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AverageTimeToDamageMs**

Average time from the first shot of a burst to its first hit, in milliseconds.

```csharp
public double AverageTimeToDamageMs { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FirstBulletAccuracy**

Share of bursts whose first bullet hit, as a percentage.

```csharp
public double FirstBulletAccuracy { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **AimStats(Int32, Double, Double, Int32, Double, Double)**

Aim measurements taken from shots and the shooter's movement.

```csharp
public AimStats(int CounterStrafeAttempts, double CounterStrafePercentage, double AverageSpeedAtShot, int EngagementsWithDamage, double AverageTimeToDamageMs, double FirstBulletAccuracy)
```

#### Parameters

`CounterStrafeAttempts` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Gun shots fired right after moving fast (counter-strafe situations).

`CounterStrafePercentage` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Share of those shots fired while nearly stationary (under 34 units per second).

`AverageSpeedAtShot` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Average horizontal speed at the moment of a gun shot, in units per second.

`EngagementsWithDamage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Bursts of fire that landed at least one hit.

`AverageTimeToDamageMs` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Average time from the first shot of a burst to its first hit, in milliseconds.

`FirstBulletAccuracy` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
Share of bursts whose first bullet hit, as a percentage.
