# IPCUser

Namespace: StrikeLink.IPC

Represents a user used in inter-process communication, containing a Steam account identifier and a username.

```csharp
public class IPCUser : System.IEquatable`1[[StrikeLink.IPC.IPCUser, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IPCUser](./strikelink/ipc/ipcuser.md)<br>
Implements [IEquatable&lt;IPCUser&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Immutable record with value-based equality; intended as a compact data transfer object for
 IPC.

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **SteamId3**

SteamId3 of the user

```csharp
public int SteamId3 { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Username**

Display name of the user.

```csharp
public string Username { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **IPCUser(Int32, String)**

Represents a user used in inter-process communication, containing a Steam account identifier and a username.

```csharp
public IPCUser(int SteamId3, string Username)
```

#### Parameters

`SteamId3` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
SteamId3 of the user

`Username` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Display name of the user.

**Remarks:**

Immutable record with value-based equality; intended as a compact data transfer object for
 IPC.

### **IPCUser(IPCUser)**

```csharp
protected IPCUser(IPCUser original)
```

#### Parameters

`original` [IPCUser](./strikelink/ipc/ipcuser.md)<br>

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

### **Equals(IPCUser)**

```csharp
public bool Equals(IPCUser other)
```

#### Parameters

`other` [IPCUser](./strikelink/ipc/ipcuser.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public IPCUser <Clone>$()
```

#### Returns

[IPCUser](./strikelink/ipc/ipcuser.md)<br>

### **Deconstruct(Int32&, String&)**

```csharp
public void Deconstruct(Int32& SteamId3, String& Username)
```

#### Parameters

`SteamId3` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Username` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
