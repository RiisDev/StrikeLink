namespace StrikeLink.Services.Config
{
	/// <summary>
	/// Provides methods for serializing <see cref="ConfigDocument"/> instances
	/// into Valve configuration file format.
	/// </summary>
	public static class ValveCfgWriter
	{
		/// <summary>
		/// Serializes a configuration document into its textual representation.
		/// </summary>
		/// <param name="document">
		/// The configuration document to write.
		/// </param>
		/// <param name="options">
		/// Optional writer options controlling formatting behavior.
		/// If <c>null</c>, <see cref="ConfigWriterOptions.Default"/> is used.
		/// </param>
		/// <returns>
		/// A string containing the serialized configuration document.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="document"/> is <c>null</c>.
		/// </exception>
		public static string Write(ConfigDocument document, ConfigWriterOptions? options = null)
		{
			ArgumentNullException.ThrowIfNull(document);

			options ??= ConfigWriterOptions.Default;

			StringBuilder builder = new();
			WriteNode(builder, document.Node, options, indentLevel: 0);
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

	/// <summary>
	/// Represents configuration options for writing Valve configuration documents.
	/// </summary>
	public sealed class ConfigWriterOptions
	{
		/// <summary>
		/// Gets the default writer options.
		/// </summary>
		public static ConfigWriterOptions Default { get; } = new();

		/// <summary>
		/// Gets the number of spaces used for indentation.
		/// </summary>
		/// <remarks>
		/// The default value is <c>4</c>.
		/// </remarks>
		public int IndentSize { get; init; } = 4;

		/// <summary>
		/// Gets a value indicating whether a trailing newline is written at the end of the document.
		/// </summary>
		/// <remarks>
		/// The default value is <c>true</c>.
		/// </remarks>
		public bool WriteTrailingNewline { get; init; } = true;
	}

}
