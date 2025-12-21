using System.Diagnostics;
using System.Text;
using System.Text.Json;
using StrikeLink.GSI;

object lockObj = new();
ServerListener listen = new(port: 5000);
HttpClient client = new();

listen.OnReady += () =>
{
	ProcessStartInfo processStartInfo = new ProcessStartInfo
	{
		FileName = "steam://rungameid/730",
		UseShellExecute = true
	};

	Process.Start(processStartInfo);
};

listen.OnPostReceived += data =>
{
	lock (lockObj)
	{
		try
		{

			File.AppendAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\log.json", data);
		}
		catch { /**/ }
	}
};

listen.PlayerStateReceived += data =>
{
	Debug.WriteLine($"[PlayerState] {JsonSerializer.Serialize(data)}");
};


listen.MapStateReceived += data =>
{
	Debug.WriteLine($"[MapState] {JsonSerializer.Serialize(data)}");
};

await listen.StartAsync();

while (true) Console.ReadLine();