# Weapon

Namespace: StrikeLink.GSI.ObjectStates

Represents a weapon currently owned by the player.

```csharp
public class Weapon : System.IEquatable`1[[StrikeLink.GSI.ObjectStates.Weapon, StrikeLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Weapon](./strikelink/gsi/objectstates/weapon.md)<br>
Implements [IEquatable&lt;Weapon&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **EqualityContract**

```csharp
protected Type EqualityContract { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Slot**

The inventory slot number of the weapon.

```csharp
public int Slot { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Name**

The specific weapon type. [WeaponType](./strikelink/gsi/objectstates/weapontype.md)

```csharp
public WeaponType Name { get; set; }
```

#### Property Value

[WeaponType](./strikelink/gsi/objectstates/weapontype.md)<br>

### **PaintKit**

The applied cosmetic paint kit identifier.

```csharp
public string PaintKit { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Type**

The general weapon category. [HeldType](./strikelink/gsi/objectstates/heldtype.md)

```csharp
public HeldType Type { get; set; }
```

#### Property Value

[HeldType](./strikelink/gsi/objectstates/heldtype.md)<br>

### **State**

The current held state of the weapon. [HeldState](./strikelink/gsi/objectstates/heldstate.md)

```csharp
public HeldState State { get; set; }
```

#### Property Value

[HeldState](./strikelink/gsi/objectstates/heldstate.md)<br>

### **AmmoClip**

The current ammunition in the magazine, if applicable.

```csharp
public Nullable<int> AmmoClip { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AmmoClipMax**

The maximum magazine capacity, if applicable.

```csharp
public Nullable<int> AmmoClipMax { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **AmmoReserve**

The remaining reserve ammunition, if applicable.

```csharp
public Nullable<int> AmmoReserve { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **Weapon(Int32, WeaponType, String, HeldType, HeldState, Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;, Nullable&lt;Int32&gt;)**

Represents a weapon currently owned by the player.

```csharp
public Weapon(int Slot, WeaponType Name, string PaintKit, HeldType Type, HeldState State, Nullable<int> AmmoClip, Nullable<int> AmmoClipMax, Nullable<int> AmmoReserve)
```

#### Parameters

`Slot` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The inventory slot number of the weapon.

`Name` [WeaponType](./strikelink/gsi/objectstates/weapontype.md)<br>
The specific weapon type. [WeaponType](./strikelink/gsi/objectstates/weapontype.md)

`PaintKit` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The applied cosmetic paint kit identifier.

`Type` [HeldType](./strikelink/gsi/objectstates/heldtype.md)<br>
The general weapon category. [HeldType](./strikelink/gsi/objectstates/heldtype.md)

`State` [HeldState](./strikelink/gsi/objectstates/heldstate.md)<br>
The current held state of the weapon. [HeldState](./strikelink/gsi/objectstates/heldstate.md)

`AmmoClip` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The current ammunition in the magazine, if applicable.

`AmmoClipMax` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The maximum magazine capacity, if applicable.

`AmmoReserve` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The remaining reserve ammunition, if applicable.

### **Weapon(Weapon)**

```csharp
protected Weapon(Weapon original)
```

#### Parameters

`original` [Weapon](./strikelink/gsi/objectstates/weapon.md)<br>

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

### **Equals(Weapon)**

```csharp
public bool Equals(Weapon other)
```

#### Parameters

`other` [Weapon](./strikelink/gsi/objectstates/weapon.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public Weapon <Clone>$()
```

#### Returns

[Weapon](./strikelink/gsi/objectstates/weapon.md)<br>

### **Deconstruct(Int32&, WeaponType&, String&, HeldType&, HeldState&, Nullable`1&, Nullable`1&, Nullable`1&)**

```csharp
public void Deconstruct(Int32& Slot, WeaponType& Name, String& PaintKit, HeldType& Type, HeldState& State, Nullable`1& AmmoClip, Nullable`1& AmmoClipMax, Nullable`1& AmmoReserve)
```

#### Parameters

`Slot` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`Name` [WeaponType&](./strikelink/gsi/objectstates/weapontype&.md)<br>

`PaintKit` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`Type` [HeldType&](./strikelink/gsi/objectstates/heldtype&.md)<br>

`State` [HeldState&](./strikelink/gsi/objectstates/heldstate&.md)<br>

`AmmoClip` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`AmmoClipMax` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>

`AmmoReserve` [Nullable`1&](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1&)<br>
