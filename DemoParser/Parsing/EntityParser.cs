using System.Text.RegularExpressions;

namespace StrikeLink.DemoParser.Parsing;

/// <summary>
/// Minimal Source 2 entity-state reader (flattened serializers + packet entities), ported from the approach used by
/// demoinfocs-golang / manta. Matchmaking demos carry no round_end / round_mvp game events, so values such as a
/// player's MVP count only exist as entity properties. Every entity update has to be decoded to stay in sync with
/// the bitstream, but only <c>CCSPlayerController</c> properties are surfaced, under their full dotted names
/// (for example <c>m_iMVPs</c>, <c>m_pActionTrackingServices.m_iKills</c> or <c>m_pActionTrackingServices.m_perRoundStats.0003.m_iDamage</c>).
/// Instance baselines are not applied: they don't affect bit consumption, and the surfaced properties are sent in full on creation.
/// </summary>
internal sealed class EntityParser
{
	/// <summary>Per-tick engine bookkeeping on the controller that carries no match information.</summary>
	private static readonly string[] NoisyPrefixes =
	[
		"m_flSimulationTime", "m_nTickBase", "m_fFlags", "m_pEntity", "m_flCreateTime", "m_nNextThinkTick", "m_flAnimTime",
		"m_vec", "m_flFriction", "m_flTimeScale",
	];

	internal const string ControllerClassName = "CCSPlayerController";

	private readonly Dictionary<string, Serializer> _serializers = [];
	private readonly Dictionary<int, ClassInfo> _classes = [];
	private readonly Dictionary<int, Entity> _entities = [];
	private readonly List<FieldPath> _pathCache = [];
	private int _classIdBits;
	private bool _fullPacketSeen;
	private int _nextPolyId;

	/// <summary>(entity index, property name, value, true while the entity is being created)</summary>
	public Action<int, string, object?, bool>? OnControllerProperty { get; set; }

	/// <summary>Raised when a player controller entity is created, before its properties are reported.</summary>
	public Action<int>? OnControllerCreated { get; set; }

	public void SetMaxClasses(int maxClasses) => _classIdBits = 32 - System.Numerics.BitOperations.LeadingZeroCount((uint)Math.Max(maxClasses, 1));

	// ---- send tables ------------------------------------------------------------------------------------------------

	/// <summary>CDemoSendTables: a length-prefixed CSVCMsg_FlattenedSerializer.</summary>
	public void OnSendTables(byte[] payload)
	{
		if (!ProtoMessage.Parse(payload).TryGetBytes(1, out byte[]? data) || data is null)
		{
			return;
		}

		EntityReader reader = new(data);
		ProtoMessage message = ProtoMessage.Parse(reader.ReadBytes((int)reader.ReadVarUInt32()));

		List<string> symbols = message.GetValues(2).Select(static v => v.GetString()).ToList();
		List<ProtoMessage> fieldMessages = message.GetValues(3).Select(static v => v.GetMessage()).ToList();
		Dictionary<int, Field> fields = [];

		foreach (ProtoFieldValue serializerValue in message.GetValues(1))
		{
			ProtoMessage serializerMessage = serializerValue.GetMessage();
			serializerMessage.TryGetInt32(1, out int nameSym);
			Serializer serializer = new(symbols[nameSym]);

			foreach (ProtoFieldValue indexValue in serializerMessage.GetValues(3))
			{
				int index = (int)indexValue.Varint;
				if (!fields.TryGetValue(index, out Field? field))
				{
					field = BuildField(fieldMessages[index], symbols);
					fields[index] = field;
				}

				serializer.Fields.Add(field);
			}

			_serializers[serializer.Name] = serializer;
		}
	}

	private Field BuildField(ProtoMessage message, List<string> symbols)
	{
		string Symbol(int number) => message.TryGetInt32(number, out int sym) ? symbols[sym] : "";
		int? OptionalInt(int number) => message.TryGetInt32(number, out int value) ? value : null;
		float? OptionalFloat(int number) => message.TryGetFloat(number, out float value) ? value : null;

		Field field = new()
		{
			VarName = Symbol(2),
			VarType = Symbol(1),
			SendNode = Symbol(9),
			SerializerName = Symbol(7),
			Encoder = Symbol(10),
			EncodeFlags = OptionalInt(6),
			BitCount = OptionalInt(3),
			LowValue = OptionalFloat(4),
			HighValue = OptionalFloat(5),
		};
		field.FieldType = new FieldType(field.VarType);

		if (field.VarName is "m_flSimulationTime" or "m_flAnimTime")
		{
			field.Encoder = "simtime";
		}

		if (field.SerializerName != "")
		{
			field.Serializer = _serializers.GetValueOrDefault(field.SerializerName);
		}

		List<ProtoMessage> polymorphic = message.GetValues(11).Select(static v => v.GetMessage()).ToList();
		if (polymorphic.Count > 0)
		{
			field.PolyTypes = [field.Serializer ?? throw new InvalidDataException($"polymorphic field {field.VarName}: unknown serializer {field.SerializerName}")];
			foreach (ProtoMessage poly in polymorphic)
			{
				poly.TryGetInt32(1, out int polyNameSym);
				field.PolyTypes.Add(_serializers.GetValueOrDefault(symbols[polyNameSym])
					?? throw new InvalidDataException($"polymorphic field {field.VarName}: unknown serializer {symbols[polyNameSym]}"));
			}
		}

		if (field.Serializer is not null || field.PolyTypes is not null)
		{
			if (field.FieldType.Pointer || PointerTypes.Contains(field.FieldType.BaseType) || field.PolyTypes is not null)
			{
				if (field.PolyTypes is not null)
				{
					field.PolySerializerId = _nextPolyId++;
				}

				field.SetModel(FieldModel.FixedTable);
			}
			else
			{
				field.SetModel(FieldModel.VariableTable);
			}
		}
		else if (field.FieldType.Count > 0 && field.FieldType.BaseType != "char")
		{
			field.SetModel(FieldModel.FixedArray);
		}
		else if (field.FieldType.BaseType is "CUtlVector" or "CNetworkUtlVectorBase")
		{
			field.SetModel(FieldModel.VariableArray);
		}
		else
		{
			field.SetModel(FieldModel.Simple);
		}

		return field;
	}

