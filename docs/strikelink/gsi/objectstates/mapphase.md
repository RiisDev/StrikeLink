# MapPhase

Namespace: StrikeLink.GSI.ObjectStates

Represents the current phase of the map.

```csharp
public enum MapPhase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [MapPhase](./strikelink/gsi/objectstates/mapphase.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Live | 0 | The match is currently live. |
| WarmUp | 1 | The match is in warm-up mode. |
| Intermission | 2 | The match is between halves. |
| GameOver | 3 | The match has ended. |
