// ReSharper disable ArrangeNamespaceBody
namespace StrikeLink.ChatBot;

/// <summary>
/// Represents the chat channel a message is sent to.
/// </summary>
public enum ChatChannel
{
	/// <summary>
	/// Sends the message to the team chat channel.
	/// </summary>
	Team,

	/// <summary>
	/// Sends the message to the global chat channel.
	/// </summary>
	Global
}

/// <summary>
/// Represents a request to send a new chat message.
/// </summary>
/// <param name="Channel">
/// The target chat channel.
/// </param>
/// <param name="Message">
/// The message content to be sent.
/// </param>
public record NewChatMessage(ChatChannel Channel, string Message);

/// <summary>
/// Represents a chat message received from the chat system.
/// </summary>
/// <param name="Username">
/// The name of the user who sent the message.
/// </param>
/// <param name="Message">
/// The message content.
/// </param>
/// <param name="Dead">
/// Indicates whether the sender was dead at the time the message was sent.
/// </param>
public record ChatMessage(string Username, string Message, bool Dead);

/// <summary>
/// Provides configuration options for the <see cref="ChatService"/>.
/// </summary>
/// <param name="Keybind">
/// The virtual key used to activate chat input.
/// </param>
/// <param name="IgnoreLocalUser">
/// Indicates whether messages sent by the local user should be ignored.
/// </param>
/// <param name="OnGlobalChat">
/// Optional callback invoked when a global chat message is received.
/// </param>
/// <param name="OnTeamChat">
/// Optional callback invoked when a team chat message is received.
/// </param>
public record Config(Win32.VirtualKey Keybind, bool IgnoreLocalUser = false, Action<ChatMessage>? OnGlobalChat = null, Action<ChatMessage>? OnTeamChat = null);