# IPCKill

Namespace: StrikeLink.IPC

Represents a kill made by the local player, identifying the victim and the weapon used.

```csharp
public class IPCKill : System.IEquatable`1[[StrikeLink.IPC.IPCKill, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCKill](./strikelink/ipc/ipckill.md)<br>
Implements [IEquatable&lt;IPCKill&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Victim**

The player who was killed.

```csharp
public IPCUser Victim { get; set; }
```

#### Property Value

[IPCUser](./strikelink/ipc/ipcuser.md)<br>

### **Weapon**

The weapon used to secure the kill.

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCKill(IPCUser, String)**

Represents a kill made by the local player, identifying the victim and the weapon used.

```csharp
public IPCKill(IPCUser Victim, string Weapon)
```

#### Parameters

`Victim` [IPCUser](./strikelink/ipc/ipcuser.md)<br>
The player who was killed.

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The weapon used to secure the kill.

### **IPCKill(IPCKill)**

```csharp
protected IPCKill(IPCKill original)
```

#### Parameters

`original` [IPCKill](./strikelink/ipc/ipckill.md)<br>

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

### **Equals(IPCKill)**

```csharp
public bool Equals(IPCKill other)
```

#### Parameters

`other` [IPCKill](./strikelink/ipc/ipckill.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCKill <Clone>$()
```

#### Returns

[IPCKill](./strikelink/ipc/ipckill.md)<br>

### **Deconstruct(IPCUser&, String&)**

```csharp
public void Deconstruct(IPCUser& Victim, String& Weapon)
```

#### Parameters

`Victim` [IPCUser&](./strikelink/ipc/ipcuser&.md)<br>

`Weapon` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
