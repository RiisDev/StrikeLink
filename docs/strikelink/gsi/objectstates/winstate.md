# WinState

Namespace: StrikeLink.GSI.ObjectStates

Represents the win condition of a round.

```csharp
public enum WinState
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [WinState](./strikelink/gsi/objectstates/winstate.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| CounterTerroristEliminated | 0 | All Counter-Terrorist players were eliminated. |
| TerroristsEliminated | 1 | All Terrorist players were eliminated. |
| BombDefused | 2 | The bomb was successfully defused. |
| BombExploded | 3 | The bomb exploded. |
