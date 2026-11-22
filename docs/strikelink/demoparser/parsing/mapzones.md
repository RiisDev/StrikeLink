# MapZones

Namespace: StrikeLink.DemoParser.Parsing

Maps world positions to coarse A, B and Mid regions using per-map bounding boxes.

```csharp
public static class MapZones
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [MapZones](./strikelink/demoparser/parsing/mapzones.md)

## Methods

### **Find(String, PlayerPosition)**

The region containing a position on a map (name with or without the de_ prefix), or [MapZone.None](./strikelink/demoparser/parsing/mapzone.md#none).

```csharp
public static MapZone Find(string? map, PlayerPosition position)
```

#### Parameters

`map` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>

`position` [PlayerPosition](./strikelink/demoparser/parsing/playerposition.md)<br>

#### Returns

[MapZone](./strikelink/demoparser/parsing/mapzone.md)<br>
