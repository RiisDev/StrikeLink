using StrikeLink.GSI.ObjectStates;
using System.ComponentModel;

namespace StrikeLink.GSI.Parsing
{
	internal sealed class RoundStateParser : IGsiParser
	{
		[EditorBrowsable(EditorBrowsableState.Never)] // Fix public intellisense
		public bool CanParse(JsonElement root) => root.TryGetProperty("round", out JsonElement playerElement) && playerElement.TryGetProperty("phase", out _);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public IGsiPayload Parse(JsonElement root)
		{
			JsonElement player = root.GetProperty("round");

			return new RoundState(
				Phase: Enum.TryParse(player.GetProperty("phase").GetString()!, true, out RoundPhase phase) ? phase : RoundPhase.Live,
				WinTeam: player.TryGetProperty("win_team", out JsonElement winTeamElement) ? TeamParse(winTeamElement) : null,
				BombState: player.TryGetProperty("bomb", out JsonElement bombElement) ? BombParse(bombElement) : null
			);
		}

		private static Team TeamParse(JsonElement input)
		{
			string? value = input.GetString();
			return value switch
			{
				"CT" => Team.CounterTerrorist,
				"T" => Team.Terrorist,
				_ => throw new NotImplementedException()
			};
		}

		private static BombState BombParse(JsonElement input) => Enum.TryParse(input.GetString()!, true, out BombState state) ? state : BombState.Planted;
	}
}
