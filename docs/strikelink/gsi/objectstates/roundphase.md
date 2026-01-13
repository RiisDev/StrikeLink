# RoundPhase

Namespace: StrikeLink.GSI.ObjectStates

Represents the current phase of a round.

```csharp
public enum RoundPhase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [RoundPhase](./strikelink/gsi/objectstates/roundphase.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Live | 0 | The round is actively in progress. |
| FreezeTime | 1 | The freeze time before the round begins or after a round ends. |
| Over | 2 | The round has ended. |
