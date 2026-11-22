# ControllerPropertySchema

Namespace: StrikeLink.DemoParser.Parsing

One property of the controller entity as declared by the demo's own schema.

```csharp
public sealed record class ControllerPropertySchema : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.ControllerPropertySchema, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ControllerPropertySchema](./strikelink/demoparser/parsing/controllerpropertyschema.md)<br>
Implements [IEquatable&lt;ControllerPropertySchema&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Name**

Dotted property name; array elements are written as `[]`, for example `m_pActionTrackingServices.m_perRoundStats.[].m_iKills`.

```csharp
public string Name { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Type**

The declared network type, for example `int32` or `CUtlSymbolLarge`.

```csharp
public string Type { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ControllerPropertySchema(String, String)**

One property of the controller entity as declared by the demo's own schema.

```csharp
public ControllerPropertySchema(string Name, string Type)
```

#### Parameters

`Name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Dotted property name; array elements are written as `[]`, for example `m_pActionTrackingServices.m_perRoundStats.[].m_iKills`.

`Type` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The declared network type, for example `int32` or `CUtlSymbolLarge`.
