# PlayerEconomySnapshot

Namespace: StrikeLink.DemoParser.Parsing

```csharp
public sealed class PlayerEconomySnapshot : System.IEquatable`1[[StrikeLink.DemoParser.Parsing.PlayerEconomySnapshot, StrikeLink, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerEconomySnapshot](./strikelink/demoparser/parsing/playereconomysnapshot.md)<br>
Implements [IEquatable&lt;PlayerEconomySnapshot&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **SteamId**

```csharp
public ulong SteamId { get; set; }
```

#### Property Value

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **PlayerName**

```csharp
public string PlayerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Money**

```csharp
public Nullable<int> Money { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **PlayerEconomySnapshot(UInt64, String, Nullable&lt;Int32&gt;)**

```csharp
public PlayerEconomySnapshot(ulong SteamId, string PlayerName, Nullable<int> Money)
```

#### Parameters

`SteamId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`PlayerName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`Money` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **Equals(PlayerEconomySnapshot)**

```csharp
public bool Equals(PlayerEconomySnapshot other)
```

#### Parameters

`other` [PlayerEconomySnapshot](./strikelink/demoparser/parsing/playereconomysnapshot.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public PlayerEconomySnapshot <Clone>$()
```

#### Returns

[PlayerEconomySnapshot](./strikelink/demoparser/parsing/playereconomysnapshot.md)<br>

### **Deconstruct(UInt64&, String&, Nullable`1&)**

```csharp
public void Deconstruct(UInt64& SteamId, String& PlayerName, Nullable`1& Money)
```

#### Parameters

`SteamId` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

`PlayerName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Money` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
