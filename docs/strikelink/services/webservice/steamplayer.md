# SteamPlayer

Namespace: StrikeLink.Services.WebService

Represents a Steam user with display name, unique Steam ID, and profile URL.

```csharp
public class SteamPlayer : System.IEquatable`1[[StrikeLink.Services.WebService.SteamPlayer, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SteamPlayer](./strikelink/services/webservice/steamplayer.md)<br>
Implements [IEquatable&lt;SteamPlayer&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **DisplayName**

The display name of the Steam user as shown on their profile. Cannot be null.

```csharp
public string DisplayName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SteamId**

The unique Steam identifier for the user. Cannot be null.

```csharp
public string SteamId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SteamMiniId**

```csharp
public string SteamMiniId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProfileUrl**

The URL to the user's Steam profile. Cannot be null.

```csharp
public string ProfileUrl { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **SteamPlayer(String, String, String, String)**

Represents a Steam user with display name, unique Steam ID, and profile URL.

```csharp
public SteamPlayer(string DisplayName, string SteamId, string SteamMiniId, string ProfileUrl)
```

#### Parameters

`DisplayName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the Steam user as shown on their profile. Cannot be null.

`SteamId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique Steam identifier for the user. Cannot be null.

`SteamMiniId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ProfileUrl` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The URL to the user's Steam profile. Cannot be null.

### **SteamPlayer(SteamPlayer)**

```csharp
protected SteamPlayer(SteamPlayer original)
```

#### Parameters

`original` [SteamPlayer](./strikelink/services/webservice/steamplayer.md)<br>

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

### **Equals(SteamPlayer)**

```csharp
public bool Equals(SteamPlayer other)
```

#### Parameters

`other` [SteamPlayer](./strikelink/services/webservice/steamplayer.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public SteamPlayer <Clone>$()
```

#### Returns

[SteamPlayer](./strikelink/services/webservice/steamplayer.md)<br>

### **Deconstruct(String&, String&, String&, String&)**

```csharp
public void Deconstruct(String& DisplayName, String& SteamId, String& SteamMiniId, String& ProfileUrl)
```

#### Parameters

`DisplayName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`SteamId` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`SteamMiniId` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ProfileUrl` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
