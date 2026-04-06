using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedPositionalProperty.Local
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
#pragma warning disable CA5394
#pragma warning disable CA1031
#pragma warning disable CA1308

// Thank god for claude again, I couldn't figure out parsing, but now I can get this ingrained to see how it's done.

namespace StrikeLink.DemoParser.Parsing
{
	/// <summary>
	/// Provides functionality to parse and extract structured data from Counter-Strike 2 (CS2) demo files, producing
	/// match, player, and round statistics from the legacy Source 2 demo format.
	/// </summary>
	/// <remarks>The parser supports only CS2 Source 2 demo files with the ".dem" extension. It processes the demo
	/// file using legacy event and string table data, without relying on packet-entity or send-table decoding. Some
	/// advanced statistics are not available due to these
	/// limitations. The parser is designed for single-use per file and is not thread-safe. Dispose the instance after use
	/// to release file resources.</remarks>
	public sealed class Cs2DemoParser : IDisposable
	{
		/// <summary>
		/// Releases all resources used by the <see cref="Cs2DemoParser"/>.
		/// </summary>
		public void Dispose() => _fileStream?.Dispose();

		private const uint CompressedFlag = 64;

		private readonly FileStream? _fileStream;
		private readonly ParseState _state;

		/// <summary>
		/// Provides a mapping of event names to their associated data collections.
		/// </summary>
		/// <remarks>Each key in the outer dictionary represents an event name. The corresponding value is a
		/// dictionary that maps string keys to dynamic values, allowing storage of arbitrary event-related data. This
		/// collection is static and read-only; its contents should be initialized at application startup and not modified at
		/// runtime.</remarks>
		public static readonly Dictionary<string, Dictionary<string, dynamic>> EventData = [];

		/// <summary>
		/// Initializes a new instance of the Cs2DemoParser class for reading and parsing a demo file using the specified
		/// authorization data.
		/// </summary>
		/// <param name="demoFile">The demo file to be parsed. The file must have a ".dem" extension and must not be null.</param>
		/// <param name="authData">The authorization data used to access or decrypt the demo file. May be null if no authorization is required.</param>
		/// <exception cref="FormatException">Thrown if demoFile does not have a ".dem" file extension.</exception>
		public Cs2DemoParser(FileInfo demoFile, DemoAuthorization authData)
		{
			ArgumentNullException.ThrowIfNull(demoFile);
			if (!demoFile.Name.EndsWith(".dem", StringComparison.InvariantCultureIgnoreCase))
				throw new FormatException($"Invalid demo file format: {Path.GetExtension(demoFile.Name)}");

			FileInfo fileInfo = demoFile;
			_fileStream = File.OpenRead(demoFile.FullName);
			_state = new ParseState(authData, fileInfo);
		}

		/// <summary>
		/// Parses the current CS2 demo file stream and returns the parsed result.
		/// </summary>
		/// <remarks>The method reads and processes the demo file from the provided stream, extracting commands and
		/// payloads as defined by the CS2 demo format. The stream must be positioned at the start of a valid demo file. The
		/// method finalizes any open rounds before returning the result.</remarks>
		/// <returns>A <see cref="Cs2DemoParseResult"/> containing the parsed data from the demo file.</returns>
		/// <exception cref="InvalidDataException">Thrown if the demo file format is not supported or does not match the expected CS2 Source 2 demo format.</exception>
		public Cs2DemoParseResult ParseDemo()
		{
			ArgumentNullException.ThrowIfNull(_fileStream);
			DemoStreamReader reader = new(_fileStream);

			string fileStamp = reader.ReadAscii(8);
			if (!fileStamp.StartsWith("PBDEMS2", StringComparison.Ordinal))
			{
				throw new InvalidDataException($"Unsupported demo format '{fileStamp}'. This parser only supports CS2 Source 2 demos.");
			}

			reader.Skip(8);

			while (reader.Position < reader.Length && reader.TryReadVarUInt32(out uint rawCommand))
			{
				DemoCommand command = (DemoCommand)(rawCommand & ~CompressedFlag);
				bool isCompressed = (rawCommand & CompressedFlag) != 0;

				if (!reader.TryReadVarUInt32(out uint tick)) break;

				_state.ObserveFrameTick(tick == uint.MaxValue ? 0 : unchecked((int)tick));

				if (command == DemoCommand.Stop) break;
				if (!reader.TryReadVarUInt32(out uint size)) break;

				byte[] payload = reader.ReadBytesExact(checked((int)size));

				if (isCompressed) payload = SnappyBlockDecoder.Decode(payload);

				ProcessDemoCommand(_state, command, payload);
			}

			_state.FinalizeOpenRound();
			return _state.BuildResult();
		}

		private static void ProcessDemoCommand(ParseState state, DemoCommand command, byte[] payload)
		{
			try
			{
				switch (command)
				{
					case DemoCommand.FileHeader:
						ApplyFileHeader(state, ProtoMessage.Parse(payload));
						break;

					case DemoCommand.FileInfo:
						ApplyFileInfo(state, ProtoMessage.Parse(payload));
						break;

					case DemoCommand.StringTables:
						ApplyStringTables(state, ProtoMessage.Parse(payload));
						break;

					case DemoCommand.Packet:
					case DemoCommand.SignonPacket:
						ProcessPacketMessage(state, ExtractPacketData(payload));
						break;

					case DemoCommand.FullPacket:
						ProcessPacketMessage(state, ExtractEmbeddedFullPacketData(payload));
						break;
					case DemoCommand.Error:
					case DemoCommand.Stop:
					case DemoCommand.SyncTick:
					case DemoCommand.SendTables:
					case DemoCommand.ClassInfo:
					case DemoCommand.ConsoleCmd:
					case DemoCommand.CustomData:
					case DemoCommand.CustomDataCallbacks:
					case DemoCommand.UserCmd:
					case DemoCommand.SaveGame:
					case DemoCommand.SpawnGroups:
					case DemoCommand.AnimationData:
					case DemoCommand.AnimationHeader:
					case DemoCommand.Recovery:
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(command), command, null);
				}
			}
			catch (Exception ex)
			{
				state.AddWarning($"Failed to process {command} at tick {state.CurrentFrameTick}: {ex.Message}");
			}
		}

		private static byte[]? ExtractPacketData(byte[] payload)
		{
			ProtoMessage message = ProtoMessage.Parse(payload);
			return message.TryGetBytes(3, out byte[]? data) ? data : null;
		}

		private static byte[]? ExtractEmbeddedFullPacketData(byte[] payload)
		{
			ProtoMessage message = ProtoMessage.Parse(payload);
			return !message.TryGetBytes(2, out byte[]? packetPayload) || packetPayload is null ? null : ExtractPacketData(packetPayload);
		}

		private static void ApplyFileHeader(ParseState state, ProtoMessage message)
		{
			if (message.TryGetString(3, out string? serverName))
			{
				if (string.IsNullOrWhiteSpace(serverName)) return;

				state.ServerName = serverName;
				_ = TryParseServerEndpoint(serverName, out string? address, out int? port);
				state.ServerAddress ??= address;
				state.ServerPort ??= port;
				string[] serverLocation = serverName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

				state.ServerLocation = serverLocation.FirstOrDefault(x => x.Contains('_', StringComparison.Ordinal)) ?? "Unknown";
			}

			if (message.TryGetString(4, out string? clientName))
			{
				state.ClientName = clientName;
			}

			if (message.TryGetString(5, out string? mapName))
			{
				state.MapName = mapName;
			}

			if (message.TryGetInt32(2, out int patchVersion))
			{
				state.NetworkProtocol = patchVersion;
			}
		}

		private static void ApplyFileInfo(ParseState state, ProtoMessage message)
		{
			if (message.TryGetFloat(1, out float playbackTime))
			{
				state.PlaybackTime = TimeSpan.FromSeconds(playbackTime);
			}

			if (message.TryGetInt32(2, out int playbackTicks))
			{
				state.PlaybackTicks = playbackTicks;
			}
		}

		private static void ApplyStringTables(ParseState state, ProtoMessage message)
		{
			foreach (ProtoFieldValue tableValue in message.GetValues(1))
			{
				ProtoMessage table = tableValue.GetMessage();
				if (!table.TryGetString(1, out string? tableName) || !string.Equals(tableName, "userinfo", StringComparison.Ordinal))
				{
					continue;
				}

				foreach (ProtoFieldValue itemValue in table.GetValues(2))
				{
					ProtoMessage item = itemValue.GetMessage();
					if (!item.TryGetString(1, out string? itemKey) || !int.TryParse(itemKey, out int playerIndex))
					{
						continue;
					}

					if (!item.TryGetBytes(2, out byte[]? playerInfoPayload) || playerInfoPayload is null || playerInfoPayload.Length == 0)
					{
						continue;
					}

					ApplyPlayerInfo(state, playerIndex, playerInfoPayload);
				}
			}
		}

		private static void ApplyPlayerInfo(ParseState state, int playerIndex, byte[] payload)
		{
			ProtoMessage message = ProtoMessage.Parse(payload);
			bool hasName = message.TryGetString(1, out string? name);
			bool hasXuid = message.TryGetUInt64(2, out ulong xuid);
			bool hasLegacyUserId = message.TryGetInt32(3, out int legacyUserId) && legacyUserId > 0;
			bool hasSteamId = message.TryGetUInt64(4, out ulong steamId);
			bool isBot = message.TryGetInt32(5, out int fakePlayerFlag) ? fakePlayerFlag != 0 : message.TryGetUInt64(5, out ulong fakePlayerUInt64) && fakePlayerUInt64 != 0;
			bool isHltv = message.TryGetInt32(6, out int hltvFlag) ? hltvFlag != 0 : message.TryGetUInt64(6, out ulong hltvUInt64) && hltvUInt64 != 0;

			ulong resolvedSteamId = hasXuid && xuid != 0 ? xuid : hasSteamId ? steamId : 0;

			if ((!hasName || string.IsNullOrWhiteSpace(name)) && resolvedSteamId == 0) return;
			if (isHltv) return;

			state.UpsertPlayerIdentity(playerIndex, name, resolvedSteamId, isBot, hasLegacyUserId ? legacyUserId : 0, mapUserIdAsEntitySlot: true);
		}

		private static void ProcessPacketMessage(ParseState state, byte[]? packetData)
		{
			if (packetData is null || packetData.Length == 0) return;

			PacketBitReader reader = new(packetData);
			while (reader.HasAtLeastBits(8))
			{
				try
				{
					int messageType = unchecked((int)reader.ReadUBitInt());
					int messageSize = checked((int)reader.ReadVarUInt32());
					if (messageSize < 0 || !reader.HasAtLeastBits(messageSize * 8))
					{
						state.AddWarning($"Skipping truncated packet message {messageType} at tick {state.CurrentFrameTick}.");
						break;
					}

					byte[] payload = reader.ReadBytes(messageSize);
					ProcessNetMessage(state, messageType, payload);
				}
				catch (Exception ex)
				{
					state.AddWarning($"Failed to process packet payload at tick {state.CurrentFrameTick}: {ex.Message}");
					break;
				}
			}
		}

