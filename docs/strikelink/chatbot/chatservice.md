# ChatService

Namespace: StrikeLink.ChatBot

Provides high-level chat orchestration and message delivery services.

```csharp
public class ChatService : System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ChatService](./strikelink/chatbot/chatservice.md)<br>
Implements [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This service manages the lifecycle of chat interactions and delegates
 message handling to underlying infrastructure components.

## Constructors

### **ChatService(ConsoleServiceConfig, ConsoleService)**

Initializes a new instance of the [ChatService](./strikelink/chatbot/chatservice.md) class.

```csharp
public ChatService(ConsoleServiceConfig consoleServiceConfig, ConsoleService console)
```

#### Parameters

`consoleServiceConfig` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
The configuration object containing chat service settings and dependencies, [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)

`console` [ConsoleService](./strikelink/services/consoleservice.md)<br>
A premade instance of the console service

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `consoleServiceConfig` is `null`.

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

### **DisposeAsync()**

Releases the unmanaged resources used by the object and optionally releases the managed resources.

```csharp
public ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>

**Remarks:**

This method is called by public Dispose methods and the finalizer. When disposing is true, this
 method disposes all managed resources referenced by the object. Override this method to release additional
 resources.
