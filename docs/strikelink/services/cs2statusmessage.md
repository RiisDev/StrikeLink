# Cs2StatusMessage

Namespace: StrikeLink.Services

Represents a snapshot of the current status of a CS2 server, including server information, player data, and spawn
 group details.

```csharp
public class Cs2StatusMessage : System.IEquatable`1[[StrikeLink.Services.Cs2StatusMessage, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Cs2StatusMessage](./strikelink/services/cs2statusmessage.md)<br>
Implements [IEquatable&lt;Cs2StatusMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Timestamp**

The date and time when the status message was generated.

```csharp
public DateTimeOffset Timestamp { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **CurrentState**

The current operational state of the server, such as running, paused, or stopped.

```csharp
public string CurrentState { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Server**

Information about the server, including its configuration and status.

```csharp
public ServerInfo Server { get; set; }
```

#### Property Value

[ServerInfo](./strikelink/services/serverinfo.md)<br>

### **SpawnGroups**

A read-only list of spawn group details present on the server at the time of the status message.

```csharp
public IReadOnlyList<SpawnGroup> SpawnGroups { get; set; }
```

#### Property Value

[IReadOnlyList&lt;SpawnGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **Players**

A read-only list of player entries representing all players currently connected to the server.

```csharp
public IReadOnlyList<PlayerEntry> Players { get; set; }
```

#### Property Value

[IReadOnlyList&lt;PlayerEntry&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ServerTag**

A tag or identifier associated with the server, used for categorization or filtering.

```csharp
public string ServerTag { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **Cs2StatusMessage(DateTimeOffset, String, ServerInfo, IReadOnlyList&lt;SpawnGroup&gt;, IReadOnlyList&lt;PlayerEntry&gt;, String)**

Represents a snapshot of the current status of a CS2 server, including server information, player data, and spawn
 group details.

```csharp
public Cs2StatusMessage(DateTimeOffset Timestamp, string CurrentState, ServerInfo Server, IReadOnlyList<SpawnGroup> SpawnGroups, IReadOnlyList<PlayerEntry> Players, string ServerTag)
```

#### Parameters

`Timestamp` [DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>
The date and time when the status message was generated.

`CurrentState` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The current operational state of the server, such as running, paused, or stopped.

`Server` [ServerInfo](./strikelink/services/serverinfo.md)<br>
Information about the server, including its configuration and status.

`SpawnGroups` [IReadOnlyList&lt;SpawnGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of spawn group details present on the server at the time of the status message.

`Players` [IReadOnlyList&lt;PlayerEntry&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of player entries representing all players currently connected to the server.

`ServerTag` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A tag or identifier associated with the server, used for categorization or filtering.

### **Cs2StatusMessage(Cs2StatusMessage)**

```csharp
protected Cs2StatusMessage(Cs2StatusMessage original)
```

#### Parameters

`original` [Cs2StatusMessage](./strikelink/services/cs2statusmessage.md)<br>

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

### **Equals(Cs2StatusMessage)**

```csharp
public bool Equals(Cs2StatusMessage other)
```

#### Parameters

`other` [Cs2StatusMessage](./strikelink/services/cs2statusmessage.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Cs2StatusMessage <Clone>$()
```

#### Returns

[Cs2StatusMessage](./strikelink/services/cs2statusmessage.md)<br>

### **Deconstruct(DateTimeOffset&, String&, ServerInfo&, IReadOnlyList`1&, IReadOnlyList`1&, String&)**

```csharp
public void Deconstruct(DateTimeOffset& Timestamp, String& CurrentState, ServerInfo& Server, IReadOnlyList`1& SpawnGroups, IReadOnlyList`1& Players, String& ServerTag)
```

#### Parameters

`Timestamp` [DateTimeOffset&](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset&)<br>

`CurrentState` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Server` [ServerInfo&](./strikelink/services/serverinfo&.md)<br>

`SpawnGroups` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`Players` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>

`ServerTag` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
