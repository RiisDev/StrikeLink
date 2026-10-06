# CrosshairSettings

Namespace: StrikeLink.Crosshair

Represents the full set of CS2 crosshair configuration values that map directly to in-game console variables.

```csharp
public sealed class CrosshairSettings : System.IEquatable`1[[StrikeLink.Crosshair.CrosshairSettings, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
Implements [IEquatable&lt;CrosshairSettings&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This record is used with [CrosshairShareCode](./strikelink/crosshair/crosshairsharecode.md) to encode and decode `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`
 share codes, and with [CrosshairService](./strikelink/crosshair/crosshairservice.md) to apply, read, and export crosshair settings.

## Properties

### **Size**

Gets the crosshair size (`cl_crosshairsize`). Default is `2.0`.

```csharp
public float Size { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Gap**

Gets the crosshair gap (`cl_crosshairgap`). Negative values bring lines closer together. Default is `-3.0`.

```csharp
public float Gap { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Thickness**

Gets the crosshair line thickness (`cl_crosshairthickness`). Default is `0.5`.

```csharp
public float Thickness { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Alpha**

Gets the crosshair alpha transparency (`cl_crosshairalpha`). Range 0–255. Default is `200`.

```csharp
public int Alpha { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Color**

Gets the preset crosshair color (`cl_crosshaircolor`). Default is [CrosshairColor.Green](./strikelink/crosshair/crosshaircolor.md#green).

```csharp
public CrosshairColor Color { get; set; }
```

#### Property Value

[CrosshairColor](./strikelink/crosshair/crosshaircolor.md)<br>

### **CustomColorR**

Gets the custom red channel (`cl_crosshaircolor_r`). Used when [CrosshairSettings.Color](./strikelink/crosshair/crosshairsettings.md#color) is [CrosshairColor.Custom](./strikelink/crosshair/crosshaircolor.md#custom). Range 0–255.

```csharp
public int CustomColorR { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CustomColorG**

Gets the custom green channel (`cl_crosshaircolor_g`). Used when [CrosshairSettings.Color](./strikelink/crosshair/crosshairsettings.md#color) is [CrosshairColor.Custom](./strikelink/crosshair/crosshaircolor.md#custom). Range 0–255.

```csharp
public int CustomColorG { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CustomColorB**

Gets the custom blue channel (`cl_crosshaircolor_b`). Used when [CrosshairSettings.Color](./strikelink/crosshair/crosshairsettings.md#color) is [CrosshairColor.Custom](./strikelink/crosshair/crosshaircolor.md#custom). Range 0–255.

```csharp
public int CustomColorB { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Dot**

Gets a value indicating whether the center dot is shown (`cl_crosshairdot`). Default is `false`.

```csharp
public bool Dot { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TStyle**

Gets a value indicating whether the crosshair uses T-style (no top line) (`cl_crosshair_t`). Default is `false`.

```csharp
public bool TStyle { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **DrawOutline**

Gets a value indicating whether an outline is drawn around the crosshair lines (`cl_crosshair_drawoutline`). Default is `false`.

```csharp
public bool DrawOutline { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **OutlineThickness**

Gets the outline thickness (`cl_crosshair_outlinethickness`). Range 0.0–3.0. Default is `1.0`.

```csharp
public float OutlineThickness { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **UseAlpha**

Gets a value indicating whether alpha transparency is applied (`cl_crosshairusealpha`). Default is `true`.

```csharp
public bool UseAlpha { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **UseWeaponGap**

Gets a value indicating whether the crosshair gap scales with the equipped weapon (`cl_crosshairgap_useweaponvalue`). Default is `false`.

```csharp
public bool UseWeaponGap { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SniperWidth**

Gets the sniper scope crosshair width (`cl_crosshair_sniper_width`). Default is `1.0`.

```csharp
public float SniperWidth { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

## Constructors

### **CrosshairSettings()**

```csharp
public CrosshairSettings()
```

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

### **Equals(CrosshairSettings)**

```csharp
public bool Equals(CrosshairSettings other)
```

#### Parameters

`other` [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **&lt;Clone&gt;$()**

```csharp
public CrosshairSettings <Clone>$()
```

#### Returns

[CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
