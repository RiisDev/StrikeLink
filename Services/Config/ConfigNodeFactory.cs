#pragma warning disable CA1720
namespace StrikeLink.Services.Config
{
	/// <summary>
	/// Provides factory methods for creating <see cref="ConfigNode"/> instances.
	/// </summary>
	public static class ConfigNodeFactory
	{
		/// <summary>
		/// Creates a configuration node that represents an object with named properties.
		/// </summary>
		/// <param name="properties">
		/// The dictionary of child property nodes.
		/// </param>
		/// <returns>
		/// A <see cref="ConfigNode"/> of type <see cref="ConfigNodeType.Object"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="properties"/> is <c>null</c>.
		/// </exception>
		public static ConfigNode Object(IDictionary<string, ConfigNode> properties) => new(ConfigNodeType.Object, value: null, properties: new Dictionary<string, ConfigNode>(properties));
		
		/// <summary>
		/// Creates a configuration node that represents a primitive value.
		/// </summary>
		/// <param name="value">
		/// The string value of the node.
		/// </param>
		/// <returns>
		/// A <see cref="ConfigNode"/> of type <see cref="ConfigNodeType.Value"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public static ConfigNode Value(string value) => new(ConfigNodeType.Value, value, properties: null);
	}
}
