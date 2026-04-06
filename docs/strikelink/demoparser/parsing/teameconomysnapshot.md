# TeamEconomySnapshot

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class TeamEconomySnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.TeamEconomySnapshot, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TeamEconomySnapshot](./strikelink/demoparser/parsing/teameconomysnapshot.md)<br>
Implements [IEquatable&lt;TeamEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Team**

```csharp
public CsTeamSide Team { get; set; }
```

#### Property Value

[CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

### **Value**

```csharp
public Nullable<int> Value { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **TeamEconomySnapshot(CsTeamSide, Nullable&lt;Int32&gt;)**

```csharp
public TeamEconomySnapshot(CsTeamSide Team, Nullable<int> Value)
```

#### Parameters

`Team` [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>

`Value` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

### **Equals(TeamEconomySnapshot)**

```csharp
public bool Equals(TeamEconomySnapshot other)
```

#### Parameters

`other` [TeamEconomySnapshot](./strikelink/demoparser/parsing/teameconomysnapshot.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public TeamEconomySnapshot <Clone>$()
```

#### Returns

[TeamEconomySnapshot](./strikelink/demoparser/parsing/teameconomysnapshot.md)<br>

### **Deconstruct(CsTeamSide&, Nullable`1&)**

```csharp
public void Deconstruct(CsTeamSide& Team, Nullable`1& Value)
```

#### Parameters

`Team` [CsTeamSide&](./strikelink/demoparser/parsing/csteamside&.md)<br>

`Value` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
