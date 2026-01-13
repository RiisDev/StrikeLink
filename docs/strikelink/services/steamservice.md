# SteamService

Namespace: StrikeLink.Services

Provides utility methods for interacting with the local Steam installation,
 including resolving install paths, game locations, user configuration, and launch options.

```csharp
public static class SteamService
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SteamService](./strikelink/services/steamservice.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Methods

### **GetSteamPath()**

Gets the root installation path of Steam for the current operating system.

```csharp
public static string GetSteamPath()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The absolute path to the Steam installation directory.

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
Thrown when Steam cannot be located.

[KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keynotfoundexception)<br>
Thrown when the Steam registry key or path value cannot be found on Windows.

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the operating system is unsupported.

### **TryGetGamePath(Int32, String&)**

Attempts to locate the installation directory of a Steam game.

```csharp
public static bool TryGetGamePath(int gameId, String& gamePath)
```

#### Parameters

`gameId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The Steam application ID of the game.

`gamePath` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
When this method returns, contains the absolute path to the game directory if found;
 otherwise, `null`.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if the game was found in a Steam library folder; otherwise, `false`.

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the Steam library configuration file cannot be found.

### **GetGamePath(Int32)**

Gets the installation directory of a Steam game.

```csharp
public static string GetGamePath(int gameId)
```

#### Parameters

`gameId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The Steam application ID of the game.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The absolute path to the game's installation directory.

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
Thrown when the game cannot be found in any Steam library folder.

### **GetUserConfig(Nullable&lt;Int64&gt;)**

Gets the Steam user configuration for the specified or currently active user.

```csharp
public static ValveCfgReader GetUserConfig(Nullable<long> userId)
```

#### Parameters

`userId` [Nullable&lt;Int64&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Optional Steam user ID. If `null`, the currently active user is inferred.

#### Returns

[ValveCfgReader](./strikelink/services/config/valvecfgreader.md)<br>
A [ValveCfgReader](./strikelink/services/config/valvecfgreader.md) for the user's local Steam configuration file.

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the user configuration file cannot be found.

### **GetGameLaunchOptions(Int32)**

Gets the configured launch options for a Steam game.

```csharp
public static String[] GetGameLaunchOptions(int gameId)
```

#### Parameters

`gameId` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The Steam application ID of the game.

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
An array of individual launch options, each prefixed with a dash.
 Returns an empty array if no launch options are configured.

#### Exceptions

[KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keynotfoundexception)<br>
Thrown when the game entry or launch options cannot be found in the user configuration.

### **GetCurrentUserId()**

Attempts to determine the currently logged-in Steam user ID by reading the Steam connection log.

```csharp
public static long GetCurrentUserId()
```

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The Steam ID of the currently active user.

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown when the Steam connection log cannot be found.

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when no valid Steam ID can be extracted from the log file.
