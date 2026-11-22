# MatchOutcome

Namespace: StrikeLink.DemoParser.Parsing

Specifies the possible outcomes of a match.

```csharp
public enum MatchOutcome
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/en-us/dotnet/api/system.enum) → [MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>
Implements [IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes [JsonConverterAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Used when parsing fails, or the match ends to a fore-fit / vac-live. |
| Victory | 1 | Used when the match is a victory based on base user. |
| Defeat | 2 | Used when the match is a defeat based on base user. |
| Draw | 3 | Used when the match is a draw based on base user. |
