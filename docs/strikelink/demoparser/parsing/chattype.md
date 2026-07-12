# ChatType

Namespace: StrikeLink.DemoParser.Parsing

Represents the different chat message formats used in CS.

```csharp
public enum ChatType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ChatType](./strikelink/demoparser/parsing/chattype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)<br>
Attributes JsonConverterAttribute

## Fields

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | Empty |
| ChatAll | 1 | [ALL] %s1: %s2 %s1 = Player name %s2 = Message |
| ChatAllDead | 2 | [ALL] %s1 [DEAD]: %s2 %s1 = Player name %s2 = Message |
| ChatAllSpec | 3 | [ALL] %s1 [SPEC]: %s2 %s1 = Player name %s2 = Message |
| ChatCt | 10 | [CT] %s1: %s2 %s1 = Player name %s2 = Message |
| ChatCtDead | 11 | [CT] %s1 [DEAD]: %s2 %s1 = Player name %s2 = Message |
| ChatCtLoc | 12 | [CT] %s1 @ %s3: %s2 %s1 = Player name %s2 = Message %s3 = Location |
| ChatT | 20 | [T] %s1: %s2 %s1 = Player name %s2 = Message |
| ChatTDead | 21 | [T] %s1 [DEAD]: %s2 %s1 = Player name %s2 = Message |
| ChatTLoc | 22 | [T] %s1 @ %s3: %s2 %s1 = Player name %s2 = Message %s3 = Location |
| ChatSpec | 30 | [SPEC] %s1: %s2 %s1 = Player name %s2 = Message |
