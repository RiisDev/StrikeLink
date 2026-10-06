# IPCChatMessage

Namespace: StrikeLink.IPC

Represents a chat message received during a CS2 match.

```csharp
public class IPCChatMessage : System.IEquatable`1[[StrikeLink.IPC.IPCChatMessage, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCChatMessage](./strikelink/ipc/ipcchatmessage.md)<br>
Implements [IEquatable&lt;IPCChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **User**

The player who sent the message.

```csharp
public IPCUser User { get; set; }
```

#### Property Value

[IPCUser](./strikelink/ipc/ipcuser.md)<br>

### **Message**

The text content of the message.

```csharp
public string Message { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCChatMessage(IPCUser, String)**

Represents a chat message received during a CS2 match.

```csharp
public IPCChatMessage(IPCUser User, string Message)
```

#### Parameters

`User` [IPCUser](./strikelink/ipc/ipcuser.md)<br>
The player who sent the message.

`Message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The text content of the message.

### **IPCChatMessage(IPCChatMessage)**

```csharp
protected IPCChatMessage(IPCChatMessage original)
```

#### Parameters

`original` [IPCChatMessage](./strikelink/ipc/ipcchatmessage.md)<br>

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

### **Equals(IPCChatMessage)**

```csharp
public bool Equals(IPCChatMessage other)
```

#### Parameters

`other` [IPCChatMessage](./strikelink/ipc/ipcchatmessage.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCChatMessage <Clone>$()
```

#### Returns

[IPCChatMessage](./strikelink/ipc/ipcchatmessage.md)<br>

### **Deconstruct(IPCUser&, String&)**

```csharp
public void Deconstruct(IPCUser& User, String& Message)
```

#### Parameters

`User` [IPCUser&](./strikelink/ipc/ipcuser&.md)<br>

`Message` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
