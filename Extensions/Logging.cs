using System.Runtime.CompilerServices;

namespace CounterConnect.Extensions
{
	internal static class Logging
	{
		// Was introduced in .Net 9.0
#if NET9_0_OR_GREATER
		private static readonly Lock LogLock = new();
#else
		private static readonly object LogLock = new();
#endif

		internal static void Log(string message, [CallerMemberName] string caller = "", bool outputConsole = false)
		{
			lock (LogLock)
			{
				string data = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{caller}] {message}";
				Debug.WriteLine(data);
				if (outputConsole) Console.WriteLine(data);
			}
		}
	}
}
