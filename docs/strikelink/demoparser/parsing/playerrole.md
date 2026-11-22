# PlayerRole

Namespace: StrikeLink.DemoParser.Parsing

The playstyle that dominated a player's match.

```csharp
public enum PlayerRole
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/en-us/dotnet/api/system.enum) → [PlayerRole](./strikelink/demoparser/parsing/playerrole.md)<br>
Implements [IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes [JsonConverterAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unclassified | 0 | Too few rounds to classify. |
| Entry | 1 | Takes and dies in T-side opening duels. |
| Support | 2 | Contributes mostly through flashes. |
| Anchor | 3 | Survives CT-side rounds. |
| Trade | 4 | Frequently trades or is traded. |
| Closer | 5 | Frequently wins clutches. |
| Hybrid | 6 | No single tendency stands out. |
