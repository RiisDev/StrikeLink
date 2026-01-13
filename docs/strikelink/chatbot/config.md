# Config

Namespace: StrikeLink.ChatBot

Provides configuration options for the [ChatService](./strikelink/chatbot/chatservice.md).

```csharp
public class Config : System.IEquatable`1[[StrikeLink.ChatBot.Config, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Config](./strikelink/chatbot/config.md)<br>
Implements [IEquatable&lt;Config&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Keybind**

The virtual key used to activate chat input.

```csharp
public VirtualKey Keybind { get; set; }
```

#### Property Value

[VirtualKey](./strikelink/extensions/win32/virtualkey.md)<br>

### **IgnoreLocalUser**

Indicates whether messages sent by the local user should be ignored.

```csharp
public bool IgnoreLocalUser { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **OnGlobalChat**

Optional callback invoked when a global chat message is received.

```csharp
public Action<ChatMessage> OnGlobalChat { get; set; }
```

#### Property Value

[Action&lt;ChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

### **OnTeamChat**

Optional callback invoked when a team chat message is received.

```csharp
public Action<ChatMessage> OnTeamChat { get; set; }
```

#### Property Value

[Action&lt;ChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

## Constructors

### **Config(VirtualKey, Boolean, Action&lt;ChatMessage&gt;, Action&lt;ChatMessage&gt;)**

Provides configuration options for the [ChatService](./strikelink/chatbot/chatservice.md).

```csharp
public Config(VirtualKey Keybind, bool IgnoreLocalUser, Action<ChatMessage> OnGlobalChat, Action<ChatMessage> OnTeamChat)
```

#### Parameters

`Keybind` [VirtualKey](./strikelink/extensions/win32/virtualkey.md)<br>
The virtual key used to activate chat input.

`IgnoreLocalUser` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Indicates whether messages sent by the local user should be ignored.

`OnGlobalChat` [Action&lt;ChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
Optional callback invoked when a global chat message is received.

`OnTeamChat` [Action&lt;ChatMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
Optional callback invoked when a team chat message is received.

### **Config(Config)**

```csharp
protected Config(Config original)
```

#### Parameters

`original` [Config](./strikelink/chatbot/config.md)<br>

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

### **Equals(Config)**

```csharp
public bool Equals(Config other)
```

#### Parameters

`other` [Config](./strikelink/chatbot/config.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Config <Clone>$()
```

#### Returns

[Config](./strikelink/chatbot/config.md)<br>

### **Deconstruct(VirtualKey&, Boolean&, Action`1&, Action`1&)**

```csharp
public void Deconstruct(VirtualKey& Keybind, Boolean& IgnoreLocalUser, Action`1& OnGlobalChat, Action`1& OnTeamChat)
```

#### Parameters

`Keybind` [VirtualKey&](./strikelink/extensions/win32/virtualkey&.md)<br>

`IgnoreLocalUser` [Boolean&](https://docs.microsoft.com/en-us/dotnet/api/system.boolean&)<br>

`OnGlobalChat` [Action`1&](https://docs.microsoft.com/en-us/dotnet/api/system.action-1&)<br>

`OnTeamChat` [Action`1&](https://docs.microsoft.com/en-us/dotnet/api/system.action-1&)<br>
