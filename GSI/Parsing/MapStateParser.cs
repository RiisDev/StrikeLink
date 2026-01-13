using StrikeLink.GSI.ObjectStates;
using System.ComponentModel;
using System.Globalization;

namespace StrikeLink.GSI.Parsing
{
	internal sealed class MapStateParser : IGsiParser
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool CanParse(JsonElement root) => root.TryGetProperty("map", out _);

		[EditorBrowsable(EditorBrowsableState.Never)]
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
				MatchesToWinSeries: map.GetProperty("num_matches_to_win_series").GetInt32(),
				RoundWins: ParseRoundWins(map)
			);
		}

		private MapMode ParseMode(string? mode)
		{
			return mode switch
			{
				"gungameprogressive" => MapMode.ArmsRace,
				"scrimcomp2v2" => MapMode.Wingman,
				_ => Enum.TryParse(mode, true, out MapMode modeData) ? modeData : MapMode.Casual
			};
		}

		private MapPhase ParsePhase(string? phase) => phase == null ? MapPhase.GameOver : Enum.Parse<MapPhase>(phase, ignoreCase: true);

		private Stats ParseStats(JsonElement root)
		{
			return new Stats(
				Score: root.GetProperty("score").GetInt32(),
				ConsecutiveLosses: root.GetProperty("consecutive_round_losses").GetInt32(),
				TimeoutsRemaining: root.GetProperty("timeouts_remaining").GetInt32(),
				MatchesWonThisSeries: root.GetProperty("matches_won_this_series").GetInt32()
			);
		}

		private Dictionary<int, WinState> ParseRoundWins(JsonElement root)
		{
			Dictionary<int, WinState> roundWins = [];

			if (!root.TryGetProperty("round_wins", out JsonElement roundWinsElement)) return roundWins;

			foreach (JsonProperty property in roundWinsElement.EnumerateObject())
			{
				int roundNumber = int.Parse(property.Name, CultureInfo.InvariantCulture);
				roundWins.Add(roundNumber, WinStateMappings[property.Value.GetString() ?? "t_win_bomb"]);
			}

			return roundWins;
		}

		private static readonly Dictionary<string, WinState> WinStateMappings = new()
		{
			{ "t_win_elimination", WinState.CounterTerroristEliminated },
			{ "ct_win_elimination", WinState.TerroristsEliminated },
			{ "ct_win_defuse", WinState.BombDefused },
			{ "t_win_bomb", WinState.BombExploded }
		};
	}
}
