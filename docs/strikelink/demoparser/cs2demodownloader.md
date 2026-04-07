# Cs2DemoDownloader

Namespace: StrikeLink.DemoParser

Downloads CS2 demo files directly from replay servers using a match share code.

```csharp
public sealed class Cs2DemoDownloader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Cs2DemoDownloader](./strikelink/demoparser/cs2demodownloader.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class does not use SteamKit or any external package. It uses the replay URL shape:
 https://replay{tvPort}.valve.net/730/{matchId}_{reservationId}.dem.bz2

## Constructors

### **Cs2DemoDownloader()**

```csharp
public Cs2DemoDownloader()
```

## Methods

### **BuildReplayUri(String)**

Builds the replay URL from a CS2 share code.

```csharp
public static Task<Uri> BuildReplayUri(string shareCode)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;Uri&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **DownloadToMemoryAsync(String, CancellationToken)**

Downloads the replay into memory and returns a readable stream.

```csharp
public static Task<DemoDownloadResult> DownloadToMemoryAsync(string shareCode, CancellationToken cancellationToken)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task&lt;DemoDownloadResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **DownloadToTempFileAsync(String, Boolean, CancellationToken)**

Downloads the replay into a temp file and returns a file stream.

```csharp
public static Task<DemoDownloadResult> DownloadToTempFileAsync(string shareCode, bool deleteFileOnClose, CancellationToken cancellationToken)
```

#### Parameters

`shareCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`deleteFileOnClose` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task&lt;DemoDownloadResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

**Remarks:**

Set `deleteFileOnClose` to true when you only need ephemeral storage.
