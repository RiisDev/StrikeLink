# CoPlaySession

Namespace: StrikeLink.Services.WebService

Represents a record of a co-play gaming session, including game details, session timing, and participating players.

```csharp
public class CoPlaySession : System.IEquatable`1[[StrikeLink.Services.WebService.CoPlaySession, StrikeLink, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CoPlaySession](./strikelink/services/webservice/coplaysession.md)<br>
Implements [IEquatable&lt;CoPlaySession&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **GameName**

The name of the game played during the session. Cannot be null.

```csharp
public string GameName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PlayedOn**

The date or platform on which the session was played. The format and meaning depend on the application's context.
 Cannot be null.

```csharp
public string PlayedOn { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Duration**

The duration of the session, typically represented as a formatted string (for example, "2h 15m"). Cannot be null.

```csharp
public string Duration { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Players**

A read-only list of players who participated in the session. Cannot be null and may be empty if no players are
 recorded.

```csharp
public IReadOnlyList<SteamPlayer> Players { get; set; }
```

#### Property Value

[IReadOnlyList&lt;SteamPlayer&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **CoPlaySession(String, String, String, IReadOnlyList&lt;SteamPlayer&gt;)**

Represents a record of a co-play gaming session, including game details, session timing, and participating players.

```csharp
public CoPlaySession(string GameName, string PlayedOn, string Duration, IReadOnlyList<SteamPlayer> Players)
```

#### Parameters

`GameName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the game played during the session. Cannot be null.

`PlayedOn` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The date or platform on which the session was played. The format and meaning depend on the application's context.
 Cannot be null.

`Duration` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The duration of the session, typically represented as a formatted string (for example, "2h 15m"). Cannot be null.

`Players` [IReadOnlyList&lt;SteamPlayer&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of players who participated in the session. Cannot be null and may be empty if no players are
 recorded.

### **CoPlaySession(CoPlaySession)**

```csharp
protected CoPlaySession(CoPlaySession original)
```

#### Parameters

`original` [CoPlaySession](./strikelink/services/webservice/coplaysession.md)<br>

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

### **Equals(CoPlaySession)**

```csharp
public bool Equals(CoPlaySession other)
```

#### Parameters

`other` [CoPlaySession](./strikelink/services/webservice/coplaysession.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public CoPlaySession <Clone>$()
```

#### Returns

[CoPlaySession](./strikelink/services/webservice/coplaysession.md)<br>

### **Deconstruct(String&, String&, String&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(String& GameName, String& PlayedOn, String& Duration, IReadOnlyList`1& Players)
```

#### Parameters

`GameName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`PlayedOn` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Duration` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Players` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
