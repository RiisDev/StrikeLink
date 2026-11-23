using System.Globalization;
using StrikeLink.ChatBot;
using StrikeLink.Services;
using StrikeLink.Services.Config;
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#pragma warning disable CA1822

namespace StrikeLink.Crosshair
{
	/// <summary>
	/// Provides crosshair import, export, and application for CS2.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Crosshair settings can be applied in two ways:
	/// <list type="bullet">
	///   <item><description><see cref="ApplyShareCodeAsync"/> - sends <c>cl_crosshaircode CS...</c> via the console,
	///   which is the same mechanism CS2 uses natively when importing a share code.</description></item>
	///   <item><description><see cref="ApplySettingsAsync"/> - writes every individual cvar to a temporary cfg file
	///   and execs it, giving full control over each setting independently.</description></item>
	/// </list>
	/// </para>
	/// <para>
	/// Current crosshair settings can be read from the user's saved <c>cs2_user_convars*.vcfg</c> file without
	/// requiring CS2 to be running.
	/// </para>
	/// </remarks>
	public class CrosshairService
	{
		private readonly ConsoleService _console;
		private readonly string _cfgDirectory;

		/// <summary>
		/// Initializes a new instance of the <see cref="CrosshairService"/> class.
		/// </summary>
		/// <param name="console">
		/// A <see cref="ConsoleService"/> instance used to send commands to CS2.
		/// </param>
		/// <exception cref="DirectoryNotFoundException">Thrown when the CS2 installation directory cannot be located.</exception>
		public CrosshairService(ConsoleService console)
		{
			ArgumentNullException.ThrowIfNull(console);
			_console = console;
			_cfgDirectory = Path.Combine(SteamService.GetGamePath(730), "game", "csgo", "cfg");
		}

		/// <summary>
		/// Encodes a <see cref="CrosshairSettings"/> record into a CS2 share code string.
		/// </summary>
		/// <param name="settings">The settings to encode.</param>
		/// <returns>A share code in the format <c>CS...</c>.</returns>
		public static string ToShareCode(CrosshairSettings settings) =>
			CrosshairShareCode.Encode(settings);

		/// <summary>
		/// Decodes a CS2 share code into a <see cref="CrosshairSettings"/> record.
		/// </summary>
		/// <param name="shareCode">A share code in the format <c>CS...</c>.</param>
		/// <returns>The decoded <see cref="CrosshairSettings"/>.</returns>
		/// <exception cref="ArgumentException">Thrown when the share code is invalid.</exception>
		public static CrosshairSettings FromShareCode(string shareCode) =>
			CrosshairShareCode.Decode(shareCode);

		/// <summary>
		/// Applies a crosshair share code in CS2 via the <c>cl_crosshaircode</c> console command.
		/// </summary>
		/// <param name="shareCode">
		/// A valid share code in the format <c>CS...</c>.
		/// </param>
		/// <param name="config">Optional console configuration; uses the instance default when <see langword="null"/>.</param>
		/// <param name="ct">Cancellation token.</param>
		/// <exception cref="ArgumentException">Thrown when the share code format is invalid.</exception>
		public async Task ApplyShareCodeAsync(string shareCode, ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			CrosshairShareCode.Decode(shareCode);
			await _console.SendConsoleCommand($"cl_crosshaircode {shareCode}", config, ct).ConfigureAwait(false);
		}

		/// <summary>
		/// Encodes the given settings into a share code and applies it in CS2 via <c>cl_crosshaircode</c>.
		/// </summary>
		/// <param name="settings">The settings to apply.</param>
		/// <param name="config">Optional console configuration; uses the instance default when <see langword="null"/>.</param>
		/// <param name="ct">Cancellation token.</param>
		public Task ApplyShareCodeAsync(CrosshairSettings settings, ConsoleServiceConfig? config = null, CancellationToken ct = default) =>
			ApplyShareCodeAsync(CrosshairShareCode.Encode(settings), config, ct);

