namespace StrikeLink.GSI.ObjectStates;

public enum RoundPhase
{
	Live,
	FreezeTime,
	Over
}

public enum BombState
{
	Planted,
	Defused,
	Exploded
}

public record RoundState(
	RoundPhase Phase,
	Team? WinTeam,
	BombState? BombState
) : IGsiPayload;