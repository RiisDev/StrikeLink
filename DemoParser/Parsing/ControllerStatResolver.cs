using System.Text.RegularExpressions;

namespace StrikeLink.DemoParser.Parsing;

/// <summary>Controller properties the parser itself needs to recognise while the demo streams by.</summary>
internal enum ControllerKey
{
	None,
	SteamId,
	PlayerName,
	Mvps,
}

/// <summary>
/// Turns the raw, build-dependent controller property dictionary into <see cref="ControllerStats"/>.
/// Property names come from each demo's own schema, but Valve can rename or move them between builds, so nothing reads a
/// raw name directly: every stat lists the names it has been known by, then falls back to the one non-array property that
/// shares the stat's last segment. Stats that can't be found stay null and are reported once per demo.
/// </summary>
internal static partial class ControllerStatResolver
{
	[GeneratedRegex(@"^(?<prefix>.*perRoundStats)\.(?<index>\d+)\.(?<leaf>[^.]+)$", RegexOptions.IgnoreCase)]
	private static partial Regex RoundStatPattern();

	/// <summary>True for array/vector elements (a numeric segment such as <c>m_perRoundStats.0003.m_iKills</c>).</summary>
	public static bool IsElement(string name)
	{
		foreach (string segment in name.Split('.'))
		{
			if (segment.Length > 0 && segment.All(char.IsAsciiDigit))
			{
				return true;
			}
		}

		return false;
	}

	public static string Leaf(string name) => name[(name.LastIndexOf('.') + 1)..];

	public static ControllerKey Classify(string name)
	{
		if (IsElement(name))
		{
			return ControllerKey.None;
		}

		return Leaf(name) switch
		{
			"m_steamID" => ControllerKey.SteamId,
			"m_iszPlayerName" => ControllerKey.PlayerName,
			"m_iMVPs" => ControllerKey.Mvps,
			_ => ControllerKey.None,
		};
	}

	/// <summary>
	/// Builds the typed view of one player's controller.
	/// <paramref name="schema"/> is the controller's property list for this demo (names, array elements collapsed to <c>[]</c>).
	/// A property the demo never sent is at its default (0) when the schema declares it, and genuinely unavailable otherwise;
	/// only the latter is added to <paramref name="missing"/>.
	/// </summary>
	public static ControllerStats Build(IReadOnlyDictionary<string, object?> properties, IReadOnlySet<string>? schema, ISet<string> missing)
	{
		Lookup lookup = new(properties, schema, missing);

		const string tracking = "m_pActionTrackingServices.";
		const string money = "m_pInGameMoneyServices.";

		return new ControllerStats(
			Score: lookup.Int("Score", "m_iScore"),
			Mvps: lookup.Int("Mvps", "m_iMVPs"),
			Kills: lookup.Int("Kills", tracking + "m_iKills"),
			Deaths: lookup.Int("Deaths", tracking + "m_iDeaths"),
			Assists: lookup.Int("Assists", tracking + "m_iAssists"),
			Damage: lookup.Int("Damage", tracking + "m_iDamage"),
			UtilityDamage: lookup.Int("UtilityDamage", tracking + "m_iUtilityDamage"),
			EnemiesFlashed: lookup.Int("EnemiesFlashed", tracking + "m_iEnemiesFlashed"),
			HeadshotKills: lookup.Int("HeadshotKills", tracking + "m_iHeadShotKills"),
			Objective: lookup.Int("Objective", tracking + "m_iObjective"),
			LiveTime: lookup.Int("LiveTime", tracking + "m_iLiveTime"),
			Enemy3Ks: lookup.Int("Enemy3Ks", tracking + "m_iEnemy3Ks"),
			Enemy4Ks: lookup.Int("Enemy4Ks", tracking + "m_iEnemy4Ks"),
			Enemy5Ks: lookup.Int("Enemy5Ks", tracking + "m_iEnemy5Ks"),
			EnemyKnifeKills: lookup.Int("EnemyKnifeKills", tracking + "m_iEnemyKnifeKills"),
			EnemyTaserKills: lookup.Int("EnemyTaserKills", tracking + "m_iEnemyTaserKills"),
			EquipmentValue: lookup.Int("EquipmentValue", tracking + "m_iEquipmentValue"),
			MoneySaved: lookup.Int("MoneySaved", tracking + "m_iMoneySaved"),
			KillReward: lookup.Int("KillReward", tracking + "m_iKillReward"),
			CashEarned: lookup.Int("CashEarned", tracking + "m_iCashEarned"),
			Account: lookup.Int("Account", money + "m_iAccount"),
			TotalCashSpent: lookup.Int("TotalCashSpent", money + "m_iTotalCashSpent"),
			Ping: lookup.Int("Ping", "m_iPing"),
			CompetitiveRanking: lookup.Int("CompetitiveRanking", "m_iCompetitiveRanking"),
			CompetitiveWins: lookup.Int("CompetitiveWins", "m_iCompetitiveWins"),
			CompetitiveRankType: lookup.Int("CompetitiveRankType", "m_iCompetitiveRankType"),
			Clan: lookup.Text("Clan", "m_szClan"),
			CrosshairCode: lookup.Text("CrosshairCode", "m_szCrosshairCodes"),
			RoundStats: BuildRoundStats(properties, schema, missing));
	}