		/// <summary>
		/// Applies all crosshair settings individually by writing a temporary cfg file and executing it via <c>exec</c>.
		/// </summary>
		/// <remarks>
		/// This writes <c>strike_link_crosshair.cfg</c> to the CS2 cfg directory and execs it.
		/// Use this method when you need per-cvar control rather than share-code import.
		/// </remarks>
		/// <param name="settings">The settings to apply.</param>
		/// <param name="config">Optional console configuration; uses the instance default when <see langword="null"/>.</param>
		/// <param name="ct">Cancellation token.</param>
		public async Task ApplySettingsAsync(CrosshairSettings settings, ConsoleServiceConfig? config = null, CancellationToken ct = default)
		{
			ArgumentNullException.ThrowIfNull(settings);

			string cfgPath = Path.Combine(_cfgDirectory, "strike_link_crosshair.cfg");
			await File.WriteAllTextAsync(cfgPath, BuildCfg(settings), ct).ConfigureAwait(false);
			await _console.ExecuteCfgFile("strike_link_crosshair.cfg", config, ct).ConfigureAwait(false);
		}

		/// <summary>
		/// Reads the crosshair CS2 last saved for a Steam account and returns it as a share code.
		/// </summary>
		/// <param name="userId">The Steam account ID to read from; uses the current user when <see langword="null"/>.</param>
		/// <returns>The share code, or <see langword="null"/> if the convars file is missing or has no crosshair in the current format.</returns>
		public static string? ReadCurrentShareCode(long? userId = null) =>
			ReadCurrentSettings(userId) is { } settings ? CrosshairShareCode.Encode(settings) : null;

		/// <summary>
		/// Reads the crosshair cvars CS2 last saved to <c>cs2_user_convars*.vcfg</c>; CS2 does not need to be running.
		/// Cvars missing from the file fall back to the <see cref="CrosshairSettings"/> defaults.
		/// </summary>
		/// <param name="userId">The Steam account ID to read from; uses the current user when <see langword="null"/>.</param>
		/// <returns>
		/// The saved settings, or <see langword="null"/> if the file is missing or predates the new crosshair cvars
		/// (no <c>cl_crosshair_length</c>).
		/// </returns>
		public static CrosshairSettings? ReadCurrentSettings(long? userId = null)
		{
			ConfigNode? root = TryGetConfigRoot(userId);
			if (root is null || !root.Value.TryGetProperty("convars", out ConfigNode convars) || !convars.TryGetProperty("cl_crosshair_length", out _))
				return null;

			return FromConvars(convars);
		}

		private static CrosshairSettings FromConvars(ConfigNode convars)
		{
			CrosshairSettings d = new();

			string? Get(string name) => convars.TryGetProperty(name, out ConfigNode node) ? node.GetString() : null;
			double Num(string name, double fallback) => double.TryParse(Get(name), NumberStyles.Float, CultureInfo.InvariantCulture, out double v) ? v : fallback;
			int Int(string name, int fallback) => (int)Math.Round(Num(name, fallback));
			bool Bool(string name, bool fallback) => Get(name) switch { "true" or "1" => true, "false" or "0" => false, _ => fallback };

			// cl_crosshair_drawoutline is saved as 0/1/2, tolerate true/false too.
			int outline = Get("cl_crosshair_drawoutline") switch { "true" => 1, "false" => 0, _ => Int("cl_crosshair_drawoutline", 0) };

			return new CrosshairSettings
			{
				Style = (CrosshairStyle)Int("cl_crosshairstyle", (int)d.Style),
				FollowRecoil = Bool("cl_crosshair_recoil", d.FollowRecoil),
				Dot = Bool("cl_crosshairdot", d.Dot),
				Length = Int("cl_crosshair_length", d.Length),
				Thickness = Int("cl_crosshair_thickness", d.Thickness),
				Gap = Int("cl_crosshair_gap", d.Gap),
				Outline = (CrosshairOutline)Math.Clamp(outline, 0, 2),
				OutlineRed = Int("cl_crosshairoutline_r", d.OutlineRed),
				OutlineGreen = Int("cl_crosshairoutline_g", d.OutlineGreen),
				OutlineBlue = Int("cl_crosshairoutline_b", d.OutlineBlue),
				OutlineAlpha = Int("cl_crosshairoutline_a", d.OutlineAlpha),
				Red = Int("cl_crosshaircolor_r", d.Red),
				Green = Int("cl_crosshaircolor_g", d.Green),
				Blue = Int("cl_crosshaircolor_b", d.Blue),
				Alpha = Int("cl_crosshaircolor_a", d.Alpha),
				TStyle = Bool("cl_crosshair_t", d.TStyle),
				SplitDistance = Int("cl_crosshair_dynamic_splitdist", d.SplitDistance),
				InnerSplitAlpha = Num("cl_crosshair_dynamic_splitalpha_innermod", d.InnerSplitAlpha),
				OuterSplitAlpha = Num("cl_crosshair_dynamic_splitalpha_outermod", d.OuterSplitAlpha),
				SplitSizeRatio = Num("cl_crosshair_dynamic_maxdist_splitratio", d.SplitSizeRatio),
				SpreadLimit = Int("cl_crosshair_dynamic_spread_limit", d.SpreadLimit),
				ScreenHeight = Int("cl_crosshair_screen_height", d.ScreenHeight),
				ScopeDotUsesCrosshairColor = Bool("cl_ironsight_usecrosshaircolor", d.ScopeDotUsesCrosshairColor),
				ScopeDotScale = Num("cl_ironsight_dot_scale", d.ScopeDotScale),
				SniperWidth = Int("cl_crosshair_sniper_width", d.SniperWidth)
			};
		}

