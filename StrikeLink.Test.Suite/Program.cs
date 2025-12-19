using System.Diagnostics;
using System.Text;
using System.Text.Json;
using StrikeLink.GSI;

ServerListener listen = new(port: 5000);
HttpClient client = new();

listen.OnReady += async () =>
{
	using StringContent content = new(
		"{\"player\": {\"health\": 100, \"armor\": 50}}",
		Encoding.UTF8,
		"application/json"
	);
	await client.PostAsync($"http://127.0.0.1:{listen.Port}/", content);
};

listen.PlayerStateReceived += data =>
{
	Debug.WriteLine($"[PlayerState] {JsonSerializer.Serialize(data)}");
};


listen.MapStateReceived += data =>
{
	Debug.WriteLine($"[MapState] {JsonSerializer.Serialize(data)}");
};

listen.Start();

while (true) Console.ReadLine();