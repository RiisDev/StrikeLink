# ConfigNodeType

Namespace: StrikeLink.Services.Config

Specifies the type of node [ConfigNode](./strikelink/services/config/confignode.md).

```csharp
public enum ConfigNodeType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ConfigNodeType](./strikelink/services/config/confignodetype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Object | 0 | Represents an object node containing named child properties. |
| Value | 1 | Represents a primitive value node. |
