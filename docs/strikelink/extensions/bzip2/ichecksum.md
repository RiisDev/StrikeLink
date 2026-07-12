# IChecksum

Namespace: StrikeLink.Extensions.BZip2

Interface to compute a data checksum used by checked input/output streams.
 A data checksum can be updated by one byte or with a byte array. After each
 update the value of the current checksum can be returned by calling

```csharp
getValue
```

. The complete checksum object can also be reset
 so it can be used again with new data.

```csharp
public interface IChecksum
```

## Properties

### **Value**

Returns the data checksum computed so far.

```csharp
public abstract long Value { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

## Methods

### **Reset()**

Resets the data checksum as if no update was ever called.

```csharp
void Reset()
```

### **Update(Int32)**

Adds one byte to the data checksum.

```csharp
void Update(int bval)
```

#### Parameters

`bval` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
the data value to add. The high byte of the int is ignored.

### **Update(Byte[])**

Updates the data checksum with the bytes taken from the array.

```csharp
void Update(Byte[] buffer)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
buffer an array of bytes

### **Update(ArraySegment&lt;Byte&gt;)**

Adds the byte array to the data checksum.

```csharp
void Update(ArraySegment<byte> segment)
```

#### Parameters

`segment` [ArraySegment&lt;Byte&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.arraysegment-1)<br>
The chunk of data to add
