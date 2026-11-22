# PlayerPosition

Namespace: StrikeLink.DemoParser.Parsing

A position in world coordinates.

```csharp
public sealed record class PlayerPosition : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerPosition, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>
Implements [IEquatable&lt;PlayerPosition&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **X**

```csharp
public float X { get; init; }
```

#### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Y**

```csharp
public float Y { get; init; }
```

#### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Z**

```csharp
public float Z { get; init; }
```

#### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

## Constructors

### **PlayerPosition(Single, Single, Single)**

A position in world coordinates.

```csharp
public PlayerPosition(float X, float Y, float Z)
```

#### Parameters

`X` [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

`Y` [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

`Z` [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>
