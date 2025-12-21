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
	Holstered,
	Reloading
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

public enum WeaponType
{
	DesertEagle,
	DualBerettas,
	FiveSeven,
	Glock18,
	Ak47,
	Aug,
	Awp,
	Famas,
	G3sg1,
	Galil,
	M249,
	M4a4,
	Mac10,
	P90,
	Mp5,
	Ump45,
	Xm1014,
	PpBizon,
	Mag7,
	Negev,
	SawedOff,
	Tec9,
	Zeus,
	P2000,
	Mp7,
	Mp9,
	Nova,
	P250,
	Scar20,
	Sg553,
	Ssg08,
	M4a1S,
	UspS,
	Cz75,
	R8Revolver,

	// Utilities
	C4,
	Flashbang,
	HighExplosiveGrenade,
	SmokeGrenade,
	DecoyGrenade,
	IncendiaryGrenade,
	Molotov,

	// Knives
	DefaultKnife,
	M9Bayonet,
	Karambit,
	Bayonet,
	BowieKnife,
	ButterflyKnife,
	FalchionKnife,
	FlipKnife,
	GutKnife,
	HuntsmanKnife,
	ShadowDaggers,
	NavajaKnife,
	StilettoKnife,
	TalonKnife,
	UrsusKnife,
	ClassicKnife,
	ParacordKnife,
	SurvivalKnife,
	NomadKnife,
	SkeletonKnife,
	KukriKnife
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
	WeaponType Name,
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