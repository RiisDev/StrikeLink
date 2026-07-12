# ConsoleService

Namespace: StrikeLink.Services

Provides access to the game console output and emits high-level events
 for game state changes, chat messages, server activity, and addon progress.

```csharp
public class ConsoleService : System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConsoleService](./strikelink/services/consoleservice.md)<br>
Implements [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Constructors

### **ConsoleService(ConsoleServiceConfig)**

Initializes a new instance of the [ConsoleService](./strikelink/services/consoleservice.md) class.

```csharp
public ConsoleService(ConsoleServiceConfig config)
```

#### Parameters

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
ConsoleServiceConfig record, only needs to be set if intending to use [ConsoleService.SendConsoleCommand(String, ConsoleServiceConfig, CancellationToken)](./strikelink/services/consoleservice.md#sendconsolecommandstring-consoleserviceconfig-cancellationtoken).

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
Thrown when the Counter-Strike 2 installation directory cannot be located.

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when the game was not launched with the `-condebug` launch option.

## Methods

### **SendConsoleCommand(String, ConsoleServiceConfig, CancellationToken)**

Sends a console command by enqueuing a command request and asynchronously returns the response message and a
 success flag.

```csharp
public Task<ValueTuple<string, bool>> SendConsoleCommand(string command, ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`command` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Console command text to execute; must not be null, empty, or whitespace. Leading and trailing whitespace are
 trimmed.

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional execution configuration; when null the instance configuration is used.

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token to cancel the asynchronous operation.

#### Returns

[Task&lt;ValueTuple&lt;String, Boolean&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task that completes with a (Message, Success) tuple containing the response message and a boolean indicating
 success.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when neither the provided consoleServiceConfig nor the instance configuration is available.

**Remarks:**

The command is enqueued to an internal channel and the returned task completes when the command
 processing signals its result.

### **GetStatus(ConsoleServiceConfig, CancellationToken)**

Sends the "status" console command and returns the next Cs2StatusMessage received in response.

```csharp
public Task<Cs2StatusMessage> GetStatus(ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Execution configuration to use; if null the instance configuration is used. Throws InvalidOperationException if
 neither is available.

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token that can cancel the operation. The operation is also subject to a 5-second timeout combined
 with this token.

#### Returns

[Task&lt;Cs2StatusMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task that completes with the next Cs2StatusMessage received in response to the sent command.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when no configuration is provided and no instance configuration is available.

**Remarks:**

Registers a temporary event handler, sends the "status" console command via SendConsoleCommand,
 awaits the response or cancellation, and unsubscribes the handler. The response wait is subject to a 5-second
 timeout.

### **ExecuteCfgFile(String, ConsoleServiceConfig, CancellationToken)**

Executes a CS2 configuration file by validating its presence and issuing an exec
 console command.

```csharp
public Task ExecuteCfgFile(string cfgName, ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`cfgName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the configuration file to execute (including extension) located in the game's cfg directory.

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional ConsoleServiceConfig to use; when null, the configured default is used.

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token that can cancel the operation. The operation is also subject to a 5-second timeout combined
 with this token.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A Task that represents the asynchronous operation.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when no ConsoleServiceConfig is available from either the parameter or the configured default.

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the specified configuration file does not exist in the game's cfg directory.

**Remarks:**

Checks that the cfg file exists under game/csgo/cfg before sending the console command.

### **GetCurrentMap(ConsoleServiceConfig, CancellationToken)**

Gets the name of the currently loaded map from the console status.

```csharp
public Task<string> GetCurrentMap(ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional console service configuration used to retrieve status; when null, the default configuration is used.

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token to observe while awaiting the operation.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task that resolves to the current map name, or "N/A" if no map is found.

**Remarks:**

Retrieves status via GetStatus and selects the first spawn group with LoadType "mapload".
 Exceptions from status retrieval propagate to the caller; the operation honors the provided cancellation
 token.

### **GetPing(ConsoleServiceConfig, CancellationToken)**

Gets the current local Steam user's ping from the CS2 status.

```csharp
public Task<int> GetPing(ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Console service configuration used when fetching status, or null to use defaults.

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token to cancel the asynchronous operation.

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The local user's ping in milliseconds, or -1 if the user is not present in the status.

**Remarks:**

Identifies the local player via SteamService.GetLocalUsername and queries GetStatus to obtain the
 Players list.

### **DisposeAsync()**

Completes the command queue (no new commands accepted), waits for any
 in-flight commands to finish, then cancels the log listener.

```csharp
public ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>

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

### **OnStatusMessage**

Occurs when a status message is available.

```csharp
public event Action<Cs2StatusMessage> OnStatusMessage;
```

**Remarks:**

Subscribers receive an object containing status information. The specific type and content of the
 object depend on the context in which the event is raised. Handlers should verify the type of the event argument
 before use.

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

### **OnMatchPrompt**

Occurs when the client receives 'Match Found' screen.

```csharp
public event Action OnMatchPrompt;
```
