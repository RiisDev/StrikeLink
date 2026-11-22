namespace StrikeLink.DemoParser.Parsing
{
	/// <summary>Coarse bombsite regions of a map.</summary>
	[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter<MapZone>))]
	public enum MapZone
	{
		/// <summary>Outside every tracked region.</summary>
		None = 0,
		/// <summary>The A bombsite and its approaches.</summary>
		A,
		/// <summary>The B bombsite and its approaches.</summary>
		B,
		/// <summary>Mid.</summary>
		Mid,
	}

	/// <summary>Maps world positions to coarse A, B and Mid regions using per-map bounding boxes.</summary>
	public static class MapZones
	{
		private readonly record struct Box(MapZone Zone, float MinX, float MaxX, float MinY, float MaxY, float MinZ, float MaxZ);

		// Boxes are from the cs2-demo-analyzer project (MIT); areas outside them count as none.
		private static readonly Dictionary<string, Box[]> Boxes = new(StringComparer.OrdinalIgnoreCase)
		{
			["ancient"] =
			[
				new(MapZone.A, -2175f, -1966f, 828f, 1282f, -200f, 320f),
				new(MapZone.A, -1496f, -686f, 1261f, 1526f, -200f, 320f),
				new(MapZone.A, -1611f, -1231f, 1036f, 1256f, -200f, 320f),
				new(MapZone.A, -1931f, -1681f, 356f, 501f, -200f, 320f),
				new(MapZone.A, -1727f, -1158f, 469f, 708f, -200f, 320f),
				new(MapZone.A, -1201f, -681f, 941f, 1181f, -200f, 320f),
				new(MapZone.A, -2155f, -1532f, -982f, -513f, -200f, 320f),
				new(MapZone.A, -2175f, -1911f, 334f, 833f, -200f, 320f),
				new(MapZone.A, -1976f, -1602f, 509f, 1287f, -200f, 320f),
				new(MapZone.A, -1198f, -1004f, 658f, 963f, -200f, 320f),
				new(MapZone.A, -1617f, -1183f, 693f, 972f, -500f, 500f),
				new(MapZone.B, 1155f, 1419f, 230f, 1002f, -200f, 320f),
				new(MapZone.B, 347f, 517f, -389f, -199f, -200f, 320f),
				new(MapZone.B, -32f, 532f, -658f, -364f, -200f, 320f),
				new(MapZone.B, -76f, 529f, 761f, 951f, -200f, 320f),
				new(MapZone.B, 756f, 1155f, 738f, 1042f, -200f, 320f),
				new(MapZone.B, 612f, 1170f, -79f, 399f, -500f, 500f),
				new(MapZone.Mid, -999f, -705f, -339f, -25f, -200f, 320f),
				new(MapZone.Mid, -306f, -111f, -294f, -104f, -200f, 320f),
				new(MapZone.Mid, 587f, 761f, -593f, -468f, -200f, 320f),
				new(MapZone.Mid, -311f, -206f, -912f, -299f, -200f, 320f),
				new(MapZone.Mid, -720f, -291f, -892f, -663f, -200f, 320f),
				new(MapZone.Mid, -1253f, -705f, -987f, -553f, -200f, 320f),
				new(MapZone.Mid, -710f, -306f, -678f, -99f, -500f, 500f),
				new(MapZone.Mid, -665f, -386f, -104f, 304f, -500f, 500f),
			],
			["inferno"] =
			[
				new(MapZone.A, 1774f, 2438f, -88f, 780f, -500f, 500f),
				new(MapZone.A, 1881f, 2530f, 775f, 1283f, -8f, 410f),
				new(MapZone.A, 1281f, 1808f, -103f, 434f, -8f, 410f),
				new(MapZone.A, 1134f, 1291f, 92f, 409f, -8f, 410f),
				new(MapZone.A, 1769f, 1940f, 585f, 756f, -8f, 410f),
				new(MapZone.B, -32f, 617f, 2406f, 3270f, -500f, 500f),
				new(MapZone.B, -135f, 651f, 990f, 2020f, -500f, 500f),
				new(MapZone.B, 339f, 651f, 2411f, 2616f, -8f, 410f),
				new(MapZone.B, 334f, 593f, 2991f, 3353f, -8f, 410f),
				new(MapZone.Mid, -8f, 973f, 414f, 1000f, -500f, 500f),
				new(MapZone.Mid, 964f, 1540f, 424f, 1054f, -500f, 500f),
			],
			["dust2"] =
			[
				new(MapZone.A, 1008f, 1254f, 2319f, 2805f, -10000f, 10000f),
				new(MapZone.A, 1254f, 1640f, 2310f, 2726f, -10000f, 10000f),
				new(MapZone.A, 1201f, 1591f, 789f, 2003f, -10000f, 10000f),
				new(MapZone.A, 504f, 947f, 2437f, 2682f, -10000f, 10000f),
				new(MapZone.A, 241f, 522f, 1920f, 2687f, -10000f, 10000f),
				new(MapZone.B, -1748f, -1350f, 2468f, 2893f, -10000f, 10000f),
				new(MapZone.B, -2209f, -1341f, 723f, 1578f, -10000f, 10000f),
				new(MapZone.B, -1258f, -530f, 1324f, 1556f, -10000f, 10000f),
				new(MapZone.Mid, -556f, -276f, 807f, 1324f, -10000f, 10000f),
				new(MapZone.Mid, -802f, 84f, 210f, 820f, -10000f, 10000f),
				new(MapZone.Mid, -276f, 228f, 789f, 1600f, -10000f, 10000f),
			],
			["mirage"] =
			[
				new(MapZone.A, -844f, -67f, -2545f, -1604f, -500f, 500f),
				new(MapZone.A, -87f, 1292f, -2446f, -2117f, -500f, 500f),
				new(MapZone.A, 172f, 570f, -1748f, -1385f, -368f, 70f),
				new(MapZone.A, 336f, 500f, -1534f, -1405f, -368f, 70f),
				new(MapZone.B, -2348f, -1586f, -150f, 607f, -500f, 500f),
				new(MapZone.B, -2139f, -1178f, 627f, 981f, -500f, 500f),
				new(MapZone.B, -2164f, -1895f, 607f, 991f, -368f, 70f),
				new(MapZone.Mid, -1058f, 57f, -897f, -449f, -500f, 500f),
				new(MapZone.Mid, -3f, 610f, -927f, -30f, -500f, 500f),
				new(MapZone.Mid, -834f, 27f, -483f, 35f, -500f, 500f),
			],
			["nuke"] =
			[
				new(MapZone.A, 325f, 968f, -1448f, -328f, -415.969f, -280f),
				new(MapZone.A, 288f, 1000f, -725f, -272f, -127.969f, -14.469f),
				new(MapZone.A, 1000f, 1275f, -475f, -325f, -127.969f, -21.469f),
				new(MapZone.B, 320f, 1016f, -1328f, -100f, -768f, -504f),
				new(MapZone.B, 1000f, 1416f, -750f, -350f, -639.969f, -520f),
			],
			["overpass"] =
			[
				new(MapZone.A, -3099f, -1694f, 428f, 1107f, 350f, 700f),
				new(MapZone.A, -4001f, -3384f, -2569f, -29f, 350f, 700f),
				new(MapZone.A, -2373f, -2073f, -915f, 163f, 350f, 700f),
				new(MapZone.B, -1285f, -662f, -293f, 340f, -500f, 300f),
				new(MapZone.B, -1222f, -828f, -671f, -158f, -500f, 300f),
				new(MapZone.Mid, -2882f, -2254f, -1641f, -1014f, -10f, 680f),
				new(MapZone.Mid, -2809f, -2368f, -1040f, -49f, 350f, 700f),
			],
			["vertigo"] =
			[
				new(MapZone.A, -768f, -48f, -1424f, -80f, 11712f, 11936f),
				new(MapZone.A, -864f, -288f, -1376f, -496f, 11504f, 11904f),
				new(MapZone.B, -2624f, -1648f, 160f, 1088f, 11776f, 11936f),
				new(MapZone.Mid, -2032f, -1296f, -288f, 432f, 11776f, 11952f),
				new(MapZone.Mid, -1440f, -576f, -464f, 64f, 11488f, 11632f),
			],
			["anubis"] =
			[
				new(MapZone.A, 880f, 1560f, 1408f, 2320f, -160f, 32f),
				new(MapZone.A, 1274f, 1862f, 788f, 1668f, -500f, 500f),
				new(MapZone.A, 702f, 1160f, 1225f, 1689f, -500f, 500f),
				new(MapZone.B, -1693f, -787f, 231f, 991f, -500f, 500f),
				new(MapZone.B, -2042f, -924f, -504f, 224f, -500f, 500f),
				new(MapZone.Mid, -214f, 405f, 455f, 1725f, -500f, 500f),
				new(MapZone.Mid, -522f, 103f, -534f, -34f, -260f, 230f),
			],
			["train"] =
			[
				new(MapZone.A, -448f, 496f, -448f, 672f, -144f, 48f),
				new(MapZone.B, 194f, 706f, -1544f, -1032f, -240f, -48f),
				new(MapZone.Mid, 96f, 608f, -880f, -320f, -208f, -16f),
			],
		};

		/// <summary>The region containing a position on a map (name with or without the de_ prefix), or <see cref="MapZone.None"/>.</summary>
		public static MapZone Find(string? map, PlayerPosition position)
		{
			ArgumentNullException.ThrowIfNull(position);
			if (string.IsNullOrWhiteSpace(map) || !Boxes.TryGetValue(map.Trim().Replace("de_", "", StringComparison.OrdinalIgnoreCase), out Box[]? boxes)) return MapZone.None;

			foreach (Box box in boxes)
			{
				if (position.X >= box.MinX && position.X <= box.MaxX && position.Y >= box.MinY && position.Y <= box.MaxY && position.Z >= box.MinZ && position.Z <= box.MaxZ) return box.Zone;
			}

			return MapZone.None;
		}
	}
}
