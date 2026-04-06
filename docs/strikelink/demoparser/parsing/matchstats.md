# MatchStats

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class MatchStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.MatchStats, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>
Implements [IEquatable&lt;MatchStats&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Duration**

```csharp
public TimeSpan Duration { get; set; }
```

#### Property Value

[TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

### **TerroristScore**

```csharp
public int TerroristScore { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CounterTerroristScore**

```csharp
public int CounterTerroristScore { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Outcome**

```csharp
public MatchOutcome Outcome { get; set; }
```

#### Property Value

[MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>

### **ServerLocation**

```csharp
public string ServerLocation { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ServerAddress**

```csharp
public string ServerAddress { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ServerPort**

```csharp
public Nullable<int> ServerPort { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **GameType**

```csharp
public string GameType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaxPlayers**

```csharp
public Nullable<int> MaxPlayers { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Date**

```csharp
public Nullable<DateTimeOffset> Date { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Map**

```csharp
public string Map { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MatchShareCode**

```csharp
public string MatchShareCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ServerName**

```csharp
public string ServerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DemoClientName**

```csharp
public string DemoClientName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **NetworkProtocol**

```csharp
public Nullable<int> NetworkProtocol { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FocusSteamId**

```csharp
public Nullable<ulong> FocusSteamId { get; set; }
```

#### Property Value

[Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **MatchStats(TimeSpan, Int32, Int32, MatchOutcome, String, String, Nullable&lt;Int32&gt;, String, Nullable&lt;Int32&gt;, Nullable&lt;DateTimeOffset&gt;, String, String, String, String, Nullable&lt;Int32&gt;, Nullable&lt;UInt64&gt;)**

```csharp
public MatchStats(TimeSpan Duration, int TerroristScore, int CounterTerroristScore, MatchOutcome Outcome, string ServerLocation, string ServerAddress, Nullable<int> ServerPort, string GameType, Nullable<int> MaxPlayers, Nullable<DateTimeOffset> Date, string Map, string MatchShareCode, string ServerName, string DemoClientName, Nullable<int> NetworkProtocol, Nullable<ulong> FocusSteamId)
```

#### Parameters

`Duration` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

`TerroristScore` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`CounterTerroristScore` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`Outcome` [MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>

`ServerLocation` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ServerAddress` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ServerPort` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`GameType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`MaxPlayers` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`Date` [Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`Map` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`MatchShareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ServerName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`DemoClientName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`NetworkProtocol` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`FocusSteamId` [Nullable&lt;UInt64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

### **Equals(MatchStats)**

```csharp
public bool Equals(MatchStats other)
```

#### Parameters

`other` [MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public MatchStats <Clone>$()
```

#### Returns

[MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>

### **Deconstruct(TimeSpan&, Int32&, Int32&, MatchOutcome&, String&, String&, Nullable`1&, String&, Nullable`1&, Nullable`1&, String&, String&, String&, String&, Nullable`1&, Nullable`1&)**

```csharp
public void Deconstruct(TimeSpan& Duration, Int32& TerroristScore, Int32& CounterTerroristScore, MatchOutcome& Outcome, String& ServerLocation, String& ServerAddress, Nullable`1& ServerPort, String& GameType, Nullable`1& MaxPlayers, Nullable`1& Date, String& Map, String& MatchShareCode, String& ServerName, String& DemoClientName, Nullable`1& NetworkProtocol, Nullable`1& FocusSteamId)
```

#### Parameters

`Duration` [TimeSpan&](https://docs.microsoft.com/en-us/dotnet/api/system.timespan&)<br>

`TerroristScore` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`CounterTerroristScore` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Outcome` [MatchOutcome&](./strikelink/demoparser/parsing/matchoutcome&.md)<br>

`ServerLocation` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ServerAddress` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ServerPort` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`GameType` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`MaxPlayers` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Date` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Map` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`MatchShareCode` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ServerName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`DemoClientName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`NetworkProtocol` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`FocusSteamId` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
