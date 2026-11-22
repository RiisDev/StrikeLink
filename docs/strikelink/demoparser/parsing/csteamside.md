# CsTeamSide

Namespace: StrikeLink.DemoParser.Parsing

Different playable sides within a Counter Strike match.

```csharp
public enum CsTeamSide
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/en-us/dotnet/api/system.enum) → [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
Implements [IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes [JsonConverterAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Used when parsing fails or a user disconnects. |
| Spectator | 1 | Spectator Team. |
| Terrorists | 2 | Terrorists Team. |
| CounterTerrorists | 3 | CounterTerrorists Team. |
