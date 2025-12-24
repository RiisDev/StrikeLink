using StrikeLink.ChatBot;
using StrikeLink.Extensions;
using StrikeLink.GSI;
using StrikeLink.Services;
using StrikeLink.Services.Config;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

bool sent = false;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

ChatService? chatService = null;

Action<ChatMessage> onChatMessage = async message =>
{
	Console.WriteLine($"[ChatMessage] [{message.Username}] [{message.Message}]");
	await chatService?.SendChatAsync(new NewChatMessage
	(
		ChatChannel.Global,
		$"Hello, {message.Username}! You said: {message.Message}"
	));
};

chatService = new ChatService(new Config(Win32.VirtualKey.L, false, onChatMessage, onChatMessage));


while (true) Console.ReadLine();

return;

object lockObj = new();
ServerListener listen = new();
ConsoleService service = new();
service.OnLogReceived += Console.WriteLine;
service.StartListening();

listen.OnReady += _ =>
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

//await listen.StartAsync();

while (true) Console.ReadLine();