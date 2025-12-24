// ReSharper disable ArrangeNamespaceBody
namespace StrikeLink.ChatBot;

public enum ChatChannel
{
	Team,
	Global
}

public record NewChatMessage(ChatChannel Channel, string Message);
public record ChatMessage(string Username, string Message, bool Dead);

public record Config(Win32.VirtualKey Keybind, bool IgnoreLocalUser = false, Action<ChatMessage>? OnGlobalChat = null, Action<ChatMessage>? OnTeamChat = null);