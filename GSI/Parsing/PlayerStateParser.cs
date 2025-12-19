using StrikeLink.GSI.ObjectStates;
using System.Globalization;
using Activity = StrikeLink.GSI.ObjectStates.Activity;

namespace StrikeLink.GSI.Parsing
{
	// Example class, non-functional
	internal sealed class PlayerStateParser : IGsiParser
	{
		public bool CanParse(JsonElement root) => root.TryGetProperty("player", out _);

		public IGsiPayload Parse(JsonElement root)
		{
			JsonElement player = root.GetProperty("player");

			return new PlayerState(
				SteamId: player.GetProperty("steamid").GetString()!,
				Name: player.GetProperty("name").GetString()!,
				Clan: player.TryGetProperty("clan", out JsonElement clan) ? clan.GetString() : null,
				ObserverSlot: player.TryGetProperty("observer_slot", out JsonElement slot) ? slot.GetInt32() : null,

				Team: player.TryGetProperty("team", out JsonElement teamElement) ? teamElement.GetString() == "T" ? Team.Terrorist : Team.CounterTerrorist : null,
				Activity: player.TryGetProperty("activity", out JsonElement activityElement) ? activityElement.GetString() == "playing" ? Activity.Playing : Activity.Menu : Activity.Menu,

				MatchStats: ParseMatchStats(player),
				Vitals: ParseVitals(player),
				Weapons: ParseWeapons(player)
			);
		}

		private static MatchStats? ParseMatchStats(JsonElement player)
		{
			if (!player.TryGetProperty("match_stats", out JsonElement matchStatsElement))
				return null;

			int kills = matchStatsElement.GetProperty("kills").GetInt32();
			int assists = matchStatsElement.GetProperty("assists").GetInt32();
			int deaths = matchStatsElement.GetProperty("deaths").GetInt32();
			int mvps = matchStatsElement.GetProperty("mvps").GetInt32();
			int score = matchStatsElement.GetProperty("score").GetInt32();

			return new MatchStats(
				Kills: kills,
				Assists: assists,
				Deaths: deaths,
				MVPs: mvps,
				Score: score
			);
		}

		private static Vitals? ParseVitals(JsonElement player)
		{
			if (!player.TryGetProperty("state", out JsonElement stateElement))
				return null;

			int health = stateElement.GetProperty("health").GetInt32();
			int armor = stateElement.GetProperty("armor").GetInt32();
			bool helmet = stateElement.GetProperty("helmet").GetBoolean();

			int flashed = stateElement.GetProperty("flashed").GetInt32();
			int smoked = stateElement.GetProperty("smoked").GetInt32();
			int burning = stateElement.GetProperty("burning").GetInt32();

			int money = stateElement.GetProperty("money").GetInt32();
			int roundKills = stateElement.GetProperty("round_kills").GetInt32();

			int roundHeadshotKills = stateElement.TryGetProperty("round_killhs", out JsonElement hs) ? hs.GetInt32() : 0;
			int? equippedSlot = stateElement.TryGetProperty("equip_slot", out JsonElement slot) ? slot.GetInt32() : null;
			int? equippedValue = stateElement.TryGetProperty("equip_value", out JsonElement value) ? value.GetInt32() : null;

			return new Vitals(
				Health: health,
				Armor: armor,
				Helmet: helmet,
				Flashed: flashed,
				Smoked: smoked,
				Burning: burning,
				Money: money,
				RoundKills: roundKills,
				RoundHeadshotKills: roundHeadshotKills,
				EquippedSlot: equippedSlot,
				EquippedValue: equippedValue
			);
		}

		private static List<Weapons>? ParseWeapons(JsonElement player)
		{
			if (!player.TryGetProperty("weapons", out JsonElement weaponsElement))
				return null;

			List<Weapons> weapons = [];

			foreach (JsonProperty weaponProperty in weaponsElement.EnumerateObject())
			{
				JsonElement weaponElement = weaponProperty.Value;

				int slot = int.Parse(weaponProperty.Name.AsSpan()["weapon_".Length..], CultureInfo.InvariantCulture);

				string name = weaponElement.GetProperty("name").GetString()!;
				string paintKit = weaponElement.GetProperty("paintkit").GetString()!;

				HeldType type = Enum.Parse<HeldType>(weaponElement.GetProperty("type").GetString()!, ignoreCase: true);
				HeldState state = Enum.Parse<HeldState>(weaponElement.GetProperty("state").GetString()!, ignoreCase: true);

				int? ammoClip = weaponElement.TryGetProperty("ammo_clip", out JsonElement clip) ? clip.GetInt32() : null;
				int? ammoClipMax = weaponElement.TryGetProperty("ammo_clip_max", out JsonElement clipMax) ? clipMax.GetInt32() : null;
				int? ammoReserve = weaponElement.TryGetProperty("ammo_reserve", out JsonElement reserve) ? reserve.GetInt32() : null;

				weapons.Add(new Weapons(
					Slot: slot,
					Name: name,
					PaintKit: paintKit,
					Type: type,
					State: state,
					AmmoClip: ammoClip,
					AmmoClipMax: ammoClipMax,
					AmmoReserve: ammoReserve
				));
			}

			return weapons;
		}

	}
}
