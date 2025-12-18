namespace StrikeLink.GSI.Parsing
{
	// Example class, non-functional
	public sealed class PlayerStateParser : IGsiParser
	{
		public bool CanParse(JsonElement root) => root.TryGetProperty("player", out _);

		public IGsiPayload Parse(JsonElement root)
		{
			JsonElement player = root.GetProperty("player");
			try
			{
				return new PlayerStatePayload
				{
					Health = player.GetProperty("health").GetInt32(), 
					Armor = player.GetProperty("armor").GetInt32()
				};
			}
			catch
			{
				return new PlayerStatePayload
				{
					Health = -1,
					Armor = -1
				};
			}
			
		}
	}
}
