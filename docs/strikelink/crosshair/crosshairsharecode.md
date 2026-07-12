# CrosshairShareCode

Namespace: StrikeLink.Crosshair

Encodes and decodes CS2 crosshair share codes in the `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX` format.

```csharp
public static class CrosshairShareCode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CrosshairShareCode](./strikelink/crosshair/crosshairsharecode.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Share codes are Base57-encoded representations of 18 bytes of packed crosshair cvar data.
 The alphabet used is `ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789`
 (standard Base57 — no `I`, `O`, `g`, or `l`).

## Methods

### **Decode(String)**

Decodes a CS2 crosshair share code into a [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md) record.

```csharp
public static CrosshairSettings Decode(string shareCode)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A share code in the format `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`.

#### Returns

[CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The decoded [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md).

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the share code format is invalid or contains unknown characters.

### **Encode(CrosshairSettings)**

Encodes a [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md) record into a CS2 crosshair share code.

```csharp
public static string Encode(CrosshairSettings settings)
```

#### Parameters

`settings` [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The crosshair settings to encode.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A share code string in the format `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when `settings` is .

### **IsValid(String)**

Returns  if the given string is a syntactically valid share code.

```csharp
public static bool IsValid(string shareCode)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string to validate.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
