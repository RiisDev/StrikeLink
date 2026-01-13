# BombState

Namespace: StrikeLink.GSI.ObjectStates

Represents the state of the bomb in a round.

```csharp
public enum BombState
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [BombState](./strikelink/gsi/objectstates/bombstate.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Planted | 0 | The bomb has been planted. |
| Defused | 1 | The bomb has been defused. |
| Exploded | 2 | The bomb has exploded. |
