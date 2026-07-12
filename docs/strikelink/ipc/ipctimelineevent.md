# IPCTimelineEvent

Namespace: StrikeLink.IPC

Represents a Steam timeline event emitted by CS2 (for example, `cs2_gun_kill` or `cs2_death`).

```csharp
public class IPCTimelineEvent : System.IEquatable`1[[StrikeLink.IPC.IPCTimelineEvent, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCTimelineEvent](./strikelink/ipc/ipctimelineevent.md)<br>
Implements [IEquatable&lt;IPCTimelineEvent&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Text**

The primary label of the timeline event.

```csharp
public string Text { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SubText**

Additional detail text attached to the event.

```csharp
public string SubText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Type**

The event type identifier (for example, `cs2_gun_kill`).

```csharp
public string Type { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCTimelineEvent(String, String, String)**

Represents a Steam timeline event emitted by CS2 (for example, `cs2_gun_kill` or `cs2_death`).

```csharp
public IPCTimelineEvent(string Text, string SubText, string Type)
```

#### Parameters

`Text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The primary label of the timeline event.

`SubText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Additional detail text attached to the event.

`Type` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The event type identifier (for example, `cs2_gun_kill`).

### **IPCTimelineEvent(IPCTimelineEvent)**

```csharp
protected IPCTimelineEvent(IPCTimelineEvent original)
```

#### Parameters

`original` [IPCTimelineEvent](./strikelink/ipc/ipctimelineevent.md)<br>

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

### **Equals(IPCTimelineEvent)**

```csharp
public bool Equals(IPCTimelineEvent other)
```

#### Parameters

`other` [IPCTimelineEvent](./strikelink/ipc/ipctimelineevent.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCTimelineEvent <Clone>$()
```

#### Returns

[IPCTimelineEvent](./strikelink/ipc/ipctimelineevent.md)<br>

### **Deconstruct(String&, String&, String&)**

```csharp
public void Deconstruct(String& Text, String& SubText, String& Type)
```

#### Parameters

`Text` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`SubText` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Type` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
