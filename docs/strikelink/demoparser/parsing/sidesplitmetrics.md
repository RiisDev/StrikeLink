# SideSplitMetrics

Namespace: StrikeLink.DemoParser.Parsing

Represents a set of metrics that provide overall and side-specific values for a match or scenario.

```csharp
public sealed class SideSplitMetrics : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.SideSplitMetrics, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>
Implements [IEquatable&lt;SideSplitMetrics&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Overall**

The overall metric value, representing the combined result across all sides.

```csharp
public double Overall { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Terrorists**

The metric value specific to the Terrorists side.

```csharp
public double Terrorists { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **CounterTerrorists**

The metric value specific to the Counter-Terrorists side.

```csharp
public double CounterTerrorists { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **SideSplitMetrics(Double, Double, Double)**

Represents a set of metrics that provide overall and side-specific values for a match or scenario.

```csharp
public SideSplitMetrics(double Overall, double Terrorists, double CounterTerrorists)
```

#### Parameters

`Overall` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The overall metric value, representing the combined result across all sides.

`Terrorists` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The metric value specific to the Terrorists side.

`CounterTerrorists` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The metric value specific to the Counter-Terrorists side.

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

### **Equals(SideSplitMetrics)**

```csharp
public bool Equals(SideSplitMetrics other)
```

#### Parameters

`other` [SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public SideSplitMetrics <Clone>$()
```

#### Returns

[SideSplitMetrics](./strikelink/demoparser/parsing/sidesplitmetrics.md)<br>

### **Deconstruct(Double&, Double&, Double&)**

```csharp
public void Deconstruct(Double& Overall, Double& Terrorists, Double& CounterTerrorists)
```

#### Parameters

`Overall` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`Terrorists` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>

`CounterTerrorists` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>
