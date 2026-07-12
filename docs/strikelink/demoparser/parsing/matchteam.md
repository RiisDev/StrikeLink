# MatchTeam

Namespace: StrikeLink.DemoParser.Parsing

Represents the two persistent team identities in a single match.
 Team A and Team B are lineup identities and do not imply a fixed side.

```csharp
public enum MatchTeam
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [MatchTeam](./strikelink/demoparser/parsing/matchteam.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes JsonConverterAttribute

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Unknown team identity. |
| TeamA | 1 | The first persistent lineup identity in the match. |
| TeamB | 2 | The second persistent lineup identity in the match. |
