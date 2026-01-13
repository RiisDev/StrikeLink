#pragma warning disable CA1720 // Identifier contains type name
#pragma warning disable CA1024 // Use properties where appropriate
namespace StrikeLink.Services.Config
{
	/// <summary>
	/// Represents a parsed configuration document.
	/// </summary>
	/// <param name="Node">
	/// The root configuration node produced by the parser.
	/// </param>
	public sealed record ConfigDocument(ConfigNode Node)
	{
		/// <summary>
		/// Gets the root object node of the configuration document.
		/// </summary>
		public ConfigNode Root { get; } = Node.EnumerateObject().First().Value;
	};

	/// <summary>
	/// Specifies the type of node <see cref="ConfigNode"/>.
	/// </summary>
	public enum ConfigNodeType
	{
		/// <summary>
		/// Represents an object node containing named child properties.
		/// </summary>
		Object,

		/// <summary>
		/// Represents a primitive value node.
		/// </summary>
		Value
	}

	/// <summary>
	/// Represents a node within a hierarchical configuration structure.
	/// </summary>
	/// <remarks>
	/// A node may represent either an object with named properties or a single value.
	/// </remarks>
	public readonly struct ConfigNode
	{
		private readonly string? _value;
		private readonly IReadOnlyDictionary<string, ConfigNode>? _properties;

		/// <summary>
		/// Gets the type of this configuration node.
		/// </summary>
		public ConfigNodeType NodeType { get; }

		internal ConfigNode(ConfigNodeType type, string? value, IReadOnlyDictionary<string, ConfigNode>? properties)
		{
			_value = value;
			NodeType = type;
			_properties = properties;
		}

		/// <summary>
		/// Gets a named child property from this node.
		/// </summary>
		/// <param name="name">
		/// The name of the property.
		/// </param>
		/// <returns>
		/// The requested child <see cref="ConfigNode"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when this node is not an object.
		/// </exception>
		/// <exception cref="KeyNotFoundException">
		/// Thrown when the specified property does not exist.
		/// </exception>
		public ConfigNode GetProperty(string name)
		{
			if (NodeType != ConfigNodeType.Object) throw new InvalidOperationException("Node is not an object.");

			return !_properties!.TryGetValue(name, out ConfigNode node) ? throw new KeyNotFoundException($"Property '{name}' not found.") : node;
		}

		/// <summary>
		/// Attempts to get a named child property from this node.
		/// </summary>
		/// <param name="name">
		/// The name of the property.
		/// </param>
		/// <param name="node">
		/// When this method returns, contains the child node if found; otherwise, <c>default</c>. <see cref="ConfigNode"/>
		/// </param>
		/// <returns>
		/// <c>true</c> if the property exists; otherwise, <c>false</c>.
		/// </returns>
		public bool TryGetProperty(string name, out ConfigNode node)
		{
			if (NodeType == ConfigNodeType.Object && _properties!.TryGetValue(name, out node)) return true;

			node = default;
			return false;
		}

		/// <summary>
		/// Enumerates all child properties of this node.
		/// </summary>
		/// <returns>
		/// A sequence of property name and node pairs.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when this node is not an object.
		/// </exception>
		public IEnumerable<KeyValuePair<string, ConfigNode>> EnumerateObject() => NodeType != ConfigNodeType.Object ? throw new InvalidOperationException("Node is not an object.") : _properties!;
		
		/// <summary>
		/// Gets the string value of this node.
		/// </summary>
		/// <returns>
		/// The string value represented by this node.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when this node is not a value node.
		/// </exception>
		public string GetString() => NodeType != ConfigNodeType.Value ? throw new InvalidOperationException("Node is not a value.") : _value!;
		
		/// <summary>
		/// Gets the value of this node as a 32-bit integer.
		/// </summary>
		/// <returns>
		/// The parsed 32-bit integer value.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when this node is not a value node or the value cannot be parsed.
		/// </exception>
		public int GetInt32()
		{
			if (NodeType != ConfigNodeType.Value) throw new InvalidOperationException("Node is not a value.");

			return int.TryParse(_value!, out int result) ? result : throw new InvalidOperationException("value is not an int32");
		}

		/// <summary>
		/// Gets the value of this node as a 64-bit integer.
		/// </summary>
		/// <returns>
		/// The parsed 64-bit integer value.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when this node is not a value node or the value cannot be parsed.
		/// </exception>
		public long GetInt64()
		{
			if (NodeType != ConfigNodeType.Value) throw new InvalidOperationException("Node is not a value.");

			return long.TryParse(_value!, out long result) ? result : throw new InvalidOperationException("value is not an int64");
		}
		
	}

}
