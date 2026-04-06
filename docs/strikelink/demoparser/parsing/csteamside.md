# CsTeamSide

Namespace: StrikeLink.DemoParser.Parsing

Different playable sides within a Counter Strike match.

```csharp
public enum CsTeamSide
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [CsTeamSide](./strikelink/demoparser/parsing/csteamside.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Used when parsing fails or a user disconnects. |
| Spectator | 1 | Spectator Team. |
| Terrorists | 2 | Terrorists Team. |
| CounterTerrorists | 3 | CounterTerrorists Team. |