		private static void ProcessNetMessage(ParseState state, int messageType, byte[] payload)
		{
			state.ObserveNetMessage(messageType);
			
			switch (messageType)
			{
				case MessageTypeIds.SvcServerInfo:
					ApplyServerInfo(state, ProtoMessage.Parse(payload));
					break;

				case MessageTypeIds.SvcUserMessage:
					ApplyUserMessage(state, ProtoMessage.Parse(payload));
					break;

				case MessageTypeIds.CsUmServerRankUpdate:
					ApplyServerRankUpdate(state, ProtoMessage.Parse(payload));
					break;

				case MessageTypeIds.GeSource1LegacyGameEventList:
					ApplyGameEventList(state, ProtoMessage.Parse(payload));
					break;

				case MessageTypeIds.GeSource1LegacyGameEvent:
					ApplyGameEvent(state, ProtoMessage.Parse(payload));
					break;
			}
		}

		private static void ApplyServerInfo(ParseState state, ProtoMessage message)
		{
			if (message.TryGetFloat(13, out float tickInterval) && tickInterval > 0)
			{
				state.TickIntervalSeconds = tickInterval;
			}

			if (message.TryGetString(15, out string? mapName) && !string.IsNullOrWhiteSpace(mapName))
			{
				state.MapName = mapName;
			}

			if (message.TryGetString(17, out string? hostName) && !string.IsNullOrWhiteSpace(hostName))
			{
				state.ServerName = hostName;
				_ = TryParseServerEndpoint(hostName, out string? address, out int? port);
				state.ServerAddress ??= address;
				state.ServerPort ??= port;
			}

			if (message.TryGetInt32(11, out int maxPlayers) && maxPlayers is > 0 and <= 128)
			{
				state.MaxPlayers = maxPlayers;
			}

			if (state.MaxPlayers is null)
			{
				foreach (int candidateField in new[] { 10, 12 })
				{
					if (message.TryGetInt32(candidateField, out int candidateMaxPlayers) && candidateMaxPlayers is > 0 and <= 128)
					{
						state.MaxPlayers = candidateMaxPlayers;
						break;
					}
				}
			}

			if (message.TryGetBytes(19, out byte[]? serverBlob) && serverBlob is { Length: > 0 })
			{
				ApplyServerMetadataBlob(state, serverBlob);
			}

			if (message.TryGetString(19, out string? serverBlobText) && !string.IsNullOrWhiteSpace(serverBlobText))
			{
				ApplyServerMetadataText(state, serverBlobText);
			}
		}

		private static void ApplyServerMetadataBlob(ParseState state, byte[] blob)
		{
			string utf8 = Encoding.UTF8.GetString(blob);
			string latin1 = Encoding.Latin1.GetString(blob);
			ApplyServerMetadataText(state, utf8.Length >= latin1.Length ? utf8 : latin1);
		}

		private static void ApplyServerMetadataText(ParseState state, string rawText)
		{
			string text = new (rawText.Select(ch => char.IsControl(ch) && ch != '\n' && ch != '\r' && ch != '\t' ? ' ' : ch).ToArray());

			Match endpointMatch = Regex.Match(text, @"\b(?<ip>(?:\d{1,3}\.){3}\d{1,3}):(?<port>\d{1,5})\b");
			if (endpointMatch.Success && IPAddress.TryParse(endpointMatch.Groups["ip"].Value, out _))
			{
				state.ServerAddress ??= endpointMatch.Groups["ip"].Value;
				if (int.TryParse(endpointMatch.Groups["port"].Value, out int parsedPort) && parsedPort is >= 0 and <= 65535)
				{
					state.ServerPort ??= parsedPort == 0 ? null : parsedPort;
				}
			}

			Match slotsMatch = Regex.Match(text, @"numSlots\D{0,16}(?<slots>\d{1,3})", RegexOptions.IgnoreCase);
			if (!slotsMatch.Success)
			{
				slotsMatch = Regex.Match(text, @"(?:maxplayers|sv_visiblemaxplayers)\D{0,16}(?<slots>\d{1,3})", RegexOptions.IgnoreCase);
			}
			if (slotsMatch.Success && int.TryParse(slotsMatch.Groups["slots"].Value, out int parsedSlots) && parsedSlots is > 0 and <= 128)
			{
				state.MaxPlayers ??= parsedSlots;
			}

			string? gameType = TryExtractTaggedNumeric(text, "c_game_type") ?? TryExtractTaggedNumeric(text, "default_game_type");
			string? gameMode = TryExtractTaggedNumeric(text, "c_game_mode") ?? TryExtractTaggedNumeric(text, "default_game_mode");
			if (!string.IsNullOrWhiteSpace(gameType) || !string.IsNullOrWhiteSpace(gameMode))
			{
				state.GameType = string.IsNullOrWhiteSpace(gameMode) ? gameType : $"{gameType}/{gameMode}";
			}

			if (string.IsNullOrWhiteSpace(state.GameType))
			{
				Match nameBasedGameType = Regex.Match(text, @"\b(competitive|premier|casual|wingman|deathmatch|armsrace|retakes)\b", RegexOptions.IgnoreCase);
				if (nameBasedGameType.Success)
				{
					state.GameType = nameBasedGameType.Groups[1].Value.ToLowerInvariant();
				}
			}

			if (string.IsNullOrWhiteSpace(state.MapName))
			{
				Match mapMatch = Regex.Match(text, @"\b(de|cs|ar|fy)_[a-z0-9_]+\b", RegexOptions.IgnoreCase);
				if (mapMatch.Success)
				{
					state.MapName = mapMatch.Value;
				}
			}
		}

		private static string? TryExtractTaggedNumeric(string text, string tag)
		{
			Match match = Regex.Match(text, $@"{Regex.Escape(tag)}\D{{0,16}}(?<value>\d{{1,3}})", RegexOptions.IgnoreCase);
			return match.Success ? match.Groups["value"].Value : null;
		}

		private static bool TryParseServerEndpoint(string input, out string? address, out int? port)
		{
			address = null;
			port = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return false;
			}

			string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			foreach (string token in tokens)
			{
				string candidate = token.Trim('[', ']', '(', ')', ';', ',');
				int colonIndex = candidate.LastIndexOf(':');
				if (colonIndex <= 0 || colonIndex >= candidate.Length - 1)
				{
					continue;
				}

				string hostPart = candidate[..colonIndex];
				string portPart = candidate[(colonIndex + 1)..];
				if (!int.TryParse(portPart, out int parsedPort) || parsedPort is <= 0 or > 65535)
				{
					continue;
				}

				if (IPAddress.TryParse(hostPart, out _))
				{
					address = hostPart;
					port = parsedPort;
					return true;
				}
			}