	private static readonly HashSet<string> PointerTypes =
	[
		"CBodyComponentDCGBaseAnimating", "CBodyComponentBaseAnimating", "CBodyComponentBaseAnimatingOverlay",
		"CBodyComponentBaseModelEntity", "CBodyComponent", "CBodyComponentSkeletonInstance", "CBodyComponentPoint",
		"CLightComponent", "CRenderComponent", "CPhysicsComponent",
	];

	// ---- classes ----------------------------------------------------------------------------------------------------

	/// <summary>CDemoClassInfo.</summary>
	public void OnClassInfo(byte[] payload)
	{
		foreach (ProtoFieldValue value in ProtoMessage.Parse(payload).GetValues(1))
		{
			ProtoMessage item = value.GetMessage();
			if (item.TryGetInt32(1, out int id) && item.TryGetString(2, out string? name) && name is not null)
			{
				_classes[id] = new ClassInfo(name);
			}
		}
	}

	/// <summary>CSVCMsg_ClassInfo (fallback when no CDemoClassInfo is present).</summary>
	public void OnSvcClassInfo(byte[] payload)
	{
		foreach (ProtoFieldValue value in ProtoMessage.Parse(payload).GetValues(2))
		{
			ProtoMessage item = value.GetMessage();
			if (item.TryGetInt32(1, out int id) && item.TryGetString(3, out string? name) && name is not null)
			{
				_classes.TryAdd(id, new ClassInfo(name));
			}
		}
	}

	// ---- packet entities --------------------------------------------------------------------------------------------

	/// <summary>CSVCMsg_PacketEntities.</summary>
	public void OnPacketEntities(byte[] payload)
	{
		ProtoMessage message = ProtoMessage.Parse(payload);
		if (!message.TryGetBytes(7, out byte[]? entityData) || entityData is null)
		{
			return;
		}

		message.TryGetInt32(2, out int updates);
		bool isDelta = message.TryGetInt32(3, out int delta) && delta != 0;
		bool hasPvsVisBits = message.TryGetInt32(16, out int pvs) && pvs > 0;

		// Only the first full snapshot is applied; later ones repeat state we already track.
		if (!isDelta)
		{
			if (_fullPacketSeen)
			{
				return;
			}

			_fullPacketSeen = true;
		}

		EntityReader reader = new(entityData);
		int index = -1;

		for (; updates > 0; updates--)
		{
			index += (int)reader.ReadUBitVar() + 1;
			uint cmd = reader.ReadBits(2);

			if ((cmd & 1) == 0)
			{
				if ((cmd & 2) != 0)
				{
					int classId = (int)reader.ReadBits(_classIdBits);
					reader.ReadBits(17); // serial
					reader.ReadVarUInt32();

					if (!_classes.TryGetValue(classId, out ClassInfo? classInfo))
					{
						throw new InvalidDataException($"unable to find new class {classId}");
					}

					Entity entity = new(index, classInfo, ResolveClass(classInfo));
					_entities[index] = entity;
					if (classInfo.Name == ControllerClassName)
					{
						OnControllerCreated?.Invoke(index);
					}

					ReadFields(reader, entity, created: true);
				}
				else
				{
					if (hasPvsVisBits && (reader.ReadBits(2) & 1) != 0)
					{
						continue;
					}

					if (!_entities.TryGetValue(index, out Entity? entity))
					{
						throw new InvalidDataException($"unable to find existing entity {index}");
					}

					ReadFields(reader, entity, created: false);
				}
			}
			else if ((cmd & 2) != 0)
			{
				_entities.Remove(index);
			}
		}
	}

	private ClassSerializer ResolveClass(ClassInfo classInfo)
	{
		if (classInfo.Resolved is { } resolved)
		{
			return resolved;
		}

		Serializer serializer = _serializers.GetValueOrDefault(classInfo.Name)
			?? throw new InvalidDataException($"no serializer for class {classInfo.Name}");
		classInfo.Resolved = new ClassSerializer(serializer, classInfo.Name == ControllerClassName);
		return classInfo.Resolved;
	}

	private static bool IsNoisy(string name) => NoisyPrefixes.Any(prefix => name.StartsWith(prefix, StringComparison.Ordinal));

	/// <summary>
	/// Every property the schema declares for a class, with array elements written as <c>[]</c>, for example
	/// <c>m_pActionTrackingServices.m_perRoundStats.[].m_iKills</c>. Mirrors what is reported for controllers.
	/// </summary>
	public IReadOnlyList<(string Name, string Type)> DescribeClass(string className)
	{
		if (!_serializers.TryGetValue(className, out Serializer? serializer))
		{
			return [];
		}

		Dictionary<string, string> found = [];
		Describe(serializer, "", found, []);
		return found.Where(entry => !IsNoisy(entry.Key)).Select(static entry => (entry.Key, entry.Value)).ToList();
	}

	private static void Describe(Serializer serializer, string prefix, Dictionary<string, string> into, HashSet<Serializer> path)
	{
		if (!path.Add(serializer))
		{
			return;
		}

		foreach (Field field in serializer.Fields)
		{
			string name = prefix + field.VarName;
			switch (field.Model)
			{
				case FieldModel.Simple:
					into[name] = field.VarType;
					break;

				case FieldModel.FixedArray:
				case FieldModel.VariableArray:
					into[name + ".[]"] = field.VarType;
					break;

				case FieldModel.FixedTable:
					foreach (Serializer nested in field.PolyTypes ?? (field.Serializer is null ? [] : [field.Serializer]))
					{
						Describe(nested, name + ".", into, path);
					}

					break;

				case FieldModel.VariableTable:
					if (field.Serializer is not null)
					{
						Describe(field.Serializer, name + ".[].", into, path);
					}

					break;
			}
		}

		path.Remove(serializer);
	}

