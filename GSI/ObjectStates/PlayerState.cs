// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace StrikeLink.GSI.ObjectStates;

/// <summary>
/// Represents the team a player belongs to.
/// </summary>
public enum Team
{
	/// <summary>
	/// Terrorist team.
	/// </summary>
	Terrorist,

	/// <summary>
	/// Counter-Terrorist team.
	/// </summary>
	CounterTerrorist
}

/// <summary>
/// Represents the player's current activity state.
/// </summary>
public enum Activity
{
	/// <summary>
	/// The player is actively playing in a match.
	/// </summary>
	Playing,

	/// <summary>
	/// The player is in a menu or otherwise not actively playing.
	/// </summary>
	Menu
}

/// <summary>
/// Represents the current state of a held weapon.
/// </summary>
public enum HeldState
{
	/// <summary>
	/// The weapon is actively equipped.
	/// </summary>
	Active,

	/// <summary>
	/// The weapon is holstered.
	/// </summary>
	Holstered,

	/// <summary>
	/// The weapon is currently reloading.
	/// </summary>
	Reloading
}

/// <summary>
/// Represents a general category of a held weapon.
/// </summary>
public enum HeldType
{
	/// <summary>
	/// Sniper rifle weapon category.
	/// </summary>
	SniperRifle,

	/// <summary>
	/// Rifle weapon category.
	/// </summary>
	Rifle,

	/// <summary>
	/// Submachine gun weapon category.
	/// </summary>
	SMG,

	/// <summary>
	/// Pistol weapon category.
	/// </summary>
	Pistol,

	/// <summary>
	/// Heavy weapon category.
	/// </summary>
	Heavy,

	/// <summary>
	/// Grenade weapon category.
	/// </summary>
	Grenade,

	/// <summary>
	/// C4 explosive.
	/// </summary>
	C4,

	/// <summary>
	/// Knife weapon category.
	/// </summary>
	Knife,

	/// <summary>
	/// Zeus taser.
	/// </summary>
	Zeus
}

/// <summary>
/// Represents a specific weapon or utility type.
/// </summary>
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

/// <summary>
/// Represents aggregate player statistics for the current match.
/// </summary>
/// <param name="Kills">
/// The total number of kills.
/// </param>
/// <param name="Assists">
/// The total number of assists.
/// </param>
/// <param name="Deaths">
/// The total number of deaths.
/// </param>
/// <param name="MVPs">
/// The number of MVP awards earned.
/// </param>
/// <param name="Score">
/// The player's current score.
/// </param>
public record MatchStats(
	int Kills,
	int Assists,
	int Deaths,
	int MVPs,
	int Score
);

/// <summary>
/// Represents the player's current vital statistics and round-specific metrics.
/// </summary>
/// <param name="Health">
/// The player's current health.
/// </param>
/// <param name="Armor">
/// The player's current armor value.
/// </param>
/// <param name="Helmet">
/// Indicates whether the player has a helmet equipped.
/// </param>
/// <param name="Flashed">
/// The flashbang effect duration (in milliseconds).
/// </param>
/// <param name="Smoked">
/// The smoke effect duration (in milliseconds).
/// </param>
/// <param name="Burning">
/// The burn effect duration (in milliseconds).
/// </param>
/// <param name="Money">
/// The player's current money amount.
/// </param>
/// <param name="RoundKills">
/// The number of kills in the current round.
/// </param>
/// <param name="RoundHeadshotKills">
/// The number of headshot kills in the current round.
/// </param>
/// <param name="EquippedSlot">
/// The currently equipped weapon slot, if available.
/// </param>
/// <param name="EquippedValue">
/// The value of the currently equipped weapon, if available.
/// </param>
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

/// <summary>
/// Represents a weapon currently owned by the player.
/// </summary>
/// <param name="Slot">
/// The inventory slot number of the weapon.
/// </param>
/// <param name="Name">
/// The specific weapon type. <see cref="WeaponType"/>
/// </param>
/// <param name="PaintKit">
/// The applied cosmetic paint kit identifier.
/// </param>
/// <param name="Type">
/// The general weapon category. <see cref="HeldType"/>
/// </param>
/// <param name="State">
/// The current held state of the weapon. <see cref="HeldState"/>
/// </param>
/// <param name="AmmoClip">
/// The current ammunition in the magazine, if applicable.
/// </param>
/// <param name="AmmoClipMax">
/// The maximum magazine capacity, if applicable.
/// </param>
/// <param name="AmmoReserve">
/// The remaining reserve ammunition, if applicable.
/// </param>
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

/// <summary>
/// Represents the current state of a player as reported by GSI.
/// </summary>
/// <param name="SteamId">
/// The Steam ID of the player.
/// </param>
/// <param name="Name">
/// The display name of the player.
/// </param>
/// <param name="Clan">
/// The clan tag of the player, if present.
/// </param>
/// <param name="ObserverSlot">
/// The observer slot index, if applicable.
/// </param>
/// <param name="Team">
/// The team the player belongs to. <see cref="Team"/>
/// </param>
/// <param name="Activity">
/// The player's current activity state. <see cref="Activity"/>
/// </param>
/// <param name="MatchStats">
/// Match-level statistics for the player. <see cref="MatchStats"/>
/// </param>
/// <param name="Vitals">
/// Current vital and round-specific statistics. <see cref="Vitals"/>
/// </param>
/// <param name="Weapons">
/// The list of weapons currently owned by the player. <see cref="Weapon"/>
/// </param>
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