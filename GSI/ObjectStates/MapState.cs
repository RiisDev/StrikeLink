// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
namespace StrikeLink.GSI.ObjectStates;

public enum MapPhase
{
	Live,
	WarmUp,
	FreezeTime,
	Intermission,
	GameOver,
	Bomb
}

public enum MapMode
{
	Casual,
	Competitive,
	Premier,
	Deathmatch,
	Wingman,
	ArmsRace
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
	int MatchesToWinSeries
) : IGsiPayload;