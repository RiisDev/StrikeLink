# CrosshairSettings

Namespace: StrikeLink.Crosshair

CS2 crosshair configuration, one property per console variable, in the units the game and the
 `CS...` share code use (lengths, thickness and gap are whole pixels).

```csharp
public sealed record class CrosshairSettings : System.IEquatable`1[[StrikeLink.Crosshair.CrosshairSettings, StrikeLink, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
Implements [IEquatable&lt;CrosshairSettings&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Style**

`cl_crosshairstyle`. 0-31 fits the share code.

```csharp
public CrosshairStyle Style { get; init; }
```

#### Property Value

[CrosshairStyle](./strikelink/crosshair/crosshairstyle.md)<br>

### **FollowRecoil**

`cl_crosshair_recoil`.

```csharp
public bool FollowRecoil { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Dot**

`cl_crosshairdot`.

```csharp
public bool Dot { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Length**

`cl_crosshair_length` in pixels, 0-255.

```csharp
public int Length { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Thickness**

`cl_crosshair_thickness` in pixels, 0-32.

```csharp
public int Thickness { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Gap**

`cl_crosshair_gap` in pixels, -3840 to 3840.

```csharp
public int Gap { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Outline**

`cl_crosshair_drawoutline`.

```csharp
public CrosshairOutline Outline { get; init; }
```

#### Property Value

[CrosshairOutline](./strikelink/crosshair/crosshairoutline.md)<br>

### **OutlineRed**

`cl_crosshairoutline_r`, 0-255.

```csharp
public int OutlineRed { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **OutlineGreen**

`cl_crosshairoutline_g`, 0-255.

```csharp
public int OutlineGreen { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **OutlineBlue**

`cl_crosshairoutline_b`, 0-255.

```csharp
public int OutlineBlue { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **OutlineAlpha**

`cl_crosshairoutline_a`, 0-255.

```csharp
public int OutlineAlpha { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Red**

`cl_crosshaircolor_r`, 0-255.

```csharp
public int Red { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Green**

`cl_crosshaircolor_g`, 0-255.

```csharp
public int Green { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Blue**

`cl_crosshaircolor_b`, 0-255.

```csharp
public int Blue { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Alpha**

`cl_crosshaircolor_a`, 0-255.

```csharp
public int Alpha { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TStyle**

`cl_crosshair_t`.

```csharp
public bool TStyle { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SplitDistance**

`cl_crosshair_dynamic_splitdist`, 0-127.

```csharp
public int SplitDistance { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **InnerSplitAlpha**

`cl_crosshair_dynamic_splitalpha_innermod`, 0-1 in 0.01 steps.

```csharp
public double InnerSplitAlpha { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OuterSplitAlpha**

`cl_crosshair_dynamic_splitalpha_outermod`, 0.3-1 in 0.01 steps.

```csharp
public double OuterSplitAlpha { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SplitSizeRatio**

`cl_crosshair_dynamic_maxdist_splitratio`, 0-1 in 0.01 steps.

```csharp
public double SplitSizeRatio { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SpreadLimit**

`cl_crosshair_dynamic_spread_limit`, 0-255.

```csharp
public int SpreadLimit { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ScreenHeight**

`cl_crosshair_screen_height`, 240-65535.

```csharp
public int ScreenHeight { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ScopeDotUsesCrosshairColor**

`cl_ironsight_usecrosshaircolor`.

```csharp
public bool ScopeDotUsesCrosshairColor { get; init; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ScopeDotScale**

`cl_ironsight_dot_scale`, 0.1-2 in 0.01 steps.

```csharp
public double ScopeDotScale { get; init; }
```

#### Property Value

[Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

### **SniperWidth**

`cl_crosshair_sniper_width`. Applied through [CrosshairService.ApplySettingsAsync(CrosshairSettings, ConsoleServiceConfig, CancellationToken)](./strikelink/crosshair/crosshairservice.md#applysettingsasynccrosshairsettings-consoleserviceconfig-cancellationtoken) only, the share code does not carry it.

```csharp
public int SniperWidth { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **CrosshairSettings()**

```csharp
public CrosshairSettings()
```
