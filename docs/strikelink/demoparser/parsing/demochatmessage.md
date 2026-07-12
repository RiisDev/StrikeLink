# DemoChatMessage

Namespace: StrikeLink.DemoParser.Parsing

Represents a chat message sent within the demo.

```csharp
public sealed class DemoChatMessage : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.DemoChatMessage, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DemoChatMessage](./strikelink/demoparser/parsing/demochatmessage.md)<br>
Implements [IEquatable&lt;DemoChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **ChatType**

The Chat type [DemoChatMessage.ChatType](./strikelink/demoparser/parsing/demochatmessage.md#chattype)

```csharp
public ChatType ChatType { get; set; }
```

#### Property Value

[ChatType](./strikelink/demoparser/parsing/chattype.md)<br>

### **Username**

The username of the individual

```csharp
public string Username { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Message**

The message.

```csharp
public string Message { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Tick**

The tick the message was sent on

```csharp
public int Tick { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **DemoChatMessage(ChatType, String, String, Int32)**

Represents a chat message sent within the demo.

```csharp
public DemoChatMessage(ChatType ChatType, string Username, string Message, int Tick)
```

#### Parameters

`ChatType` [ChatType](./strikelink/demoparser/parsing/chattype.md)<br>
The Chat type [DemoChatMessage.ChatType](./strikelink/demoparser/parsing/demochatmessage.md#chattype)

`Username` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The username of the individual

`Message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message.

`Tick` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The tick the message was sent on

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

### **Equals(DemoChatMessage)**

```csharp
public bool Equals(DemoChatMessage other)
```

#### Parameters

`other` [DemoChatMessage](./strikelink/demoparser/parsing/demochatmessage.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public DemoChatMessage <Clone>$()
```

#### Returns

[DemoChatMessage](./strikelink/demoparser/parsing/demochatmessage.md)<br>

### **Deconstruct(ChatType&, String&, String&, Int32&)**

```csharp
public void Deconstruct(ChatType& ChatType, String& Username, String& Message, Int32& Tick)
```

#### Parameters

`ChatType` [ChatType&](./strikelink/demoparser/parsing/chattype&.md)<br>

`Username` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Message` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Tick` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
