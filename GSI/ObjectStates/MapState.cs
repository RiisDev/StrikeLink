// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
namespace StrikeLink.GSI.ObjectStates;

public enum MapPhase
{
	Live,
	WarmUp,
	Intermission,
	GameOver
}

public enum MapMode
{
	Casual,
	Competitive,
	Deathmatch,
	Wingman,
	ArmsRace
}

public enum WinState
{
	CounterTerroristEliminated,
	TerroristsEliminated,
	BombDefused,
	BombExploded
}

public record Stats(
	int Score,
	int ConsecutiveLosses,
	int TimeoutsRemaining,
	int MatchesWonThisSeries
);

public record MapState(
	MapMode Mode,
	string Name,
	MapPhase Phase,
	int Round,
	Stats CounterTerroristStats,
	Stats TerroristsStats,
	int MatchesToWinSeries,
	Dictionary<int, WinState> RoundWins
) : IGsiPayload;