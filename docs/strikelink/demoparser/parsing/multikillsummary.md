# MultiKillSummary

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class MultiKillSummary : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.MultiKillSummary, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MultiKillSummary](./strikelink/demoparser/parsing/multikillsummary.md)<br>
Implements [IEquatable&lt;MultiKillSummary&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TwoKs**

```csharp
public int TwoKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ThreeKs**

```csharp
public int ThreeKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FourKs**

```csharp
public int FourKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FiveKs**

```csharp
public int FiveKs { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TwoKRounds**

```csharp
public IReadOnlyList<int> TwoKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ThreeKRounds**

```csharp
public IReadOnlyList<int> ThreeKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FourKRounds**

```csharp
public IReadOnlyList<int> FourKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **FiveKRounds**

```csharp
public IReadOnlyList<int> FiveKRounds { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **MultiKillSummary(Int32, Int32, Int32, Int32, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;, IReadOnlyList&lt;Int32&gt;)**

```csharp
public MultiKillSummary(int TwoKs, int ThreeKs, int FourKs, int FiveKs, IReadOnlyList<int> TwoKRounds, IReadOnlyList<int> ThreeKRounds, IReadOnlyList<int> FourKRounds, IReadOnlyList<int> FiveKRounds)
```

#### Parameters

`TwoKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`ThreeKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`FourKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`FiveKs` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`TwoKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`ThreeKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`FourKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

`FiveKRounds` [IReadOnlyList&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

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
