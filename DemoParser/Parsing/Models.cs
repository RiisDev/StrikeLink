using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace StrikeLink.DemoParser.Parsing
{
	/// <summary>
	/// Different playable sides within a Counter Strike match.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter<CsTeamSide>))]
	public enum CsTeamSide
	{
		/// <summary>
		/// Used when parsing fails or a user disconnects.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Spectator Team.
		/// </summary>
		Spectator = 1,

		/// <summary>
		/// Terrorists Team.
		/// </summary>
		Terrorists = 2,

		/// <summary>
		/// CounterTerrorists Team.
		/// </summary>
		CounterTerrorists = 3,
	}

	/// <summary>
	/// Specifies the possible outcomes of a match.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter<MatchOutcome>))]
	public enum MatchOutcome
	{
		/// <summary>
		/// Used when parsing fails, or the match ends to a fore-fit / vac-live.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Used when the match is a victory based on base user.
		/// </summary>
		Victory = 1,

		/// <summary>
		/// Used when the match is a defeat based on base user.
		/// </summary>
		Defeat = 2,

		/// <summary>
		/// Used when the match is a draw based on base user.
		/// </summary>
		Draw = 3,
	}

	/// <summary>
	/// Represents the two persistent team identities in a single match.
	/// Team A and Team B are lineup identities and do not imply a fixed side.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter<MatchTeam>))]
	public enum MatchTeam
	{
		/// <summary>
		/// Unknown team identity.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// The first persistent lineup identity in the match.
		/// </summary>
		TeamA = 1,
		/// <summary>
		/// The second persistent lineup identity in the match.
		/// </summary>
		TeamB = 2,
	}
	
	/// <summary>
	/// Represents the result of parsing a Counter-Strike 2 demo, including match statistics, player statistics, round
	/// details, and any warnings encountered during parsing.
	/// </summary>
	/// <remarks>This record aggregates all relevant data extracted from a demo file. Consumers should inspect the
	/// Warnings property to determine if any non-fatal issues occurred during parsing.</remarks>
	/// <param name="Match">The overall statistics for the parsed match.</param>
	/// <param name="Players">A read-only list containing statistics for each player in the match.</param>
	/// <param name="Rounds">A read-only list containing statistics for each round in the match.</param>
	/// <param name="Warnings">A read-only list of warning messages generated during parsing. The list will be empty if no warnings were
	/// encountered.</param>
	public sealed record Cs2DemoParseResult(
		MatchStats Match,
		IReadOnlyList<PlayerStats> Players,
		IReadOnlyList<RoundStats> Rounds,
		IReadOnlyList<string> Warnings);


	/// <summary>
	/// Represents the different chat message formats used in CS.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter<ChatType>))]
	public enum ChatType
	{
		/// <summary>
		/// Empty
		/// </summary>
		None = 0,

		/// <summary>
		/// [ALL] %s1: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatAll = 1,

		/// <summary>
		/// [ALL] %s1 [DEAD]: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatAllDead = 2,

		/// <summary>
		/// [ALL] %s1 [SPEC]: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatAllSpec = 3,

		/// <summary>
		/// [CT] %s1: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatCt = 10,

		/// <summary>
		/// [CT] %s1 [DEAD]: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatCtDead = 11,

		/// <summary>
		/// [CT] %s1 @ %s3: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// %s3 = Location
		/// </summary>
		ChatCtLoc = 12,

		/// <summary>
		/// [T] %s1: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatT = 20,

		/// <summary>
		/// [T] %s1 [DEAD]: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatTDead = 21,

		/// <summary>
		/// [T] %s1 @ %s3: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// %s3 = Location
		/// </summary>
		ChatTLoc = 22,

		/// <summary>
		/// [SPEC] %s1: %s2
		/// %s1 = Player name
		/// %s2 = Message
		/// </summary>
		ChatSpec = 30
	}

	/// <summary>
	/// Represents a chat message sent within the demo.
	/// </summary>
	/// <param name="ChatType">The Chat type <see cref="ChatType"/></param>
	/// <param name="Username">The username of the individual</param>
	/// <param name="Message">The message.</param>
	/// <param name="Tick">The tick the message was sent on</param>>
	public sealed record DemoChatMessage(ChatType ChatType, string Username, string Message, int Tick);

	/// <summary>
	/// Represents summary statistics and metadata for a completed match, including scores, duration, server details, and
	/// identifying information.
	/// </summary>
	/// <param name="Duration">The total duration of the match.</param>
	/// <param name="TeamAScore">The final score achieved by Team A.</param>
	/// <param name="TeamBScore">The final score achieved by Team B.</param>
	/// <param name="Outcome">The outcome of the match, indicating which team won or if the match was drawn.</param>
	/// <param name="ServerLocation">The geographic location of the server where the match was played, or null if not available.</param>
	/// <param name="GameType">The type or mode of the game played, or null if not specified.</param>
	/// <param name="MaxPlayers">The maximum number of players allowed on the server during the match, or null if not specified.</param>
	/// <param name="Date">The date and time when the match occurred, or null if not available.</param>
	/// <param name="Map">The name of the map on which the match was played, or null if not specified.</param>
	/// <param name="MatchShareCode">A shareable code that uniquely identifies the match, or null if not available.</param>
	/// <param name="ServerName">The display name of the server, or null if not specified.</param>
	/// <param name="DemoClientName">The name of the client used to record the match demo, or null if not available.</param>
	/// <param name="NetworkProtocol">The network protocol version used by the server, or null if not specified.</param>
	/// <param name="FocusSteamId">The Steam ID of the player in focus for this match, or null if not specified.</param>
	/// <param name="ChatMessages">Ordered list of chat messages sent in-game.</param>
	/// <remarks>Date may be incorrect as it is pulled from FileInfo if it fails to be parsed via demo</remarks>
	public sealed record MatchStats(
		TimeSpan Duration,
		int TeamAScore,
		int TeamBScore,
		MatchOutcome Outcome,
		string? ServerLocation,
		string? GameType,
		int? MaxPlayers,
		DateTimeOffset? Date,
		string? Map,
		string? MatchShareCode,
		string? ServerName,
		string? DemoClientName,
		int? NetworkProtocol,
		ulong? FocusSteamId,
		IReadOnlyList<DemoChatMessage> ChatMessages);

	/// <summary>
	/// Represents a comprehensive snapshot of a player's in-game statistics and performance metrics for a match or series.
	/// </summary>
	/// <remarks>This record aggregates a wide range of player performance data, enabling detailed analysis and
	/// comparison across matches. All values are captured at the time of record creation and are intended for read-only
	/// use.</remarks>
	/// <param name="SteamId">The unique Steam identifier for the player.</param>
	/// <param name="Name">The display name of the player at the time the statistics were recorded.</param>
	/// <param name="UserId">The in-game user ID assigned to the player during the match.</param>
	/// <param name="IsBot">Indicates whether the player is a bot. Set to <see langword="true"/> if the player is an AI-controlled bot;
	/// otherwise, <see langword="false"/>.</param>
	/// <param name="Team">The persistent lineup identity of the player in the match.</param>
	/// <param name="RoundsWon">The number of rounds won by the player's team while the player participated.</param>
	/// <param name="RoundsLost">The number of rounds lost by the player's team while the player participated.</param>
	/// <param name="RoundsParticipated">The total number of rounds in which the player actively participated.</param>
	/// <param name="Kills">The total number of kills achieved by the player.</param>
	/// <param name="Deaths">The total number of times the player died.</param>
	/// <param name="Assists">The total number of assists credited to the player.</param>
	/// <param name="UtilityDamage">The total amount of damage the player dealt with utility.</param>
	/// <param name="MvpCount">The number of Most Valuable Player (MVP) awards earned by the player.</param>
	/// <param name="Rank">The player's rank snapshot at the time of the statistics capture.</param>
	/// <param name="Adr">The average damage dealt per round by the player.</param>
	/// <param name="MultiKills">A summary of the player's multi-kill rounds.</param>
	/// <param name="AimRating">A rating representing the player's aiming performance.</param>
	/// <param name="UtilityRating">A rating representing the player's effectiveness with utility usage.</param>
	/// <param name="Trading">Statistics related to the player's trading actions, such as trade kills.</param>
	/// <param name="Clutches">Statistics summarizing the player's clutch performance.</param>
	/// <param name="HeadshotPercentage">The percentage of kills made by the player that were headshots.</param>
	/// <param name="TotalAccuracy">The player's overall weapon accuracy percentage.</param>
	/// <param name="SprayAccuracy">The player's accuracy percentage during spray firing.</param>
	/// <param name="Utility">Statistics related to the player's use of grenades and other utility items.</param>
	/// <param name="TeamDamage">Statistics summarizing damage dealt to teammates by the player.</param>
	/// <param name="Weapons">A read-only list of weapon-specific statistics for the player.</param>
	/// <param name="Impact">Statistics measuring the player's impact on the match, such as entry kills or opening duels.</param>
	/// <param name="BombPlants">The number of times the player planted the bomb.</param>
	/// <param name="BombDefuses">The number of times the player defused the bomb.</param>
	public sealed record PlayerStats(
		ulong SteamId,
		string Name,
		int UserId,
		bool IsBot,
		MatchTeam Team,
		int RoundsWon,
		int RoundsLost,
		int RoundsParticipated,
		int Kills,
		int Deaths,
		int Assists,
		int UtilityDamage,
		int MvpCount,
		RankSnapshot Rank,
		double Adr,
		MultiKillSummary MultiKills,
		double AimRating,
		double UtilityRating,
		TradingStats Trading,
		ClutchStats Clutches,
		double HeadshotPercentage,
		double TotalAccuracy,
		double SprayAccuracy,
		UtilityStats Utility,
		TeamDamageStats TeamDamage,
		IReadOnlyList<WeaponStats> Weapons,
		PlayerImpactStats Impact,
		int BombPlants,
		int BombDefuses);

	/// <summary>
	/// Represents a set of impact statistics for a player, including overall match impact and per-side performance
	/// metrics.
	/// </summary>
	/// <param name="MatchImpactPercentage">The percentage value indicating the player's overall impact on the match. Typically ranges from 0 to 100.</param>
	/// <param name="KillsPerRound">The player's average number of kills per round, split by side.</param>
	/// <param name="RoundImpact">The player's impact per round, split by side. This metric may include factors such as multi-kills or key actions.</param>
	/// <param name="WinProbability">The player's contribution to round win probability, split by side.</param>
	/// <param name="Rounds">A read-only list of snapshots detailing the player's impact in each round.</param>
	public sealed record PlayerImpactStats(
		double MatchImpactPercentage,
		SideSplitMetrics KillsPerRound,
		SideSplitMetrics RoundImpact,
		SideSplitMetrics WinProbability,
		IReadOnlyList<RoundImpactSnapshot> Rounds);

	/// <summary>
	/// Represents a set of metrics that provide overall and side-specific values for a match or scenario.
	/// </summary>
	/// <param name="Overall">The overall metric value, representing the combined result across all sides.</param>
	/// <param name="Terrorists">The metric value specific to the Terrorists side.</param>
	/// <param name="CounterTerrorists">The metric value specific to the Counter-Terrorists side.</param>
	public sealed record SideSplitMetrics(
		double Overall,
		double Terrorists,
		double CounterTerrorists);

	/// <summary>
	/// Represents a snapshot of a player's impact and performance metrics for a single round in a match.
	/// </summary>
	/// <param name="RoundNumber">The number of the round within the match. Must be a positive integer.</param>
	/// <param name="Side">The team side the player was on during the round.</param>
	/// <param name="Won">A value indicating whether the player's team won the round. Set to <see langword="true"/> if the round was won;
	/// otherwise, <see langword="false"/>.</param>
	/// <param name="Kills">The number of kills achieved by the player in the round. Must be zero or greater.</param>
	/// <param name="Assists">The number of assists credited to the player in the round. Must be zero or greater.</param>
	/// <param name="Deaths">The number of times the player died in the round. Must be zero or greater.</param>
	/// <param name="Damage">The total amount of damage dealt by the player in the round. Must be zero or greater.</param>
	/// <param name="WinProbability">The probability, as a value between 0.0 and 1.0, that the player's team would win the round at the start of the
	/// round.</param>
	/// <param name="RoundImpact">A calculated value representing the player's overall impact on the round, based on various performance metrics.</param>
	/// <param name="RoundRating">A rating value summarizing the player's performance in the round, typically normalized for comparison across
	/// rounds.</param>
	public sealed record RoundImpactSnapshot(
		int RoundNumber,
		CsTeamSide Side,
		bool Won,
		int Kills,
		int Assists,
		int Deaths,
		int Damage,
		double WinProbability,
		double RoundImpact,
		double RoundRating);

	/// <summary>
	/// Provides a mapping of competitive rank IDs to their corresponding display names for Counter-Strike ranks.
	/// </summary>
	/// <remarks>This class offers a centralized source for retrieving the display name of a rank based on its
	/// integer identifier. It is intended for use in scenarios where rank names need to be displayed or processed based on
	/// their numeric representation.</remarks>
	public static class CsRankNames
	{
		/// <summary>
		/// Provides a read-only mapping of rank identifiers to their corresponding rank names.
		/// </summary>
		/// <remarks>The dictionary contains predefined rank names indexed by integer values. The mapping is immutable
		/// and can be used to retrieve the display name for a given rank identifier.</remarks>
		public static readonly IReadOnlyDictionary<int, string> Names = new Dictionary<int, string>
		{
			{ 0,  "Unranked" },
			{ 1,  "Silver I" },
			{ 2,  "Silver II" },
			{ 3,  "Silver III" },
			{ 4,  "Silver IV" },
			{ 5,  "Silver Elite" },
			{ 6,  "Silver Elite Master" },
			{ 7,  "Gold Nova I" },
			{ 8,  "Gold Nova II" },
			{ 9,  "Gold Nova III" },
			{ 10, "Gold Nova Master" },
			{ 11, "Master Guardian I" },
			{ 12, "Master Guardian II" },
			{ 13, "Master Guardian Elite" },
			{ 14, "Distinguished Master Guardian" },
			{ 15, "Legendary Eagle" },
			{ 16, "Legendary Eagle Master" },
			{ 17, "Supreme Master First Class" },
			{ 18, "The Global Elite" },
		};

		/// <summary>
		/// Retrieves the name associated with the specified rank identifier.
		/// </summary>
		/// <param name="rankId">The identifier of the rank to look up. If null, the method returns "Unknown".</param>
		/// <returns>The name corresponding to the specified rank identifier if found; otherwise, "Unknown".</returns>
		public static string GetName(int? rankId) =>
			rankId.HasValue && Names.TryGetValue(rankId.Value, out string? name) ? name : "Unknown";
	}

	/// <summary>
	/// Represents a snapshot of a player's rank and related statistics at a specific point in time.
	/// </summary>
	/// <param name="RankBefore">The player's previous rank, or null if not available.</param>
	/// <param name="Rank">The player's current rank, or null if not available.</param>
	/// <param name="RankChange">The change in rank since the previous snapshot. A positive value indicates an increase in rank; a negative value
	/// indicates a decrease. Null if not available.</param>
	/// <param name="Wins">The number of wins associated with this snapshot, or null if not available.</param>
	/// <param name="RankTypeId">The identifier for the type of rank, or null if not specified.</param>
	/// <param name="VisibleSkill">The visible skill value associated with the player at the time of the snapshot, or null if not available.</param>
	public sealed record RankSnapshot(
		int? RankBefore,
		int? Rank,
		double? RankChange,
		int? Wins,
		int? RankTypeId,
		int? VisibleSkill)
	{
		/// <summary>
		/// Gets the display name of the rank that precedes the current rank.
		/// </summary>
		public string RankBeforeName => CsRankNames.GetName(RankBefore);

		/// <summary>
		/// Gets the display name of the current rank.
		/// </summary>
		public string RankName => CsRankNames.GetName(Rank);
	}

	/// <summary>
	/// Represents a summary of multi-kill statistics, including counts and round indices for two-, three-, four-, and
	/// five-kill events.
	/// </summary>
	/// <param name="TwoKs">The total number of rounds in which two kills occurred.</param>
	/// <param name="ThreeKs">The total number of rounds in which three kills occurred.</param>
	/// <param name="FourKs">The total number of rounds in which four kills occurred.</param>
	/// <param name="FiveKs">The total number of rounds in which five kills occurred.</param>
	/// <param name="TwoKRounds">A read-only list containing the indices of rounds where two kills were achieved.</param>
	/// <param name="ThreeKRounds">A read-only list containing the indices of rounds where three kills were achieved.</param>
	/// <param name="FourKRounds">A read-only list containing the indices of rounds where four kills were achieved.</param>
	/// <param name="FiveKRounds">A read-only list containing the indices of rounds where five kills were achieved.</param>
	public sealed record MultiKillSummary(
		int TwoKs,
		int ThreeKs,
		int FourKs,
		int FiveKs,
		IReadOnlyList<int> TwoKRounds,
		IReadOnlyList<int> ThreeKRounds,
		IReadOnlyList<int> FourKRounds,
		IReadOnlyList<int> FiveKRounds);

	/// <summary>
	/// Represents aggregated statistics related to trading kills and deaths in a trading scenario.
	/// </summary>
	/// <param name="TradeKills">The total number of kills achieved through trades.</param>
	/// <param name="TradedDeaths">The total number of deaths that occurred as a result of being traded.</param>
	/// <param name="TradeKillRate">The ratio of trade kills to total opportunities, expressed as a double. Must be between 0.0 and 1.0.</param>
	public sealed record TradingStats(
		int TradeKills,
		int TradedDeaths,
		double TradeKillRate);

	/// <summary>
	/// Represents statistical data for clutch scenarios, including total attempts, wins, and a breakdown of wins by the
	/// number of opponents.
	/// </summary>
	/// <param name="Attempts">The total number of clutch attempts recorded.</param>
	/// <param name="Wins">The total number of successful clutch wins.</param>
	/// <param name="WinsByOpponentCount">A read-only dictionary mapping the number of opponents faced to the number of wins achieved against that count.</param>
	public sealed record ClutchStats(
		int Attempts,
		int Wins,
		IReadOnlyDictionary<int, int> WinsByOpponentCount);

	/// <summary>
	/// Represents a set of statistics related to utility usage and effects in a match, including kills, deaths, and
	/// actions involving grenades and other utility items.
	/// </summary>
	/// <remarks>This record is typically used to aggregate and analyze a player's effectiveness with utility items
	/// during a match. All values are non-negative and represent cumulative statistics for a given period or
	/// match.</remarks>
	/// <param name="FragKills">The number of kills achieved with utility grenades, such as HE grenades or molotovs.</param>
	/// <param name="FragDeaths">The number of deaths caused by enemy utility grenades.</param>
	/// <param name="PlayersFlashed">The number of opposing players blinded by the player's flashbangs.</param>
	/// <param name="TimesFlashed">The number of times the player was blinded by enemy flashbangs.</param>
	/// <param name="MollyKills">The number of kills achieved specifically with molotovs or incendiary grenades.</param>
	/// <param name="MollyDeaths">The number of times the player was killed by enemy molotovs or incendiary grenades.</param>
	/// <param name="UtilityDamage">The total amount of damage dealt to opponents using utility grenades.</param>
	/// <param name="FlashbangsThrown">The number of flashbang grenades thrown by the player.</param>
	/// <param name="HeGrenadesThrown">The number of high-explosive (HE) grenades thrown by the player.</param>
	/// <param name="MolotovsThrown">The number of molotov or incendiary grenades thrown by the player.</param>
	public sealed record UtilityStats(
		int FragKills,
		int FragDeaths,
		int PlayersFlashed,
		int TimesFlashed,
		int MollyKills,
		int MollyDeaths,
		int UtilityDamage,
		int FlashbangsThrown,
		int HeGrenadesThrown,
		int MolotovsThrown);

	/// <summary>
	/// Represents aggregated team damage and team kill statistics for a match or round.
	/// </summary>
	/// <param name="Damage">The total amount of damage dealt to teammates.</param>
	/// <param name="TeamKillsUtility">The number of team kills caused by utility, such as grenades or other equipment.</param>
	/// <param name="TeamKillsOther">The number of team kills caused by means other than utility.</param>
	/// <param name="TeamFlashes">The number of times teammates were blinded by a flash effect.</param>
	public sealed record TeamDamageStats(
		int Damage,
		int TeamKillsUtility,
		int TeamKillsOther,
		int TeamFlashes);

	/// <summary>
	/// Represents statistical data for a specific weapon, including kills, deaths, assists, damage, shots fired, hits, and
	/// accuracy.
	/// </summary>
	/// <param name="Weapon">The name of the weapon for which the statistics are recorded. Cannot be null or empty.</param>
	/// <param name="Kills">The total number of kills achieved with the weapon. Must be zero or greater.</param>
	/// <param name="Deaths">The total number of deaths incurred while using the weapon. Must be zero or greater.</param>
	/// <param name="Assists">The total number of assists made with the weapon. Must be zero or greater.</param>
	/// <param name="Damage">The total amount of damage dealt using the weapon. Must be zero or greater.</param>
	/// <param name="Shots">The total number of shots fired with the weapon. Must be zero or greater.</param>
	/// <param name="Hits">The total number of successful hits made with the weapon. Must be zero or greater.</param>
	/// <param name="Accuracy">The accuracy percentage for the weapon, calculated as the ratio of hits to shots. Must be between 0.0 and 100.0.</param>
	public sealed record WeaponStats(
		string Weapon,
		int Kills,
		int Deaths,
		int Assists,
		int Damage,
		int Shots,
		int Hits,
		double Accuracy);

	/// <summary>
	/// Represents statistical data for a single round, including round number, duration, winning team, kills, and damage
	/// events.
	/// </summary>
	/// <param name="RoundNumber">The zero-based index of the round within the match. Must be non-negative.</param>
	/// <param name="StartTick">Beginning Tick of the round.</param>
	/// <param name="EndTick">End tick of the round.</param>
	/// <param name="Duration">The total elapsed time of the round, or null if the duration is not available.</param>
	/// <param name="WinnerTeam">The lineup identity that won the round.</param>
	/// <param name="WinnerSide">The side (T/CT) that won the round.</param>
	/// <param name="TeamASide">The side Team A played on for this round.</param>
	/// <param name="TeamBSide">The side Team B played on for this round.</param>
	/// <param name="Kills">A read-only list of all kill events that occurred during the round. Never null.</param>
	/// <param name="Damage">A read-only list of all damage events that occurred during the round. Never null.</param>
	public sealed record RoundStats(
		int RoundNumber,
		int StartTick,
		int EndTick,
		TimeSpan? Duration,
		MatchTeam WinnerTeam,
		CsTeamSide WinnerSide,
		CsTeamSide TeamASide,
		CsTeamSide TeamBSide,
		IReadOnlyList<RoundKillEvent> Kills,
		IReadOnlyList<RoundDamageEvent> Damage);

	/// <summary>
	/// Represents a kill event that occurred during a round, including details about the participants, weapon used, and
	/// contextual information about the kill.
	/// </summary>
	/// <param name="Tick">The game tick at which the kill event occurred.</param>
	/// <param name="KillerSteamId">The Steam ID of the player who performed the kill, or null if not available.</param>
	/// <param name="KillerName">The display name of the player who performed the kill, or null if not available.</param>
	/// <param name="VictimSteamId">The Steam ID of the player who was killed, or null if not available.</param>
	/// <param name="VictimName">The display name of the player who was killed, or null if not available.</param>
	/// <param name="AssisterSteamId">The Steam ID of the player who assisted in the kill, or null if there was no assister.</param>
	/// <param name="AssisterName">The display name of the player who assisted in the kill, or null if there was no assister.</param>
	/// <param name="Weapon">The name of the weapon used to perform the kill.</param>
	/// <param name="IsHeadshot">true if the kill was a headshot; otherwise, false.</param>
	/// <param name="IsTeamKill">true if the kill was a team kill; otherwise, false.</param>
	/// <param name="IsTrade">true if the kill was a trade (i.e., occurred shortly after the killer's teammate was killed by the victim);
	/// otherwise, false.</param>
	/// <param name="IsWallBang">true if the kill was performed through a wall or other penetrable surface; otherwise, false.</param>
	/// <param name="ThroughSmoke">true if the kill occurred through smoke; otherwise, false.</param>
	/// <param name="AttackerBlind">true if the attacker was blinded at the time of the kill; otherwise, false.</param>
	public sealed record RoundKillEvent(
		int Tick,
		ulong? KillerSteamId,
		string? KillerName,
		ulong? VictimSteamId,
		string? VictimName,
		ulong? AssisterSteamId,
		string? AssisterName,
		string Weapon,
		bool IsHeadshot,
		bool IsTeamKill,
		bool IsTrade,
		bool IsWallBang,
		bool ThroughSmoke,
		bool AttackerBlind);

	/// <summary>
	/// Represents a damage event that occurred during a round, including information about the attacker, victim, weapon
	/// used, and damage details.
	/// </summary>
	/// <param name="Tick">The game tick at which the damage event occurred.</param>
	/// <param name="AttackerSteamId">The Steam ID of the attacker, or null if the attacker is not identified.</param>
	/// <param name="AttackerName">The display name of the attacker, or null if the attacker is not identified.</param>
	/// <param name="VictimSteamId">The Steam ID of the victim, or null if the victim is not identified.</param>
	/// <param name="VictimName">The display name of the victim, or null if the victim is not identified.</param>
	/// <param name="Weapon">The name of the weapon used to inflict the damage.</param>
	/// <param name="Damage">The total amount of damage dealt in the event.</param>
	/// <param name="HealthDamage">The amount of health damage inflicted on the victim.</param>
	/// <param name="ArmorDamage">The amount of armor damage inflicted on the victim.</param>
	/// <param name="IsFriendlyFire">true if the damage was caused by a teammate; otherwise, false.</param>
	public sealed record RoundDamageEvent(
		int Tick,
		ulong? AttackerSteamId,
		string? AttackerName,
		ulong? VictimSteamId,
		string? VictimName,
		string Weapon,
		int Damage,
		int HealthDamage,
		int ArmorDamage,
		bool IsFriendlyFire);

	/// <summary>
	/// Provides extension methods for converting mutable collection types to their read-only counterparts.
	/// </summary>
	/// <remarks>These helper methods simplify the process of exposing collections as read-only interfaces, which
	/// can help prevent unintended modifications and improve API safety. All methods are static and intended to be used as
	/// extension methods on supported collection types.</remarks>
	internal static class ModelHelpers
	{
		public static IReadOnlyList<int> ToReadOnlyList(this List<int> rounds)
			=> new ReadOnlyCollection<int>(rounds);

		public static IReadOnlyDictionary<int, int> ToReadOnlyDictionary(this Dictionary<int, int> values)
			=> new ReadOnlyDictionary<int, int>(values);
	}
}