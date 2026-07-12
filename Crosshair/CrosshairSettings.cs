namespace StrikeLink.Crosshair
{
	/// <summary>
	/// Represents the preset color options available for a CS2 crosshair.
	/// </summary>
	public enum CrosshairColor
	{
		/// <summary>Red.</summary>
		Red = 0,

		/// <summary>Green (default).</summary>
		Green = 1,

		/// <summary>Yellow.</summary>
		Yellow = 2,

		/// <summary>Blue.</summary>
		Blue = 3,

		/// <summary>Cyan.</summary>
		Cyan = 4,

		/// <summary>Custom RGB color defined by <see cref="CrosshairSettings.CustomColorR"/>, <see cref="CrosshairSettings.CustomColorG"/>, and <see cref="CrosshairSettings.CustomColorB"/>.</summary>
		Custom = 5
	}

	/// <summary>
	/// Represents the full set of CS2 crosshair configuration values that map directly to in-game console variables.
	/// </summary>
	/// <remarks>
	/// This record is used with <see cref="CrosshairShareCode"/> to encode and decode <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>
	/// share codes, and with <see cref="CrosshairService"/> to apply, read, and export crosshair settings.
	/// </remarks>
	public sealed record CrosshairSettings
	{
		/// <summary>
		/// Gets the crosshair size (<c>cl_crosshairsize</c>). Default is <c>2.0</c>.
		/// </summary>
		public float Size { get; init; } = 2.0f;

		/// <summary>
		/// Gets the crosshair gap (<c>cl_crosshairgap</c>). Negative values bring lines closer together. Default is <c>-3.0</c>.
		/// </summary>
		public float Gap { get; init; } = -3.0f;

		/// <summary>
		/// Gets the crosshair line thickness (<c>cl_crosshairthickness</c>). Default is <c>0.5</c>.
		/// </summary>
		public float Thickness { get; init; } = 0.5f;

		/// <summary>
		/// Gets the crosshair alpha transparency (<c>cl_crosshairalpha</c>). Range 0–255. Default is <c>200</c>.
		/// </summary>
		public int Alpha { get; init; } = 200;

		/// <summary>
		/// Gets the preset crosshair color (<c>cl_crosshaircolor</c>). Default is <see cref="CrosshairColor.Green"/>.
		/// </summary>
		public CrosshairColor Color { get; init; } = CrosshairColor.Green;

		/// <summary>
		/// Gets the custom red channel (<c>cl_crosshaircolor_r</c>). Used when <see cref="Color"/> is <see cref="CrosshairColor.Custom"/>. Range 0–255.
		/// </summary>
		public int CustomColorR { get; init; } = 50;

		/// <summary>
		/// Gets the custom green channel (<c>cl_crosshaircolor_g</c>). Used when <see cref="Color"/> is <see cref="CrosshairColor.Custom"/>. Range 0–255.
		/// </summary>
		public int CustomColorG { get; init; } = 250;

		/// <summary>
		/// Gets the custom blue channel (<c>cl_crosshaircolor_b</c>). Used when <see cref="Color"/> is <see cref="CrosshairColor.Custom"/>. Range 0–255.
		/// </summary>
		public int CustomColorB { get; init; } = 50;

		/// <summary>
		/// Gets a value indicating whether the center dot is shown (<c>cl_crosshairdot</c>). Default is <c>false</c>.
		/// </summary>
		public bool Dot { get; init; }

		/// <summary>
		/// Gets a value indicating whether the crosshair uses T-style (no top line) (<c>cl_crosshair_t</c>). Default is <c>false</c>.
		/// </summary>
		public bool TStyle { get; init; }

		/// <summary>
		/// Gets a value indicating whether an outline is drawn around the crosshair lines (<c>cl_crosshair_drawoutline</c>). Default is <c>false</c>.
		/// </summary>
		public bool DrawOutline { get; init; }

		/// <summary>
		/// Gets the outline thickness (<c>cl_crosshair_outlinethickness</c>). Range 0.0–3.0. Default is <c>1.0</c>.
		/// </summary>
		public float OutlineThickness { get; init; } = 1.0f;

		/// <summary>
		/// Gets a value indicating whether alpha transparency is applied (<c>cl_crosshairusealpha</c>). Default is <c>true</c>.
		/// </summary>
		public bool UseAlpha { get; init; } = true;

		/// <summary>
		/// Gets a value indicating whether the crosshair gap scales with the equipped weapon (<c>cl_crosshairgap_useweaponvalue</c>). Default is <c>false</c>.
		/// </summary>
		public bool UseWeaponGap { get; init; }

		/// <summary>
		/// Gets the sniper scope crosshair width (<c>cl_crosshair_sniper_width</c>). Default is <c>1.0</c>.
		/// </summary>
		public float SniperWidth { get; init; } = 1.0f;
	}
}