			return false;
		}

		private static void ApplyUserMessage(ParseState state, ProtoMessage envelope)
		{
			if (!envelope.TryGetInt32(1, out int userMessageType) || !envelope.TryGetBytes(2, out byte[]? payload) || payload is null)
				return;

			ProtoMessage decodedPayload = ProtoMessage.Parse(payload);
			switch (userMessageType)
			{
				case MessageTypeIds.CsUmServerRankUpdate:
					ApplyServerRankUpdate(state, decodedPayload);
					break;

				case MessageTypeIds.CsUmXRankUpd:
					state.AddWarning("Received XRank update payload. The message does not contain a player identifier, so it cannot be mapped safely without additional client-state decoding.");
					break;
			}
		}

		private static void ApplyServerRankUpdate(ParseState state, ProtoMessage rankMessage)
		{
			foreach (ProtoFieldValue updateField in rankMessage.GetValues(1))
			{
				ProtoMessage update = updateField.GetMessage();
				if (!update.TryGetInt32(1, out int accountId))
				{
					continue;
				}

				PlayerAccumulator? player = state.FindByAccountId((uint)accountId);
				if (player is null)
				{
					continue;
				}

				if (update.TryGetInt32(2, out int oldRank))
				{
					player.RankOld = oldRank;
				}

				if (update.TryGetInt32(3, out int newRank))
				{
					player.Rank = newRank;
				}

				if (update.TryGetInt32(4, out int wins))
				{
					player.RankWins = wins;
				}

				if (update.TryGetInt32(6, out int rankType))
				{
					player.RankTypeId = rankType;
				}

				if (update.TryGetFloat(5, out float rankChange))
				{
					player.RankChange = rankChange;
				}
			}
		}

		private static void ApplyGameEventList(ParseState state, ProtoMessage message)
		{
			foreach (ProtoFieldValue descriptorValue in message.GetValues(1))
			{
				ProtoMessage descriptorMessage = descriptorValue.GetMessage();
				if (!descriptorMessage.TryGetInt32(1, out int eventId) || !descriptorMessage.TryGetString(2, out string? eventName))
				{
					continue;
				}

				List<EventKeyDescriptor> keys = [];
				foreach (ProtoFieldValue keyValue in descriptorMessage.GetValues(3))
				{
					ProtoMessage keyMessage = keyValue.GetMessage();
					if (!keyMessage.TryGetInt32(1, out int type) || !keyMessage.TryGetString(2, out string? keyName) || string.IsNullOrWhiteSpace(keyName))
					{
						continue;
					}

					keys.Add(new EventKeyDescriptor(keyName, type));
				}

				EventData.TryAdd($"{eventId}_{eventName}", new Dictionary<string, dynamic> { { "", keys } });
				state.EventDescriptors[eventId] = new EventDescriptor(eventId, eventName!, keys);
			}
		}


		private static void ApplyGameEvent(ParseState state, ProtoMessage message)
		{
			state.GameEventMessagesSeen++;

			if (!message.TryGetInt32(2, out int eventId))
			{
				message.TryGetString(2, out string? messageString);
				state.AddWarning($"Failed to parse int32 out of message: {messageString} | {JsonSerializer.Serialize(message)}");
				return;
			}

			if (!state.EventDescriptors.TryGetValue(eventId, out EventDescriptor? descriptor))
			{
				state.AddWarning($"Received legacy game event {eventId} before its descriptor list.");
				return;
			}

			Dictionary<string, GameEventValue> data = new(StringComparer.OrdinalIgnoreCase);
			int index = 0;
			foreach (ProtoFieldValue keyValue in message.GetValues(3))
			{
				if (index >= descriptor.Keys.Count) break;

				data[descriptor.Keys[index].Name] = DecodeGameEventValue(descriptor.Keys[index].Type, keyValue.GetMessage());
				index++;
			}

			int eventTick = message.TryGetInt32(4, out int serverTick) ? serverTick : state.CurrentFrameTick;
			EventData.Add($"{eventTick}_{eventId}_{Random.Shared.Next(1, int.MaxValue)}", new Dictionary<string, dynamic> { { descriptor.Name, data } });

			state.ObserveEventName(descriptor.Name);
			state.HandleGameEvent(descriptor.Name, eventTick, data);
		}

		private static GameEventValue DecodeGameEventValue(int type, ProtoMessage message)
		{
			object? value = type switch
			{
				1 when message.TryGetString(2, out string? text) => text,
				2 when message.TryGetFloat(3, out float single) => single,
				3 when message.TryGetInt32(4, out int longValue) => longValue,
				4 when message.TryGetInt32(5, out int shortValue) => shortValue,
				5 when message.TryGetInt32(6, out int byteValue) => byteValue,
				6 when message.TryGetInt32(7, out int boolAsInt) => boolAsInt != 0,
				6 when message.TryGetUInt64(7, out ulong boolAsUInt) => boolAsUInt != 0,
				7 when message.TryGetUInt64(8, out ulong u64) => u64,
				// CS2 extended types: 8 = pawn entity handle, 9 = controller/player entity handle
				// The handle value is stored in val_long (proto field 4, int32), same as type 3.
				// Entity slot index = (uint)handle & 0x3FFF (lower 14 bits).
				8 when message.TryGetInt32(4, out int pawnHandle) => (uint)pawnHandle,
				// Type 9: try val_short (field 5) first, then val_long (field 4), then val_uint64 (field 8)
				9 when message.TryGetInt32(5, out int ctrlHandleShort) => (uint)ctrlHandleShort,
				9 when message.TryGetInt32(4, out int ctrlHandleLong) => (uint)ctrlHandleLong,
				9 when message.TryGetUInt64(8, out ulong ctrlHandleU64) => (uint)(ctrlHandleU64 & 0xFFFFFFFF),
				_ => null,
			};

			if (value is null && type == 6 && message.TryGetUInt64(7, out ulong fallbackBool))
			{
				value = fallbackBool != 0;
			}

			return new GameEventValue(type, value);
		}

		private sealed class ParseState(DemoAuthorization config, FileInfo demoPath)
		{
			private readonly HashSet<string> _warningSet = new(StringComparer.Ordinal);

			public Dictionary<int, EventDescriptor> EventDescriptors { get; } = [];

			private Dictionary<int, int> NetMessageCounts { get; } = [];

			private Dictionary<string, int> EventNameCounts { get; } = new(StringComparer.OrdinalIgnoreCase);

			private List<string> TrackedEventSamples { get; } = [];

			private int RoundMvpEventsSeen { get; set; }

			private int RoundMvpResolved { get; set; }

			public int GameEventMessagesSeen { get; set; }

			private Dictionary<int, PlayerAccumulator> PlayersByUserId { get; } = [];

			private Dictionary<int, PlayerAccumulator> PlayersByEntitySlot { get; } = [];

			private Dictionary<int, PlayerAccumulator> PlayersByLegacyUserId { get; } = [];

			private Dictionary<ulong, PlayerAccumulator> PlayersBySteamId { get; } = [];

			private Dictionary<uint, PlayerAccumulator> PlayersByAccountId { get; } = [];

			private List<RoundAccumulator> Rounds { get; } = [];

			private List<string> Warnings { get; } = [];

			private RoundAccumulator? CurrentRound { get; set; }

			public string? ServerName { get; set; }

			public string? ServerAddress { get; set; }

			public int? ServerPort { get; set; }

			public string? GameType { get; set; }

			public int? MaxPlayers { get; set; }

			public string? ClientName { get; set; }

			public string? MapName { get; set; }
			
			public string? ServerLocation { get; set; }

			public int? NetworkProtocol { get; set; }

			public TimeSpan PlaybackTime { get; set; }

			public int PlaybackTicks { get; set; }

			public float TickIntervalSeconds { get; set; }

			/// <summary>Round number at which the first halftime team swap occurred (0 = not yet detected).</summary>
			private int HalfTimeRoundNumber { get; set; }

			public int CurrentFrameTick { get; set; }

			public int MaxObservedTick { get; private set; }

			public void ObserveFrameTick(int tick)
			{
				CurrentFrameTick = tick;
				if (tick > MaxObservedTick)
				{
					MaxObservedTick = tick;
				}
			}

			private int TerroristScore { get; set; }

			private int CounterTerroristScore { get; set; }

			public void AddWarning(string warning)
			{
				if (_warningSet.Add(warning))
				{
					Warnings.Add(warning);
				}
			}

			public PlayerAccumulator? FindByAccountId(uint accountId)
				=> PlayersByAccountId.GetValueOrDefault(accountId);

			public void ObserveNetMessage(int messageType)
				=> NetMessageCounts[messageType] = NetMessageCounts.GetValueOrDefault(messageType) + 1;

			public void ObserveEventName(string eventName)
				=> EventNameCounts[eventName] = EventNameCounts.GetValueOrDefault(eventName) + 1;

			private void ObserveTrackedEventSample(string eventName, IReadOnlyDictionary<string, GameEventValue> data)
			{
				if (TrackedEventSamples.Count >= 18)
				{
					return;
				}

				if (eventName is not ("player_death" or "player_hurt" or "bullet_damage" or "weapon_fire" or "player_spawn" or "player_team" or "round_mvp" or "player_disconnect" or "round_end" or "round_start" or "round_officially_ended"))
				{
					return;
				}

				string formatted = string.Join(", ", data.OrderBy(static entry => entry.Key, StringComparer.OrdinalIgnoreCase)
					.Select(static entry => $"{entry.Key}={FormatGameEventValue(entry.Value)}"));
				TrackedEventSamples.Add($"{eventName}: {formatted}");
			}

			public void UpsertPlayerIdentity(int userId, string? name, ulong steamId, bool isBot, int legacyUserId = 0, bool mapUserIdAsEntitySlot = true)
			{
				PlayerAccumulator player;
				if (steamId != 0 && PlayersBySteamId.TryGetValue(steamId, out PlayerAccumulator? existingBySteam))
				{
					player = existingBySteam;
				}
				else if (legacyUserId != 0 && PlayersByLegacyUserId.TryGetValue(legacyUserId, out PlayerAccumulator? existingByLegacy))
				{
					player = existingByLegacy;
				}
				else
				{
					player = ResolvePlayer(userId, allowZeroUserId: true)!;
				}

				if (!string.IsNullOrWhiteSpace(name))
				{
					player.Name = name;
				}

				player.IsBot = player.IsBot || isBot;
				player.IsConnected = true;

				if (mapUserIdAsEntitySlot)
				{
					PlayersByEntitySlot[userId] = player;
				}

				if (legacyUserId != 0)
				{
					PlayersByLegacyUserId[legacyUserId] = player;
				}

				if (steamId == 0)
				{
					return;
				}

				player.SteamId = steamId;
				PlayersBySteamId[steamId] = player;
				player.AccountId = (uint)(steamId & 0xFFFFFFFF);
				if (player.AccountId != 0)
				{
					PlayersByAccountId[player.AccountId] = player;
				}
			}

			public void FinalizeOpenRound()
			{
				if (CurrentRound is null)
				{
					return;
				}

				CompleteRound(CurrentRound, winner: null, CurrentFrameTick);
			}

			public void HandleGameEvent(string name, int tick, IReadOnlyDictionary<string, GameEventValue> data)
			{
				ObserveTrackedEventSample(name, data);

				switch (name)
				{
					case "player_connect":
						HandlePlayerConnect(data);
						break;

					case "player_spawn":
						HandlePlayerSpawn(data);
						break;

					case "player_team":
						HandlePlayerTeam(data);
						break;

					case "player_disconnect":
						HandlePlayerDisconnect(data);
						break;

					case "begin_new_match":
						break;

					case "server_cvar":
						HandleServerCvar(data);
						break;

					case "round_start":
						StartRound(tick);
						break;

					case "cs_round_start_beep":
					case "round_freeze_end":
						if (ShouldStartRoundFromSignal(tick))
						{
							StartRound(tick);
						}
						break;

					case "round_end":
						CompleteRound(CurrentRound, GetTeamValue(data, "winner"), tick);
						break;

					case "round_officially_ended":
						CompleteRound(CurrentRound, InferRoundWinner(CurrentRound), tick);
						break;

					case "player_death":
						if (EnsureTrackedRound())
						{
							HandlePlayerDeath(tick, data);
						}
						break;

					case "player_hurt":
						if (EnsureTrackedRound())
						{
							HandlePlayerHurt(tick, data);
						}
						break;

					case "weapon_fire":
						if (EnsureTrackedRound())
						{
							HandleWeaponFire(tick, data);
						}
						break;

					case "player_blind":
						if (EnsureTrackedRound())
						{
							HandlePlayerBlind(data);
						}
						break;

					case "flashbang_detonate":
						IncrementUtilityCount(data, WeaponKind.Flashbang);
						break;

					case "hegrenade_detonate":
						IncrementUtilityCount(data, WeaponKind.HeGrenade);
						break;

					case "inferno_startburn":
						IncrementUtilityCount(data, WeaponKind.Molotov);
						break;

					case "round_mvp":
						HandleRoundMvp(data);
						break;

					case "bomb_exploded":
						HandleBombExploded();
						break;

					case "bomb_planted":
						if (EnsureTrackedRound())
						{
							PlayerAccumulator? player = ResolvePlayerFromField(data, "userid") ?? ResolvePlayerFromField(data, "userid_pawn");
							if (player is not null)
							{
								player.BombPlants++;
								CurrentRound!.PlanterUserId = player.UserId;
							}
						}
						break;

					case "bomb_defused":
						if (EnsureTrackedRound())
						{
							PlayerAccumulator? player = ResolvePlayerFromField(data, "userid") ?? ResolvePlayerFromField(data, "userid_pawn");
							if (player is not null)
							{
								player.BombDefuses++;
								CurrentRound!.DefuserUserId = player.UserId;
							}
						}
						break;

					case "item_purchase":
						break;
				}
			}

			public Cs2DemoParseResult BuildResult()
			{
				if (TickIntervalSeconds <= 0)
				{
					AddWarning("Tick interval was not available from server info. Round duration, trade timing, and spray segmentation may be less accurate.");
				}

				if (Rounds.Count > 0 && PlayersByUserId.Count > 0 && PlayersByUserId.Values.All(static player => player is { Kills: 0, DamageDealt: 0, Shots: 0 }))
				{
					AddWarning($"Tracked event samples: {string.Join(" | ", TrackedEventSamples)}");
				}
				
				// Retroactively infer winners for rounds completed before team assignments were available.
				// All player team data is now known, so InferRoundWinner will have correct team info.
				foreach (RoundAccumulator round in Rounds.Where(r => r.Winner is null))
				{
					round.Winner = InferRoundWinner(round);
					switch (round.Winner)
					{
						case CsTeamSide.Unknown:
						case CsTeamSide.Spectator:
							break;
						case null:
							continue;
						case CsTeamSide.Terrorists:
							TerroristScore++;
							break;
						case CsTeamSide.CounterTerrorists:
							CounterTerroristScore++;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}

					// Update per-player RoundsWon/RoundsLost accounting for halftime side swap
					bool roundIsFirstHalf = HalfTimeRoundNumber > 0 && round.Number <= HalfTimeRoundNumber;
					foreach (PlayerAccumulator player in PlayersByUserId.Values.Where(static p => p.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists))
					{
						// In first half, teams are swapped relative to current assignment
						CsTeamSide playerSideThisRound = (roundIsFirstHalf && player.Team == CsTeamSide.Terrorists) ? CsTeamSide.CounterTerrorists
							: (roundIsFirstHalf && player.Team == CsTeamSide.CounterTerrorists) ? CsTeamSide.Terrorists
							: player.Team;
						if (playerSideThisRound == round.Winner)
						{
							player.RoundsWon++;
						}
						else
						{
							player.RoundsLost++;
						}
					}
				}

				if (PlayersByUserId.Count == 0 && Rounds.Count == 0)
				{
					string emptyTopMessageTypes = string.Join(", ", NetMessageCounts
						.OrderByDescending(static entry => entry.Value)
						.ThenBy(static entry => entry.Key)
						.Take(12)
						.Select(static entry => $"{entry.Key}x{entry.Value}"));
					string topEventNames = string.Join(", ", EventNameCounts
						.OrderByDescending(static entry => entry.Value)
						.ThenBy(static entry => entry.Key, StringComparer.OrdinalIgnoreCase)
						.Take(20)
						.Select(static entry => $"{entry.Key}x{entry.Value}"));

					AddWarning($"No players or rounds were extracted. Legacy game event messages seen: {GameEventMessagesSeen}. Top packet message ids: {emptyTopMessageTypes}. Top event names: {topEventNames}.");
				}

				if (RoundMvpEventsSeen > 0)
				{
					AddWarning($"MVP event diagnostics: round_mvp seen={RoundMvpEventsSeen}, resolved={RoundMvpResolved}.");
				}

				ApplyRoundMvpAwards();

				List<PlayerStats> players = PlayersByUserId.Values
					.Where(static player => !string.IsNullOrWhiteSpace(player.Name))
					.OrderByDescending(static player => player.Kills)
					.ThenBy(static player => player.Deaths)
					.ThenBy(static player => player.Name, StringComparer.OrdinalIgnoreCase)
					.Select(BuildPlayer)
					.ToList();

				List<RoundStats> rounds = Rounds
					.Select(BuildRound)
					.ToList();

				TimeSpan roundDuration = rounds.Aggregate(TimeSpan.Zero, (current, roundStats) => current.Add(roundStats.Duration ?? TimeSpan.Zero));
				TimeSpan playbackTickDuration = TickIntervalSeconds > 0 && PlaybackTicks > 0
					? TimeSpan.FromSeconds(PlaybackTicks * TickIntervalSeconds)
					: TimeSpan.Zero;
				TimeSpan observedTickDuration = TickIntervalSeconds > 0 && MaxObservedTick > 0
					? TimeSpan.FromSeconds(MaxObservedTick * TickIntervalSeconds)
					: TimeSpan.Zero;

				// Prefer the largest trustworthy duration estimate.
				// Round aggregation can undercount when a round starts before first tracked combat event.
				TimeSpan matchDuration = new[] { PlaybackTime, playbackTickDuration, observedTickDuration, roundDuration }
					.Where(duration => duration > TimeSpan.Zero)
					.DefaultIfEmpty(TimeSpan.Zero)
					.Max();

				DateTime fileDateUtc = File.GetCreationTimeUtc(demoPath.FullName);
				if (fileDateUtc == DateTime.MinValue)
				{
					fileDateUtc = File.GetLastWriteTimeUtc(demoPath.FullName);
				}

				PlayerStats? focusPlayer = players.FirstOrDefault(player => player.SteamId == (ulong)config.SteamId);

				MatchOutcome outcome = MatchOutcome.Unknown;
				if (focusPlayer is not null)
				{
					PlayerAccumulator internalPlayer = PlayersByUserId.Values.First(player => player.SteamId == focusPlayer.SteamId);
					if (internalPlayer.RoundsWon > internalPlayer.RoundsLost)
					{
						outcome = MatchOutcome.Victory;
					}
					else if (internalPlayer.RoundsLost > internalPlayer.RoundsWon)
					{
						outcome = MatchOutcome.Defeat;
					}
				}

				MatchStats match = new(
					Duration: matchDuration,
					TerroristScore: TerroristScore,
					CounterTerroristScore: CounterTerroristScore,
					Outcome: outcome,
					ServerLocation: ServerLocation,
					ServerAddress: ServerAddress,
					ServerPort: ServerPort,
					GameType: GameType,
					MaxPlayers: MaxPlayers,
					Date: new DateTimeOffset(fileDateUtc, TimeSpan.Zero),
					Map: MapName,
					MatchShareCode: config.MatchShareCode,
					ServerName: ServerName,
					DemoClientName: ClientName,
					NetworkProtocol: NetworkProtocol,
					FocusSteamId: (ulong)config.SteamId);
				
				
				return new Cs2DemoParseResult(match, players, rounds, new ReadOnlyCollection<string>(Warnings));
			}

			private static RoundStats BuildRound(RoundAccumulator round) =>
				new(
					round.Number,
					round.Duration,
					round.Winner,
					new ReadOnlyCollection<RoundKillEvent>(round.Kills),
					new ReadOnlyCollection<RoundDamageEvent>(round.Damage));

			private PlayerStats BuildPlayer(PlayerAccumulator player)
			{
				int roundsPlayed = Math.Max(player.RoundsParticipated, 1);
				double adr = player.DamageDealt / (double)roundsPlayed;
				double hsPct = player.Kills == 0 ? 0 : player.HeadshotKills * 100d / player.Kills;
				double totalAccuracy = player.Shots == 0 ? 0 : player.Hits * 100d / player.Shots;
				double sprayAccuracy = player.SprayShots == 0 ? 0 : player.SprayHits * 100d / player.SprayShots;
				List<RoundImpactSnapshot> roundSnapshots = BuildRoundImpactSnapshots(player);
				PlayerImpactStats impact = BuildPlayerImpact(roundSnapshots);

				double aimRating = Math.Clamp(
					(hsPct * 0.30) +
					(totalAccuracy * 0.35) +
					(sprayAccuracy * 0.20) +
					(Math.Min(adr, 150) / 150d * 100d * 0.15),
					0,
					100);

				double utilityRating = Math.Clamp(
					(Math.Min(player.UtilityDamage / (double)roundsPlayed, 75) / 75d * 45d) +
					(Math.Min(player.PlayersFlashed / (double)roundsPlayed, 2.5d) / 2.5d * 25d) +
					(Math.Min(player.FragKills + player.MollyKills, 5) / 5d * 20d) +
					(Math.Min(player.TeamFlashes, 10) / 10d * -10d),
					0,
					100);

				List<WeaponStats> weaponStats = player.WeaponStats.Values
					.OrderByDescending(static weapon => weapon.Kills)
					.ThenBy(static weapon => weapon.Weapon, StringComparer.OrdinalIgnoreCase)
					.Select(static weapon => new WeaponStats(
						weapon.Weapon,
						weapon.Kills,
						weapon.Deaths,
						weapon.Assists,
						weapon.Damage,
						weapon.Shots,
						weapon.Hits,
						weapon.Shots == 0 ? 0 : weapon.Hits * 100d / weapon.Shots))
					.ToList();

				return new PlayerStats(
					SteamId: player.SteamId,
					Name: player.Name,
					UserId: player.UserId,
					IsBot: player.IsBot,
					Team: player.Team,
					RoundsWon: player.RoundsWon,
					RoundsLost: player.RoundsLost,
					RoundsParticipated: player.RoundsParticipated,
					Kills: player.Kills,
					Deaths: player.Deaths,
					Assists: player.Assists,
					UtilityDamage: player.UtilityDamage,
					MvpCount: player.MvpCount,
					Rank: new RankSnapshot(player.RankOld, player.Rank, player.RankChange, player.RankWins, player.RankTypeId, player.VisibleSkill),
					Adr: Math.Round(adr, 2),
					MultiKills: new MultiKillSummary(
						player.TwoKs.Count,
						player.ThreeKs.Count,
						player.FourKs.Count,
						player.FiveKs.Count,
						player.TwoKs.ToReadOnlyList(),
						player.ThreeKs.ToReadOnlyList(),
						player.FourKs.ToReadOnlyList(),
						player.FiveKs.ToReadOnlyList()),
					AimRating: Math.Round(aimRating, 2),
					UtilityRating: Math.Round(utilityRating, 2),
					Trading: new TradingStats(
						player.TradeKills,
						player.TradedDeaths,
						player.TradeOpportunities == 0 ? 0 : Math.Round(player.TradeKills * 100d / player.TradeOpportunities, 2)),
					Clutches: new ClutchStats(player.ClutchAttempts, player.ClutchWins, player.ClutchWinsByOpponents.ToReadOnlyDictionary()),
					HeadshotPercentage: Math.Round(hsPct, 2),
					TotalAccuracy: Math.Round(totalAccuracy, 2),
					SprayAccuracy: Math.Round(sprayAccuracy, 2),
					Utility: new UtilityStats(
						player.FragKills,
						player.FragDeaths,
						player.PlayersFlashed,
						player.TimesFlashed,
						player.MollyKills,
						player.MollyDeaths,
						player.UtilityDamage,
						player.FlashbangsThrown,
						player.HeGrenadesThrown,
						player.MolotovsThrown),
					TeamDamage: new TeamDamageStats(
						player.TeamDamage,
						player.TeamKillsUtility,
						player.TeamKillsOther,
						player.TeamFlashes),
					Weapons: new ReadOnlyCollection<WeaponStats>(weaponStats),
					Impact: impact,
					BombPlants: player.BombPlants,
					BombDefuses: player.BombDefuses
					);
			}

			private List<RoundImpactSnapshot> BuildRoundImpactSnapshots(PlayerAccumulator player)
			{
				List<RoundImpactSnapshot> snapshots = [];

				foreach (RoundAccumulator round in Rounds)
				{
					bool participated = round.Participants.Contains(player.UserId)
						|| round.Alive.ContainsKey(player.UserId)
						|| round.Kills.Any(k => k.KillerSteamId == player.SteamId || k.VictimSteamId == player.SteamId || k.AssisterSteamId == player.SteamId)
						|| round.Damage.Any(d => d.AttackerSteamId == player.SteamId || d.VictimSteamId == player.SteamId);

					if (!participated)
					{
						continue;
					}

					CsTeamSide side = ResolvePlayerRoundSide(player, round);
					int kills = round.Kills.Count(k => k.KillerSteamId == player.SteamId && !k.IsTeamKill);
					int assists = round.Kills.Count(k => k.AssisterSteamId == player.SteamId);
					int deaths = round.Kills.Count(k => k.VictimSteamId == player.SteamId && !k.IsTeamKill);
					int damage = round.Damage
						.Where(d => d.AttackerSteamId == player.SteamId && !d.IsFriendlyFire)
						.Sum(d => d.HealthDamage > 0 ? d.HealthDamage : d.Damage);

					bool won = round.Winner is not null && round.Winner == side;

					double roundImpact = Math.Clamp(
						(kills * 24d) +
						(assists * 12d) +
						(Math.Min(damage, 160) / 160d * 26d) +
						(deaths == 0 ? 10d : 0d) +
						(won ? 8d : -4d),
						0,
						100);

					double winProbability = Math.Clamp(
						0.45d +
						(kills * 0.09d) +
						(assists * 0.04d) +
						(Math.Min(damage, 180) / 180d * 0.22d) +
						(deaths == 0 ? 0.08d : -0.03d),
						0.01d,
						0.99d);

					double roundRating = Math.Clamp((roundImpact * 0.7d) + (winProbability * 100d * 0.3d), 0, 100);

					snapshots.Add(new RoundImpactSnapshot(
						RoundNumber: round.Number,
						Side: side,
						Won: won,
						Kills: kills,
						Assists: assists,
						Deaths: deaths,
						Damage: damage,
						WinProbability: Math.Round(winProbability * 100d, 2),
						RoundImpact: Math.Round(roundImpact, 2),
						RoundRating: Math.Round(roundRating, 2)));
				}

				return snapshots;
			}

			private static PlayerImpactStats BuildPlayerImpact(List<RoundImpactSnapshot> rounds)
			{
				if (rounds.Count == 0)
				{
					return new PlayerImpactStats(
						MatchImpactPercentage: 0,
						KillsPerRound: new SideSplitMetrics(0, 0, 0),
						RoundImpact: new SideSplitMetrics(0, 0, 0),
						WinProbability: new SideSplitMetrics(0, 0, 0),
						Rounds: new ReadOnlyCollection<RoundImpactSnapshot>(rounds));
				}

				SideSplitMetrics killsPerRound = BuildSideSplitMetrics(rounds, static x => x.Kills);
				SideSplitMetrics impact = BuildSideSplitMetrics(rounds, static x => x.RoundImpact);
				SideSplitMetrics winProbability = BuildSideSplitMetrics(rounds, static x => x.WinProbability);

				double matchImpact = rounds.Average(static x => x.RoundImpact);

				return new PlayerImpactStats(
					MatchImpactPercentage: Math.Round(matchImpact, 2),
					KillsPerRound: killsPerRound,
					RoundImpact: impact,
					WinProbability: winProbability,
					Rounds: new ReadOnlyCollection<RoundImpactSnapshot>(rounds));
			}

			private static SideSplitMetrics BuildSideSplitMetrics(List<RoundImpactSnapshot> rounds, Func<RoundImpactSnapshot, double> selector)
			{
				double overall = rounds.Count == 0 ? 0 : rounds.Average(selector);

				List<RoundImpactSnapshot> tRounds = rounds
					.Where(static round => round.Side == CsTeamSide.Terrorists)
					.ToList();
				double tValue = tRounds.Count == 0 ? 0 : tRounds.Average(selector);

				List<RoundImpactSnapshot> ctRounds = rounds
					.Where(static round => round.Side == CsTeamSide.CounterTerrorists)
					.ToList();
				double ctValue = ctRounds.Count == 0 ? 0 : ctRounds.Average(selector);

				return new SideSplitMetrics(
					Overall: Math.Round(overall, 2),
					Terrorists: Math.Round(tValue, 2),
					CounterTerrorists: Math.Round(ctValue, 2));
			}

			private CsTeamSide ResolvePlayerRoundSide(PlayerAccumulator player, RoundAccumulator round)
			{
				if (round.PlayerSideAtStart.TryGetValue(player.UserId, out CsTeamSide sideAtStart) && sideAtStart is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists)
				{
					return sideAtStart;
				}

				bool isFirstHalf = HalfTimeRoundNumber > 0 && round.Number <= HalfTimeRoundNumber;
				return !isFirstHalf ? player.Team : player.Team switch
				{
					CsTeamSide.Terrorists => CsTeamSide.CounterTerrorists,
					CsTeamSide.CounterTerrorists => CsTeamSide.Terrorists,
					_ => CsTeamSide.Unknown
				};
			}

			private bool EnsureTrackedRound()
			{
				if (CurrentRound is not null)
				{
					return true;
				}

				if (PlayersByUserId.Count <= 0) return false;

				StartRound(CurrentFrameTick, isSynthetic: true);
				return true;

			}

			private void HandlePlayerConnect(IReadOnlyDictionary<string, GameEventValue> data)
			{
				int userId = GetIntValue(data, "userid");
				ulong steamId = GetUInt64Value(data, "xuid");
				UpsertPlayerIdentity(userId, GetStringValue(data, "name"), steamId, GetBoolValue(data, "bot"), userId, mapUserIdAsEntitySlot: false);
			}

			private void HandleServerCvar(IReadOnlyDictionary<string, GameEventValue> data)
			{
				string? cvarName = GetStringValue(data, "cvarname") ?? GetStringValue(data, "name");
				string? cvarValue = GetStringValue(data, "cvarvalue") ?? GetStringValue(data, "value");
				if (string.IsNullOrWhiteSpace(cvarName) || string.IsNullOrWhiteSpace(cvarValue))
				{
					return;
				}

				string key = cvarName.Trim().ToLowerInvariant();
				switch (key)
				{
					case "hostname":
						ServerName = cvarValue;
						_ = TryParseServerEndpoint(cvarValue, out string? hostAddress, out int? hostPort);
						ServerAddress ??= hostAddress;
						ServerPort ??= hostPort;
						break;

					case "hostip":
					case "ip":
					case "net_public_adr":
						if (TryParseIpValue(cvarValue, out string? parsedAddress))
						{
							ServerAddress = parsedAddress;
						}
						break;

					case "hostport":
						if (int.TryParse(cvarValue, out int parsedPort) && parsedPort is > 0 and <= 65535)
						{
							ServerPort = parsedPort;
						}
						break;

					case "mapname":
						MapName = cvarValue;
						break;

					case "game_type":
					case "game_mode":
					case "mp_gamemode":
						GameType = cvarValue;
						break;

					case "maxplayers":
					case "sv_visiblemaxplayers":
					case "sv_maxplayers":
						if (int.TryParse(cvarValue, out int parsedMaxPlayers) && parsedMaxPlayers is > 0 and <= 128)
						{
							MaxPlayers = parsedMaxPlayers;
						}
						break;
				}
			}

			private static bool TryParseIpValue(string value, out string? ip)
			{
				ip = null;
				string trimmed = value.Trim();
				if (IPAddress.TryParse(trimmed, out _))
				{
					ip = trimmed;
					return true;
				}

				if (uint.TryParse(trimmed, out uint numericHostIp))
				{
					ip = new IPAddress(numericHostIp).ToString();
					return true;
				}

				return false;
			}

			private void HandlePlayerSpawn(IReadOnlyDictionary<string, GameEventValue> data)
			{
				// In CS2, player_spawn carries userid as a type-9 controller handle.
				// Use it to map entity slot -> player and ensure team assignment is current.
				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn");
				if (player is null)
				{
					return;
				}

				// teamnum field is type 5 (byte) in this event
				int teamNum = GetIntValue(data, "teamnum");
				if (teamNum != 0)
				{
					player.Team = (CsTeamSide)teamNum;
				}
			}

			private void HandlePlayerTeam(IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn") ??
				                            ResolvePlayer(GetIntValue(data, "userid"));
				if (player is null)
				{
					return;
				}

				player.Team = (CsTeamSide)GetIntValue(data, "team");
				player.IsConnected = true;

				// Detect halftime: player_team fires between rounds (CurrentRound null) and we have rounds already
				if (CurrentRound is null && Rounds.Count > 0 && HalfTimeRoundNumber == 0)
				{
					HalfTimeRoundNumber = Rounds.Count;
				}
			}

			private void HandlePlayerDisconnect(IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn") ??
				                            ResolvePlayer(GetIntValue(data, "userid"));
				if (player is null)
				{
					return;
				}

				player.IsConnected = false;
			}

			private void StartRound(int tick, bool isSynthetic = false)
			{
				if (CurrentRound is not null)
				{
					CompleteRound(CurrentRound, InferRoundWinner(CurrentRound), tick);
				}

				RoundAccumulator round = new(Rounds.Count + 1, tick, isSynthetic);
				foreach (PlayerAccumulator player in PlayersByUserId.Values.Where(static player => player.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists))
				{
					round.Alive[player.UserId] = true;
					round.PlayerSideAtStart[player.UserId] = player.Team;
				}

				CurrentRound = round;
				Rounds.Add(round);
			}

			private void CompleteRound(RoundAccumulator? round, CsTeamSide? winner, int tick)
			{
				if (round is null)
				{
					return;
				}

				round.EndTick = tick;
				round.Winner = winner;
				round.Duration = GetDuration(round.StartTick, tick);
				round.CompleteShotSequences();

				// Increment RoundsParticipated for all players observed in this round.
				// Fall back to players already tracked in Alive dict (from StartRound team assignment).
				IEnumerable<int> participantIds = round.Participants.Count > 0 ? round.Participants : round.Alive.Keys;
				foreach (int participantId in participantIds)
				{
					if (PlayersByUserId.TryGetValue(participantId, out PlayerAccumulator? participant))
					{
						participant.RoundsParticipated++;
					}
				}

				switch (winner)
				{
					case CsTeamSide.Terrorists:
						TerroristScore++;
						break;
					case CsTeamSide.CounterTerrorists:
						CounterTerroristScore++;
						break;
					case CsTeamSide.Unknown:
					case CsTeamSide.Spectator:
					case null:
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(winner), winner, null);
				}

				foreach ((int userId, int killsThisRound) in round.KillsByPlayer)
				{
					if (!PlayersByUserId.TryGetValue(userId, out PlayerAccumulator? player))
					{
						continue;
					}

					switch (killsThisRound)
					{
						case 2:
							player.TwoKs.Add(round.Number);
							break;
						case 3:
							player.ThreeKs.Add(round.Number);
							break;
						case 4:
							player.FourKs.Add(round.Number);
							break;
						default:
							if (killsThisRound >= 5)
							{
								player.FiveKs.Add(round.Number);
							}
							break;
					}
				}

				if (winner is not null)
				{
					foreach (PlayerAccumulator player in PlayersByUserId.Values.Where(static player => player.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists))
					{
						if (player.Team == winner)
						{
							player.RoundsWon++;
						}
						else
						{
							player.RoundsLost++;
						}
					}
				}

				ResolveClutches(round, winner);
				CurrentRound = null;
			}

			private void ResolveClutches(RoundAccumulator round, CsTeamSide? winner)
			{
				if (winner is null)
				{
					return;
				}

				foreach (ClutchCandidate candidate in round.ClutchCandidates)
				{
					if (!PlayersByUserId.TryGetValue(candidate.UserId, out PlayerAccumulator? player))
					{
						continue;
					}

					player.ClutchAttempts++;
					if (player.Team == winner && round.Alive.TryGetValue(candidate.UserId, out bool alive) && alive)
					{
						player.ClutchWins++;
						player.ClutchWinsByOpponents[candidate.OpponentsAlive] = player.ClutchWinsByOpponents.GetValueOrDefault(candidate.OpponentsAlive) + 1;
					}
				}
			}

			/* TODO:
			 * Figure out why all kills aren't being handled,
			 *		-> It appears kills on the enemy team were read correctly, but not local user team?
			 *		-> I think I found the cause: user 'clue777' doesn't seem to register inside the demo,
			 *			and kills towards him aren't being counted because of it
			 * */
			private void HandlePlayerDeath(int tick, IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? killer = ResolvePlayerFromCandidates(data, "attacker", "attacker_pawn");
				PlayerAccumulator? victim = ResolvePlayerFromCandidates(data, "userid", "userid_pawn", "victim");
				PlayerAccumulator? assister = ResolvePlayerFromCandidates(data, "assister", "assister_pawn");
				string weapon = NormalizeWeapon(GetStringValue(data, "weapon"));
				bool isHeadshot = GetBoolValue(data, "headshot");
				int penetrated = GetIntValue(data, "penetrated");
				bool throughSmoke = GetBoolValue(data, "thrusmoke");
				bool attackerBlind = GetBoolValue(data, "attackerblind");

				if (victim is null || CurrentRound is null)
				{
					return;
				}

				// Track participation for RoundsParticipated counting
				CurrentRound.Participants.Add(victim.UserId);
				if (killer is not null) CurrentRound.Participants.Add(killer.UserId);
				if (assister is not null) CurrentRound.Participants.Add(assister.UserId);

				bool isTeamKill = killer is not null
				                  && killer.Team == victim.Team
				                  && killer.UserId != victim.UserId
				                  && killer.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists;
				bool isTrade = TryResolveTrade(killer, victim, tick);

				victim.Deaths++;
				victim.Weapon(weapon).Deaths++;
				CurrentRound.Alive[victim.UserId] = false;

				if (killer is not null && killer.UserId != victim.UserId)
				{
					killer.Kills++;
					killer.KillsByWeapon(weapon).Kills++;
					CurrentRound.KillsByPlayer[killer.UserId] = CurrentRound.KillsByPlayer.GetValueOrDefault(killer.UserId) + 1;
					if (isHeadshot)
					{
						killer.HeadshotKills++;
					}

					if (isTeamKill)
					{
						if (ClassifyWeapon(weapon).IsUtility)
						{
							killer.TeamKillsUtility++;
						}
						else
						{
							killer.TeamKillsOther++;
						}
					}

					if (ClassifyWeapon(weapon).IsFrag)
					{
						killer.FragKills++;
						victim.FragDeaths++;
					}

					if (ClassifyWeapon(weapon).IsMolotov)
					{
						killer.MollyKills++;
						victim.MollyDeaths++;
					}
				}

				bool canCountAssist = assister is not null
				                      && killer is not null
				                      && killer.UserId != victim.UserId
				                      && !isTeamKill
				                      && assister.UserId != victim.UserId
				                      && assister.UserId != killer.UserId;

				if (canCountAssist && assister != null)
				{
					assister.Assists++;
					assister.Weapon(weapon).Assists++;
				}

				CurrentRound.Kills.Add(new RoundKillEvent(
					Tick: tick,
					KillerSteamId: killer?.SteamId,
					KillerName: killer?.Name,
					VictimSteamId: victim.SteamId,
					VictimName: victim.Name,
					AssisterSteamId: assister?.SteamId,
					AssisterName: assister?.Name,
					Weapon: weapon,
					IsHeadshot: isHeadshot,
					IsTeamKill: isTeamKill,
					IsTrade: isTrade,
					IsWallBang: penetrated > 0,
					ThroughSmoke: throughSmoke,
					AttackerBlind: attackerBlind));

				if (killer is not null && killer.Team != victim.Team && killer.UserId != victim.UserId)
				{
					CurrentRound.PendingTrades.Add(new TradeWindow(victim.UserId, victim.Team, killer.UserId, tick + GetTradeWindowTicks()));
				}

				EvaluateClutchCandidates();
			}

			private bool TryResolveTrade(PlayerAccumulator? killer, PlayerAccumulator victim, int tick)
			{
				if (killer is null || CurrentRound is null)
				{
					return false;
				}

				for (int i = 0; i < CurrentRound.PendingTrades.Count; i++)
				{
					TradeWindow pending = CurrentRound.PendingTrades[i];
					if (pending.ExpiresAtTick < tick)
					{
						continue;
					}

					if (pending.KillerUserId != victim.UserId || pending.Team != killer.Team || pending.OriginalVictimUserId == killer.UserId)
					{
						continue;
					}

					killer.TradeKills++;
					killer.TradeOpportunities++;
					if (PlayersByUserId.TryGetValue(pending.OriginalVictimUserId, out PlayerAccumulator? tradedPlayer))
					{
						tradedPlayer.TradedDeaths++;
					}

					CurrentRound.PendingTrades.RemoveAt(i);
					return true;
				}

				return false;
			}

			private void HandlePlayerHurt(int tick, IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? attacker = ResolvePlayerFromCandidates(data, "attacker", "attacker_pawn");
				PlayerAccumulator? victim = ResolvePlayerFromCandidates(data, "userid", "userid_pawn", "victim");
				if (attacker is null || victim is null)
				{
					return;
				}

				// Skip damage to already-dead players to avoid overkill accumulation.
				if (CurrentRound is not null && CurrentRound.Alive.TryGetValue(victim.UserId, out bool stillAlive) && !stillAlive)
				{
					return;
				}

				// Track participation for RoundsParticipated counting
				CurrentRound?.Participants.Add(attacker.UserId);
				CurrentRound?.Participants.Add(victim.UserId);

				string weapon = NormalizeWeapon(GetStringValue(data, "weapon"));
				int rawDamage = GetIntValue(data, "dmg_health");
				int armorDamage = GetIntValue(data, "dmg_armor");
				bool friendlyFire = attacker.Team == victim.Team
				                    && attacker.UserId != victim.UserId
				                    && attacker.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists;

				// Cap cumulative damage per victim per round to 100 to exclude overkill.
				int damage;
				if (CurrentRound is not null)
				{
					int alreadyDealt = CurrentRound.HpDealt.GetValueOrDefault(victim.UserId);
					int remaining = Math.Max(0, 100 - alreadyDealt);
					damage = Math.Min(rawDamage, remaining);
					CurrentRound.HpDealt[victim.UserId] = alreadyDealt + damage;
				}
				else
				{
					damage = rawDamage;
				}

				if (damage <= 0)
				{
					return;
				}

				attacker.DamageDealt += damage;
				attacker.Hits++;
				attacker.Weapon(weapon).Damage += damage;
				attacker.Weapon(weapon).Hits++;

				victim.DamageTaken += damage;
				if (friendlyFire)
				{
					attacker.TeamDamage += damage;
				}

				if (ClassifyWeapon(weapon).IsUtility)
				{
					attacker.UtilityDamage += damage;
				}

				CurrentRound?.RegisterHit(attacker.UserId);
				CurrentRound?.Damage.Add(new RoundDamageEvent(
					Tick: tick,
					AttackerSteamId: attacker.SteamId,
					AttackerName: attacker.Name,
					VictimSteamId: victim.SteamId,
					VictimName: victim.Name,
					Weapon: weapon,
					Damage: damage,
					HealthDamage: damage,
					ArmorDamage: armorDamage,
					IsFriendlyFire: friendlyFire));
			}

			private void HandleWeaponFire(int tick, IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn");
				if (player is null || CurrentRound is null)
				{
					return;
				}

				string weapon = NormalizeWeapon(GetStringValue(data, "weapon"));
				player.Shots++;
				player.Weapon(weapon).Shots++;
				CurrentRound.RegisterShot(player.UserId, tick, player, weapon, GetSprayGapTicks());
			}

			private void HandlePlayerBlind(IReadOnlyDictionary<string, GameEventValue> data)
			{
				PlayerAccumulator? victim = ResolvePlayerFromCandidates(data, "userid", "userid_pawn", "victim");
				if (victim is null)
				{
					return;
				}

				victim.TimesFlashed++;

				PlayerAccumulator? attacker = ResolvePlayerFromCandidates(data, "attacker", "attacker_pawn");
				if (attacker is null || attacker.UserId == victim.UserId) return;

				attacker.PlayersFlashed++;
				if (attacker.Team == victim.Team && attacker.Team is CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists)
				{
					attacker.TeamFlashes++;
				}
			}

			private void IncrementUtilityCount(IReadOnlyDictionary<string, GameEventValue> data, WeaponKind weaponKind)
			{
				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn");
				if (player is null)
				{
					return;
				}

				switch (weaponKind)
				{
					case WeaponKind.Flashbang:
						player.FlashbangsThrown++;
						break;
					case WeaponKind.HeGrenade:
						player.HeGrenadesThrown++;
						break;
					case WeaponKind.Molotov:
						player.MolotovsThrown++;
						break;
					case WeaponKind.Other:
					case WeaponKind.OtherUtility:
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(weaponKind), weaponKind, null);
				}
			}

			private void HandleRoundMvp(IReadOnlyDictionary<string, GameEventValue> data)
			{
				RoundMvpEventsSeen++;

				PlayerAccumulator? player = ResolvePlayerFromCandidates(data, "userid", "userid_pawn", "player", "playerid", "entindex", "entityid", "index", "slot", "client")
				                            ?? ResolvePlayerByName(GetStringValue(data, "name") ?? GetStringValue(data, "playername"));
				if (player is null)
				{
					return;
				}

				RoundMvpResolved++;
				RoundAccumulator? round = CurrentRound ?? Rounds.LastOrDefault();
				if (round is not null)
				{
					round.ExplicitMvpUserId = player.UserId;
				}
			}

			private void HandleBombExploded()
			{
				if (CurrentRound is not null)
				{
					CurrentRound.BombExploded = true;
				}
			}

			private void ApplyRoundMvpAwards()
			{
				foreach (PlayerAccumulator player in PlayersByUserId.Values)
				{
					player.MvpCount = 0;
				}

				foreach (RoundAccumulator round in Rounds)
				{
					int? mvpUserId = round.ExplicitMvpUserId ?? InferRoundMvpUserId(round);
					if (mvpUserId is int resolvedUserId && PlayersByUserId.TryGetValue(resolvedUserId, out PlayerAccumulator? player))
					{
						player.MvpCount++;
					}
				}
			}

			private int? InferRoundMvpUserId(RoundAccumulator round)
			{
				if (round.Winner is not (CsTeamSide.Terrorists or CsTeamSide.CounterTerrorists))
				{
					return null;
				}

				if (round.Winner == CsTeamSide.CounterTerrorists && round.DefuserUserId is int defuserUserId)
				{
					return defuserUserId;
				}

				if (round.Winner == CsTeamSide.Terrorists && round.BombExploded && round.PlanterUserId is int planterUserId)
				{
					return planterUserId;
				}

				IEnumerable<PlayerAccumulator> candidates = PlayersByUserId.Values
					.Where(player => ResolvePlayerRoundSide(player, round) == round.Winner)
					.Where(player => round.PlayerSideAtStart.ContainsKey(player.UserId)
						|| round.Participants.Contains(player.UserId)
						|| round.Alive.ContainsKey(player.UserId));

				PlayerAccumulator? bestPlayer = candidates
					.OrderByDescending(player => round.KillsByPlayer.GetValueOrDefault(player.UserId))
					.ThenByDescending(player => round.Damage
						.Where(damage => damage.AttackerSteamId == player.SteamId && !damage.IsFriendlyFire)
						.Sum(damage => damage.HealthDamage > 0 ? damage.HealthDamage : damage.Damage))
					.ThenByDescending(player => round.Alive.GetValueOrDefault(player.UserId))
					.ThenBy(player => player.UserId)
					.FirstOrDefault();

				return bestPlayer?.UserId;
			}

			private void EvaluateClutchCandidates()
			{
				if (CurrentRound is null)
				{
					return;
				}

				int tAlive = CurrentRound.Alive.Count(entry => entry.Value && PlayersByUserId.GetValueOrDefault(entry.Key)?.Team == CsTeamSide.Terrorists);
				int ctAlive = CurrentRound.Alive.Count(entry => entry.Value && PlayersByUserId.GetValueOrDefault(entry.Key)?.Team == CsTeamSide.CounterTerrorists);

				if (tAlive == 1)
				{
					int player = CurrentRound.Alive.FirstOrDefault(entry => entry.Value && PlayersByUserId.GetValueOrDefault(entry.Key)?.Team == CsTeamSide.Terrorists).Key;
					if (player != 0 && CurrentRound.ClutchCandidates.All(candidate => candidate.UserId != player))
					{
						CurrentRound.ClutchCandidates.Add(new ClutchCandidate(player, ctAlive));
					}
				}

				if (ctAlive == 1)
				{
					int player = CurrentRound.Alive.FirstOrDefault(entry => entry.Value && PlayersByUserId.GetValueOrDefault(entry.Key)?.Team == CsTeamSide.CounterTerrorists).Key;
					if (player != 0 && CurrentRound.ClutchCandidates.All(candidate => candidate.UserId != player))
					{
						CurrentRound.ClutchCandidates.Add(new ClutchCandidate(player, tAlive));
					}
				}
			}

			private bool ShouldStartRoundFromSignal(int tick) => PlayersByUserId.Count != 0 && (CurrentRound is null || tick - CurrentRound.StartTick >= GetRoundRestartSignalGapTicks());

			private CsTeamSide? InferRoundWinner(RoundAccumulator? round)
			{
				if (round is null)
				{
					return null;
				}

				// Determine if this is a first-half round (teams are swapped relative to current assignments)
				bool isFirstHalf = HalfTimeRoundNumber > 0 && round.Number <= HalfTimeRoundNumber;

				// Helper: resolve team for a player, accounting for halftime swap
				CsTeamSide? GetPlayerTeam(int userId)
				{
					CsTeamSide? team = PlayersByUserId.GetValueOrDefault(userId)?.Team;
					return team is null or CsTeamSide.Unknown or CsTeamSide.Spectator 
						? null
						: !isFirstHalf 
							? team :
								team == CsTeamSide.Terrorists ? CsTeamSide.CounterTerrorists : CsTeamSide.Terrorists;
				}

				// Primary: use Alive tracking with PlayerSideAtStart (teams captured at round start)
				// For rounds where sideSnapshot was filled, use it. Other-wise use current teams (halftime-adjusted).
				int tAlive, ctAlive;
				if (round.PlayerSideAtStart.Count > 0)
				{
					tAlive = round.Alive.Count(entry => entry.Value && round.PlayerSideAtStart.GetValueOrDefault(entry.Key) == CsTeamSide.Terrorists);
					ctAlive = round.Alive.Count(entry => entry.Value && round.PlayerSideAtStart.GetValueOrDefault(entry.Key) == CsTeamSide.CounterTerrorists);
				}
				else
				{
					tAlive = round.Alive.Count(entry => entry.Value && GetPlayerTeam(entry.Key) == CsTeamSide.Terrorists);
					ctAlive = round.Alive.Count(entry => entry.Value && GetPlayerTeam(entry.Key) == CsTeamSide.CounterTerrorists);
				}

				if (tAlive > 0 && ctAlive == 0) return CsTeamSide.Terrorists;
				if (ctAlive > 0 && tAlive == 0) return CsTeamSide.CounterTerrorists;

				// Fallback: from kill events, count how many T vs CT players died using halftime-adjusted teams.
				int tPlayerCount = PlayersByUserId.Values.Count(p => GetPlayerTeam(p.UserId) == CsTeamSide.Terrorists);
				int ctPlayerCount = PlayersByUserId.Values.Count(p => GetPlayerTeam(p.UserId) == CsTeamSide.CounterTerrorists);

				if (tPlayerCount == 0 || ctPlayerCount == 0)
				{
					return null;
				}

				int tDeaths = round.Kills
					.Select(k => k.VictimSteamId)
					.Distinct()
					.Count(sid => sid.HasValue && PlayersByUserId.Values.Any(p => p.SteamId == sid.Value && GetPlayerTeam(p.UserId) == CsTeamSide.Terrorists));
				int ctDeaths = round.Kills
					.Select(k => k.VictimSteamId)
					.Distinct()
					.Count(sid => sid.HasValue && PlayersByUserId.Values.Any(p => p.SteamId == sid.Value && GetPlayerTeam(p.UserId) == CsTeamSide.CounterTerrorists));

				// A team is fully eliminated if all their players died
				bool tEliminated = tDeaths >= tPlayerCount;
				bool ctEliminated = ctDeaths >= ctPlayerCount;

				if (ctEliminated && !tEliminated) return CsTeamSide.Terrorists;
				if (tEliminated && !ctEliminated) return CsTeamSide.CounterTerrorists;

				// Soft fallback: if one team has significantly more deaths, they likely lost
				if (ctDeaths > tDeaths && ctDeaths > ctPlayerCount / 2) return CsTeamSide.Terrorists;
				if (tDeaths > ctDeaths && tDeaths > tPlayerCount / 2) return CsTeamSide.CounterTerrorists;

				return null;
			}

			private int GetSprayGapTicks() => TickIntervalSeconds <= 0 ? 16 : Math.Max(1, (int)Math.Round(0.25d / TickIntervalSeconds));

			private int GetRoundRestartSignalGapTicks() => TickIntervalSeconds <= 0 ? 640 : Math.Max(1, (int)Math.Round(10d / TickIntervalSeconds));

			private int GetTradeWindowTicks() => TickIntervalSeconds <= 0 ? 320 : Math.Max(1, (int)Math.Round(TimeSpan.FromSeconds(5).TotalSeconds / TickIntervalSeconds));

			private TimeSpan? GetDuration(int startTick, int endTick)
			{
				if (TickIntervalSeconds <= 0 || endTick < startTick)
				{
					return null;
				}

				return TimeSpan.FromSeconds((endTick - startTick) * TickIntervalSeconds);
			}

			private PlayerAccumulator? ResolvePlayer(int userId, bool allowZeroUserId = false)
			{
				if (userId == 0 && !allowZeroUserId)
				{
					return null;
				}

				if (PlayersByUserId.TryGetValue(userId, out PlayerAccumulator? player))
					return player;

				player = new PlayerAccumulator(userId);
				PlayersByUserId[userId] = player;
				PlayersByEntitySlot[userId] = player;

				return player;
			}

			// Resolve a player from a CS2 entity handle (type 8 or 9).
			// The lower 14 bits of the handle are the entity slot index, which
			// matches the player slot stored as UserId from the userinfo string table.
			private const uint EntityHandleIndexMask = 0x3FFF;

			private PlayerAccumulator? ResolvePlayerByHandle(ulong handle)
			{
				if (handle is uint.MaxValue or ulong.MaxValue)
				{
					return null;
				}

				int slot = (int)(handle & EntityHandleIndexMask);
				return PlayersByEntitySlot.TryGetValue(slot, out PlayerAccumulator? bySlot) ? bySlot : PlayersByUserId.GetValueOrDefault(slot);
			}

			private PlayerAccumulator? ResolvePlayerBySteamId(ulong steamId)
				=> steamId != 0 && PlayersBySteamId.TryGetValue(steamId, out PlayerAccumulator? player) ? player : null;

			private PlayerAccumulator? ResolvePlayerByName(string? name)
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					return null;
				}

				return PlayersByUserId.Values.FirstOrDefault(player =>
					!string.IsNullOrWhiteSpace(player.Name)
					&& string.Equals(player.Name, name, StringComparison.OrdinalIgnoreCase));
			}

			// Try to resolve a player from a game-event field that may be either a
			// legacy short userid (types 1-7) or a CS2 entity handle (types 8-9).
			private PlayerAccumulator? ResolvePlayerFromField(IReadOnlyDictionary<string, GameEventValue> data, string key)
			{
				if (!data.TryGetValue(key, out GameEventValue value))
				{
					return null;
				}

				// CS2 entity handle types — stored as uint (int32 reinterpreted as unsigned)
				if (value.Type is 8 or 9)
				{
					return ResolvePlayerByHandle(value.AsUInt64());
				}

				// Legacy short userid
				int intId = value.AsInt32();
				return intId == 0 
					? null : PlayersByLegacyUserId.TryGetValue(intId, out PlayerAccumulator? byLegacyUserId) 
					? byLegacyUserId : ResolvePlayer(intId);
			}

			private PlayerAccumulator? ResolvePlayerFromCandidates(IReadOnlyDictionary<string, GameEventValue> data, params string[] keys)
			{
				foreach (string key in keys)
				{
					PlayerAccumulator? byField = ResolvePlayerFromField(data, key);
					if (byField is not null)
					{
						return byField;
					}

					// Some demos encode identity as xuid/steamid fields rather than userid/entity handles.
					PlayerAccumulator? bySteamId = ResolvePlayerBySteamId(GetUInt64Value(data, key + "_xuid"))
					                             ?? ResolvePlayerBySteamId(GetUInt64Value(data, key + "xuid"))
					                             ?? ResolvePlayerBySteamId(GetUInt64Value(data, key + "_steamid"));
					if (bySteamId is not null)
					{
						return bySteamId;
					}
				}

				return ResolvePlayerBySteamId(GetUInt64Value(data, "xuid"))
				       ?? ResolvePlayerBySteamId(GetUInt64Value(data, "attacker_xuid"))
				       ?? ResolvePlayerBySteamId(GetUInt64Value(data, "victim_xuid"))
				       ?? ResolvePlayerBySteamId(GetUInt64Value(data, "assister_xuid"));
			}

			private static int GetIntValue(IReadOnlyDictionary<string, GameEventValue> data, string key)
				=> data.TryGetValue(key, out GameEventValue value) ? value.AsInt32() : 0;

			private static bool TryGetIntValue(IReadOnlyDictionary<string, GameEventValue> data, string key, out int value)
			{
				if (data.TryGetValue(key, out GameEventValue raw))
				{
					value = raw.AsInt32();
					return true;
				}

				value = 0;
				return false;
			}

			private static ulong GetUInt64Value(IReadOnlyDictionary<string, GameEventValue> data, string key)
				=> data.TryGetValue(key, out GameEventValue value) ? value.AsUInt64() : 0;

			private static string? GetStringValue(IReadOnlyDictionary<string, GameEventValue> data, string key)
				=> data.TryGetValue(key, out GameEventValue value) ? value.AsString() : null;

			private static bool GetBoolValue(IReadOnlyDictionary<string, GameEventValue> data, string key)
				=> data.TryGetValue(key, out GameEventValue value) && value.AsBool();

			private static CsTeamSide GetTeamValue(IReadOnlyDictionary<string, GameEventValue> data, string key)
				=> (CsTeamSide)GetIntValue(data, key);

			private static string FormatGameEventValue(GameEventValue value)
			{
				string raw = value.Value switch
				{
					null => "null",
					bool boolValue => boolValue ? "true" : "false",
					string stringValue => stringValue,
					_ => Convert.ToString(value.Value, CultureInfo.InvariantCulture) ?? "null",
				};
				if (value.Type is 8 or 9 && value.Value is uint handleU)
				{
					int slot = (int)(handleU & 0x3FFF);
					return $"[t{value.Type}]handle={handleU}(slot={slot})";
				}
				return $"[t{value.Type}]{raw}";
			}

			private static string NormalizeWeapon(string? weapon)
			{
				if (string.IsNullOrWhiteSpace(weapon))
				{
					return "unknown";
				}

				string lower = weapon.Trim().ToLowerInvariant();
				// weapon_fire events prefix weapon names with "weapon_"; strip it so
				// the same key is used as in player_hurt / player_death events.
				if (lower.StartsWith("weapon_", StringComparison.Ordinal))
				{
					lower = lower["weapon_".Length..];
				}

				return lower;
			}

			private static WeaponClass ClassifyWeapon(string weapon)
				=> weapon switch
				{
					"hegrenade" => new WeaponClass(WeaponKind.HeGrenade, true, true, false),
					"flashbang" => new WeaponClass(WeaponKind.Flashbang, true, false, false),
					"molotov" or "inferno" or "incgrenade" => new WeaponClass(WeaponKind.Molotov, true, false, true),
					"smokegrenade" or "decoy" => new WeaponClass(WeaponKind.OtherUtility, true, false, false),
					_ => new WeaponClass(WeaponKind.Other, false, false, false),
				};
		}

		private sealed record EventDescriptor(int EventId, string Name, IReadOnlyList<EventKeyDescriptor> Keys);

		private sealed record EventKeyDescriptor(string Name, int Type);

		private readonly record struct GameEventValue(int Type, object? Value)
		{
			public int AsInt32()
				=> Value switch
				{
					int intValue => intValue,
					long longValue => checked((int)longValue),
					uint uintValue => checked((int)uintValue),
					ulong ulongValue => checked((int)ulongValue),
					bool boolValue => boolValue ? 1 : 0,
					float floatValue => checked((int)floatValue),
					_ => 0,
				};

			public ulong AsUInt64()
				=> Value switch
				{
					ulong ulongValue => ulongValue,
					uint uintValue => uintValue,
					int intValue and >= 0 => (ulong)intValue,
					long longValue and >= 0 => (ulong)longValue,
					_ => 0,
				};

			public string? AsString() => Value as string;

			public bool AsBool()
				=> Value switch
				{
					bool boolValue => boolValue,
					int intValue => intValue != 0,
					ulong ulongValue => ulongValue != 0,
					_ => false,
				};
		}

		private sealed class PlayerAccumulator(int userId)
		{
			public int UserId { get; } = userId;

			public string Name { get; set; } = string.Empty;

			public ulong SteamId { get; set; }

			public uint AccountId { get; set; }

			public bool IsBot { get; set; }

			public bool IsConnected { get; set; }

			public CsTeamSide Team { get; set; }

			public int Kills { get; set; }

			public int Deaths { get; set; }

			public int Assists { get; set; }

			public int MvpCount { get; set; }

			public int HeadshotKills { get; set; }

			public int DamageDealt { get; set; }

			public int DamageTaken { get; set; }

			public int UtilityDamage { get; set; }

			public int Shots { get; set; }

			public int Hits { get; set; }

			public int SprayShots { get; set; }

			public int SprayHits { get; set; }

			public int PlayersFlashed { get; set; }

			public int TimesFlashed { get; set; }

			public int FlashbangsThrown { get; set; }

			public int HeGrenadesThrown { get; set; }

			public int MolotovsThrown { get; set; }

			public int FragKills { get; set; }

			public int FragDeaths { get; set; }

			public int MollyKills { get; set; }

			public int MollyDeaths { get; set; }

			public int TeamDamage { get; set; }

			public int TeamKillsUtility { get; set; }

			public int TeamKillsOther { get; set; }

			public int TeamFlashes { get; set; }

			public int BombPlants { get; set; }

			public int BombDefuses { get; set; }

			public int TradeKills { get; set; }

			public int TradeOpportunities { get; set; }

			public int TradedDeaths { get; set; }

			public int ClutchAttempts { get; set; }

			public int ClutchWins { get; set; }

			public int RoundsParticipated { get; set; }

			public int RoundsWon { get; set; }

			public int RoundsLost { get; set; }

			public int? RankOld { get; set; }

			public int? Rank { get; set; }

			public double? RankChange { get; set; }

			public int? RankWins { get; set; }

			public int? RankTypeId { get; set; }

			public int? VisibleSkill { get; set; }

			public List<int> TwoKs { get; } = [];

			public List<int> ThreeKs { get; } = [];

			public List<int> FourKs { get; } = [];

			public List<int> FiveKs { get; } = [];

			public Dictionary<int, int> ClutchWinsByOpponents { get; } = [];

			public Dictionary<string, WeaponAccumulator> WeaponStats { get; } = new(StringComparer.OrdinalIgnoreCase);

			public WeaponAccumulator Weapon(string weapon)
			{
				if (!WeaponStats.TryGetValue(weapon, out WeaponAccumulator? stats))
				{
					stats = new WeaponAccumulator(weapon);
					WeaponStats[weapon] = stats;
				}

				return stats;
			}

			public WeaponAccumulator KillsByWeapon(string weapon) => Weapon(weapon);
		}

		private sealed record WeaponAccumulator(string Weapon)
		{
			public int Kills { get; set; }
			public int Deaths { get; set; }
			public int Assists { get; set; }
			public int Damage { get; set; }
			public int Shots { get; set; }
			public int Hits { get; set; }
		}

		private sealed class RoundAccumulator(int number, int startTick, bool isSynthetic)
		{
			public int Number { get; } = number;

			public int StartTick { get; } = startTick;

			public int EndTick { get; set; }

			public bool IsSynthetic { get; } = isSynthetic;

			public CsTeamSide? Winner { get; set; }

			public TimeSpan? Duration { get; set; }

			public Dictionary<int, bool> Alive { get; } = [];

			// Tracks cumulative HP damage dealt to each player in this round,
			// capped at 100 to ignore overkill in player_hurt events.
			public Dictionary<int, int> HpDealt { get; } = [];

			public HashSet<int> Participants { get; } = [];

			public Dictionary<int, CsTeamSide> PlayerSideAtStart { get; } = [];

			public Dictionary<int, int> KillsByPlayer { get; } = [];

			public List<RoundKillEvent> Kills { get; } = [];

			public List<RoundDamageEvent> Damage { get; } = [];

			public List<TradeWindow> PendingTrades { get; } = [];

			public int? ExplicitMvpUserId { get; set; }

			public int? PlanterUserId { get; set; }

			public int? DefuserUserId { get; set; }

			public bool BombExploded { get; set; }

			public List<ClutchCandidate> ClutchCandidates { get; } = [];

			private Dictionary<int, ShotSequence> ShotSequences { get; } = [];

			public void RegisterShot(int userId, int tick, PlayerAccumulator player, string weapon, int sprayGapTicks)
			{
				if (!ShotSequences.TryGetValue(userId, out ShotSequence? sequence) || sequence.ShouldSplit(tick, weapon, sprayGapTicks))
				{
					sequence?.Commit(player);
					sequence = new ShotSequence(player, weapon, tick);
					ShotSequences[userId] = sequence;
				}

				sequence.RecordShot(tick);
			}

			public void RegisterHit(int userId)
			{
				if (ShotSequences.TryGetValue(userId, out ShotSequence? sequence))
				{
					sequence.RecordHit();
				}
			}

			public void CompleteShotSequences()
			{
				foreach (ShotSequence sequence in ShotSequences.Values)
				{
					sequence.Commit(sequence.Owner);
				}

				ShotSequences.Clear();
			}
		}

		private sealed class ShotSequence(PlayerAccumulator owner, string weapon, int startTick)
		{
			public PlayerAccumulator Owner { get; } = owner;

			private string Weapon { get; } = weapon;

			public int StartTick { get; } = startTick;

			private int LastTick { get; set; } = startTick;

			private int Shots { get; set; }

			private int Hits { get; set; }

			public bool ShouldSplit(int tick, string weapon, int sprayGapTicks)
				=> !string.Equals(Weapon, weapon, StringComparison.OrdinalIgnoreCase) || tick - LastTick > sprayGapTicks;

			public void RecordShot(int tick)
			{
				LastTick = tick;
				Shots++;
			}

			public void RecordHit() => Hits++;

			public void Commit(PlayerAccumulator player)
			{
				if (Shots >= 4)
				{
					player.SprayShots += Shots;
					player.SprayHits += Hits;
				}
			}
		}

		private readonly record struct TradeWindow(int OriginalVictimUserId, CsTeamSide Team, int KillerUserId, int ExpiresAtTick);

		private readonly record struct ClutchCandidate(int UserId, int OpponentsAlive);

		private enum WeaponKind
		{
			Other = 0,
			Flashbang = 1,
			HeGrenade = 2,
			Molotov = 3,
			OtherUtility = 4,
		}

		private readonly record struct WeaponClass(WeaponKind Kind, bool IsUtility, bool IsFrag, bool IsMolotov);
	}
}