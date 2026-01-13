namespace StrikeLink.GSI.Parsing
{
	internal sealed class GsiDispatcher(IEnumerable<IGsiParser> parsers)
	{
		private readonly IGsiParser[] _parsers = parsers.ToArray();

		internal IEnumerable<IGsiPayload> Dispatch(JsonDocument document)
		{
			JsonElement root = document.RootElement;

			foreach (IGsiParser parser in _parsers.Where(parser => parser.CanParse(root))) yield return parser.Parse(root);
		}
	}

}
