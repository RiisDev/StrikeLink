# IPCDeath

Namespace: StrikeLink.IPC

Represents a death event, identifying the killer and the weapon used.

```csharp
public class IPCDeath : System.IEquatable`1[[StrikeLink.IPC.IPCDeath, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCDeath](./strikelink/ipc/ipcdeath.md)<br>
Implements [IEquatable&lt;IPCDeath&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Killer**

The player who caused the death.

```csharp
public IPCUser Killer { get; set; }
```

#### Property Value

[IPCUser](./strikelink/ipc/ipcuser.md)<br>

### **Weapon**

The weapon used to kill the local player.

```csharp
public string Weapon { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCDeath(IPCUser, String)**

Represents a death event, identifying the killer and the weapon used.

```csharp
public IPCDeath(IPCUser Killer, string Weapon)
```

#### Parameters

`Killer` [IPCUser](./strikelink/ipc/ipcuser.md)<br>
The player who caused the death.

`Weapon` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The weapon used to kill the local player.

### **IPCDeath(IPCDeath)**

```csharp
protected IPCDeath(IPCDeath original)
```

#### Parameters

`original` [IPCDeath](./strikelink/ipc/ipcdeath.md)<br>

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

### **Equals(IPCDeath)**

```csharp
public bool Equals(IPCDeath other)
```

#### Parameters

`other` [IPCDeath](./strikelink/ipc/ipcdeath.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCDeath <Clone>$()
```

#### Returns

[IPCDeath](./strikelink/ipc/ipcdeath.md)<br>

### **Deconstruct(IPCUser&, String&)**

```csharp
public void Deconstruct(IPCUser& Killer, String& Weapon)
```

#### Parameters

`Killer` [IPCUser&](./strikelink/ipc/ipcuser&.md)<br>

`Weapon` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
