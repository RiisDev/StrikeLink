#pragma warning disable CA1720

namespace StrikeLink.Services.Config
{
	/// <summary>
	/// Represents the type of token encountered while parsing a Valve configuration file.
	/// </summary>
	public enum ConfigTokenType
	{
		/// <summary>
		/// A quoted string token.
		/// </summary>
		String,

		/// <summary>
		/// An opening brace (<c>{</c>) token.
		/// </summary>
		OpenBrace,

		/// <summary>
		/// A closing brace (<c>}</c>) token.
		/// </summary>
		CloseBrace,

		/// <summary>
		/// Indicates the end of the input stream.
		/// </summary>
		EndOfFile
	}

	/// <summary>
	/// Reads and parses Valve configuration files (<c>.cfg</c>, <c>.vdf</c>, <c>.vcfg</c>, <c>.acf</c>)
	/// into a structured <see cref="ConfigDocument"/>.
	/// </summary>
	public class ValveCfgReader
	{
		/// <summary>
		/// Gets the name of the configuration root object.
		/// </summary>
		public string ConfigName { get; private set; }

		/// <summary>
		/// Gets the file name (without extension) of the parsed configuration file.
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		/// Gets the parsed configuration document.
		/// </summary>
		public ConfigDocument Document { get; private set; }

		private readonly List<string> _supportedExtensions =
		[
			".cfg",
			".vdf",
			".vcfg",
			".acf"
		];

		/// <summary>
		/// Initializes a new instance of the <see cref="ValveCfgReader"/> class
		/// and parses the specified configuration file.
		/// </summary>
		/// <param name="filePath">
		/// The path to the configuration file.
		/// </param>
		/// <exception cref="FileNotFoundException">
		/// Thrown when the specified file does not exist.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the file format or structure is invalid.
		/// </exception>
		public ValveCfgReader(string filePath)
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException($"{filePath} not found.");

			string fileExtension = Path.GetExtension(filePath);
			FileName = Path.GetFileNameWithoutExtension(filePath);

			if (!_supportedExtensions.Contains(fileExtension))
				throw new InvalidOperationException($"{fileExtension} is not a valid file format.");

			string[] fileLines = File.ReadAllText(filePath).Split([Environment.NewLine], StringSplitOptions.None);

			if (!fileLines[0].Contains('"', StringComparison.InvariantCulture))
				throw new InvalidOperationException("File structure is not valid");

			ConfigName = fileLines[0].Trim('"').Trim();

			Document = Parse(File.ReadAllText(filePath).AsSpan());
		}

		private static ConfigDocument Parse(ReadOnlySpan<char> input)
		{
			ConfigReader reader = new(input);

			reader.Read();
			string rootName = reader.StringValue!;

			reader.Read();

			ConfigNode rootObject = ReadObject(ref reader);

			Dictionary<string, ConfigNode> root = new() { [rootName] = rootObject };

			return new ConfigDocument(new ConfigNode(ConfigNodeType.Object, null, root));
		}

		private static ConfigNode ReadObject(ref ConfigReader reader)
		{
			Dictionary<string, ConfigNode> properties = new(StringComparer.OrdinalIgnoreCase);

			while (reader.Read())
			{
				if (reader.TokenType == ConfigTokenType.CloseBrace) break;
				if (reader.TokenType != ConfigTokenType.String) throw new FormatException("Expected string key.");

				string key = reader.StringValue!;

				reader.Read();

				switch (reader.TokenType)
				{
					case ConfigTokenType.String:
						properties[key] = new ConfigNode(ConfigNodeType.Value, reader.StringValue, null);
						break;
					case ConfigTokenType.OpenBrace:
						ConfigNode child = ReadObject(ref reader);
						properties[key] = child;
						break;
					default:
						throw new FormatException("Invalid object member.");
				}
			}

			return new ConfigNode(ConfigNodeType.Object, null, properties);
		}
	}

	internal ref struct ConfigReader(ReadOnlySpan<char> input)
	{
		private readonly ReadOnlySpan<char> _input = input;
		private int _position = 0;

		internal ConfigTokenType TokenType { get; private set; }

		internal string? StringValue { get; private set; }

		internal bool Read()
		{
			SkipWhitespaceAndComments();

			if (_position >= _input.Length) { TokenType = ConfigTokenType.EndOfFile; return false; }

			char c = _input[_position];

			switch (c)
			{
				case '{':
					_position++;
					TokenType = ConfigTokenType.OpenBrace;
					return true;
				case '}':
					_position++;
					TokenType = ConfigTokenType.CloseBrace;
					return true;
				case '"':
					StringValue = ReadString();
					TokenType = ConfigTokenType.String;
					return true;
				default:
					throw new FormatException($"Unexpected character '{c}' at {_position}");
			}
		}
		
		private string ReadString()
		{
			_position++;

			StringBuilder builder = new();
			bool escape = false;

			while (_position < _input.Length)
			{
				char c = _input[_position++];

				if (escape)
				{
					builder.Append(c switch
					{
						'"' => '"',
						'\\' => '\\',
						'n' => '\n',
						't' => '\t',
						_ => c
					});

					escape = false;
					continue;
				}

				switch (c)
				{
					case '\\':
						escape = true;
						continue;
					case '"':
						return builder.ToString();
					default:
						builder.Append(c);
						break;
				}
			}

			throw new FormatException("Unterminated string.");
		}

		private void SkipWhitespaceAndComments()
		{
			while (_position < _input.Length)
			{
				if (char.IsWhiteSpace(_input[_position]))
				{
					_position++;
					continue;
				}

				if (_input[_position] == '/' && _position + 1 < _input.Length && _input[_position + 1] == '/')
				{
					_position += 2;

					while (_position < _input.Length && _input[_position] != '\n') _position++;

					continue;
				}

				break;
			}
		}
	}

}
