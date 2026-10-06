# IPCConfig

Namespace: StrikeLink.IPC

Configuration for IPC that specifies whether the Steam client should be started or restarted.

```csharp
public class IPCConfig : System.IEquatable`1[[StrikeLink.IPC.IPCConfig, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCConfig](./strikelink/ipc/ipcconfig.md)<br>
Implements [IEquatable&lt;IPCConfig&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Immutable record used to convey Steam start and restart options over IPC.

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **StartSteam**

Whether to start the Steam client if it is not running.

```csharp
public bool StartSteam { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **RestartSteam**

Whether to restart the Steam client if it is already running.

```csharp
public bool RestartSteam { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **IPCConfig(Boolean, Boolean)**

Configuration for IPC that specifies whether the Steam client should be started or restarted.

```csharp
public IPCConfig(bool StartSteam, bool RestartSteam)
```

#### Parameters

`StartSteam` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to start the Steam client if it is not running.

`RestartSteam` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to restart the Steam client if it is already running.

**Remarks:**

Immutable record used to convey Steam start and restart options over IPC.

### **IPCConfig(IPCConfig)**

```csharp
protected IPCConfig(IPCConfig original)
```

#### Parameters

`original` [IPCConfig](./strikelink/ipc/ipcconfig.md)<br>

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

### **Equals(IPCConfig)**

```csharp
public bool Equals(IPCConfig other)
```

#### Parameters

`other` [IPCConfig](./strikelink/ipc/ipcconfig.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCConfig <Clone>$()
```

#### Returns

[IPCConfig](./strikelink/ipc/ipcconfig.md)<br>

### **Deconstruct(Boolean&, Boolean&)**

```csharp
public void Deconstruct(Boolean& StartSteam, Boolean& RestartSteam)
```

#### Parameters

`StartSteam` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`RestartSteam` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>
