namespace StrikeLink.GSI.Parsing
{
	// Example class, non-functional
	public sealed class PlayerStateParser : IGsiPayload, IGsiParser
	{
		public bool CanParse(JsonElement root) => root.TryGetProperty("player", out _);

		public IGsiPayload Parse(JsonElement root)
		{
			JsonElement player = root.GetProperty("player");
			return this;
		}

		public void Dispatch<T>(IGsiPayload payload, IGsiPayload? cachedPayload, ref Action<T>? action)
		{
			if (payload is PlayerState playerState)
			{

			}

		}
	}
}
