# TeamDamageStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class TeamDamageStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.TeamDamageStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>
Implements [IEquatable&lt;TeamDamageStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Damage**

```csharp
public int Damage { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamKillsUtility**

```csharp
public int TeamKillsUtility { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamKillsOther**

```csharp
public int TeamKillsOther { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamFlashes**

```csharp
public int TeamFlashes { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TeamDamageStats(Int32, Int32, Int32, Int32)**

```csharp
public TeamDamageStats(int Damage, int TeamKillsUtility, int TeamKillsOther, int TeamFlashes)
```

#### Parameters

`Damage` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`TeamKillsUtility` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`TeamKillsOther` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`TeamFlashes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **Equals(TeamDamageStats)**

```csharp
public bool Equals(TeamDamageStats other)
```

#### Parameters

`other` [TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public TeamDamageStats <Clone>$()
```

#### Returns

[TeamDamageStats](./strikelink/demoparser/parsing/teamdamagestats.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Int32&)**

```csharp
public void Deconstruct(Int32& Damage, Int32& TeamKillsUtility, Int32& TeamKillsOther, Int32& TeamFlashes)
```

#### Parameters

`Damage` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TeamKillsUtility` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TeamKillsOther` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TeamFlashes` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
