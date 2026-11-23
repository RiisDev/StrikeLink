namespace StrikeLink.Crosshair
{
	/// <summary>Value of <c>cl_crosshairstyle</c>. Numbering matches the share code style field.</summary>
	public enum CrosshairStyle
	{
		DynamicCross = 0,
		DynamicCircle = 1,
		DynamicCrossClassic = 2,
		StaticCircle = 3,
		StaticCross = 4,
		ShotFeedback = 5,
		DotOnly = 6,
		DynamicQuadrant = 7,
		StaticSquare = 8,
		StaticQuadrant = 9
	}

	/// <summary>Value of <c>cl_crosshair_drawoutline</c>.</summary>
	public enum CrosshairOutline
	{
		Off = 0,
		On = 1,
		Half = 2
	}

	/// <summary>
	/// CS2 crosshair configuration, one property per console variable, in the units the game and the
	/// <c>CS...</c> share code use (lengths, thickness and gap are whole pixels).
	/// </summary>
	public sealed record CrosshairSettings
	{
		/// <summary><c>cl_crosshairstyle</c>. 0-31 fits the share code.</summary>
		public CrosshairStyle Style { get; init; } = CrosshairStyle.StaticCross;

		/// <summary><c>cl_crosshair_recoil</c>.</summary>
		public bool FollowRecoil { get; init; }

		/// <summary><c>cl_crosshairdot</c>.</summary>
		public bool Dot { get; init; }

		/// <summary><c>cl_crosshair_length</c> in pixels, 0-255.</summary>
		public int Length { get; init; } = 8;

		/// <summary><c>cl_crosshair_thickness</c> in pixels, 0-32.</summary>
		public int Thickness { get; init; } = 2;

		/// <summary><c>cl_crosshair_gap</c> in pixels, -3840 to 3840.</summary>
		public int Gap { get; init; } = 4;

		/// <summary><c>cl_crosshair_drawoutline</c>.</summary>
		public CrosshairOutline Outline { get; init; } = CrosshairOutline.Off;

		/// <summary><c>cl_crosshairoutline_r</c>, 0-255.</summary>
		public int OutlineRed { get; init; }

		/// <summary><c>cl_crosshairoutline_g</c>, 0-255.</summary>
		public int OutlineGreen { get; init; }

		/// <summary><c>cl_crosshairoutline_b</c>, 0-255.</summary>
		public int OutlineBlue { get; init; }

		/// <summary><c>cl_crosshairoutline_a</c>, 0-255.</summary>
		public int OutlineAlpha { get; init; } = 255;

		/// <summary><c>cl_crosshaircolor_r</c>, 0-255.</summary>
		public int Red { get; init; }

		/// <summary><c>cl_crosshaircolor_g</c>, 0-255.</summary>
		public int Green { get; init; } = 255;

		/// <summary><c>cl_crosshaircolor_b</c>, 0-255.</summary>
		public int Blue { get; init; }

		/// <summary><c>cl_crosshaircolor_a</c>, 0-255.</summary>
		public int Alpha { get; init; } = 255;

		/// <summary><c>cl_crosshair_t</c>.</summary>
		public bool TStyle { get; init; }

		/// <summary><c>cl_crosshair_dynamic_splitdist</c>, 0-127.</summary>
		public int SplitDistance { get; init; } = 3;

		/// <summary><c>cl_crosshair_dynamic_splitalpha_innermod</c>, 0-1 in 0.01 steps.</summary>
		public double InnerSplitAlpha { get; init; }

		/// <summary><c>cl_crosshair_dynamic_splitalpha_outermod</c>, 0.3-1 in 0.01 steps.</summary>
		public double OuterSplitAlpha { get; init; } = 1;

		/// <summary><c>cl_crosshair_dynamic_maxdist_splitratio</c>, 0-1 in 0.01 steps.</summary>
		public double SplitSizeRatio { get; init; } = 1;

		/// <summary><c>cl_crosshair_dynamic_spread_limit</c>, 0-255.</summary>
		public int SpreadLimit { get; init; } = 255;

		/// <summary><c>cl_crosshair_screen_height</c>, 240-65535.</summary>
		public int ScreenHeight { get; init; } = 1080;

		/// <summary><c>cl_ironsight_usecrosshaircolor</c>.</summary>
		public bool ScopeDotUsesCrosshairColor { get; init; }

		/// <summary><c>cl_ironsight_dot_scale</c>, 0.1-2 in 0.01 steps.</summary>
		public double ScopeDotScale { get; init; } = 1;

		/// <summary><c>cl_crosshair_sniper_width</c>. Applied through <see cref="CrosshairService.ApplySettingsAsync"/> only, the share code does not carry it.</summary>
		public int SniperWidth { get; init; }
	}
}
