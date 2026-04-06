# CsRankNames

Namespace: StrikeLink.DemoParser.Parsing

Provides a mapping of competitive rank IDs to their corresponding display names for Counter-Strike ranks.

```csharp
public static class CsRankNames
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CsRankNames](./strikelink/demoparser/parsing/csranknames.md)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

This class offers a centralized source for retrieving the display name of a rank based on its
 integer identifier. It is intended for use in scenarios where rank names need to be displayed or processed based on
 their numeric representation.

## Fields

### **Names**

Provides a read-only mapping of rank identifiers to their corresponding rank names.

```csharp
public static IReadOnlyDictionary<int, string> Names;
```

**Remarks:**

The dictionary contains predefined rank names indexed by integer values. The mapping is immutable
 and can be used to retrieve the display name for a given rank identifier.

## Methods

### **GetName(Nullable&lt;Int32&gt;)**

Retrieves the name associated with the specified rank identifier.

```csharp
public static string GetName(Nullable<int> rankId)
```

#### Parameters

`rankId` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The identifier of the rank to look up. If null, the method returns "Unknown".

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name corresponding to the specified rank identifier if found; otherwise, "Unknown".
