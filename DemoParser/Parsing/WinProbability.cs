namespace StrikeLink.DemoParser.Parsing
{
	/// <summary>
	/// Round win probability from the number of players alive per side and whether the bomb is planted, used to value kills by
	/// how much they moved the round.
	/// </summary>
	internal static class WinProbability
	{
		// Terrorist win probability for [tAlive, ctAlive] (index 0..5), from public CS round win-rate tables.
		private static readonly double[][] TerroristTable =
		[
			[0.500, 0.001, 0.001, 0.001, 0.001, 0.001],
			[0.999, 0.551, 0.189, 0.045, 0.009, 0.002],
			[0.999, 0.870, 0.547, 0.258, 0.099, 0.032],
			[0.999, 0.974, 0.819, 0.546, 0.300, 0.143],
			[0.999, 0.996, 0.942, 0.769, 0.525, 0.315],
			[0.999, 0.999, 0.981, 0.890, 0.702, 0.488],
		];

		private const double BombPlantedLogitShift = 0.55;
		private const double PostPlantManAdvantageLogit = 0.80;

		/// <summary>Probability that the terrorists win the round from this state.</summary>
		public static double Terrorists(int tAlive, int ctAlive, bool bombPlanted)
		{
			tAlive = Math.Clamp(tAlive, 0, 5);
			ctAlive = Math.Clamp(ctAlive, 0, 5);
			if (ctAlive == 0 && tAlive > 0) return 0.999;
			if (tAlive == 0 && !bombPlanted) return 0.001;

			double z = Logit(TerroristTable[tAlive][ctAlive]);
			if (bombPlanted) z += BombPlantedLogitShift + (PostPlantManAdvantageLogit * (tAlive - ctAlive));
			return Math.Clamp(1 / (1 + Math.Exp(-z)), 0.001, 0.999);
		}

		private static double Logit(double p) => Math.Log(p / (1 - p));
	}
}
