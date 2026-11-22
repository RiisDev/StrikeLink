# SideSplitMetrics

Namespace: StrikeLink.DemoParser.Parsing

Represents a set of metrics that provide overall and side-specific values for a match or scenario.

```csharp
public sealed record class SideSplitMetrics : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.SideSplitMetrics, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>
Implements [IEquatable&lt;SideSplitMetrics&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Overall**

The overall metric value, representing the combined result across all sides.

```csharp
public double Overall { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Terrorists**

The metric value specific to the Terrorists side.

```csharp
public double Terrorists { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **CounterTerrorists**

The metric value specific to the Counter-Terrorists side.

```csharp
public double CounterTerrorists { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **SideSplitMetrics(Double, Double, Double)**

Represents a set of metrics that provide overall and side-specific values for a match or scenario.

```csharp
public SideSplitMetrics(double Overall, double Terrorists, double CounterTerrorists)
```

#### Parameters

`Overall` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The overall metric value, representing the combined result across all sides.

`Terrorists` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The metric value specific to the Terrorists side.

`CounterTerrorists` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The metric value specific to the Counter-Terrorists side.
