# TradingStats

Namespace: StrikeLink.DemoParser.Parsing

Represents aggregated statistics related to trading kills and deaths in a trading scenario.

```csharp
public sealed record class TradingStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.TradingStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [TradingStats](./strikelink/demoparser/parsing/tradingstats.md)<br>
Implements [IEquatable&lt;TradingStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TradeKills**

The total number of kills achieved through trades.

```csharp
public int TradeKills { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TradedDeaths**

The total number of deaths that occurred as a result of being traded.

```csharp
public int TradedDeaths { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TradeKillRate**

The ratio of trade kills to total opportunities, expressed as a double. Must be between 0.0 and 1.0.

```csharp
public double TradeKillRate { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **TradingStats(Int32, Int32, Double)**

Represents aggregated statistics related to trading kills and deaths in a trading scenario.

```csharp
public TradingStats(int TradeKills, int TradedDeaths, double TradeKillRate)
```

#### Parameters

`TradeKills` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of kills achieved through trades.

`TradedDeaths` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of deaths that occurred as a result of being traded.

`TradeKillRate` [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>
The ratio of trade kills to total opportunities, expressed as a double. Must be between 0.0 and 1.0.
