using System.Buffers;

namespace StrikeLink.DemoParser.Parsing
{
	internal enum DemoCommand
	{
		Error = -1,
		Stop = 0,
		FileHeader = 1,
		FileInfo = 2,
		SyncTick = 3,
		SendTables = 4,
		ClassInfo = 5,
		StringTables = 6,
		Packet = 7,
		SignonPacket = 8,
		ConsoleCmd = 9,
		CustomData = 10,
		CustomDataCallbacks = 11,
		UserCmd = 12,
		FullPacket = 13,
		SaveGame = 14,
		SpawnGroups = 15,
		AnimationData = 16,
		AnimationHeader = 17,
		Recovery = 18,
	}

	internal static class MessageTypeIds
	{
		public const int SvcServerInfo = 40;
		public const int SvcUserMessage = 72;
		public const int GeSource1LegacyGameEventList = 205;
		public const int GeSource1LegacyGameEvent = 207;

		public const int CsUmPlayerStatsUpdate = 336;
		public const int CsUmXRankUpd = 341;
		public const int CsUmServerRankUpdate = 352;
	}

	internal sealed class DemoStreamReader(Stream stream)
	{
		public long Position => stream.Position;

		public long Length => stream.Length;

		public void Skip(int bytes)
		{
			if (stream.CanSeek)
			{
				stream.Seek(bytes, SeekOrigin.Current);
				return;
			}

			Span<byte> buffer = stackalloc byte[256];
			int remaining = bytes;
			while (remaining > 0)
			{
				int read = stream.Read(buffer[..Math.Min(buffer.Length, remaining)]);
				if (read == 0)
				{
					throw new EndOfStreamException("Unexpected end of demo while skipping bytes.");
				}

				remaining -= read;
			}
		}

		public bool TryReadVarUInt32(out uint value)
		{
			value = 0;
			int shift = 0;

			while (shift < 35)
			{
				int current = stream.ReadByte();
				if (current < 0)
				{
					return shift != 0 ? throw new EndOfStreamException("Unexpected end of demo inside varint.") : false;
				}

				value |= (uint)(current & 0x7F) << shift;
				if ((current & 0x80) == 0)
				{
					return true;
				}

				shift += 7;
			}

			throw new InvalidDataException("Encountered an invalid 32-bit varint.");
		}

		public byte[] ReadBytesExact(int count)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(count);

