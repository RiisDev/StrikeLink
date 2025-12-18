namespace CounterConnect.GSI
{
	// Empty interface.
#pragma warning disable CA1040
	public interface IGsiPayload { }
#pragma warning restore CA1040

	public interface IGsiParser
	{
		bool CanParse(JsonElement root);
		IGsiPayload Parse(JsonElement root);
	}

	public sealed class PlayerStatePayload : IGsiPayload
	{
		public int Health { get; init; }
		public int Armor { get; init; }
	}

	public sealed class MatchStatePayload : IGsiPayload
	{
		public string Phase { get; init; } = string.Empty;
	}
}