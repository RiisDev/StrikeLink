# MatchOutcome

Namespace: StrikeLink.DemoParser.Parsing

Specifies the possible outcomes of a match.

```csharp
public enum MatchOutcome
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [MatchOutcome](./strikelink/demoparser/parsing/matchoutcome.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Used when parsing fails, or the match ends to a fore-fit / vac-live. |
| Victory | 1 | Used when the match is a victory based on base user. |
| Defeat | 2 | Used when the match is a defeat based on base user. |
