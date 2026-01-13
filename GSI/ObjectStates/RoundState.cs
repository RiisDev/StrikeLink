namespace StrikeLink.GSI.ObjectStates;

/// <summary>
/// Represents the current phase of a round.
/// </summary>
public enum RoundPhase
{
	/// <summary>
	/// The round is actively in progress.
	/// </summary>
	Live,

	/// <summary>
	/// The freeze time before the round begins or after a round ends.
	/// </summary>
	FreezeTime,

	/// <summary>
	/// The round has ended.
	/// </summary>
	Over
}

/// <summary>
/// Represents the state of the bomb in a round.
/// </summary>
public enum BombState
{
	/// <summary>
	/// The bomb has been planted.
	/// </summary>
	Planted,

	/// <summary>
	/// The bomb has been defused.
	/// </summary>
	Defused,

	/// <summary>
	/// The bomb has exploded.
	/// </summary>
	Exploded
}

/// <summary>
/// Represents the current state of a round as reported by GSI.
/// </summary>
/// <param name="Phase">
/// The current phase of the round. <see cref="RoundPhase"/>
/// </param>
/// <param name="WinTeam">
/// The team that won the round, if the round has ended; otherwise <c>null</c>. <see cref="Team"/>
/// </param>
/// <param name="BombState">
/// The current state of the bomb, if applicable; otherwise <c>null</c>. <see cref="BombState"/>
/// </param>
public record RoundState(
	RoundPhase Phase,
	Team? WinTeam,
	BombState? BombState
) : IGsiPayload;