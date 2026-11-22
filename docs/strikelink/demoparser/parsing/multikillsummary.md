# MultiKillSummary

Namespace: StrikeLink.DemoParser.Parsing

Represents a summary of multi-kill statistics, including counts and round indices for two-, three-, four-, and
 five-kill events.

```csharp
public sealed record class MultiKillSummary : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.MultiKillSummary, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>
Implements [IEquatable&lt;MultiKillSummary&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TwoKs**

The total number of rounds in which two kills occurred.

```csharp
public int TwoKs { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ThreeKs**

The total number of rounds in which three kills occurred.

```csharp
public int ThreeKs { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FourKs**

The total number of rounds in which four kills occurred.

```csharp
public int FourKs { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FiveKs**

The total number of rounds in which five kills occurred.

```csharp
public int FiveKs { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TwoKRounds**

A read-only list containing the indices of rounds where two kills were achieved.

```csharp
public IReadOnlyList<int> TwoKRounds { get; init; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ThreeKRounds**

A read-only list containing the indices of rounds where three kills were achieved.

```csharp
public IReadOnlyList<int> ThreeKRounds { get; init; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FourKRounds**

A read-only list containing the indices of rounds where four kills were achieved.

```csharp
public IReadOnlyList<int> FourKRounds { get; init; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FiveKRounds**

A read-only list containing the indices of rounds where five kills were achieved.

```csharp
public IReadOnlyList<int> FiveKRounds { get; init; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **MultiKillSummary(Int32, Int32, Int32, Int32, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;)**

Represents a summary of multi-kill statistics, including counts and round indices for two-, three-, four-, and
 five-kill events.

```csharp
public MultiKillSummary(int TwoKs, int ThreeKs, int FourKs, int FiveKs, IReadOnlyList<int> TwoKRounds, IReadOnlyList<int> ThreeKRounds, IReadOnlyList<int> FourKRounds, IReadOnlyList<int> FiveKRounds)
```

#### Parameters

`TwoKs` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which two kills occurred.

`ThreeKs` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which three kills occurred.

`FourKs` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which four kills occurred.

`FiveKs` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which five kills occurred.

`TwoKRounds` [IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where two kills were achieved.

`ThreeKRounds` [IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where three kills were achieved.

`FourKRounds` [IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where four kills were achieved.

`FiveKRounds` [IReadOnlyList&lt;Int32&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where five kills were achieved.
