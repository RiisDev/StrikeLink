using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable CA1008
#pragma warning disable CA1028
#pragma warning disable CA1720

namespace StrikeLink.Extensions
{
	public static class Win32
	{
		public enum VirtualKey : uint
		{
			Backspace = 0x08,
			Tab = 0x09,
			Enter = 0x0D,
			Shift = 0x10,
			Ctrl = 0x11,
			Alt = 0x12,
			Pause = 0x13,
			CapsLock = 0x14,
			Esc = 0x1B,
			Spacebar = 0x20,
			PageUp = 0x21,
			PageDown = 0x22,
			End = 0x23,
			Home = 0x24,
			LeftArrow = 0x25,
			UpArrow = 0x26,
			RightArrow = 0x27,
			DownArrow = 0x28,
			Insert = 0x2D,
			Delete = 0x2E,

			// Numbers
			D0 = 0x30,
			D1 = 0x31,
			D2 = 0x32,
			D3 = 0x33,
			D4 = 0x34,
			D5 = 0x35,
			D6 = 0x36,
			D7 = 0x37,
			D8 = 0x38,
			D9 = 0x39,

			// Letters
			A = 0x41,
			B = 0x42,
			C = 0x43,
			D = 0x44,
			E = 0x45,
			F = 0x46,
			G = 0x47,
			H = 0x48,
			I = 0x49,
			J = 0x4A,
			K = 0x4B,
			L = 0x4C,
			M = 0x4D,
			N = 0x4E,
			O = 0x4F,
			P = 0x50,
			Q = 0x51,
			R = 0x52,
			S = 0x53,
			T = 0x54,
			U = 0x55,
			V = 0x56,
			W = 0x57,
			X = 0x58,
			Y = 0x59,
			Z = 0x5A,

			// Numpad
			Numpad0 = 0x60,
			Numpad1 = 0x61,
			Numpad2 = 0x62,
			Numpad3 = 0x63,
			Numpad4 = 0x64,
			Numpad5 = 0x65,
			Numpad6 = 0x66,
			Numpad7 = 0x67,
			Numpad8 = 0x68,
			Numpad9 = 0x69,
			Multiply = 0x6A,
			Add = 0x6B,
			Subtract = 0x6D,
			Decimal = 0x6E,
			Divide = 0x6F,

			// Function keys
			F1 = 0x70,
			F2 = 0x71,
			F3 = 0x72,
			F4 = 0x73,
			F5 = 0x74,
			F6 = 0x75,
			F7 = 0x76,
			F8 = 0x77,
			F9 = 0x78,
			F10 = 0x79,
			F11 = 0x7A,
			F12 = 0x7B,
			F13 = 0x7C,
			F14 = 0x7D,
			F15 = 0x7E,
			F16 = 0x7F,
			F17 = 0x80,
			F18 = 0x81,
			F19 = 0x82,
			F20 = 0x83,
			F21 = 0x84,
			F22 = 0x85,
			F23 = 0x86,
			F24 = 0x87,
			
			LeftShift = 0xA0,
			RightShift = 0xA1,
			LeftCtrl = 0xA2,
			RightCtrl = 0xA3,
			LeftAlt = 0xA4,
			RightAlt = 0xA5,

			Semicolon = 0xBA,
			Plus = 0xBB,
			Comma = 0xBC,
			Minus = 0xBD,
			Period = 0xBE,
			Slash = 0xBF,
			LeftBrace = 0xDB,
			Backslash = 0xDC,
			RightBrace = 0xDD,
			Apostrophe = 0xDE,

			ScrollLock = 0x91,
			NumLock = 0x90
		}
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

