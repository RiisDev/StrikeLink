# ConsoleService

Namespace: StrikeLink.Services

Provides access to the game console output and emits high-level events
 for game state changes, chat messages, server activity, and addon progress.

```csharp
public class ConsoleService : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConsoleService](./strikelink/services/consoleservice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Constructors

### **ConsoleService()**

Initializes a new instance of the [ConsoleService](./strikelink/services/consoleservice.md) class.

```csharp
public ConsoleService()
```

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
Thrown when the Counter-Strike 2 installation directory cannot be located.

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when the game was not launched with the `-condebug` launch option.

## Methods

### **StartListening()**

Starts monitoring the game console log and begins emitting console events.

```csharp
public void StartListening()
```

**Remarks:**

This method must be called before any console-related events will fire.

### **Dispose()**

Releases all resources used by the [ChatService](./strikelink/chatbot/chatservice.md).

```csharp
public void Dispose()
```

**Remarks:**

This method suppresses finalization and disposes managed resources.

## Events

### **OnLogReceived**

Occurs when a new console log line is received.

```csharp
public event Action<string> OnLogReceived;
```

### **OnPlayerConnected**

Occurs when a player connects to the server.

```csharp
public event Action<string> OnPlayerConnected;
```

### **OnMapJoined**

Occurs when the local player joins a map.

```csharp
public event Action<string> OnMapJoined;
```

### **OnGlobalChatMessageReceived**

Occurs when a global chat message is received.

```csharp
public event Action<ChatMessage> OnGlobalChatMessageReceived;
```

### **OnTeamChatMessageReceived**

Occurs when a team chat message is received.

```csharp
public event Action<ChatMessage> OnTeamChatMessageReceived;
```

### **OnUiStateChanged**

Occurs when the game UI state changes.

```csharp
public event Action<StateChanged> OnUiStateChanged;
```

### **OnAddonProgress**

Occurs when addon download progress is updated.

```csharp
public event Action<AddonProgress> OnAddonProgress;
```

### **OnAddonFinished**

Occurs when an addon has finished downloading.

```csharp
public event Action OnAddonFinished;
```

### **OnServerJoining**

Occurs when the client begins joining a server.

```csharp
public event Action OnServerJoining;
```

### **OnServerConnected**

Occurs when the client successfully connects to a server.

```csharp
public event Action OnServerConnected;
```

### **OnServerDisconnected**

Occurs when the client disconnects from a server.

```csharp
public event Action OnServerDisconnected;
```
