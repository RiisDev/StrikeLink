# ChatMessage

Namespace: StrikeLink.ChatBot

Represents a chat message received from the chat system.

```csharp
public class ChatMessage : System.IEquatable`1[[StrikeLink.ChatBot.ChatMessage, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ChatMessage](./strikelink/chatbot/chatmessage.md)<br>
Implements [IEquatable&lt;ChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Username**

The name of the user who sent the message.

```csharp
public string Username { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Message**

The message content.

```csharp
public string Message { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Dead**

Indicates whether the sender was dead at the time the message was sent.

```csharp
public bool Dead { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **ChatMessage(String, String, Boolean)**

Represents a chat message received from the chat system.

```csharp
public ChatMessage(string Username, string Message, bool Dead)
```

#### Parameters

`Username` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the user who sent the message.

`Message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message content.

`Dead` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Indicates whether the sender was dead at the time the message was sent.

### **ChatMessage(ChatMessage)**

```csharp
protected ChatMessage(ChatMessage original)
```

#### Parameters

`original` [ChatMessage](./strikelink/chatbot/chatmessage.md)<br>

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

### **Equals(ChatMessage)**

```csharp
public bool Equals(ChatMessage other)
```

#### Parameters

`other` [ChatMessage](./strikelink/chatbot/chatmessage.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public ChatMessage <Clone>$()
```

#### Returns

[ChatMessage](./strikelink/chatbot/chatmessage.md)<br>

### **Deconstruct(String&, String&, Boolean&)**

```csharp
public void Deconstruct(String& Username, String& Message, Boolean& Dead)
```

#### Parameters

`Username` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Message` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Dead` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