		private static ConfigNode? TryGetConfigRoot(long? userId)
		{
			long id = userId ?? SteamService.GetCurrentUserId();
			string cfgPath = Path.Combine(
				SteamService.GetSteamPath(),
				"userdata",
				id.ToString(CultureInfo.InvariantCulture),
				"730", "local", "cfg");

			if (!Directory.Exists(cfgPath)) return null;

			string? file = Directory.GetFiles(cfgPath, "cs2_user_convars*.vcfg")
				.OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
				.FirstOrDefault();
			if (file is null) return null;

			try { return new ValveCfgReader(file).Document.Root; }
			catch (IOException) { return null; }
		}

		private static string BuildCfg(CrosshairSettings s)
		{
			StringBuilder sb = new();
			void Add(string cvar, object value) =>
				sb.AppendLine(CultureInfo.InvariantCulture, $"{cvar} {value}");

			Add("cl_crosshairstyle", (int)s.Style);
			Add("cl_crosshair_recoil", s.FollowRecoil ? 1 : 0);
			Add("cl_crosshairdot", s.Dot ? 1 : 0);
			Add("cl_crosshair_length", s.Length);
			Add("cl_crosshair_thickness", s.Thickness);
			Add("cl_crosshair_gap", s.Gap);
			Add("cl_crosshair_drawoutline", (int)s.Outline);
			Add("cl_crosshairoutline_r", s.OutlineRed);
			Add("cl_crosshairoutline_g", s.OutlineGreen);
			Add("cl_crosshairoutline_b", s.OutlineBlue);
			Add("cl_crosshairoutline_a", s.OutlineAlpha);
			Add("cl_crosshaircolor_r", s.Red);
			Add("cl_crosshaircolor_g", s.Green);
			Add("cl_crosshaircolor_b", s.Blue);
			Add("cl_crosshaircolor_a", s.Alpha);
			Add("cl_crosshair_t", s.TStyle ? 1 : 0);
			Add("cl_crosshair_dynamic_splitdist", s.SplitDistance);
			Add("cl_crosshair_dynamic_splitalpha_innermod", s.InnerSplitAlpha);
			Add("cl_crosshair_dynamic_splitalpha_outermod", s.OuterSplitAlpha);
			Add("cl_crosshair_dynamic_maxdist_splitratio", s.SplitSizeRatio);
			Add("cl_crosshair_sniper_width", s.SniperWidth);
			Add("cl_crosshair_dynamic_spread_limit", s.SpreadLimit);
			Add("cl_crosshair_screen_height", s.ScreenHeight);
			Add("cl_ironsight_usecrosshaircolor", s.ScopeDotUsesCrosshairColor ? 1 : 0);
			Add("cl_ironsight_dot_scale", s.ScopeDotScale);
			return sb.ToString();
		}
	}
}
