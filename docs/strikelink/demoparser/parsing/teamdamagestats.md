# TeamDamageStats

Namespace: StrikeLink.DemoParser.Parsing

Represents aggregated team damage and team kill statistics for a match or round.

```csharp
public sealed record class TeamDamageStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.TeamDamageStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>
Implements [IEquatable&lt;TeamDamageStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Damage**

The total amount of damage dealt to teammates.

```csharp
public int Damage { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamKillsUtility**

The number of team kills caused by utility, such as grenades or other equipment.

```csharp
public int TeamKillsUtility { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamKillsOther**

The number of team kills caused by means other than utility.

```csharp
public int TeamKillsOther { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamFlashes**

The number of times teammates were blinded by a flash effect.

```csharp
public int TeamFlashes { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TeamDamageStats(Int32, Int32, Int32, Int32)**

Represents aggregated team damage and team kill statistics for a match or round.

```csharp
public TeamDamageStats(int Damage, int TeamKillsUtility, int TeamKillsOther, int TeamFlashes)
```

#### Parameters

`Damage` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total amount of damage dealt to teammates.

`TeamKillsUtility` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of team kills caused by utility, such as grenades or other equipment.

`TeamKillsOther` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of team kills caused by means other than utility.

`TeamFlashes` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of times teammates were blinded by a flash effect.
