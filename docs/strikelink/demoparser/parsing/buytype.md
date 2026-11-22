# BuyType

Namespace: StrikeLink.DemoParser.Parsing

How a team bought in a round, from the average equipment value per player at round start.

```csharp
public enum BuyType
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/en-us/dotnet/api/system.enum) → [BuyType](./strikelink/demoparser/parsing/buytype.md)<br>
Implements [IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes [JsonConverterAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Equipment values were not available (the demo has no entity state). |
| Pistol | 1 | First round of a half. |
| Eco | 2 | Average equipment value under 1000. |
| Force | 3 | Average equipment value from 1000 to 1999. |
| Half | 4 | Average equipment value from 2000 to 3499. |
| Full | 5 | Average equipment value of 3500 or more. |
