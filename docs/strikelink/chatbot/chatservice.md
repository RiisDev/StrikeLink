# ChatService

Namespace: StrikeLink.ChatBot

Provides high-level chat orchestration and message delivery services.

```csharp
public class ChatService : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ChatService](./strikelink/chatbot/chatservice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This service manages the lifecycle of chat interactions and delegates
 message handling to underlying infrastructure components.

## Constructors

### **ChatService(Config)**

Initializes a new instance of the [ChatService](./strikelink/chatbot/chatservice.md) class.

```csharp
public ChatService(Config config)
```

#### Parameters

`config` [Config](./strikelink/chatbot/config.md)<br>
The configuration object containing chat service settings and dependencies, [Config](./strikelink/chatbot/config.md)

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `config` is `null`.

## Methods

### **SendChatAsync(NewChatMessage)**

Sends a chat message asynchronously.

```csharp
public Task SendChatAsync(NewChatMessage message)
```

#### Parameters

`message` [NewChatMessage](./strikelink/chatbot/newchatmessage.md)<br>
The chat message payload to be sent [NewChatMessage](./strikelink/chatbot/newchatmessage.md)

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous send operation.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `message` is `null`.

### **Dispose()**

Releases all resources used by the [ChatService](./strikelink/chatbot/chatservice.md).

```csharp
public void Dispose()
```

**Remarks:**

This method suppresses finalization and disposes managed resources.
