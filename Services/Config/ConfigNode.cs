#pragma warning disable CA1720 // Identifier contains type name
#pragma warning disable CA1024 // Use properties where appropriate
namespace StrikeLink.Services.Config
{
	public sealed record ConfigDocument(ConfigNode Node)
	{
		public ConfigNode Root { get; } = Node.EnumerateObject().First().Value;
	};

	public enum ConfigNodeType
	{
		Object,
		Value
	}

	public readonly struct ConfigNode
	{
		private readonly string? _value;
		private readonly IReadOnlyDictionary<string, ConfigNode>? _properties;

		public ConfigNodeType NodeType { get; }

		internal ConfigNode(ConfigNodeType type, string? value, IReadOnlyDictionary<string, ConfigNode>? properties)
		{
			_value = value;
			NodeType = type;
			_properties = properties;
		}

		public ConfigNode GetProperty(string name)
		{
			if (NodeType != ConfigNodeType.Object) throw new InvalidOperationException("Node is not an object.");

			return !_properties!.TryGetValue(name, out ConfigNode node) ? throw new KeyNotFoundException($"Property '{name}' not found.") : node;
		}

		public bool TryGetProperty(string name, out ConfigNode node)
		{
			if (NodeType == ConfigNodeType.Object && _properties!.TryGetValue(name, out node)) return true;

			node = default;
			return false;
		}

		public IEnumerable<KeyValuePair<string, ConfigNode>> EnumerateObject() => NodeType != ConfigNodeType.Object ? throw new InvalidOperationException("Node is not an object.") : _properties!;


		public string GetString() => NodeType != ConfigNodeType.Value ? throw new InvalidOperationException("Node is not a value.") : _value!;

		public int GetInt32()
		{
			if (NodeType != ConfigNodeType.Value) throw new InvalidOperationException("Node is not a value.");

			return int.TryParse(_value!, out int result) ? result : throw new InvalidOperationException("value is not an int32");
		}

		public long GetInt64()
		{
			if (NodeType != ConfigNodeType.Value) throw new InvalidOperationException("Node is not a value.");

			return long.TryParse(_value!, out long result) ? result : throw new InvalidOperationException("value is not an int64");
		}
		
	}

}
