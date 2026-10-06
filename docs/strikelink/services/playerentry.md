# PlayerEntry

Namespace: StrikeLink.Services

Represents a player entry containing connection and status information for a player in a session.

```csharp
public class PlayerEntry : System.IEquatable`1[[StrikeLink.Services.PlayerEntry, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerEntry](./strikelink/services/playerentry.md)<br>
Implements [IEquatable&lt;PlayerEntry&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Id**

The unique identifier for the player entry.

```csharp
public int Id { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Channel**

The name of the channel to which the player is connected, or null if not assigned.

```csharp
public string Channel { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TimeConnected**

The duration for which the player has been connected.

```csharp
public TimeSpan TimeConnected { get; set; }
```

#### Property Value

[TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

### **Ping**

The current network ping for the player, measured in milliseconds.

```csharp
public int Ping { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Loss**

The percentage of packet loss experienced by the player.

```csharp
public int Loss { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **State**

The current connection or activity state of the player.

```csharp
public string State { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Rate**

The data transfer rate allocated to the player, in bytes per second.

```csharp
public int Rate { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Name**

The display name of the player.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IsBot**

true if the player is a bot; otherwise, false.

```csharp
public bool IsBot { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **PlayerEntry(Int32, String, TimeSpan, Int32, Int32, String, Int32, String, Boolean)**

Represents a player entry containing connection and status information for a player in a session.

```csharp
public PlayerEntry(int Id, string Channel, TimeSpan TimeConnected, int Ping, int Loss, string State, int Rate, string Name, bool IsBot)
```

#### Parameters

`Id` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The unique identifier for the player entry.

`Channel` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the channel to which the player is connected, or null if not assigned.

`TimeConnected` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>
The duration for which the player has been connected.

`Ping` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The current network ping for the player, measured in milliseconds.

`Loss` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The percentage of packet loss experienced by the player.

`State` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The current connection or activity state of the player.

`Rate` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The data transfer rate allocated to the player, in bytes per second.

`Name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player.

`IsBot` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the player is a bot; otherwise, false.

### **PlayerEntry(PlayerEntry)**

```csharp
protected PlayerEntry(PlayerEntry original)
```

#### Parameters

`original` [PlayerEntry](./strikelink/services/playerentry.md)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PrintMembers(StringBuilder)**

```csharp
protected bool PrintMembers(StringBuilder builder)
```

#### Parameters

`builder` [StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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

### **Equals(PlayerEntry)**

```csharp
public bool Equals(PlayerEntry other)
```

#### Parameters

`other` [PlayerEntry](./strikelink/services/playerentry.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public PlayerEntry <Clone>$()
```

#### Returns

[PlayerEntry](./strikelink/services/playerentry.md)<br>

### **Deconstruct(Int32&, String&, TimeSpan&, Int32&, Int32&, String&, Int32&, String&, Boolean&)**

```csharp
public void Deconstruct(Int32& Id, String& Channel, TimeSpan& TimeConnected, Int32& Ping, Int32& Loss, String& State, Int32& Rate, String& Name, Boolean& IsBot)
```

#### Parameters

`Id` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Channel` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`TimeConnected` [TimeSpan&](https://docs.microsoft.com/en-us/dotnet/api/system.timespan&)<br>

`Ping` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Loss` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`State` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Rate` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Name` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`IsBot` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
