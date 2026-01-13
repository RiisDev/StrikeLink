# ServerListener

Namespace: StrikeLink.GSI

Listens for incoming Game State Integration (GSI) HTTP/TCP requests
 and dispatches parsed payloads to strongly-typed events.

```csharp
public class ServerListener : System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ServerListener](./strikelink/gsi/serverlistener.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This listener is responsible for configuring the GSI file, validating
 network bindings, and managing the lifetime of the underlying socket server.

## Properties

### **Ready**

Bool to indicate if the server is ready to accept connections, as well as invokes the OnReady event with the built Uri.

```csharp
public bool Ready { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Address**

the server is bound to.

```csharp
public IPAddress Address { get; private set; }
```

#### Property Value

IPAddress<br>

### **Port**

Port number the server is listening on.

```csharp
public int Port { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **ServerListener()**

```csharp
public ServerListener()
```

## Methods

### **StartAsync(IPAddress, Nullable&lt;Int32&gt;, String)**

Starts the server listener asynchronously.

```csharp
public Task StartAsync(IPAddress address, Nullable<int> port, string steamPath)
```

#### Parameters

`address` IPAddress<br>
Optional IP address to bind to. If `null`,  is used.

`port` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Optional port to bind to. If `null`, an available free port is selected.

`steamPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Optional explicit Steam installation path used for GSI file generation.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous startup operation.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when Counter-Strike is running during initial configuration,
 when the requested port is already in use, or when socket creation fails.

**Remarks:**

If the requested address or port is invalid or unavailable, a new endpoint
 may be generated automatically. If Counter-Strike is already running during
 this initial setup, the operation will fail to avoid inconsistent GSI state.

### **Dispose()**

Releases all resources used by the [ServerListener](./strikelink/gsi/serverlistener.md).

```csharp
public void Dispose()
```

**Remarks:**

This method suppresses finalization and disposes managed resources.

### **DisposeAsync()**

Releases all resources used by the [ServerListener](./strikelink/gsi/serverlistener.md).

```csharp
public ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>

**Remarks:**

This method suppresses finalization and disposes managed resources.

## Events

### **OnReady**

Occurs when the listener has successfully started and is ready to accept connections.

```csharp
public event Action<Uri> OnReady;
```

**Remarks:**

The provided  represents the bound listening endpoint.

### **OnPostReceived**

Occurs when a raw POST payload is received by the listener.

```csharp
public event Action<string> OnPostReceived;
```

### **OnPlayerStateReceived**

Occurs when a player state update is received.

```csharp
public event Action<PlayerState> OnPlayerStateReceived;
```

### **OnMapStateReceived**

Occurs when a map state update is received.

```csharp
public event Action<MapState> OnMapStateReceived;
```

### **OnRoundStateReceived**

Occurs when a round state update is received.

```csharp
public event Action<RoundState> OnRoundStateReceived;
```
