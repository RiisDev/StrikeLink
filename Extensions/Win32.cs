using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
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
			Clear = 0x0C,
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
			Select = 0x29,
			Print = 0x2A,
			PrintScreen = 0x2C,
			Insert = 0x2D,
			Delete = 0x2E,
			Help = 0x2F,

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
			Separator = 0x6C,
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

			NumLock = 0x90,
			ScrollLock = 0x91,

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
			OEM8 = 0xDF,
			OEM102 = 0xE2,

			// Gamepad
			GamepadA = 0xC3,
			GamepadB = 0xC4,
			GamepadX = 0xC5,
			GamepadY = 0xC6,
			GamepadRightShoulder = 0xC7,
			GamepadLeftShoulder = 0xC8,
			GamepadLeftTrigger = 0xC9,
			GamepadRightTrigger = 0xCA,
			GamepadDpadUp = 0xCB,
			GamepadDpadDown = 0xCC,
			GamepadDpadLeft = 0xCD,
			GamepadDpadRight = 0xCE,
			GamepadMenu = 0xCF,
			GamepadView = 0xD0,
		}
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

		public static void SendLKey(VirtualKey key) => keybd_event((uint)key, 0, 0, 0);

		private static void PressKey(VirtualKey key)
		{
			keybd_event((uint)key, 0, 0, 0); 
			keybd_event((uint)key, 0, 2, 0);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

		public static bool IsPrcoessActivated(Process process)
		{
			IntPtr activatedHandle = GetForegroundWindow();
			if (activatedHandle == IntPtr.Zero) return false;
			_ = GetWindowThreadProcessId(activatedHandle, out int activeProcId);
			return activeProcId == process.Id;
		}
	}
}
