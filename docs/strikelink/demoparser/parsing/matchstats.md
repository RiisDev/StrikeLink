# MatchStats

Namespace: StrikeLink.DemoParser.Parsing

Represents summary statistics and metadata for a completed match, including scores, duration, server details, and
 identifying information.

```csharp
public sealed record class MatchStats : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.MatchStats, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [MatchStats](./strikelink/demoparser/parsing/matchstats.md)<br>
Implements [IEquatable&lt;MatchStats&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Date may be incorrect as it is pulled from FileInfo if it fails to be parsed via demo

## Properties

### **Duration**

The total duration of the match.

```csharp
public TimeSpan Duration { get; init; }
```

#### Property Value

[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan)<br>

### **TeamAScore**

The final score achieved by Team A.

```csharp
public int TeamAScore { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TeamBScore**

The final score achieved by Team B.

```csharp
public int TeamBScore { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Outcome**

The outcome of the match, indicating which team won or if the match was drawn.

```csharp
public MatchOutcome Outcome { get; init; }
```

#### Property Value

[MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>

### **ServerLocation**

The geographic location of the server where the match was played, or null if not available.

```csharp
public string? ServerLocation { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GameType**

The type or mode of the game played, or null if not specified.

```csharp
public string? GameType { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaxPlayers**

The maximum number of players allowed on the server during the match, or null if not specified.

```csharp
public int? MaxPlayers { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Date**

The date and time when the match occurred, or null if not available.

```csharp
public DateTimeOffset? Date { get; init; }
```

#### Property Value

[DateTimeOffset?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Map**

The name of the map on which the match was played, or null if not specified.

```csharp
public string? Map { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MatchShareCode**

A shareable code that uniquely identifies the match, or null if not available.

```csharp
public string? MatchShareCode { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ServerName**

The display name of the server, or null if not specified.

```csharp
public string? ServerName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DemoClientName**

The name of the client used to record the match demo, or null if not available.

```csharp
public string? DemoClientName { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **NetworkProtocol**

The network protocol version used by the server, or null if not specified.

```csharp
public int? NetworkProtocol { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FocusSteamId**

The Steam ID of the player in focus for this match, or null if not specified.

```csharp
public ulong? FocusSteamId { get; init; }
```

#### Property Value

[UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **GameBuild**

The game build number recorded in the demo header, or null if not specified. Controller property names can change between builds.

```csharp
public int? GameBuild { get; init; }
```

#### Property Value

[Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **MatchStats(TimeSpan, Int32, Int32, MatchOutcome, String, String, Int32?, DateTimeOffset?, String, String, String, String, Int32?, UInt64?, Int32?)**

Represents summary statistics and metadata for a completed match, including scores, duration, server details, and
 identifying information.

```csharp
public MatchStats(TimeSpan Duration, int TeamAScore, int TeamBScore, MatchOutcome Outcome, string? ServerLocation, string? GameType, int? MaxPlayers, DateTimeOffset? Date, string? Map, string? MatchShareCode, string? ServerName, string? DemoClientName, int? NetworkProtocol, ulong? FocusSteamId, int? GameBuild)
```

#### Parameters

`Duration` [TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan)<br>
The total duration of the match.

`TeamAScore` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The final score achieved by Team A.

`TeamBScore` [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
The final score achieved by Team B.

`Outcome` [MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>
The outcome of the match, indicating which team won or if the match was drawn.

`ServerLocation` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The geographic location of the server where the match was played, or null if not available.

`GameType` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The type or mode of the game played, or null if not specified.

`MaxPlayers` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The maximum number of players allowed on the server during the match, or null if not specified.

`Date` [DateTimeOffset?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The date and time when the match occurred, or null if not available.

`Map` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The name of the map on which the match was played, or null if not specified.

`MatchShareCode` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
A shareable code that uniquely identifies the match, or null if not available.

`ServerName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The display name of the server, or null if not specified.

`DemoClientName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
The name of the client used to record the match demo, or null if not available.

`NetworkProtocol` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The network protocol version used by the server, or null if not specified.

`FocusSteamId` [UInt64?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam ID of the player in focus for this match, or null if not specified.

`GameBuild` [Int32?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The game build number recorded in the demo header, or null if not specified. Controller property names can change between builds.

**Remarks:**

Date may be incorrect as it is pulled from FileInfo if it fails to be parsed via demo