	private void ReadFields(EntityReader reader, Entity entity, bool created)
	{
		int count = FieldPath.ReadAll(reader, _pathCache);
		ClassSerializer cls = entity.Class;
		reader.Capture = cls.Capture;

		for (int i = 0; i < count; i++)
		{
			FieldPath path = _pathCache[i];
			FieldDecoder decoder = cls.Serializer.GetDecoder(path, 0, entity.Poly)
				?? throw new InvalidDataException($"no serializer for polymorphic pointer at field path {path} (desynced bitstream)");

			object? value = decoder.Decode(reader);

			if (value is PolyUpdate poly)
			{
				entity.Poly ??= [];
				entity.Poly[poly.Id] = poly.Serializer;
			}
			else if (cls.Capture && value is not null && OnControllerProperty is { } report)
			{
				string name = cls.NameOf(path, entity.Poly);
				if (!IsNoisy(name))
				{
					report(entity.Index, name, value, created);
				}
			}
		}

		reader.Capture = false;
	}

	// ---- model ------------------------------------------------------------------------------------------------------

	private sealed class ClassInfo(string name)
	{
		public string Name { get; } = name;

		public ClassSerializer? Resolved { get; set; }
	}

	private sealed class ClassSerializer(Serializer serializer, bool capture)
	{
		private readonly Dictionary<ulong, string> _names = [];

		public Serializer Serializer { get; } = serializer;

		/// <summary>True when this class's property values are decoded and reported (player controllers).</summary>
		public bool Capture { get; } = capture;

		/// <summary>Dotted property name of a field path, cached for the common shallow paths.</summary>
		public string NameOf(FieldPath path, Dictionary<int, Serializer?>? poly)
		{
			// Polymorphic entities can resolve the same path to different fields, so they bypass the cache.
			if (poly is not null || path.Last > 3)
			{
				return Resolve(path, poly);
			}

			ulong key = (ulong)path.Last << 56;
			for (int i = 0; i <= path.Last; i++)
			{
				if ((uint)path.Path[i] > 0x3FFF)
				{
					return Resolve(path, poly);
				}

				key |= (ulong)path.Path[i] << (i * 14);
			}

			if (!_names.TryGetValue(key, out string? name))
			{
				name = Resolve(path, poly);
				_names[key] = name;
			}

			return name;
		}

		private string Resolve(FieldPath path, Dictionary<int, Serializer?>? poly) => string.Join('.', Serializer.GetName(path, 0, poly));
	}

	private sealed class Entity(int index, ClassInfo info, ClassSerializer cls)
	{
		public int Index { get; } = index;

		public ClassInfo Info { get; } = info;

		public ClassSerializer Class { get; } = cls;

		/// <summary>Active serializer per polymorphic pointer field (field.PolySerializerId); null until one is selected.</summary>
		public Dictionary<int, Serializer?>? Poly { get; set; }
	}

	private enum FieldModel
	{
		Simple,
		FixedArray,
		FixedTable,
		VariableArray,
		VariableTable,
	}

	private sealed record PolyUpdate(int Id, Serializer? Serializer);

	private sealed class Serializer(string name)
	{
		public string Name { get; } = name;

		public List<Field> Fields { get; } = [];

		public FieldDecoder? GetDecoder(FieldPath path, int pos, Dictionary<int, Serializer?>? poly)
			=> Fields[path.Path[pos]].GetDecoder(path, pos + 1, poly);

		public List<string> GetName(FieldPath path, int pos, Dictionary<int, Serializer?>? poly)
			=> Fields[path.Path[pos]].GetName(path, pos + 1, poly);
	}

	private sealed class Field
	{
		public string VarName { get; init; } = "";

		public string VarType { get; init; } = "";

		public string SendNode { get; init; } = "";

		public string SerializerName { get; init; } = "";

		public string Encoder { get; set; } = "";

		public int? EncodeFlags { get; init; }

		public int? BitCount { get; init; }

		public float? LowValue { get; init; }

		public float? HighValue { get; init; }

		public FieldType FieldType { get; set; } = null!;

		public Serializer? Serializer { get; set; }

		public List<Serializer>? PolyTypes { get; set; }

		public int PolySerializerId { get; set; } = -1;

		public FieldModel Model { get; private set; }

		private FieldDecoder? _decoder;
		private FieldDecoder? _baseDecoder;
		private FieldDecoder? _childDecoder;

		public void SetModel(FieldModel model)
		{
			Model = model;

			switch (model)
			{
				case FieldModel.FixedArray:
				case FieldModel.Simple:
					_decoder = FieldDecoders.Find(this);
					break;

				case FieldModel.FixedTable:
					if (PolyTypes is null)
					{
						_baseDecoder = new FieldDecoder(static r => r.ReadBool());
					}
					else
					{
						List<Serializer> types = PolyTypes;
						int id = PolySerializerId;
						_baseDecoder = new FieldDecoder(r => new PolyUpdate(id, r.ReadBool() ? types[(int)r.ReadUBitVar()] : null));
					}

					break;

				case FieldModel.VariableArray:
					_baseDecoder = new FieldDecoder(static r => r.ReadVarUInt32(), isCollection: true);
					_childDecoder = FieldDecoders.FindByGenericType(this);
					break;

				case FieldModel.VariableTable:
					_baseDecoder = new FieldDecoder(static r => r.ReadVarUInt32(), isCollection: true);
					break;
			}
		}

		/// <summary>Name parts of the field a path points at, e.g. [m_pActionTrackingServices, m_perRoundStats, 0003, m_iKills].</summary>
		public List<string> GetName(FieldPath path, int pos, Dictionary<int, Serializer?>? poly)
		{
			List<string> parts = [VarName];

			switch (Model)
			{
				case FieldModel.FixedArray:
				case FieldModel.VariableArray:
					if (path.Last == pos)
					{
						parts.Add(path.Path[pos].ToString("D4"));
					}

					break;

				case FieldModel.FixedTable:
					if (path.Last >= pos)
					{
						Serializer? serializer = Serializer;
						if (PolySerializerId >= 0 && poly is not null)
						{
							serializer = poly.GetValueOrDefault(PolySerializerId);
						}

						if (serializer is not null)
						{
							parts.AddRange(serializer.GetName(path, pos, poly));
						}
					}

					break;

				case FieldModel.VariableTable:
					if (path.Last != pos - 1)
					{
						parts.Add(path.Path[pos].ToString("D4"));
						if (path.Last != pos)
						{
							parts.AddRange(Serializer!.GetName(path, pos + 1, poly));
						}
					}

					break;
			}

			return parts;
		}

