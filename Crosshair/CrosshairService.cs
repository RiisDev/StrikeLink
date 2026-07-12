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
	///   <item><description><see cref="ApplyShareCodeAsync"/> — sends <c>cl_crosshaircode CSGO-...</c> via the console,
	///   which is the same mechanism CS2 uses natively when importing a share code.</description></item>
	///   <item><description><see cref="ApplySettingsAsync"/> — writes every individual cvar to a temporary cfg file
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
		/// <returns>A share code in the format <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>.</returns>
		public static string ToShareCode(CrosshairSettings settings) =>
			CrosshairShareCode.Encode(settings);

		/// <summary>
		/// Decodes a CS2 share code into a <see cref="CrosshairSettings"/> record.
		/// </summary>
		/// <param name="shareCode">A share code in the format <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>.</param>
		/// <returns>The decoded <see cref="CrosshairSettings"/>.</returns>
		/// <exception cref="ArgumentException">Thrown when the share code is invalid.</exception>
		public static CrosshairSettings FromShareCode(string shareCode) =>
			CrosshairShareCode.Decode(shareCode);

		/// <summary>
		/// Applies a crosshair share code in CS2 via the <c>cl_crosshaircode</c> console command.
		/// </summary>
		/// <param name="shareCode">
		/// A valid share code in the format <c>CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX</c>.
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
		/// Reads the current crosshair share code from the user's saved CS2 convars file.
		/// </summary>
		/// <param name="userId">
		/// The Steam account ID to read from; uses the current user when <see langword="null"/>.
		/// </param>
		/// <returns>
		/// The stored share code, or <see langword="null"/> if the convars file or key is not found.
		/// </returns>
		public string? ReadCurrentShareCode(long? userId = null)
		{
			ConfigNode? root = TryGetConvarsRoot(userId);
			if (root is null) return null;

			return root.Value.TryGetProperty("cl_crosshaircode", out ConfigNode node)
				? node.GetString()
				: null;
		}

		/// <summary>
		/// Reads and decodes the current crosshair settings from the user's saved CS2 convars file.
		/// </summary>
		/// <param name="userId">
		/// The Steam account ID to read from; uses the current user when <see langword="null"/>.
		/// </param>
		/// <returns>
		/// The decoded <see cref="CrosshairSettings"/>, or <see langword="null"/> if no share code is stored
		/// or it cannot be decoded.
		/// </returns>
		public CrosshairSettings? ReadCurrentSettings(long? userId = null)
		{
			string? code = ReadCurrentShareCode(userId);
			if (string.IsNullOrEmpty(code)) return null;

			try { return CrosshairShareCode.Decode(code); }
			catch { return null; }
		}

		private static ConfigNode? TryGetConvarsRoot(long? userId)
		{
			long id = userId ?? SteamService.GetCurrentUserId();
			string cfgPath = Path.Combine(
				SteamService.GetSteamPath(),
				"userdata",
				id.ToString(CultureInfo.InvariantCulture),
				"730", "local", "cfg");

			if (!Directory.Exists(cfgPath)) return null;

			string[] files = Directory.GetFiles(cfgPath, "cs2_user_convars*.vcfg");
			if (files.Length == 0) return null;

			string file = files.OrderBy(f => f, StringComparer.OrdinalIgnoreCase).First();

			try { return new ValveCfgReader(file).Document.Root; }
			catch { return null; }
		}

		private static string BuildCfg(CrosshairSettings s)
		{
			StringBuilder sb = new();
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairsize {s.Size}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairgap {s.Gap}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairthickness {s.Thickness}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairalpha {s.Alpha}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshaircolor {(int)s.Color}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshaircolor_r {s.CustomColorR}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshaircolor_g {s.CustomColorG}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshaircolor_b {s.CustomColorB}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairdot {(s.Dot ? 1 : 0)}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshair_t {(s.TStyle ? 1 : 0)}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshair_drawoutline {(s.DrawOutline ? 1 : 0)}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshair_outlinethickness {s.OutlineThickness}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairusealpha {(s.UseAlpha ? 1 : 0)}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshairgap_useweaponvalue {(s.UseWeaponGap ? 1 : 0)}");
			sb.AppendLine(CultureInfo.InvariantCulture, $"cl_crosshair_sniper_width {s.SniperWidth}");
			return sb.ToString();
		}
	}
}
