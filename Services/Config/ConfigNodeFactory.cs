#pragma warning disable CA1720
namespace StrikeLink.Services.Config
{
	public static class ConfigNodeFactory
	{
		public static ConfigNode Object(IDictionary<string, ConfigNode> properties) => new(ConfigNodeType.Object, value: null, properties: new Dictionary<string, ConfigNode>(properties));

		public static ConfigNode Value(string value) => new(ConfigNodeType.Value, value, properties: null);
	}
}
