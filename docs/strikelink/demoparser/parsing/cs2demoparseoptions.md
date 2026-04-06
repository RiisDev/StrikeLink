# Cs2DemoParseOptions

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class Cs2DemoParseOptions : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.Cs2DemoParseOptions, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoParseOptions](./strikelink/demoparser/parsing/cs2demoparseoptions.md)<br>
Implements [IEquatable&lt;Cs2DemoParseOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **FocusSteamId**

```csharp
public Nullable<ulong> FocusSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **IncludeWarmup**

```csharp
public bool IncludeWarmup { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TradeWindow**

```csharp
public Nullable<TimeSpan> TradeWindow { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MatchShareCode**

```csharp
public string MatchShareCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ServerLocation**

```csharp
public string ServerLocation { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MatchDateOverride**

```csharp
public Nullable<DateTimeOffset> MatchDateOverride { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **Cs2DemoParseOptions(Nullable&lt;UInt64&gt;, Boolean, Nullable&lt;TimeSpan&gt;, String, String, Nullable&lt;DateTimeOffset&gt;)**

```csharp
public Cs2DemoParseOptions(Nullable<ulong> FocusSteamId, bool IncludeWarmup, Nullable<TimeSpan> TradeWindow, string MatchShareCode, string ServerLocation, Nullable<DateTimeOffset> MatchDateOverride)
```

#### Parameters

`FocusSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`IncludeWarmup` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`TradeWindow` [Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`MatchShareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ServerLocation` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`MatchDateOverride` [Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

### **Equals(Cs2DemoParseOptions)**

```csharp
public bool Equals(Cs2DemoParseOptions other)
```

#### Parameters

`other` [Cs2DemoParseOptions](./strikelink/demoparser/parsing/cs2demoparseoptions.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Cs2DemoParseOptions <Clone>$()
```

#### Returns

[Cs2DemoParseOptions](./strikelink/demoparser/parsing/cs2demoparseoptions.md)<br>

### **Deconstruct(Nullable`1&, Boolean&, Nullable`1&, String&, String&, Nullable`1&)**

```csharp
public void Deconstruct(Nullable`1& FocusSteamId, Boolean& IncludeWarmup, Nullable`1& TradeWindow, String& MatchShareCode, String& ServerLocation, Nullable`1& MatchDateOverride)
```

#### Parameters

`FocusSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`IncludeWarmup` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`TradeWindow` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`MatchShareCode` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ServerLocation` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`MatchDateOverride` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
