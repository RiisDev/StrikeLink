// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
namespace StrikeLink.GSI.ObjectStates;

public enum Team
{
	Terrorist,
	CounterTerrorist
}

public enum Activity
{
	Playing,
	Menu
}

public enum HeldState
{
	Active,
	Holstered
}

public enum HeldType
{
	SniperRifle,
	Rifle,
	SMG,
	Pistol,
	Heavy,
	Grenade,
	C4,
	Knife,
	Zeus
}

public record MatchStats(
	int Kills,
	int Assists,
	int Deaths,
	int MVPs,
	int Score
);

public record Vitals(
	int Health,
	int Armor,
	bool Helmet,
	int Flashed,
	int Smoked,
	int Burning,
	int Money,
	int RoundKills,
	int RoundHeadshotKills,
	int? EquippedSlot,
	int? EquippedValue
);

public record Weapon(
	int Slot,
	string Name,
	string PaintKit,
	HeldType Type,
	HeldState State,
	int? AmmoClip,
	int? AmmoClipMax,
	int? AmmoReserve
);

public record PlayerState(
	string SteamId,
	string Name,
	string? Clan,
	int? ObserverSlot,

	Team? Team,
	Activity Activity,

	MatchStats? MatchStats,
	Vitals? Vitals,
	IReadOnlyList<Weapon>? Weapons
) : IGsiPayload;