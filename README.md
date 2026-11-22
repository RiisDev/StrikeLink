# StrikeLink

[![License: MIT](https://img.shields.io/github/license/RiisDev/StrikeLink)](https://github.com/RiisDev/StrikeLink/blob/main/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/StrikeLink)](https://www.nuget.org/packages/StrikeLink)
[![NuGet downloads](https://img.shields.io/nuget/dt/StrikeLink?label=downloads)](https://www.nuget.org/packages/StrikeLink)
[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)
[![Last commit](https://img.shields.io/github/last-commit/RiisDev/StrikeLink)](https://github.com/RiisDev/StrikeLink/commits)
[![Support Server](https://img.shields.io/discord/477201632204161025.svg?label=Discord&logo=Discord&colorB=7289da)](https://discord.gg/yyuggrH)

## Description

StrikeLink is a .NET library for integrating with Counter-Strike 2 and related Valve ecosystem services. It gives structured access to chat events, game state changes, the CS2 console, Steam client data, Valve configuration formats and demo files, using modern C# and .NET APIs.

It is built on Windows and has not been tested on Linux.

Full API documentation: [docs/index.md](https://github.com/RiisDev/StrikeLink/blob/main/docs/index.md)

### Features

- Event-driven services for chat, game state and the CS2 console.
- Global and team chat handling, including sending messages.
- Game State Integration listener with map, player and round state events.
- Steam client helpers: Steam path, game path, launch options, user config and current user.
- Valve config reader and writer for `.vcfg`, `.cfg`, `.vdf` and `.acf` with JSON-style access.
- Demo parser: downloads CS2 matches from a share code and parses `.dem` files into match, player and round data, including positions and grenades.

### Chat Service

Access to in-game chat messages and sending.

| Member | Kind |
| --- | --- |
| `OnTeamChat` | Event |
| `OnGlobalChat` | Event |
| `SendChatAsync(NewChatMessage message)` | Method |

```csharp
public enum ChatChannel
{
	Team,
	Global
}

public record NewChatMessage(ChatChannel Channel, string Message);
```

### Game State Integration

Live game state updates, based on [Game State Integration](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) with help from [this reddit post](https://reddit.com/r/GlobalOffensive/comments/cjhcpy).

- `GsiListener` raises `OnPostReceived` for every incoming GSI payload.
- `MapState`, `PlayerState` and `RoundState` events cover map changes, player updates and round transitions.

### Client Services

`SteamService` reads Steam and CS2 client data:

- `GetSteamPath()`
- `TryGetGamePath(int gameId, out string path)` and `GetGamePath(int gameId)`
- `GetUserConfig(long? userId)`
- `GetGameLaunchOptions(int gameId)`
- `GetCurrentUserId()`

`ConsoleService` talks to the CS2 console and listens to runtime events:

- `OnLogReceived`, `OnPlayerConnected`, `OnMapJoined`
- `OnGlobalChatMessageReceived`, `OnTeamChatMessageReceived`
- `OnUiStateChanged`, `OnAddonProgress`, `OnAddonFinished`
- `OnServerJoining`, `OnServerConnected`, `OnServerDisconnected`

### Valve Config Reader and Writer

A configuration system built around `JsonDocument`-style APIs. It reads and parses `.vcfg`, `.cfg`, `.vdf` and `.acf` files, lets you modify values through structured access, and writes them back to disk.

### Demo Parser

`Cs2DemoParser` reads CS2 (Source 2) `.dem` files from a file or any readable stream. `Cs2DemoDownloader` can fetch the demo for a match share code first.

```csharp
using StrikeLink.DemoParser;
using StrikeLink.DemoParser.Parsing;

DemoAuthorization auth = new(steamId, authCode, matchShareCode, apiKey);

using Cs2DemoParser parser = new(new FileInfo("match.dem"), auth);
Cs2DemoParseResult result = parser.ParseDemo();

Console.WriteLine($"{result.Match.Map}: {result.Match.TeamAScore}-{result.Match.TeamBScore} ({result.Match.Outcome})");
foreach (PlayerStats player in result.Players)
{
	Console.WriteLine($"{player.Name} {player.Kills}/{player.Deaths}/{player.Assists} ADR {player.Adr}");
}
```

The result contains:

- Match: map, duration, score per team, outcome for the authorised Steam ID, game build.
- Players: kills, deaths, assists, ADR, KAST, multi-kills, trades, clutches, utility, weapons, opening duels, economy, aim stats (counter-strafing, time to damage, first bullet accuracy), a play style role and the game's own scoreboard values.
- Rounds: winner and side swaps (including overtime), buy types and equipment value per team, every kill and damage event with positions.
- Replay data per round: player positions, view angles, health and armor sampled every `PositionSampleIntervalTicks` (default 32), grenade flight paths with the thrower, flashes, smoke and fire areas, bomb events and A, B and Mid map zones.
- Round impact from a win probability model that values each kill by how much it moved the round.

Round winners are read from the game's team scores when the demo carries entity state, and inferred from kills and the bomb otherwise. `Cs2DemoParseResult.Warnings` lists anything that could not be resolved. The parser has been checked against demos from game builds 10924 and 10955.

## Code Stack

- C# on .NET 8, 9 and 10, with `-windows` targets for the Windows-only services
- `System.Management` and `System.Security.Cryptography.ProtectedData` (Windows targets only)
- Hand-written Source 2 demo reader: protobuf and Snappy decoding, game events, string tables and a packet-entity (send table) decoder
- `xmldoc2md` for the generated API documentation

## Requirements

- .NET 8, 9 or 10 SDK
- Windows for the Steam, console and GSI services
- Counter-Strike 2 for live integration; demo parsing works on any `.dem` file
- A Steam Web API key, auth code and match share code only for downloading demos by share code

## Quick Start

Install from NuGet:

```bash
dotnet add package StrikeLink
```

```powershell
Install-Package StrikeLink
```

Or build from source:

```bash
git clone https://github.com/RiisDev/StrikeLink.git
cd StrikeLink
dotnet build
```

## Build Steps

Build every target:

```bash
dotnet build
```

Build the Release package. The net8.0 Release build also regenerates the Markdown API documentation in `docs` when the `xmldoc2md` tool is installed:

```bash
dotnet tool install -g xmldoc2markdown
dotnet build -c Release
```

`StrikeLink.Test.Suite` is a console project that parses the demos in its `DemosToCompareCheck` folder and prints the results:

```bash
dotnet run --project StrikeLink.Test.Suite
```

## Credits

- [sneid1/cs2-demo-analyzer](https://github.com/sneid1/cs2-demo-analyzer) (MIT) - used as a reference parser while developing and testing the demo parser. Its output was used to cross-check round data, buy types and player positions, and the A, B and Mid map zone boundaries in `DemoParser/Parsing/MapZones.cs` come from its source.
- [Game State Integration](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) on the Valve Developer Community wiki, and [this reddit post](https://reddit.com/r/GlobalOffensive/comments/cjhcpy), for the GSI payload format.

## AI Disclosure

Parts of this project were developed with AI assistance. Below is a summary of where it was used:

- DemoParser/Parsing - demo and wire decoding, the packet-entity decoder, event handling and the result models, written with AI assistance
- DemoParser/Parsing/Cs2DemoParser.cs, EntityParser.cs, Models.cs - AI-assisted rework: round winners from team score entities, overtime side swaps, empty round removal, instance baselines, game build 10955 field encodings, positions and view angles, grenade, flash, effect and bomb tracking, buy types, opening duels, roles and aim stats
- DemoParser/Parsing/WinProbability.cs, MapZones.cs - win probability model, and map zone tables ported from cs2-demo-analyzer
- Services/WebService/InternalTools/SqlService.cs - written with AI assistance to avoid adding another NuGet package for a simple database query
- Services/Config - written with AI assistance, for the same reason as SqlService
- Incode `///summary` statements - base summaries from the built-in Copilot auto complete, then proof read and modified
- README generation - documentation written with AI assistance based on source code

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