			byte[] buffer = ArrayPool<byte>.Shared.Rent(count);
			try
			{
				int offset = 0;
				while (offset < count)
				{
					int read = stream.Read(buffer, offset, count - offset);
					if (read == 0)
					{
						throw new EndOfStreamException("Unexpected end of demo while reading payload.");
					}

					offset += read;
				}

				byte[] result = new byte[count];
				Array.Copy(buffer, result, count);
				return result;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer, clearArray: false);
			}
		}

		public string ReadAscii(int count)
			=> Encoding.ASCII.GetString(ReadBytesExact(count));
	}

	internal sealed class PacketBitReader(byte[] buffer)
	{
		private int _bitPosition;

		public int BitsRemaining => (buffer.Length * 8) - _bitPosition;

		public bool HasAtLeastBits(int count) => BitsRemaining >= count;

		public uint ReadBits(int count)
		{
			if (count is < 0 or > 32)
			{
				throw new ArgumentOutOfRangeException(nameof(count));
			}

			if (!HasAtLeastBits(count))
			{
				throw new EndOfStreamException("Unexpected end of packet bitstream.");
			}

			int byteIndex = _bitPosition >> 3;
			int bitOffset = _bitPosition & 7;
			_bitPosition += count;

			// Collect all bytes that span the required bits into a ulong window, then extract.
			int numBytes = (bitOffset + count + 7) >> 3;
			ulong window = 0;
			for (int i = 0; i < numBytes; i++)
				window |= (ulong)buffer[byteIndex + i] << (i << 3);

			uint mask = count < 32 ? (1u << count) - 1 : uint.MaxValue;
			return (uint)(window >> bitOffset) & mask;
		}

		public byte ReadByte()
		{
			if (!HasAtLeastBits(8))
			{
				throw new EndOfStreamException("Unexpected end of packet bitstream.");
			}

			int byteIndex = _bitPosition >> 3;
			int bitOffset = _bitPosition & 7;
			_bitPosition += 8;

			if (bitOffset == 0)
				return buffer[byteIndex];

			// Bit position straddles two bytes — shift the window across the boundary.
			return (byte)((buffer[byteIndex] >> bitOffset) | (buffer[byteIndex + 1] << (8 - bitOffset)));
		}

		public uint ReadVarUInt32()
		{
			uint result = 0;
			int shift = 0;
			while (shift < 35)
			{
				byte current = ReadByte();
				result |= (uint)(current & 0x7F) << shift;
				if ((current & 0x80) == 0)
				{
					return result;
				}

				shift += 7;
			}

			throw new InvalidDataException("Encountered an invalid packet varint.");
		}

		public uint ReadUBitInt()
		{
			uint result = ReadBits(6);
			switch (result & (16u | 32u))
			{
				case 16:
					result = (result & 15u) | (ReadBits(4) << 4);
					break;
				case 32:
					result = (result & 15u) | (ReadBits(8) << 4);
					break;
				case 48:
					result = (result & 15u) | (ReadBits(28) << 4);
					break;
			}

			return result;
		}

		public byte[] ReadBytes(int count)
		{
			byte[] readBytes = new byte[count];
			for (int i = 0; i < count; i++)
			{
				readBytes[i] = ReadByte();
			}

			return readBytes;
		}
	}

	internal enum ProtoWireType
	{
		Varint = 0,
		Fixed64 = 1,
		LengthDelimited = 2,
		Fixed32 = 5,
	}

	internal readonly record struct ProtoFieldValue(
		ProtoWireType WireType,
		ulong Varint,
		ulong Fixed64,
		uint Fixed32,
		byte[]? Bytes)
	{
		public string GetString() => Encoding.UTF8.GetString(Bytes ?? []);

		public ProtoMessage GetMessage() => ProtoMessage.Parse(Bytes ?? []);
	}

	internal sealed class ProtoMessage(Dictionary<int, List<ProtoFieldValue>> fields)
	{
		public static ProtoMessage Parse(ReadOnlySpan<byte> payload)
		{
			Dictionary<int, List<ProtoFieldValue>> fields = new();
			int index = 0;

			while (index < payload.Length)
			{
				ulong key = ReadVarUInt64(payload, ref index);
				int fieldNumber = (int)(key >> 3);
				ProtoWireType wireType = (ProtoWireType)(key & 0x7);

				ProtoFieldValue value = wireType switch
				{
					ProtoWireType.Varint => new ProtoFieldValue(wireType, ReadVarUInt64(payload, ref index), 0, 0, null),
					ProtoWireType.Fixed64 => new ProtoFieldValue(wireType, 0, ReadFixed64(payload, ref index), 0, null),
					ProtoWireType.LengthDelimited => new ProtoFieldValue(wireType, 0, 0, 0, ReadLengthDelimited(payload, ref index)),
					ProtoWireType.Fixed32 => new ProtoFieldValue(wireType, 0, 0, ReadFixed32(payload, ref index), null),
					_ => throw new InvalidDataException($"Unsupported protobuf wire type {(int)wireType}.")
				};

				if (!fields.TryGetValue(fieldNumber, out List<ProtoFieldValue>? list))
				{
					list = [];
					fields[fieldNumber] = list;
				}

				list.Add(value);
			}

			return new ProtoMessage(fields);
		}

		public bool TryGetValue(int fieldNumber, out ProtoFieldValue value)
		{
			if (fields.TryGetValue(fieldNumber, out List<ProtoFieldValue>? values) && values.Count > 0)
			{
				value = values[0];
				return true;
			}

			value = default;
			return false;
		}

		public IEnumerable<ProtoFieldValue> GetValues(int fieldNumber)
			=> fields.TryGetValue(fieldNumber, out List<ProtoFieldValue>? values) ? values : [];

		public bool TryGetString(int fieldNumber, out string? value)
		{
			if (TryGetValue(fieldNumber, out ProtoFieldValue field) && field.WireType == ProtoWireType.LengthDelimited)
			{
				value = field.GetString();
				return true;
			}

			value = null;
			return false;
		}

		public bool TryGetBytes(int fieldNumber, out byte[]? value)
		{
			if (TryGetValue(fieldNumber, out ProtoFieldValue field) && field.WireType == ProtoWireType.LengthDelimited)
			{
				value = field.Bytes;
				return true;
			}

			value = null;
			return false;
		}

		public bool TryGetInt32(int fieldNumber, out int value)
		{
			if (TryGetValue(fieldNumber, out ProtoFieldValue field))
			{
				value = field.WireType switch
				{
					ProtoWireType.Varint => unchecked((int)field.Varint),
					ProtoWireType.Fixed32 => unchecked((int)field.Fixed32),
					_ => 0,
				};

				return field.WireType is ProtoWireType.Varint or ProtoWireType.Fixed32;
			}

			value = 0;
			return false;
		}

		public bool TryGetUInt64(int fieldNumber, out ulong value)
		{
			if (TryGetValue(fieldNumber, out ProtoFieldValue field))
			{
				value = field.WireType switch
				{
					ProtoWireType.Varint => field.Varint,
					ProtoWireType.Fixed64 => field.Fixed64,
					_ => 0,
				};

				return field.WireType is ProtoWireType.Varint or ProtoWireType.Fixed64;
			}

			value = 0;
			return false;
		}

		public bool TryGetFloat(int fieldNumber, out float value)
		{
			if (TryGetValue(fieldNumber, out ProtoFieldValue field) && field.WireType == ProtoWireType.Fixed32)
			{
				value = BitConverter.Int32BitsToSingle(unchecked((int)field.Fixed32));
				return true;
			}

			value = 0;
			return false;
		}

		private static ulong ReadVarUInt64(ReadOnlySpan<byte> payload, ref int index)
		{
			ulong value = 0;
			int shift = 0;
			while (shift < 70)
			{
				if (index >= payload.Length)
				{
					throw new EndOfStreamException("Unexpected end of protobuf payload.");
				}

				byte current = payload[index++];
				value |= (ulong)(current & 0x7F) << shift;
				if ((current & 0x80) == 0)
				{
					return value;
				}

				shift += 7;
			}

			throw new InvalidDataException("Encountered an invalid protobuf varint.");
		}

		private static byte[] ReadLengthDelimited(ReadOnlySpan<byte> payload, ref int index)
		{
			int length = checked((int)ReadVarUInt64(payload, ref index));
			if (index + length > payload.Length)
			{
				throw new EndOfStreamException("Unexpected end of protobuf length-delimited field.");
			}

			byte[] bytes = payload.Slice(index, length).ToArray();
			index += length;
			return bytes;
		}

		private static uint ReadFixed32(ReadOnlySpan<byte> payload, ref int index)
		{
			if (index + 4 > payload.Length)
			{
				throw new EndOfStreamException("Unexpected end of protobuf fixed32 field.");
			}

			uint value = BitConverter.ToUInt32(payload.Slice(index, 4));
			index += 4;
			return value;
		}

		private static ulong ReadFixed64(ReadOnlySpan<byte> payload, ref int index)
		{
			if (index + 8 > payload.Length)
			{
				throw new EndOfStreamException("Unexpected end of protobuf fixed64 field.");
			}

			ulong value = BitConverter.ToUInt64(payload.Slice(index, 8));
			index += 8;
			return value;
		}
	}

	internal static class SnappyBlockDecoder
	{
		public static byte[] Decode(byte[] input)
		{
			int index = 0;
			int expectedLength = checked((int)ReadVarUInt32(input, ref index));
			byte[] output = new byte[expectedLength];
			int outputIndex = 0;

			while (index < input.Length)
			{
				byte tag = input[index++];
				int tagType = tag & 0x03;

				switch (tagType)
				{
					case 0:
						CopyLiteral(input, ref index, output, ref outputIndex, tag);
						break;

					case 1:
						CopyFromHistory(output, ref outputIndex, 4 + ((tag >> 2) & 0x07), ((tag & 0xE0) << 3) | input[index++]);
						break;

					case 2:
						CopyFromHistory(output, ref outputIndex, 1 + (tag >> 2), input[index++] | (input[index++] << 8));
						break;

					case 3:
						CopyFromHistory(
							output,
							ref outputIndex,
							1 + (tag >> 2),
							input[index++] |
							(input[index++] << 8) |
							(input[index++] << 16) |
							(input[index++] << 24));
						break;

					default:
						throw new InvalidDataException("Unsupported snappy tag type.");
				}
			}

			if (outputIndex != expectedLength)
				throw new InvalidDataException($"Snappy output length mismatch. Expected {expectedLength}, got {outputIndex}.");

			return output;
		}

		private static void CopyLiteral(byte[] input, ref int index, byte[] output, ref int outputIndex, byte tag)
		{
			int length = tag >> 2;
			if (length >= 60)
			{
				int bytesToRead = length - 59;
				length = 0;
				for (int i = 0; i < bytesToRead; i++)
				{
					length |= input[index++] << (i * 8);
				}
			}

			length += 1;

			if (index + length > input.Length)
			{
				throw new EndOfStreamException("Unexpected end of snappy literal block.");
			}

			Buffer.BlockCopy(input, index, output, outputIndex, length);
			index += length;
			outputIndex += length;
		}

		private static void CopyFromHistory(byte[] output, ref int outputIndex, int length, int offset)
		{
			if (offset <= 0 || offset > outputIndex)
			{
				throw new InvalidDataException("Encountered an invalid snappy back-reference.");
			}

			// Use a byte-at-a-time copy to correctly handle overlapping back-references (offset < length).
			for (int i = 0; i < length; i++)
			{
				output[outputIndex] = output[outputIndex - offset];
				outputIndex++;
			}
		}

		private static uint ReadVarUInt32(byte[] input, ref int index)
		{
			uint value = 0;
			int shift = 0;
			while (shift < 35)
			{
				if (index >= input.Length)
				{
					throw new EndOfStreamException("Unexpected end of snappy stream while reading preamble.");
				}

				byte current = input[index++];
				value |= (uint)(current & 0x7F) << shift;
				if ((current & 0x80) == 0)
				{
					return value;
				}

				shift += 7;
			}

			throw new InvalidDataException("Encountered an invalid snappy preamble varint.");
		}
	}
}