		public FieldDecoder? GetDecoder(FieldPath path, int pos, Dictionary<int, Serializer?>? poly)
		{
			switch (Model)
			{
				case FieldModel.FixedArray:
					return _decoder;

				case FieldModel.FixedTable:
				{
					if (path.Last == pos - 1)
					{
						return _baseDecoder;
					}

					Serializer? serializer = Serializer;
					if (PolySerializerId >= 0 && poly is not null)
					{
						serializer = poly.GetValueOrDefault(PolySerializerId);
					}

					return serializer?.GetDecoder(path, pos, poly);
				}

				case FieldModel.VariableArray:
					return path.Last == pos ? _childDecoder : _baseDecoder;

				case FieldModel.VariableTable:
					return path.Last >= pos + 1 ? Serializer!.GetDecoder(path, pos + 1, poly) : _baseDecoder;

				default:
					return _decoder;
			}
		}
	}

	private sealed class FieldType
	{
		private static readonly Regex Pattern = new(@"([^\<\[\*]+)(\<\s(.*)\s\>)?(\*)?(\[(.*)\])?", RegexOptions.Compiled);

		public string BaseType { get; }

		public FieldType? GenericType { get; }

		public bool Pointer { get; }

		public int Count { get; }

		public FieldType(string name)
		{
			Match match = Pattern.Match(name);
			BaseType = match.Groups[1].Value;
			Pointer = match.Groups[4].Value == "*";

			if (match.Groups[3].Value != "")
			{
				GenericType = new FieldType(match.Groups[3].Value);
			}

			string count = match.Groups[6].Value;
			if (count is "MAX_ITEM_STOCKS")
			{
				Count = 8;
			}
			else if (count is "MAX_ABILITY_DRAFT_ABILITIES")
			{
				Count = 48;
			}
			else if (int.TryParse(count, out int n) && n > 0)
			{
				Count = n;
			}
			else if (count != "")
			{
				Count = 1024;
			}
		}
	}

	// ---- field paths ------------------------------------------------------------------------------------------------

	private sealed class FieldPath
	{
		public int[] Path { get; } = [-1, 0, 0, 0, 0, 0, 0];

		public int Last { get; set; }

		public bool Done { get; set; }

		public override string ToString() => string.Join("/", Path.Take(Last + 1));

		private void Pop(int n)
		{
			for (int i = 0; i < n; i++)
			{
				Path[Last] = 0;
				Last--;
			}
		}

		/// <summary>Reads the field paths of one entity update into <paramref name="paths"/> and returns how many were read.</summary>
		public static int ReadAll(EntityReader r, List<FieldPath> paths)
		{
			FieldPath fp = new();
			int count = 0;

			while (!fp.Done)
			{
				HuffmanNode node = Huffman.Root;
				while (!node.IsLeaf)
				{
					node = r.ReadBool() ? node.Right! : node.Left!;
				}

				Ops[node.Value](r, fp);

				if (!fp.Done)
				{
					if (paths.Count <= count)
					{
						paths.Add(new FieldPath());
					}

					FieldPath target = paths[count];
					target.Last = fp.Last;
					Array.Copy(fp.Path, target.Path, fp.Path.Length);
					count++;
				}
			}

			return count;
		}

