using StrikeLink.GSI.ObjectStates;

namespace StrikeLink.GSI.Parsing
{
	internal sealed class MapStateParser : IGsiParser
	{
		public bool CanParse(JsonElement root) => root.TryGetProperty("map", out _);

		public IGsiPayload Parse(JsonElement root)
		{
			JsonElement map = root.GetProperty("map");

			return new MapState(
				Mode: ParseMode(map.GetProperty("mode").GetString()),
				Name: map.GetProperty("name").GetString()!,
				Phase: ParsePhase(map.GetProperty("phase").GetString()),
				Round: map.GetProperty("round").GetInt32(),
				CounterTerroristStats: ParseStats(map.GetProperty("team_ct")),
				TerroristsStats: ParseStats(map.GetProperty("team_t")),
				MatchesToWinSeries: map.GetProperty("num_matches_to_win_series").GetInt32()
			);
		}

		public MapMode ParseMode(string? mode)
		{
			return MapMode.ArmsRace;
		}

		public MapPhase ParsePhase(string? phase)
		{
			return MapPhase.Intermission;
		}

		public Stats ParseStats(JsonElement root)
		{
			return new Stats(
				Score: root.GetProperty("score").GetInt32(),
				ConsecutiveLosses: root.GetProperty("consecutive_round_losses").GetInt32(),
				TimeoutsRemaining: root.GetProperty("timeouts_remaining").GetInt32(),
				MatchesWonThisSeries: root.GetProperty("matches_won_this_series").GetInt32()
			);
		}

	}
}
