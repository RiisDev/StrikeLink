using StrikeLink.GSI.ObjectStates;
using System.Globalization;
using Activity = StrikeLink.GSI.ObjectStates.Activity;

namespace StrikeLink.GSI.Parsing
{
	internal sealed class PlayerStateParser : IGsiParser
	{
		// When spectating it doesn't have this data
		public bool CanParse(JsonElement root) => root.TryGetProperty("player", out JsonElement playerElement) && playerElement.TryGetProperty("name", out _);

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

		private static List<Weapon>? ParseWeapons(JsonElement player)
		{
			if (!player.TryGetProperty("weapons", out JsonElement weaponsElement))
				return null;

			List<Weapon> weapons = [];

			foreach (JsonProperty weaponProperty in weaponsElement.EnumerateObject())
			{
				JsonElement weaponElement = weaponProperty.Value;

				int slot = int.Parse(weaponProperty.Name.AsSpan()["weapon_".Length..], CultureInfo.InvariantCulture);

				string name = weaponElement.GetProperty("name").GetString()!;
				string paintKit = weaponElement.GetProperty("paintkit").GetString()!;

				HeldType type = name switch
				{
					"weapon_taser" => HeldType.Zeus,
					"Submachine Gun" => HeldType.SMG,
					_ => Enum.Parse<HeldType>(weaponElement.GetProperty("type").GetString()!, ignoreCase: true)
				};
				HeldState state = Enum.Parse<HeldState>(weaponElement.GetProperty("state").GetString()!, ignoreCase: true);

				int? ammoClip = weaponElement.TryGetProperty("ammo_clip", out JsonElement clip) ? clip.GetInt32() : null;
				int? ammoClipMax = weaponElement.TryGetProperty("ammo_clip_max", out JsonElement clipMax) ? clipMax.GetInt32() : null;
				int? ammoReserve = weaponElement.TryGetProperty("ammo_reserve", out JsonElement reserve) ? reserve.GetInt32() : null;

				weapons.Add(new Weapon(
					Slot: slot,
					Name: WeaponList[name],
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

		private static readonly Dictionary<string, WeaponType> WeaponList = new()
		{
			// Weapons
			{ "weapon_deagle", WeaponType.DesertEagle },
			{ "weapon_elite", WeaponType.DualBerettas },
			{ "weapon_fiveseven", WeaponType.FiveSeven },
			{ "weapon_glock", WeaponType.Glock18 },
			{ "weapon_ak47", WeaponType.Ak47 },
			{ "weapon_aug", WeaponType.Aug },
			{ "weapon_awp", WeaponType.Awp },
			{ "weapon_famas", WeaponType.Famas },
			{ "weapon_g3sg1", WeaponType.G3sg1 },
			{ "weapon_galilar", WeaponType.Galil },
			{ "weapon_m249", WeaponType.M249 },
			{ "weapon_m4a1", WeaponType.M4a4 },
			{ "weapon_mac10", WeaponType.Mac10 },
			{ "weapon_p90", WeaponType.P90 },
			{ "weapon_mp5sd", WeaponType.Mp5 },
			{ "weapon_ump45", WeaponType.Ump45 },
			{ "weapon_xm1014", WeaponType.Xm1014 },
			{ "weapon_bizon", WeaponType.PpBizon },
			{ "weapon_mag7", WeaponType.Mag7 },
			{ "weapon_negev", WeaponType.Negev },
			{ "weapon_sawedoff", WeaponType.SawedOff },
			{ "weapon_tec9", WeaponType.Tec9 },
			{ "weapon_taser", WeaponType.Zeus },
			{ "weapon_hkp2000", WeaponType.P2000 },
			{ "weapon_mp7", WeaponType.Mp7 },
			{ "weapon_mp9", WeaponType.Mp9 },
			{ "weapon_nova", WeaponType.Nova },
			{ "weapon_p250", WeaponType.P250 },
			{ "weapon_scar20", WeaponType.Scar20 },
			{ "weapon_sg556", WeaponType.Sg553 },
			{ "weapon_ssg08", WeaponType.Ssg08 },
			{ "weapon_m4a1_silencer", WeaponType.M4a1S },
			{ "weapon_usp_silencer", WeaponType.UspS },
			{ "weapon_cz75a", WeaponType.Cz75 },
			{ "weapon_revolver", WeaponType.R8Revolver },
			
			// Knives
			{ "weapon_knife", WeaponType.DefaultKnife },
			{ "weapon_knife_t", WeaponType.DefaultKnife },
			{ "weapon_knife_ct", WeaponType.DefaultKnife },
			{ "weapon_knife_m9_bayonet", WeaponType.M9Bayonet },
			{ "weapon_knife_karambit", WeaponType.Karambit },
			{ "weapon_bayonet", WeaponType.Bayonet },
			{ "weapon_knife_survival_bowie", WeaponType.BowieKnife },
			{ "weapon_knife_butterfly", WeaponType.ButterflyKnife },
			{ "weapon_knife_falchion", WeaponType.FalchionKnife },
			{ "weapon_knife_flip", WeaponType.FlipKnife },
			{ "weapon_knife_gut", WeaponType.GutKnife },
			{ "weapon_knife_tactical", WeaponType.HuntsmanKnife },
			{ "weapon_knife_push", WeaponType.ShadowDaggers },
			{ "weapon_knife_gypsy_jackknife", WeaponType.NavajaKnife },
			{ "weapon_knife_stiletto", WeaponType.StilettoKnife },
			{ "weapon_knife_widowmaker", WeaponType.TalonKnife },
			{ "weapon_knife_ursus", WeaponType.UrsusKnife },
			{ "weapon_knife_css", WeaponType.ClassicKnife },
			{ "weapon_knife_cord", WeaponType.ParacordKnife },
			{ "weapon_knife_canis", WeaponType.SurvivalKnife },
			{ "weapon_knife_outdoor", WeaponType.NomadKnife },
			{ "weapon_knife_skeleton", WeaponType.SkeletonKnife },
			{ "weapon_knife_kukri", WeaponType.KukriKnife },
			
			// Utilities
			{ "weapon_c4", WeaponType.C4 },
			{ "weapon_hegrenade", WeaponType.HighExplosiveGrenade },
			{ "weapon_flashbang", WeaponType.Flashbang },
			{ "weapon_smokegrenade", WeaponType.SmokeGrenade },
			{ "weapon_decoy", WeaponType.DecoyGrenade },
			{ "weapon_incgrenade", WeaponType.IncendiaryGrenade },
			{ "weapon_molotov", WeaponType.Molotov },
		};

	}
}