		// Weight and behaviour of each field path op, in wire order. Weights drive the (fixed) Huffman code.
		private static readonly (int Weight, Action<EntityReader, FieldPath> Run)[] OpTable =
		[
			(36271, (r, fp) => fp.Path[fp.Last]++), // PlusOne
			(10334, (r, fp) => fp.Path[fp.Last] += 2), // PlusTwo
			(1375, (r, fp) => fp.Path[fp.Last] += 3), // PlusThree
			(646, (r, fp) => fp.Path[fp.Last] += 4), // PlusFour
			(4128, (r, fp) => fp.Path[fp.Last] += r.ReadUBitVarFieldPath() + 5), // PlusN
			(35, (r, fp) => { fp.Last++; fp.Path[fp.Last] = 0; }), // PushOneLeftDeltaZeroRightZero
			(3, (r, fp) => { fp.Last++; fp.Path[fp.Last] = r.ReadUBitVarFieldPath(); }), // PushOneLeftDeltaZeroRightNonZero
			(521, (r, fp) => { fp.Path[fp.Last]++; fp.Last++; fp.Path[fp.Last] = 0; }), // PushOneLeftDeltaOneRightZero
			(2942, (r, fp) => { fp.Path[fp.Last]++; fp.Last++; fp.Path[fp.Last] = r.ReadUBitVarFieldPath(); }), // PushOneLeftDeltaOneRightNonZero
			(560, (r, fp) => { fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); fp.Last++; fp.Path[fp.Last] = 0; }), // PushOneLeftDeltaNRightZero
			(471, (r, fp) => { fp.Path[fp.Last] += r.ReadUBitVarFieldPath() + 2; fp.Last++; fp.Path[fp.Last] = r.ReadUBitVarFieldPath() + 1; }), // PushOneLeftDeltaNRightNonZero
			(10530, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadBits(3) + 2; fp.Last++; fp.Path[fp.Last] = (int)r.ReadBits(3) + 1; }), // PushOneLeftDeltaNRightNonZeroPack6Bits
			(251, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadBits(4) + 2; fp.Last++; fp.Path[fp.Last] = (int)r.ReadBits(4) + 1; }), // PushOneLeftDeltaNRightNonZeroPack8Bits
			(0, (r, fp) => { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); }), // PushTwoLeftDeltaZero
			(0, (r, fp) => { fp.Last++; fp.Path[fp.Last] = (int)r.ReadBits(5); fp.Last++; fp.Path[fp.Last] = (int)r.ReadBits(5); }), // PushTwoPack5LeftDeltaZero
			(0, (r, fp) => { for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); } }), // PushThreeLeftDeltaZero
			(0, (r, fp) => { for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] = (int)r.ReadBits(5); } }), // PushThreePack5LeftDeltaZero
			(0, (r, fp) => { fp.Path[fp.Last]++; for (int i = 0; i < 2; i++) { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); } }), // PushTwoLeftDeltaOne
			(0, (r, fp) => { fp.Path[fp.Last]++; for (int i = 0; i < 2; i++) { fp.Last++; fp.Path[fp.Last] += (int)r.ReadBits(5); } }), // PushTwoPack5LeftDeltaOne
			(0, (r, fp) => { fp.Path[fp.Last]++; for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); } }), // PushThreeLeftDeltaOne
			(0, (r, fp) => { fp.Path[fp.Last]++; for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] += (int)r.ReadBits(5); } }), // PushThreePack5LeftDeltaOne
			(0, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadUBitVar() + 2; for (int i = 0; i < 2; i++) { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); } }), // PushTwoLeftDeltaN
			(0, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadUBitVar() + 2; for (int i = 0; i < 2; i++) { fp.Last++; fp.Path[fp.Last] += (int)r.ReadBits(5); } }), // PushTwoPack5LeftDeltaN
			(0, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadUBitVar() + 2; for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] += r.ReadUBitVarFieldPath(); } }), // PushThreeLeftDeltaN
			(0, (r, fp) => { fp.Path[fp.Last] += (int)r.ReadUBitVar() + 2; for (int i = 0; i < 3; i++) { fp.Last++; fp.Path[fp.Last] += (int)r.ReadBits(5); } }), // PushThreePack5LeftDeltaN
			(0, (r, fp) => // PushN
			{
				int n = (int)r.ReadUBitVar();
				fp.Path[fp.Last] += (int)r.ReadUBitVar();
				for (int i = 0; i < n; i++)
				{
					fp.Last++;
					fp.Path[fp.Last] += r.ReadUBitVarFieldPath();
				}
			}),
			(310, (r, fp) => // PushNAndNonTopological
			{
				for (int i = 0; i <= fp.Last; i++)
				{
					if (r.ReadBool())
					{
						fp.Path[i] += r.ReadVarInt32() + 1;
					}
				}

				int count = (int)r.ReadUBitVar();
				for (int i = 0; i < count; i++)
				{
					fp.Last++;
					fp.Path[fp.Last] = r.ReadUBitVarFieldPath();
				}
			}),
			(2, (r, fp) => { fp.Pop(1); fp.Path[fp.Last]++; }), // PopOnePlusOne
			(0, (r, fp) => { fp.Pop(1); fp.Path[fp.Last] += r.ReadUBitVarFieldPath() + 1; }), // PopOnePlusN
			(1837, (r, fp) => { fp.Pop(fp.Last); fp.Path[0]++; }), // PopAllButOnePlusOne
			(149, (r, fp) => { fp.Pop(fp.Last); fp.Path[0] += r.ReadUBitVarFieldPath() + 1; }), // PopAllButOnePlusN
			(300, (r, fp) => { fp.Pop(fp.Last); fp.Path[0] += (int)r.ReadBits(3) + 1; }), // PopAllButOnePlusNPack3Bits
			(634, (r, fp) => { fp.Pop(fp.Last); fp.Path[0] += (int)r.ReadBits(6) + 1; }), // PopAllButOnePlusNPack6Bits
			(0, (r, fp) => { fp.Pop(r.ReadUBitVarFieldPath()); fp.Path[fp.Last]++; }), // PopNPlusOne
			(0, (r, fp) => { fp.Pop(r.ReadUBitVarFieldPath()); fp.Path[fp.Last] += r.ReadVarInt32(); }), // PopNPlusN
			(1, (r, fp) => // PopNAndNonTopographical
			{
				fp.Pop(r.ReadUBitVarFieldPath());
				for (int i = 0; i <= fp.Last; i++)
				{
					if (r.ReadBool())
					{
						fp.Path[i] += r.ReadVarInt32();
					}
				}
			}),
			(76, (r, fp) => // NonTopoComplex
			{
				for (int i = 0; i <= fp.Last; i++)
				{
					if (r.ReadBool())
					{
						fp.Path[i] += r.ReadVarInt32();
					}
				}
			}),
			(271, (r, fp) => fp.Path[fp.Last - 1]++), // NonTopoPenultimatePlusOne
			(99, (r, fp) => // NonTopoComplexPack4Bits
			{
				for (int i = 0; i <= fp.Last; i++)
				{
					if (r.ReadBool())
					{
						fp.Path[i] += (int)r.ReadBits(4) - 7;
					}
				}
			}),
			(25474, (r, fp) => fp.Done = true), // FieldPathEncodeFinish
		];

		private static readonly Action<EntityReader, FieldPath>[] Ops = OpTable.Select(static op => op.Run).ToArray();

		public static IReadOnlyList<int> Weights { get; } = OpTable.Select(static op => op.Weight).ToArray();
	}

	// ---- huffman ----------------------------------------------------------------------------------------------------

	private sealed class HuffmanNode(int weight, int value, HuffmanNode? left, HuffmanNode? right)
	{
		public int Weight { get; } = weight;

		public int Value { get; } = value;

		public HuffmanNode? Left { get; } = left;

		public HuffmanNode? Right { get; } = right;

		public bool IsLeaf => Left is null;
	}

	/// <summary>
	/// The field path op code. Built the same way the engine does (Go container/heap ordering, ties broken towards the larger
	/// value), because the exact tree shape defines the bit codes.
	/// </summary>
	private static class Huffman
	{
		public static readonly HuffmanNode Root = Build();

		private static bool Less(List<HuffmanNode> heap, int i, int j)
			=> heap[i].Weight == heap[j].Weight ? heap[i].Value >= heap[j].Value : heap[i].Weight < heap[j].Weight;

		private static void Swap(List<HuffmanNode> heap, int i, int j) => (heap[i], heap[j]) = (heap[j], heap[i]);

		private static void Up(List<HuffmanNode> heap, int j)
		{
			while (true)
			{
				int i = (j - 1) / 2;
				if (i == j || !Less(heap, j, i))
				{
					break;
				}

				Swap(heap, i, j);
				j = i;
			}
		}

		private static void Down(List<HuffmanNode> heap, int i0, int n)
		{
			int i = i0;
			while (true)
			{
				int j1 = (2 * i) + 1;
				if (j1 >= n || j1 < 0)
				{
					break;
				}

				int j = j1;
				int j2 = j1 + 1;
				if (j2 < n && Less(heap, j2, j1))
				{
					j = j2;
				}

				if (!Less(heap, j, i))
				{
					break;
				}

				Swap(heap, i, j);
				i = j;
			}
		}

		private static HuffmanNode Pop(List<HuffmanNode> heap)
		{
			int n = heap.Count - 1;
			Swap(heap, 0, n);
			Down(heap, 0, n);
			HuffmanNode popped = heap[^1];
			heap.RemoveAt(heap.Count - 1);
			return popped;
		}

		private static HuffmanNode Build()
		{
			List<HuffmanNode> heap = [];
			IReadOnlyList<int> weights = FieldPath.Weights;
			for (int value = 0; value < weights.Count; value++)
			{
				heap.Add(new HuffmanNode(weights[value] == 0 ? 1 : weights[value], value, null, null));
			}

			for (int i = (heap.Count / 2) - 1; i >= 0; i--)
			{
				Down(heap, i, heap.Count);
			}

			int next = 40;
			while (heap.Count > 1)
			{
				HuffmanNode a = Pop(heap);
				HuffmanNode b = Pop(heap);
				heap.Add(new HuffmanNode(a.Weight + b.Weight, next++, a, b));
				Up(heap, heap.Count - 1);
			}

			return heap[0];
		}
	}

	// ---- decoders ---------------------------------------------------------------------------------------------------

	private sealed class FieldDecoder(Func<EntityReader, object?> decode, bool isCollection = false)
	{
		/// <summary>True for the size prefix of a variable-length collection.</summary>
		public bool IsCollection { get; } = isCollection;

		public object? Decode(EntityReader reader) => decode(reader);
	}

	private static class FieldDecoders
	{
		private static FieldDecoder Of(Func<EntityReader, object?> decode) => new(decode);

		private static readonly FieldDecoder Unsigned = Of(static r => (ulong)r.ReadVarUInt32());
		private static readonly FieldDecoder Signed = Of(static r => (long)r.ReadVarInt32());
		private static readonly FieldDecoder Unsigned64 = Of(static r => r.ReadVarUInt64());
		private static readonly FieldDecoder Bool = Of(static r => r.ReadBool());
		private static readonly FieldDecoder Str = Of(static r => r.ReadString());
		private static readonly FieldDecoder NoScale = Of(static r => { float v = BitConverter.UInt32BitsToSingle(r.ReadBits(32)); return r.Capture ? v : null; });
		private static readonly FieldDecoder Component = Of(static r => r.ReadBits(1));
		private static readonly FieldDecoder BinaryBlock = Of(static r => { r.ReadBytes((int)r.ReadVarUInt32()); return null; });
		private static readonly FieldDecoder Ammo = Of(static r => (long)r.ReadVarUInt32() - 1);

		private static readonly Dictionary<string, FieldDecoder> ByType = BuildByType();

		private static Dictionary<string, FieldDecoder> BuildByType()
		{
			Dictionary<string, FieldDecoder> map = new()
			{
				["bool"] = Bool,
				["char"] = Str,
				["CUtlString"] = Str,
				["CUtlSymbolLarge"] = Str,
				["CGlobalSymbol"] = Str,
				["GameTime_t"] = NoScale,
				["CUtlBinaryBlock"] = BinaryBlock,
				["CBodyComponent"] = Component,
				["CPhysicsComponent"] = Component,
				["CLightComponent"] = Component,
				["CRenderComponent"] = Component,
				["ResourceId_t"] = Unsigned64,
			};

			foreach (string name in new[]
			{
				"uint8", "uint16", "uint32", "CHandle", "Color", "CUtlStringToken", "EHandle", "CEntityHandle", "CGameSceneNodeHandle",
				"CStrongHandle", "AttachmentHandle_t", "MoveCollide_t", "MoveType_t", "RenderMode_t", "RenderFx_t", "SolidType_t",
				"SurroundingBoundsType_t", "ModelConfigHandle_t", "WeaponState_t", "DoorState_t", "BeamClipStyle_t",
				"ValueRemapperInputType_t", "ValueRemapperOutputType_t", "ValueRemapperHapticsType_t", "ValueRemapperMomentumType_t",
				"ValueRemapperRatchetType_t", "PointWorldTextJustifyHorizontal_t", "PointWorldTextJustifyVertical_t",
				"PointWorldTextReorientMode_t", "PoseController_FModType_t", "ShardSolid_t", "ShatterPanelMode", "gender_t",
				"item_definition_index_t", "itemid_t", "style_index_t", "attributeprovidertypes_t", "DamageOptions_t", "ScreenEffectType_t",
				"MaterialModifyMode_t", "CSWeaponMode", "ESurvivalSpawnTileState", "SpawnStage_t", "ESurvivalGameRuleDecision_t",
				"RelativeDamagedDirection_t", "CSPlayerState", "MedalRank_t", "CSPlayerBlockingUseAction_t", "MoveMountingAmount_t",
				"QuestProgress::Reason", "tablet_skin_state_t",
			})
			{
				map[name] = Unsigned;
			}

			foreach (string name in new[]
			{
				"int8", "int16", "int32", "HSequence", "CEntityIndex", "NPC_STATE", "StanceType_t", "RagdollBlendDirection", "BeamType_t",
				"EntityDisolveType_t", "PrecipitationType_t", "AmmoIndex_t", "TakeDamageFlags_t",
			})
			{
				map[name] = Signed;
			}

			return map;
		}

		/// <summary>Decoder for types that depend on the field's own encoding settings (float / vector / quaternion families).</summary>
		private static FieldDecoder? FromFactory(string baseType, Field field) => baseType switch
		{
			"float32" => Float(field),
			"CNetworkedQuantizedFloat" => Quantized(field),
			"uint64" or "CStrongHandle" => field.Encoder == "fixed64" ? Of(static r => BitConverter.ToUInt64(r.ReadBytes(8), 0)) : Unsigned64,
			"Vector" or "VectorWS" => Vector(field, 3),
			"Vector2D" => Vector(field, 2),
			"Vector4D" or "Quaternion" => Vector(field, 4),
			"CTransform" => Vector(field, 6),
			"QAngle" => QAngle(field),
			_ => null,
		};

		public static FieldDecoder Find(Field field)
		{
			string baseType = field.FieldType.BaseType;
			if (FromFactory(baseType, field) is { } fromFactory)
			{
				return fromFactory;
			}

			if (field.VarName == "m_iClip1")
			{
				return Ammo;
			}

			return ByType.GetValueOrDefault(baseType) ?? Unsigned;
		}

		public static FieldDecoder FindByGenericType(Field field)
		{
			string baseType = field.FieldType.GenericType!.BaseType;
			return FromFactory(baseType, field) ?? ByType.GetValueOrDefault(baseType) ?? Unsigned;
		}

		private static FieldDecoder Float(Field field)
		{
			switch (field.Encoder)
			{
				case "coord":
					return Of(static r => { float v = r.ReadCoord(); return r.Capture ? v : null; });
				case "simtime":
					return Of(static r => { float v = r.ReadVarUInt32() * (1f / 64); return r.Capture ? v : null; });
				case "runetime":
					return Of(static r => { r.ReadBits(4); return null; });
			}

			return field.BitCount is null or <= 0 or >= 32 ? NoScale : Quantized(field);
		}

		private static FieldDecoder Quantized(Field field)
		{
			if (field.BitCount is null or <= 0 or >= 32)
			{
				return NoScale;
			}

			QuantizedFloat quantized = new(field.BitCount.Value, field.EncodeFlags, field.LowValue, field.HighValue);
			return Of(r => { float v = quantized.Decode(r); return r.Capture ? v : null; });
		}

		private static FieldDecoder Vector(Field field, int components)
		{
			if (components == 3 && field.Encoder == "normal")
			{
				return Of(static r =>
				{
					bool hasX = r.ReadBool();
					bool hasY = r.ReadBool();
					if (hasX)
					{
						r.ReadBool();
						r.ReadBits(11);
					}

					if (hasY)
					{
						r.ReadBool();
						r.ReadBits(11);
					}

					r.ReadBool();
					return null;
				});
			}

			FieldDecoder component = Float(field);
			return Of(r =>
			{
				float[]? values = r.Capture ? new float[components] : null;
				for (int i = 0; i < components; i++)
				{
					object? v = component.Decode(r);
					if (values is not null && v is float f)
					{
						values[i] = f;
					}
				}

				return values;
			});
		}

		private static FieldDecoder QAngle(Field field)
		{
			if (field.Encoder == "qangle_precise")
			{
				return Of(static r =>
				{
					bool hasX = r.ReadBool();
					bool hasY = r.ReadBool();
					bool hasZ = r.ReadBool();
					for (int i = 0; i < (hasX ? 1 : 0) + (hasY ? 1 : 0) + (hasZ ? 1 : 0); i++)
					{
						r.ReadBits(20);
					}

					return null;
				});
			}

			if (field.BitCount is { } bits and not 0)
			{
				int width = bits >= 32 ? 32 : bits;
				return Of(r =>
				{
					for (int i = 0; i < 3; i++)
					{
						r.ReadBits(width);
					}

					return null;
				});
			}

			return Of(static r =>
			{
				bool x = r.ReadBool();
				bool y = r.ReadBool();
				bool z = r.ReadBool();
				if (x)
				{
					r.ReadCoord();
				}

				if (y)
				{
					r.ReadCoord();
				}

				if (z)
				{
					r.ReadCoord();
				}

				return null;
			});
		}
	}

	/// <summary>
	/// Quantized float layout. Only the number of bits consumed matters here, and that depends on which of the optional
	/// round-up / round-down / encode-zero flag bits survive validation, so the engine's arithmetic is reproduced in single precision.
	/// </summary>
	private sealed class QuantizedFloat
	{
		private const uint RoundDown = 1 << 0;
		private const uint RoundUp = 1 << 1;
		private const uint EncodeZero = 1 << 2;
		private const uint EncodeIntegers = 1 << 3;

		private float _low;
		private float _high;
		private float _highLowMul;
		private float _decMul;
		private int _bitCount;
		private uint _flags;

		public QuantizedFloat(int bitCount, int? flags, float? low, float? high)
		{
			_bitCount = bitCount;
			_low = low ?? 0f;
			_high = high ?? 1f;
			_flags = (uint)(flags ?? 0);

			ValidateFlags();

			long steps = 1L << _bitCount;
			float offset;
			if ((_flags & RoundDown) != 0)
			{
				offset = (_high - _low) / steps;
				_high -= offset;
			}
			else if ((_flags & RoundUp) != 0)
			{
				offset = (_high - _low) / steps;
				_low += offset;
			}

			if ((_flags & EncodeIntegers) != 0)
			{
				float delta = _high - _low;
				if (delta < 1)
				{
					delta = 1;
				}

				double deltaLog2 = Math.Ceiling(Math.Log2(delta));
				long range2 = 1L << (int)deltaLog2;
				int bc = _bitCount;
				while ((1L << bc) <= range2)
				{
					bc++;
				}

				if (bc > _bitCount)
				{
					_bitCount = bc;
					steps = 1L << _bitCount;
				}

				offset = (float)range2 / steps;
				_high = _low + range2 - offset;
			}

			AssignMultipliers((uint)steps);

			if ((_flags & RoundDown) != 0 && Quantize(_low) == _low)
			{
				_flags &= ~RoundDown;
			}

			if ((_flags & RoundUp) != 0 && Quantize(_high) == _high)
			{
				_flags &= ~RoundUp;
			}

			if ((_flags & EncodeZero) != 0 && Quantize(0f) == 0f)
			{
				_flags &= ~EncodeZero;
			}
		}

		private void ValidateFlags()
		{
			if (_flags == 0)
			{
				return;
			}

			if ((_low == 0f && (_flags & RoundDown) != 0) || (_high == 0f && (_flags & RoundUp) != 0))
			{
				_flags &= ~EncodeZero;
			}

			if (_low == 0f && (_flags & EncodeZero) != 0)
			{
				_flags |= RoundDown;
				_flags &= ~EncodeZero;
			}

			if (_high == 0f && (_flags & EncodeZero) != 0)
			{
				_flags |= RoundUp;
				_flags &= ~EncodeZero;
			}

			if (_low > 0f || _high < 0f)
			{
				_flags &= ~EncodeZero;
			}

			if ((_flags & EncodeIntegers) != 0)
			{
				_flags &= ~(RoundUp | RoundDown | EncodeZero);
			}
		}

		private static readonly float[] Multipliers = [0.9999f, 0.99f, 0.9f, 0.8f, 0.7f];

		private void AssignMultipliers(uint steps)
		{
			float range = _high - _low;
			uint high = _bitCount == 32 ? 0xFFFFFFFE : (1u << _bitCount) - 1;

			float highMul = Math.Abs(range) <= 0f ? high : high / range;

			static bool Exceeds(float highMul, float range, uint high)
			{
				float product = highMul * range;
				return product > high || (double)product > high;
			}

			if (Exceeds(highMul, range, high))
			{
				foreach (float multiplier in Multipliers)
				{
					highMul = high / range * multiplier;
					if (Exceeds(highMul, range, high))
					{
						continue;
					}

					break;
				}
			}

			_highLowMul = highMul;
			_decMul = 1f / (steps - 1);
		}

		private float Quantize(float value)
		{
			if (value < _low)
			{
				return _low;
			}

			if (value > _high)
			{
				return _high;
			}

			uint i = (uint)((float)((value - _low) * _highLowMul) + 0.5f);
			return _low + (float)((_high - _low) * (float)(i * _decMul));
		}

		/// <summary>Reads one encoded value (always consumes exactly the bits the engine wrote).</summary>
		public float Decode(EntityReader r)
		{
			if ((_flags & RoundDown) != 0 && r.ReadBool())
			{
				return _low;
			}

			if ((_flags & RoundUp) != 0 && r.ReadBool())
			{
				return _high;
			}

			if ((_flags & EncodeZero) != 0 && r.ReadBool())
			{
				return 0f;
			}

			return _low + (float)((_high - _low) * (float)r.ReadBits(_bitCount) * _decMul);
		}
	}
}

