# SpawnGroup

Namespace: StrikeLink.Services

Represents a group of spawn entities with associated metadata for a specific map and lump type.

```csharp
public class SpawnGroup : System.IEquatable`1[[StrikeLink.Services.SpawnGroup, StrikeLink, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SpawnGroup](./strikelink/services/spawngroup.md)<br>
Implements [IEquatable&lt;SpawnGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Id**

The unique identifier for the spawn group.

```csharp
public int Id { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MapName**

The name of the map to which this spawn group belongs. Cannot be null.

```csharp
public string MapName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LumpType**

The type of lump associated with this spawn group. Cannot be null.

```csharp
public string LumpType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LoadType**

The load type that determines how the spawn group is processed. Cannot be null.

```csharp
public string LoadType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Flags**

A read-only list of flags that define additional properties or behaviors for the spawn group. Cannot be null.

```csharp
public IReadOnlyList<string> Flags { get; set; }
```

#### Property Value

[IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **SpawnGroup(Int32, String, String, String, IReadOnlyList&lt;String&gt;)**

Represents a group of spawn entities with associated metadata for a specific map and lump type.

```csharp
public SpawnGroup(int Id, string MapName, string LumpType, string LoadType, IReadOnlyList<string> Flags)
```

#### Parameters

`Id` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The unique identifier for the spawn group.

`MapName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the map to which this spawn group belongs. Cannot be null.

`LumpType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The type of lump associated with this spawn group. Cannot be null.

`LoadType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The load type that determines how the spawn group is processed. Cannot be null.

`Flags` [IReadOnlyList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
A read-only list of flags that define additional properties or behaviors for the spawn group. Cannot be null.

### **SpawnGroup(SpawnGroup)**

```csharp
protected SpawnGroup(SpawnGroup original)
```

#### Parameters

`original` [SpawnGroup](./strikelink/services/spawngroup.md)<br>

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

### **Equals(SpawnGroup)**

```csharp
public bool Equals(SpawnGroup other)
```

#### Parameters

`other` [SpawnGroup](./strikelink/services/spawngroup.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public SpawnGroup <Clone>$()
```

#### Returns

[SpawnGroup](./strikelink/services/spawngroup.md)<br>

### **Deconstruct(Int32&, String&, String&, String&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(Int32& Id, String& MapName, String& LumpType, String& LoadType, IReadOnlyList`1& Flags)
```

#### Parameters

`Id` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`MapName` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`LumpType` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`LoadType` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Flags` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
