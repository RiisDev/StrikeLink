# IPCLogger

Namespace: StrikeLink.IPC

Monitors Steam's ipc_SteamClient.log, raises OnLogReceived for each new log line, accumulates completed match
 segments, and exposes player lists parsed from the current segment or full session.

```csharp
public class IPCLogger : System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCLogger](./strikelink/ipc/ipclogger.md)<br>
Implements [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Starts a background reader task that watches the IPC log file and incrementally parses lines into
 match segments. Maintains an in-memory list of completed match segments and a mutable current match segment
 (accessed with internal synchronization). Can start or restart the Steam client when configured and requires IPC
 logging to be enabled. Implements IAsyncDisposable to cancel the background reader and release resources. Event
 handlers and public APIs may be invoked from background threads.

## Properties

### **MatchSegments**

Gets a read-only list of match segments.

```csharp
public IReadOnlyList<string> MatchSegments { get; }
```

#### Property Value

[IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

**Remarks:**

The returned IReadOnlyList is a read-only wrapper around the internal list; changes to the
 underlying collection are reflected in the returned view.

## Constructors

### **IPCLogger(IPCConfig)**

Initializes a new IPCLogger, ensures Steam is running with IPC logging enabled, removes any existing IPC log file,
 and begins asynchronous log reading.

```csharp
public IPCLogger(IPCConfig config)
```

#### Parameters

`config` [IPCConfig](./strikelink/ipc/ipcconfig.md)<br>
Configuration options that control whether to start or restart Steam and enable IPC logging.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when Steam is not running and StartSteam is false, or when Steam is running without IPC enabled and
 RestartSteam is false.

**Remarks:**

Throws ArgumentNullException if config is null. May start or restart Steam synchronously based on
 config. Attempts to delete any existing IPC log file (errors ignored) and starts background log reading.

## Methods

### **GetPlayerListFromCurrentSegment()**

Parses the current match segment and returns a read-only list of IPCUser instances representing the players found.

```csharp
public IReadOnlyList<IPCUser> GetPlayerListFromCurrentSegment()
```

#### Returns

[IReadOnlyList&lt;IPCUser&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of IPCUser instances parsed from the current match segment; an empty list when not currently in a
 match.

**Remarks:**

Snapshots the segment under a lock and uses FilterPlayerRegex() to extract 'steamId' and
 'playerName' groups. Steam IDs are parsed with CultureInfo.InvariantCulture and must be valid integers.

### **GetPlayerListFromSession()**

Retrieves a read-only list of IPCUser parsed from the current session log text.

```csharp
public IReadOnlyList<IPCUser> GetPlayerListFromSession()
```

#### Returns

[IReadOnlyList&lt;IPCUser&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of IPCUser parsed from the session; empty if no players are found.

**Remarks:**

Parses _currentLogText using FilterPlayerRegex(), expecting named capture groups "steamId" and
 "playerName". Steam IDs are parsed to int using CultureInfo.InvariantCulture. May throw FormatException or
 OverflowException if a steamId value is not a valid integer.

### **DisposeAsync()**

Asynchronously releases managed resources by canceling and disposing the internal CancellationTokenSource and
 suppressing finalization.

```csharp
public ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>
A ValueTask that represents the asynchronous disposal operation.

**Remarks:**

Idempotent: subsequent calls have no effect. Exceptions thrown during cancellation are
 ignored.

## Events

### **OnLogReceived**

Occurs when a new console log line is received.

```csharp
public event Action<string> OnLogReceived;
```

### **OnScoreChanged**

Occurs when the round score changes. The tuple contains `(teamScore, enemyScore)`.

```csharp
public event Action<ValueTuple<int, int>> OnScoreChanged;
```

### **OnRoundStart**

Occurs when a new round begins. The argument is the round number.

```csharp
public event Action<int> OnRoundStart;
```

### **OnRoundEnd**

Occurs when a round ends. The argument is the round number.

```csharp
public event Action<int> OnRoundEnd;
```

### **OnChatMessage**

Occurs when a chat message is received from any player in the match.

```csharp
public event Action<IPCChatMessage> OnChatMessage;
```

### **OnStatChanged**

Occurs when a career stat is read or updated in the Steam client.

```csharp
public event Action<IPCStat> OnStatChanged;
```

### **OnKdaChanged**

Occurs when the local player's kill/death/assist counters change.

```csharp
public event Action<IPCKda> OnKdaChanged;
```

### **OnPlayerDetected**

Occurs when a player is detected in the current match via Steam's `FilterText` IPC call.

```csharp
public event Action<IPCUser> OnPlayerDetected;
```

### **OnBombPlanted**

Occurs when a player plants the bomb.

```csharp
public event Action<IPCUser> OnBombPlanted;
```

### **OnKill**

Occurs when the local player kills another player. The tuple contains `(victim, weapon)`.

```csharp
public event Action<ValueTuple<IPCUser, string>> OnKill;
```

### **OnDeath**

Occurs when the local player is killed. The tuple contains `(killer, weapon)`.

```csharp
public event Action<ValueTuple<IPCUser, string>> OnDeath;
```