/// <summary>LSB-first bit reader over a byte buffer (entity update streams).</summary>
internal sealed class EntityReader(byte[] buffer)
{
	private int _pos;
	private ulong _bitVal;
	private int _bitCount;

	/// <summary>When set, float/vector decoders return their values instead of just consuming the bits.</summary>
	public bool Capture { get; set; }

	public uint ReadBits(int n)
	{
		while (n > _bitCount)
		{
			if (_pos >= buffer.Length)
			{
				throw new EndOfStreamException("entity bitstream exhausted");
			}

			_bitVal |= (ulong)buffer[_pos++] << _bitCount;
			_bitCount += 8;
		}

		uint value = (uint)(_bitVal & ((1UL << n) - 1));
		_bitVal >>= n;
		_bitCount -= n;
		return value;
	}

	public bool ReadBool() => ReadBits(1) == 1;

	public byte ReadByte() => (byte)ReadBits(8);

	public byte[] ReadBytes(int count)
	{
		if (count < 0 || count > buffer.Length - _pos + (_bitCount / 8))
		{
			throw new EndOfStreamException("entity bitstream exhausted");
		}

		byte[] bytes = new byte[count];
		for (int i = 0; i < count; i++)
		{
			bytes[i] = ReadByte();
		}

		return bytes;
	}

