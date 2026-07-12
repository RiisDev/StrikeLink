# ServerInfo

Namespace: StrikeLink.Services

Represents information about a game server, including its version, player counts, status, and reservation details.

```csharp
public class ServerInfo : System.IEquatable`1[[StrikeLink.Services.ServerInfo, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ServerInfo](./strikelink/services/serverinfo.md)<br>
Implements [IEquatable&lt;ServerInfo&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Slot**

The slot number assigned to the server instance.

```csharp
public int Slot { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Version**

The version string of the server software.

```csharp
public string Version { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **BuildNumber**

The build number identifying the specific server build.

```csharp
public string BuildNumber { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SecurityMode**

The security mode in which the server is operating.

```csharp
public string SecurityMode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Visibility**

The visibility status of the server, indicating whether it is public or private.

```csharp
public string Visibility { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SteamId**

The unique Steam identifier associated with the server.

```csharp
public string SteamId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HumanCount**

The number of human players currently connected to the server.

```csharp
public int HumanCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BotCount**

The number of bot players currently present on the server.

```csharp
public int BotCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxPlayers**

The maximum number of players that the server supports.

```csharp
public int MaxPlayers { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsHibernating**

A value indicating whether the server is currently in a hibernating state.

```csharp
public bool IsHibernating { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReservationId**

The reservation identifier associated with the server session, if any.

```csharp
public string ReservationId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ServerInfo(Int32, String, String, String, String, String, Int32, Int32, Int32, Boolean, String)**

Represents information about a game server, including its version, player counts, status, and reservation details.

```csharp
public ServerInfo(int Slot, string Version, string BuildNumber, string SecurityMode, string Visibility, string SteamId, int HumanCount, int BotCount, int MaxPlayers, bool IsHibernating, string ReservationId)
```

#### Parameters

`Slot` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The slot number assigned to the server instance.

`Version` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The version string of the server software.

`BuildNumber` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The build number identifying the specific server build.

`SecurityMode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The security mode in which the server is operating.

`Visibility` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The visibility status of the server, indicating whether it is public or private.

`SteamId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique Steam identifier associated with the server.

`HumanCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of human players currently connected to the server.

`BotCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of bot players currently present on the server.

`MaxPlayers` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The maximum number of players that the server supports.

`IsHibernating` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A value indicating whether the server is currently in a hibernating state.

`ReservationId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The reservation identifier associated with the server session, if any.

### **ServerInfo(ServerInfo)**

```csharp
protected ServerInfo(ServerInfo original)
```

#### Parameters

`original` [ServerInfo](./strikelink/services/serverinfo.md)<br>

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

### **Equals(ServerInfo)**

```csharp
public bool Equals(ServerInfo other)
```

#### Parameters

`other` [ServerInfo](./strikelink/services/serverinfo.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public ServerInfo <Clone>$()
```

#### Returns

[ServerInfo](./strikelink/services/serverinfo.md)<br>

### **Deconstruct(Int32&, String&, String&, String&, String&, String&, Int32&, Int32&, Int32&, Boolean&, String&)**

```csharp
public void Deconstruct(Int32& Slot, String& Version, String& BuildNumber, String& SecurityMode, String& Visibility, String& SteamId, Int32& HumanCount, Int32& BotCount, Int32& MaxPlayers, Boolean& IsHibernating, String& ReservationId)
```

#### Parameters

`Slot` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Version` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`BuildNumber` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`SecurityMode` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Visibility` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`SteamId` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`HumanCount` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`BotCount` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MaxPlayers` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`IsHibernating` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`ReservationId` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
