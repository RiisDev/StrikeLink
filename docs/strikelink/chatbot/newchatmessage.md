# NewChatMessage

Namespace: StrikeLink.ChatBot

Represents a request to send a new chat message.

```csharp
public class NewChatMessage : System.IEquatable`1[[StrikeLink.ChatBot.NewChatMessage, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NewChatMessage](./strikelink/chatbot/newchatmessage.md)<br>
Implements [IEquatable&lt;NewChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Channel**

The target chat channel.

```csharp
public ChatChannel Channel { get; set; }
```

#### Property Value

[ChatChannel](./strikelink/chatbot/chatchannel.md)<br>

### **Message**

The message content to be sent.

```csharp
public string Message { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **NewChatMessage(ChatChannel, String)**

Represents a request to send a new chat message.

```csharp
public NewChatMessage(ChatChannel Channel, string Message)
```

#### Parameters

`Channel` [ChatChannel](./strikelink/chatbot/chatchannel.md)<br>
The target chat channel.

`Message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message content to be sent.

### **NewChatMessage(NewChatMessage)**

```csharp
protected NewChatMessage(NewChatMessage original)
```

#### Parameters

`original` [NewChatMessage](./strikelink/chatbot/newchatmessage.md)<br>

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

### **Equals(NewChatMessage)**

```csharp
public bool Equals(NewChatMessage other)
```

#### Parameters

`other` [NewChatMessage](./strikelink/chatbot/newchatmessage.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public NewChatMessage <Clone>$()
```

#### Returns

[NewChatMessage](./strikelink/chatbot/newchatmessage.md)<br>

### **Deconstruct(ChatChannel&, String&)**

```csharp
public void Deconstruct(ChatChannel& Channel, String& Message)
```

#### Parameters

`Channel` [ChatChannel&](./strikelink/chatbot/chatchannel&.md)<br>

`Message` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
