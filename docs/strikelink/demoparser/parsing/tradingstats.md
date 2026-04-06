# TradingStats

Namespace: StrikeLink.DemoParser.Parsing

Represents aggregated statistics related to trading kills and deaths in a trading scenario.

```csharp
public sealed class TradingStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.TradingStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>
Implements [IEquatable&lt;TradingStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TradeKills**

The total number of kills achieved through trades.

```csharp
public int TradeKills { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TradedDeaths**

The total number of deaths that occurred as a result of being traded.

```csharp
public int TradedDeaths { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TradeKillRate**

The ratio of trade kills to total opportunities, expressed as a double. Must be between 0.0 and 1.0.

```csharp
public double TradeKillRate { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **TradingStats(Int32, Int32, Double)**

Represents aggregated statistics related to trading kills and deaths in a trading scenario.

```csharp
public TradingStats(int TradeKills, int TradedDeaths, double TradeKillRate)
```

#### Parameters

`TradeKills` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved through trades.

`TradedDeaths` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of deaths that occurred as a result of being traded.

`TradeKillRate` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The ratio of trade kills to total opportunities, expressed as a double. Must be between 0.0 and 1.0.

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

### **Equals(TradingStats)**

```csharp
public bool Equals(TradingStats other)
```

#### Parameters

`other` [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public TradingStats <Clone>$()
```

#### Returns

[TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>

### **Deconstruct(Int32&, Int32&, Double&)**

```csharp
public void Deconstruct(Int32& TradeKills, Int32& TradedDeaths, Double& TradeKillRate)
```

#### Parameters

`TradeKills` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TradedDeaths` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TradeKillRate` [Double&](https://docs.microsoft.com/en-us/dotnet/api/system.double&)<br>
