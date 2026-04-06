# MultiKillSummary

Namespace: StrikeLink.DemoParser.Parsing

Represents a summary of multi-kill statistics, including counts and round indices for two-, three-, four-, and
 five-kill events.

```csharp
public sealed class MultiKillSummary : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.MultiKillSummary, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>
Implements [IEquatable&lt;MultiKillSummary&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TwoKs**

The total number of rounds in which two kills occurred.

```csharp
public int TwoKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ThreeKs**

The total number of rounds in which three kills occurred.

```csharp
public int ThreeKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FourKs**

The total number of rounds in which four kills occurred.

```csharp
public int FourKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FiveKs**

The total number of rounds in which five kills occurred.

```csharp
public int FiveKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TwoKRounds**

A read-only list containing the indices of rounds where two kills were achieved.

```csharp
public IReadOnlyList<int> TwoKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ThreeKRounds**

A read-only list containing the indices of rounds where three kills were achieved.

```csharp
public IReadOnlyList<int> ThreeKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FourKRounds**

A read-only list containing the indices of rounds where four kills were achieved.

```csharp
public IReadOnlyList<int> FourKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FiveKRounds**

A read-only list containing the indices of rounds where five kills were achieved.

```csharp
public IReadOnlyList<int> FiveKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **MultiKillSummary(Int32, Int32, Int32, Int32, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;)**

Represents a summary of multi-kill statistics, including counts and round indices for two-, three-, four-, and
 five-kill events.

```csharp
public MultiKillSummary(int TwoKs, int ThreeKs, int FourKs, int FiveKs, IReadOnlyList<int> TwoKRounds, IReadOnlyList<int> ThreeKRounds, IReadOnlyList<int> FourKRounds, IReadOnlyList<int> FiveKRounds)
```

#### Parameters

`TwoKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which two kills occurred.

`ThreeKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which three kills occurred.

`FourKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which four kills occurred.

`FiveKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The total number of rounds in which five kills occurred.

`TwoKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where two kills were achieved.

`ThreeKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where three kills were achieved.

`FourKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where four kills were achieved.

`FiveKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list containing the indices of rounds where five kills were achieved.

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

### **Equals(MultiKillSummary)**

```csharp
public bool Equals(MultiKillSummary other)
```

#### Parameters

`other` [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public MultiKillSummary <Clone>$()
```

#### Returns

[MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>

### **Deconstruct(Int32&, Int32&, Int32&, Int32&, IReadOnlyList`1&, IReadOnlyList`1&, IReadOnlyList`1&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Int32& TwoKs, Int32& ThreeKs, Int32& FourKs, Int32& FiveKs, IReadOnlyList`1& TwoKRounds, IReadOnlyList`1& ThreeKRounds, IReadOnlyList`1& FourKRounds, IReadOnlyList`1& FiveKRounds)
```

#### Parameters

`TwoKs` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`ThreeKs` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`FourKs` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`FiveKs` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`TwoKRounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`ThreeKRounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`FourKRounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`FiveKRounds` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
