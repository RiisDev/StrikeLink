# HeldState

Namespace: StrikeLink.GSI.ObjectStates

Represents the current state of a held weapon.

```csharp
public enum HeldState
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [HeldState](./strikelink/gsi/objectstates/heldstate.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Active | 0 | The weapon is actively equipped. |
| Holstered | 1 | The weapon is holstered. |
| Reloading | 2 | The weapon is currently reloading. |
