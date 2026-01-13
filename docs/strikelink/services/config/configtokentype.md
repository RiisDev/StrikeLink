# ConfigTokenType

Namespace: StrikeLink.Services.Config

Represents the type of token encountered while parsing a Valve configuration file.

```csharp
public enum ConfigTokenType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ConfigTokenType](./strikelink/services/config/configtokentype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| String | 0 | A quoted string token. |
| OpenBrace | 1 | An opening brace (`{`) token. |
| CloseBrace | 2 | A closing brace (`}`) token. |
| EndOfFile | 3 | Indicates the end of the input stream. |