	private static List<ControllerRoundStats> BuildRoundStats(IReadOnlyDictionary<string, object?> properties, IReadOnlySet<string>? schema, ISet<string> missing)
	{
		SortedDictionary<int, Dictionary<string, object?>> byRound = [];
		foreach ((string name, object? value) in properties)
		{
			Match match = RoundStatPattern().Match(name);
			if (!match.Success)
			{
				continue;
			}

			int index = int.Parse(match.Groups["index"].Value, System.Globalization.CultureInfo.InvariantCulture);
			if (!byRound.TryGetValue(index, out Dictionary<string, object?>? round))
			{
				round = [];
				byRound[index] = round;
			}

			round[match.Groups["leaf"].Value] = value;
		}

		if (byRound.Count == 0 && (schema is null || !schema.Any(name => name.Contains("perRoundStats", StringComparison.OrdinalIgnoreCase))))
		{
			missing.Add("RoundStats");
		}

		static int? Int(Dictionary<string, object?> round, string leaf)
			=> round.TryGetValue(leaf, out object? value) ? ToInt(value) : null;

		// A value that never changed from its default is not re-sent, so an absent leaf in a present round means 0.
		return byRound
			.Select(entry => new ControllerRoundStats(
				Round: entry.Key + 1,
				Kills: Int(entry.Value, "m_iKills") ?? 0,
				Deaths: Int(entry.Value, "m_iDeaths") ?? 0,
				Assists: Int(entry.Value, "m_iAssists") ?? 0,
				Damage: Int(entry.Value, "m_iDamage") ?? 0,
				HeadshotKills: Int(entry.Value, "m_iHeadShotKills") ?? 0,
				LiveTime: Int(entry.Value, "m_iLiveTime") ?? 0,
				Objective: Int(entry.Value, "m_iObjective") ?? 0,
				UtilityDamage: Int(entry.Value, "m_iUtilityDamage") ?? 0,
				EnemiesFlashed: Int(entry.Value, "m_iEnemiesFlashed") ?? 0,
				EquipmentValue: Int(entry.Value, "m_iEquipmentValue") ?? 0,
				MoneySaved: Int(entry.Value, "m_iMoneySaved") ?? 0,
				KillReward: Int(entry.Value, "m_iKillReward") ?? 0,
				CashEarned: Int(entry.Value, "m_iCashEarned") ?? 0))
			.ToList();
	}

	private static int? ToInt(object? value) => value switch
	{
		long l => (int)l,
		ulong u => (int)u,
		int i => i,
		bool b => b ? 1 : 0,
		float f => (int)f,
		_ => null,
	};

	private sealed class Lookup
	{
		private readonly IReadOnlyDictionary<string, object?> _properties;
		private readonly IReadOnlySet<string>? _schema;
		private readonly ISet<string> _missing;
		private readonly Dictionary<string, List<string>> _propertiesByLeaf;
		private readonly Dictionary<string, List<string>> _schemaByLeaf;

		public Lookup(IReadOnlyDictionary<string, object?> properties, IReadOnlySet<string>? schema, ISet<string> missing)
		{
			_properties = properties;
			_schema = schema;
			_missing = missing;
			_propertiesByLeaf = IndexByLeaf(properties.Keys);
			_schemaByLeaf = IndexByLeaf(schema is null ? [] : schema);
		}

		private static Dictionary<string, List<string>> IndexByLeaf(IEnumerable<string> names)
		{
			Dictionary<string, List<string>> index = [];
			foreach (string name in names)
			{
				if (IsElement(name) || name.Contains("[]", StringComparison.Ordinal))
				{
					continue;
				}

				string leaf = Leaf(name);
				if (!index.TryGetValue(leaf, out List<string>? list))
				{
					list = [];
					index[leaf] = list;
				}

				list.Add(name);
			}

			return index;
		}

		/// <summary>Integer stat; 0 when the schema declares it but the demo never sent a non-default value.</summary>
		public int? Int(string stat, params string[] names)
		{
			object? value = Find(stat, names, out bool declared);
			return value is not null ? ToInt(value) : declared ? 0 : null;
		}

		public string? Text(string stat, params string[] names) => Find(stat, names, out _) as string;

		/// <summary>Known names first; otherwise the single non-array property whose last segment matches the first name's.</summary>
		private object? Find(string stat, string[] names, out bool declared)
		{
			declared = false;

			foreach (string name in names)
			{
				if (_properties.TryGetValue(name, out object? value))
				{
					return value;
				}
			}

			string leaf = Leaf(names[0]);
			if (_propertiesByLeaf.TryGetValue(leaf, out List<string>? candidates) && candidates.Count == 1)
			{
				return _properties[candidates[0]];
			}

			// Not sent: at its default if this build's schema has the property (under a known name or a unique matching leaf).
			declared = _schema is not null
				&& (names.Any(_schema.Contains) || (_schemaByLeaf.TryGetValue(leaf, out List<string>? declaredNames) && declaredNames.Count == 1));
			if (!declared)
			{
				_missing.Add(stat);
			}

			return null;
		}
	}
}
