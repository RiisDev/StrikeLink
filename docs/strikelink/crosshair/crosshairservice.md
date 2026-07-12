# CrosshairService

Namespace: StrikeLink.Crosshair

Provides crosshair import, export, and application for CS2.

```csharp
public class CrosshairService
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CrosshairService](./strikelink/crosshair/crosshairservice.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Crosshair settings can be applied in two ways:

- - [CrosshairService.ApplyShareCodeAsync(String, ConsoleServiceConfig, CancellationToken)](./strikelink/crosshair/crosshairservice.md#applysharecodeasyncstring-consoleserviceconfig-cancellationtoken) — sends `cl_crosshaircode CSGO-...` via the console,
 which is the same mechanism CS2 uses natively when importing a share code.
- - [CrosshairService.ApplySettingsAsync(CrosshairSettings, ConsoleServiceConfig, CancellationToken)](./strikelink/crosshair/crosshairservice.md#applysettingsasynccrosshairsettings-consoleserviceconfig-cancellationtoken) — writes every individual cvar to a temporary cfg file
 and execs it, giving full control over each setting independently.

Current crosshair settings can be read from the user's saved `cs2_user_convars*.vcfg` file without
 requiring CS2 to be running.

## Constructors

### **CrosshairService(ConsoleService)**

Initializes a new instance of the [CrosshairService](./strikelink/crosshair/crosshairservice.md) class.

```csharp
public CrosshairService(ConsoleService console)
```

#### Parameters

`console` [ConsoleService](./strikelink/services/consoleservice.md)<br>
A [ConsoleService](./strikelink/services/consoleservice.md) instance used to send commands to CS2.

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
Thrown when the CS2 installation directory cannot be located.

## Methods

### **ToShareCode(CrosshairSettings)**

Encodes a [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md) record into a CS2 share code string.

```csharp
public static string ToShareCode(CrosshairSettings settings)
```

#### Parameters

`settings` [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The settings to encode.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A share code in the format `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`.

### **FromShareCode(String)**

Decodes a CS2 share code into a [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md) record.

```csharp
public static CrosshairSettings FromShareCode(string shareCode)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A share code in the format `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`.

#### Returns

[CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The decoded [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md).

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the share code is invalid.

### **ApplyShareCodeAsync(String, ConsoleServiceConfig, CancellationToken)**

Applies a crosshair share code in CS2 via the `cl_crosshaircode` console command.

```csharp
public Task ApplyShareCodeAsync(string shareCode, ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A valid share code in the format `CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX`.

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional console configuration; uses the instance default when .

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the share code format is invalid.

### **ApplyShareCodeAsync(CrosshairSettings, ConsoleServiceConfig, CancellationToken)**

Encodes the given settings into a share code and applies it in CS2 via `cl_crosshaircode`.

```csharp
public Task ApplyShareCodeAsync(CrosshairSettings settings, ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`settings` [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The settings to apply.

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional console configuration; uses the instance default when .

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **ApplySettingsAsync(CrosshairSettings, ConsoleServiceConfig, CancellationToken)**

Applies all crosshair settings individually by writing a temporary cfg file and executing it via `exec`.

```csharp
public Task ApplySettingsAsync(CrosshairSettings settings, ConsoleServiceConfig config, CancellationToken ct)
```

#### Parameters

`settings` [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The settings to apply.

`config` [ConsoleServiceConfig](./strikelink/chatbot/consoleserviceconfig.md)<br>
Optional console configuration; uses the instance default when .

`ct` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

**Remarks:**

This writes `strike_link_crosshair.cfg` to the CS2 cfg directory and execs it.
 Use this method when you need per-cvar control rather than share-code import.

### **ReadCurrentShareCode(Nullable&lt;Int64&gt;)**

Reads the current crosshair share code from the user's saved CS2 convars file.

```csharp
public string ReadCurrentShareCode(Nullable<long> userId)
```

#### Parameters

`userId` [Nullable&lt;Int64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam account ID to read from; uses the current user when .

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The stored share code, or  if the convars file or key is not found.

### **ReadCurrentSettings(Nullable&lt;Int64&gt;)**

Reads and decodes the current crosshair settings from the user's saved CS2 convars file.

```csharp
public CrosshairSettings ReadCurrentSettings(Nullable<long> userId)
```

#### Parameters

`userId` [Nullable&lt;Int64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The Steam account ID to read from; uses the current user when .

#### Returns

[CrosshairSettings](./strikelink/crosshair/crosshairsettings.md)<br>
The decoded [CrosshairSettings](./strikelink/crosshair/crosshairsettings.md), or  if no share code is stored
 or it cannot be decoded.