		internal static void PressKey(VirtualKey key)
		{
			keybd_event((uint)key, 0, 0, 0); 
			keybd_event((uint)key, 0, 2, 0);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

		internal static bool IsPrcoessActivated(Process process)
		{
			IntPtr activatedHandle = GetForegroundWindow();
			if (activatedHandle == IntPtr.Zero) return false;
			_ = GetWindowThreadProcessId(activatedHandle, out int activeProcId);
			return activeProcId == process.Id;
		}

		internal static readonly IReadOnlyDictionary<VirtualKey, string> VirtualKeyToChar = new Dictionary<VirtualKey, string>
		{
		    // Number row (unshifted)
		    { VirtualKey.D0, "0" },
			{ VirtualKey.D1, "1" },
			{ VirtualKey.D2, "2" },
			{ VirtualKey.D3, "3" },
			{ VirtualKey.D4, "4" },
			{ VirtualKey.D5, "5" },
			{ VirtualKey.D6, "6" },
			{ VirtualKey.D7, "7" },
			{ VirtualKey.D8, "8" },
			{ VirtualKey.D9, "9" },

			// Function keys
			{ VirtualKey.F1, "f1" },
			{ VirtualKey.F2, "f2" },
			{ VirtualKey.F3, "f3" },
			{ VirtualKey.F4, "f4" },
			{ VirtualKey.F5, "f5" },
			{ VirtualKey.F6, "f6" },
			{ VirtualKey.F7, "f7" },
			{ VirtualKey.F8, "f8" },
			{ VirtualKey.F9, "f9" },
			{ VirtualKey.F10, "f10" },
			{ VirtualKey.F11, "f11" },
			{ VirtualKey.F12, "f12" },
			{ VirtualKey.F13, "f13" },
			{ VirtualKey.F14, "f14" },
			{ VirtualKey.F15, "f15" },
			{ VirtualKey.F16, "f16" },
			{ VirtualKey.F17, "f17" },
			{ VirtualKey.F18, "f18" },
			{ VirtualKey.F19, "f19" },
			{ VirtualKey.F20, "f20" },
			{ VirtualKey.F21, "f21" },
			{ VirtualKey.F22, "f22" },
			{ VirtualKey.F23, "f23" },
			{ VirtualKey.F24, "f24" },

		    // Letters (lowercase, unshifted)
		    { VirtualKey.A, "a" },
			{ VirtualKey.B, "b" },
			{ VirtualKey.C, "c" },
			{ VirtualKey.D, "d" },
			{ VirtualKey.E, "e" },
			{ VirtualKey.F, "f" },
			{ VirtualKey.G, "g" },
			{ VirtualKey.H, "h" },
			{ VirtualKey.I, "i" },
			{ VirtualKey.J, "j" },
			{ VirtualKey.K, "k" },
			{ VirtualKey.L, "l" },
			{ VirtualKey.M, "m" },
			{ VirtualKey.N, "n" },
			{ VirtualKey.O, "o" },
			{ VirtualKey.P, "p" },
			{ VirtualKey.Q, "q" },
			{ VirtualKey.R, "r" },
			{ VirtualKey.S, "s" },
			{ VirtualKey.T, "t" },
			{ VirtualKey.U, "u" },
			{ VirtualKey.V, "v" },
			{ VirtualKey.W, "w" },
			{ VirtualKey.X, "x" },
			{ VirtualKey.Y, "y" },
			{ VirtualKey.Z, "z" },

		    // Punctuation (US keyboard)
		    { VirtualKey.Semicolon, "semicolon" },
			{ VirtualKey.Plus, "=" },
			{ VirtualKey.Comma, "," },
			{ VirtualKey.Minus, "-" },
			{ VirtualKey.Period, "." },
			{ VirtualKey.Slash, "/" },
			{ VirtualKey.LeftBrace, "[" },
			{ VirtualKey.RightBrace, "]" },
			{ VirtualKey.Backslash, "\\" },
			{ VirtualKey.Apostrophe, "'" },

		    // Numpad (NumLock ON)
		    { VirtualKey.Numpad0, "kp_0" },
			{ VirtualKey.Numpad1, "kp_1" },
			{ VirtualKey.Numpad2, "kp_2" },
			{ VirtualKey.Numpad3, "kp_3" },
			{ VirtualKey.Numpad4, "kp_4" },
			{ VirtualKey.Numpad5, "kp_5" },
			{ VirtualKey.Numpad6, "kp_6" },
			{ VirtualKey.Numpad7, "kp_7" },
			{ VirtualKey.Numpad8, "kp_8" },
			{ VirtualKey.Numpad9, "kp_9" },
			{ VirtualKey.Add, "kp_plus" },
			{ VirtualKey.Subtract, "kp_minus" },
			{ VirtualKey.Multiply, "kp_multiply" },
			{ VirtualKey.Divide, "kp_divide" },
			{ VirtualKey.Decimal, "kp_del" },

			// Others
			{ VirtualKey.Tab, "tab" },
			{ VirtualKey.CapsLock, "capslock" },
			{ VirtualKey.LeftShift, "shift" },
			{ VirtualKey.LeftCtrl, "ctrl" },
			{ VirtualKey.LeftAlt, "alt" },
			{ VirtualKey.Spacebar, "space" },
			{ VirtualKey.RightShift, "shift" },
			{ VirtualKey.RightCtrl, "rctrl" },
			{ VirtualKey.RightAlt, "ralt" },
			{ VirtualKey.Enter, "enter" },
			{ VirtualKey.Backspace, "backspace" },
			{ VirtualKey.Insert, "ins" },
			{ VirtualKey.PageUp, "pgup" },
			{ VirtualKey.PageDown, "pgdn" },
			{ VirtualKey.Delete, "del" },
			{ VirtualKey.End, "end" },
			{ VirtualKey.Home, "home" },
			{ VirtualKey.Esc, "escape" },
			{ VirtualKey.Pause, "pause" },
			{ VirtualKey.Shift, "shift" },
			{ VirtualKey.Ctrl, "ctrl" },
			{ VirtualKey.Alt, "alt" },
			{ VirtualKey.ScrollLock, "scrolllock" },
			{ VirtualKey.NumLock, "numlock" },

			// Arrows
			{ VirtualKey.UpArrow, "uparrow" },
			{ VirtualKey.DownArrow, "downarrow" },
			{ VirtualKey.LeftArrow, "leftarrow" },
			{ VirtualKey.RightArrow, "rightarrow" },
		};
	}
}
