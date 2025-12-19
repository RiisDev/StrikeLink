// ReSharper disable ArrangeNamespaceBody -- Looks cleaner for the amount of structs we're doing
namespace StrikeLink.GSI;

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

public record PlayerState : IGsiPayload
{
	public string? SteamId { get; init; }
	public string? Clan { get; init; }
	public string? Name { get; init; }

	public Team? Team { get; init; }
	public Activity? Activity { get; init; }


	public int? Health { get; init; }
	public int? Armor { get; init; }
	public int? ObserverSlot { get; init; }

}