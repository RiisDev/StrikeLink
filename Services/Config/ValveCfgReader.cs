#pragma warning disable CA1720

namespace StrikeLink.Services.Config
{
	public enum ConfigTokenType
	{
		String,
		OpenBrace,
		CloseBrace,
		EndOfFile
	}

	public class ValveCfgReader
	{
		public string ConfigName { get; private set; }
		public string FileName { get; private set; }

		public ConfigDocument Document { get; private set; }

		private readonly List<string> _supportedExtensions =
		[
			".cfg",
			".vdf",
			".vcfg"
		];

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

		public static ConfigDocument Parse(ReadOnlySpan<char> input)
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

	public ref struct ConfigReader(ReadOnlySpan<char> input)
	{
		private ReadOnlySpan<char> _input = input;
		private int _position = 0;

		public ConfigTokenType TokenType { get; private set; }
		public string? StringValue { get; private set; }

		public bool Read()
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
			int start = _position;

			while (_position < _input.Length && _input[_position] != '"') _position++;

			if (_position >= _input.Length) throw new FormatException("Unterminated string.");

			string value = _input[start.._position].ToString();
			_position++;
			return value;
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
