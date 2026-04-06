# RoundDamageEvent

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class RoundDamageEvent : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.RoundDamageEvent, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RoundDamageEvent](./strikelink/demoparser/parsing/rounddamageevent.md)<br>
Implements [IEquatable&lt;RoundDamageEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Tick**

```csharp
public int Tick { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AttackerSteamId**

```csharp
public Nullable<ulong> AttackerSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AttackerName**

```csharp
public string AttackerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **VictimSteamId**

```csharp
public Nullable<ulong> VictimSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **VictimName**

```csharp
public string VictimName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Weapon**

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Damage**

```csharp
public int Damage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HealthDamage**

```csharp
public int HealthDamage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ArmorDamage**

```csharp
public int ArmorDamage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsFriendlyFire**

```csharp
public bool IsFriendlyFire { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **RoundDamageEvent(Int32, Nullable&lt;UInt64&gt;, String, Nullable&lt;UInt64&gt;, String, String, Int32, Int32, Int32, Boolean)**

```csharp
public RoundDamageEvent(int Tick, Nullable<ulong> AttackerSteamId, string AttackerName, Nullable<ulong> VictimSteamId, string VictimName, string Weapon, int Damage, int HealthDamage, int ArmorDamage, bool IsFriendlyFire)
```

#### Parameters

`Tick` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`AttackerSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`AttackerName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`VictimSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`VictimName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`Damage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`HealthDamage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`ArmorDamage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`IsFriendlyFire` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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

### **Equals(RoundDamageEvent)**

```csharp
public bool Equals(RoundDamageEvent other)
```

#### Parameters

`other` [RoundDamageEvent](./strikelink/demoparser/parsing/rounddamageevent.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public RoundDamageEvent <Clone>$()
```

#### Returns

[RoundDamageEvent](./strikelink/demoparser/parsing/rounddamageevent.md)<br>

### **Deconstruct(Int32&, Nullable`1&, String&, Nullable`1&, String&, String&, Int32&, Int32&, Int32&, Boolean&)**

```csharp
public void Deconstruct(Int32& Tick, Nullable`1& AttackerSteamId, String& AttackerName, Nullable`1& VictimSteamId, String& VictimName, String& Weapon, Int32& Damage, Int32& HealthDamage, Int32& ArmorDamage, Boolean& IsFriendlyFire)
```

#### Parameters

`Tick` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`AttackerSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`AttackerName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`VictimSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`VictimName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Weapon` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Damage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`HealthDamage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`ArmorDamage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`IsFriendlyFire` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
