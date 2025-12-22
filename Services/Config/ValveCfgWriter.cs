namespace StrikeLink.Services.Config
{
	public static class ValveCfgWriter
	{
		public static string Write(ConfigDocument document, ConfigWriterOptions? options = null)
		{
			ArgumentNullException.ThrowIfNull(document);

			options ??= ConfigWriterOptions.Default;

			StringBuilder builder = new();
			WriteNode(builder, document.Root, options, indentLevel: 0);
			return builder.ToString();
		}

		private static void WriteNode(StringBuilder builder, ConfigNode node, ConfigWriterOptions options, int indentLevel)
		{
			if (node.NodeType != ConfigNodeType.Object) throw new InvalidOperationException("Root node must be an object.");

			foreach (KeyValuePair<string, ConfigNode> kvp in node.EnumerateObject())
			{
				WriteIndent(builder, options, indentLevel);
				WriteQuoted(builder, kvp.Key);
				builder.AppendLine();

				WriteIndent(builder, options, indentLevel);
				builder.AppendLine("{");

				WriteObjectContents(builder, kvp.Value, options, indentLevel + 1);

				WriteIndent(builder, options, indentLevel);
				builder.AppendLine("}");
			}

			if (options.WriteTrailingNewline) builder.AppendLine();
		}

		private static void WriteObjectContents(StringBuilder builder, ConfigNode node, ConfigWriterOptions options, int indentLevel)
		{
			foreach (KeyValuePair<string, ConfigNode> kvp in node.EnumerateObject())
			{
				WriteIndent(builder, options, indentLevel);
				WriteQuoted(builder, kvp.Key);

				builder.Append('\t');

				if (kvp.Value.NodeType == ConfigNodeType.Value)
				{
					WriteQuoted(builder, kvp.Value.GetString());
					builder.AppendLine();
				}
				else
				{
					builder.AppendLine();
					WriteIndent(builder, options, indentLevel);
					builder.AppendLine("{");

					WriteObjectContents(builder, kvp.Value, options, indentLevel + 1);

					WriteIndent(builder, options, indentLevel);
					builder.AppendLine("}");
				}
			}
		}

		private static void WriteIndent(StringBuilder builder, ConfigWriterOptions options, int indentLevel) => builder.Append(' ', indentLevel * options.IndentSize);

		private static void WriteQuoted(StringBuilder builder, string value)
		{
			builder.Append('"');
			builder.Append(value);
			builder.Append('"');
		}
	}

	public sealed class ConfigWriterOptions
	{
		public static ConfigWriterOptions Default { get; } = new();

		public int IndentSize { get; init; } = 4;
		public bool WriteTrailingNewline { get; init; } = true;
	}
}
