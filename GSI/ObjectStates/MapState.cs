// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
namespace StrikeLink.GSI.ObjectStates;

/// <summary>
/// Represents the current phase of the map.
/// </summary>
public enum MapPhase
{
	/// <summary>
	/// The match is currently live.
	/// </summary>
	Live,

	/// <summary>
	/// The match is in warm-up mode.
	/// </summary>
	WarmUp,

	/// <summary>
	/// The match is between halves.
	/// </summary>
	Intermission,

	/// <summary>
	/// The match has ended.
	/// </summary>
	GameOver
}

/// <summary>
/// Represents the mode of the map.
/// </summary>
public enum MapMode
{
	/// <summary>
	/// Casual game mode.
	/// </summary>
	Casual,

	/// <summary>
	/// Competitive game mode.
	/// </summary>
	Competitive,

	/// <summary>
	/// Deathmatch game mode.
	/// </summary>
	Deathmatch,

	/// <summary>
	/// Wingman game mode.
	/// </summary>
	Wingman,

	/// <summary>
	/// Arms Race game mode.
	/// </summary>
	ArmsRace
}

/// <summary>
/// Represents the win condition of a round.
/// </summary>
public enum WinState
{
	/// <summary>
	/// All Counter-Terrorist players were eliminated.
	/// </summary>
	CounterTerroristEliminated,

	/// <summary>
	/// All Terrorist players were eliminated.
	/// </summary>
	TerroristsEliminated,

	/// <summary>
	/// The bomb was successfully defused.
	/// </summary>
	BombDefused,

	/// <summary>
	/// The bomb exploded.
	/// </summary>
	BombExploded
}

/// <summary>
/// Represents team statistics for the current map.
/// </summary>
/// <param name="Score">
/// The current team score.
/// </param>
/// <param name="ConsecutiveLosses">
/// The number of consecutive rounds lost.
/// </param>
/// <param name="TimeoutsRemaining">
/// The number of tactical timeouts remaining.
/// </param>
/// <param name="MatchesWonThisSeries">
/// The number of maps won in the current series.
/// </param>
public record Stats(
	int Score,
	int ConsecutiveLosses,
	int TimeoutsRemaining,
	int MatchesWonThisSeries
);

/// <summary>
/// Represents the current state of the map as reported by GSI.
/// </summary>
/// <param name="Mode">
/// The current game mode. <see cref="MapMode"/>
/// </param>
/// <param name="Name">
/// The internal map name.
/// </param>
/// <param name="Phase">
/// The current phase of the match. <see cref="MapPhase"/>
/// </param>
/// <param name="Round">
/// The current round number.
/// </param>
/// <param name="CounterTerroristStats">
/// Statistics for the Counter-Terrorist team. <see cref="Stats"/>
/// </param>
/// <param name="TerroristsStats">
/// Statistics for the Terrorist team. <see cref="Stats"/>
/// </param>
/// <param name="MatchesToWinSeries">
/// The number of matches required to win the series.
/// </param>
/// <param name="RoundWins">
/// A mapping of round numbers to their respective win conditions. <see cref="WinState"/>
/// </param>
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