	public uint ReadVarUInt32()
	{
		uint value = 0;
		int shift = 0;
		while (true)
		{
			uint b = ReadByte();
			value |= (b & 0x7F) << shift;
			shift += 7;
			if ((b & 0x80) == 0 || shift == 35)
			{
				return value;
			}
		}
	}

	public int ReadVarInt32()
	{
		uint ux = ReadVarUInt32();
		int x = (int)(ux >> 1);
		return (ux & 1) != 0 ? ~x : x;
	}

	public ulong ReadVarUInt64()
	{
		ulong value = 0;
		int shift = 0;
		while (true)
		{
			byte b = ReadByte();
			if (b < 0x80)
			{
				return value | ((ulong)b << shift);
			}

			value |= (ulong)(b & 0x7F) << shift;
			shift += 7;
			if (shift > 63)
			{
				throw new InvalidDataException("varint overflows uint64");
			}
		}
	}

	public uint ReadUBitVar()
	{
		uint ret = ReadBits(6);
		switch (ret & 0x30)
		{
			case 16:
				ret = (ret & 15) | (ReadBits(4) << 4);
				break;
			case 32:
				ret = (ret & 15) | (ReadBits(8) << 4);
				break;
			case 48:
				ret = (ret & 15) | (ReadBits(28) << 4);
				break;
		}

		return ret;
	}

	public int ReadUBitVarFieldPath()
	{
		if (ReadBool())
		{
			return (int)ReadBits(2);
		}

		if (ReadBool())
		{
			return (int)ReadBits(4);
		}

		if (ReadBool())
		{
			return (int)ReadBits(10);
		}

		if (ReadBool())
		{
			return (int)ReadBits(17);
		}

		return (int)ReadBits(31);
	}

	public string ReadString()
	{
		List<byte> bytes = [];
		while (true)
		{
			byte b = ReadByte();
			if (b == 0)
			{
				return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
			}

			bytes.Add(b);
		}
	}

	public float ReadCoord()
	{
		uint intVal = ReadBits(1);
		uint fractVal = ReadBits(1);
		if (intVal == 0 && fractVal == 0)
		{
			return 0f;
		}

		bool negative = ReadBool();
		if (intVal != 0)
		{
			intVal = ReadBits(14) + 1;
		}

		if (fractVal != 0)
		{
			fractVal = ReadBits(5);
		}

		float value = intVal + (fractVal * (1f / 32));
		return negative ? -value : value;
	}
}
