# PlayerState

Namespace: StrikeLink.GSI.ObjectStates

Represents the current state of a player as reported by GSI.

```csharp
public class PlayerState : StrikeLink.GSI.IGsiPayload, System.IEquatable`1[[StrikeLink.GSI.ObjectStates.PlayerState, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PlayerState](./strikelink/gsi/objectstates/playerstate.md)<br>
Implements [IGsiPayload](./strikelink/gsi/igsipayload.md), [IEquatable&lt;PlayerState&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **SteamId**

The Steam ID of the player.

```csharp
public string SteamId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

The display name of the player.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Clan**

The clan tag of the player, if present.

```csharp
public string Clan { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ObserverSlot**

The observer slot index, if applicable.

```csharp
public Nullable<int> ObserverSlot { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Team**

The team the player belongs to. [PlayerState.Team](./strikelink/gsi/objectstates/playerstate.md#team)

```csharp
public Nullable<Team> Team { get; set; }
```

#### Property Value

[Nullable&lt;Team&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Activity**

The player's current activity state. [PlayerState.Activity](./strikelink/gsi/objectstates/playerstate.md#activity)

```csharp
public Activity Activity { get; set; }
```

#### Property Value

[Activity](./strikelink/gsi/objectstates/activity.md)<br>

### **MatchStats**

Match-level statistics for the player. [PlayerState.MatchStats](./strikelink/gsi/objectstates/playerstate.md#matchstats)

```csharp
public MatchStats MatchStats { get; set; }
```

#### Property Value

[MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>

### **Vitals**

Current vital and round-specific statistics. [PlayerState.Vitals](./strikelink/gsi/objectstates/playerstate.md#vitals)

```csharp
public Vitals Vitals { get; set; }
```

#### Property Value

[Vitals](./strikelink/gsi/objectstates/vitals.md)<br>

### **Weapons**

The list of weapons currently owned by the player. [Weapon](./strikelink/gsi/objectstates/weapon.md)

```csharp
public IReadOnlyList<Weapon> Weapons { get; set; }
```

#### Property Value

[IReadOnlyList&lt;Weapon&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

## Constructors

### **PlayerState(String, String, String, Nullable&lt;Int32&gt;, Nullable&lt;Team&gt;, Activity, MatchStats, Vitals, IReadOnlyList&lt;Weapon&gt;)**

Represents the current state of a player as reported by GSI.

```csharp
public PlayerState(string SteamId, string Name, string Clan, Nullable<int> ObserverSlot, Nullable<Team> Team, Activity Activity, MatchStats MatchStats, Vitals Vitals, IReadOnlyList<Weapon> Weapons)
```

#### Parameters

`SteamId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Steam ID of the player.

`Name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the player.

`Clan` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The clan tag of the player, if present.

`ObserverSlot` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The observer slot index, if applicable.

`Team` [Nullable&lt;Team&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The team the player belongs to. [PlayerState.Team](./strikelink/gsi/objectstates/playerstate.md#team)

`Activity` [Activity](./strikelink/gsi/objectstates/activity.md)<br>
The player's current activity state. [PlayerState.Activity](./strikelink/gsi/objectstates/playerstate.md#activity)

`MatchStats` [MatchStats](./strikelink/gsi/objectstates/matchstats.md)<br>
Match-level statistics for the player. [PlayerState.MatchStats](./strikelink/gsi/objectstates/playerstate.md#matchstats)

`Vitals` [Vitals](./strikelink/gsi/objectstates/vitals.md)<br>
Current vital and round-specific statistics. [PlayerState.Vitals](./strikelink/gsi/objectstates/playerstate.md#vitals)

`Weapons` [IReadOnlyList&lt;Weapon&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
The list of weapons currently owned by the player. [Weapon](./strikelink/gsi/objectstates/weapon.md)

### **PlayerState(PlayerState)**

```csharp
protected PlayerState(PlayerState original)
```

#### Parameters

`original` [PlayerState](./strikelink/gsi/objectstates/playerstate.md)<br>

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

### **Equals(PlayerState)**

```csharp
public bool Equals(PlayerState other)
```

#### Parameters

`other` [PlayerState](./strikelink/gsi/objectstates/playerstate.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public PlayerState <Clone>$()
```

#### Returns

[PlayerState](./strikelink/gsi/objectstates/playerstate.md)<br>

### **Deconstruct(String&, String&, String&, Nullable`1&, Nullable`1&, Activity&, MatchStats&, Vitals&, IReadOnlyList`1&)**

```csharp
public void Deconstruct(String& SteamId, String& Name, String& Clan, Nullable`1& ObserverSlot, Nullable`1& Team, Activity& Activity, MatchStats& MatchStats, Vitals& Vitals, IReadOnlyList`1& Weapons)
```

#### Parameters

`SteamId` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Name` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Clan` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`ObserverSlot` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Team` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`Activity` [Activity&](./strikelink/gsi/objectstates/activity&.md)<br>

`MatchStats` [MatchStats&](./strikelink/gsi/objectstates/matchstats&.md)<br>

`Vitals` [Vitals&](./strikelink/gsi/objectstates/vitals&.md)<br>

`Weapons` [IReadOnlyList`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1&)<br>
