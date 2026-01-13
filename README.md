# StrikeLInk

**StrikeLInk** is a modern, cross-platform **.NET library** for integrating with **Counter-Strike 2** and related Valve ecosystem services.
It provides structured access to **chat events**, **game state changes**, **client services**, and **Valve configuration formats**, all using modern C# and .NET APIs.

> 📦 Available on **NuGet** as **StrikeLInk**

**Documentation**: [https://github.com/RiisDev/StrikeLink/blob/main/docs/index.md](https://github.com/RiisDev/StrikeLink/blob/main/docs/index.md)

![C#](https://img.shields.io/badge/-.NET%208.0-blueviolet?style=for-the-badge&logo=windows&logoColor=white)
[![Support Server](https://img.shields.io/discord/477201632204161025.svg?label=Discord&logo=Discord&colorB=7289da&style=for-the-badge)](https://discord.gg/yyuggrH)
![GitHub](https://img.shields.io/github/license/RiisDev/StrikeLink?style=for-the-badge)
![Nuget All Releases](https://img.shields.io/nuget/dt/StrikeLink?label=Nuget%20Downloads&style=for-the-badge)

---

## ✨ Features

* ✅ Modern **C#** targeting **.NET 8, 9, and 10**
* 🧩 Event-driven architecture
* 🎮 Counter-Strike 2 client & console integration
* 💬 Global & team chat handling
* 🗺️ Game state tracking (map, player, round)
* ⚙️ Valve config reader & writer (JSON-styled)
* 🌍 Cross-platform design *Built on windows, untested on linux*

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package StrikeLInk
```

Or via the NuGet Package Manager:

```powershell
Install-Package StrikeLInk
```

---

## 🧠 Core Concepts

StrikeLInk is structured around **services**, **events**, and **state integration**.
Consumers subscribe to events and invoke methods to interact with the game client.

---

## 💬 Chat Service

Provides access to in-game chat messages and sending capabilities.

### Events

* `OnTeamChat`
* `OnGlobalChat`

### Methods

* `SendChatAsync(NewChatMessage message)`

```csharp
public enum ChatChannel
{
	Team,
	Global
}

public record NewChatMessage(ChatChannel Channel, string Message);
```

---

## 🎮 Game State Integration

Track live game state changes through event-based updates.

Based on [GameStateInteraction](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) with help from [this reddit post](https://reddit.com/r/GlobalOffensive/comments/cjhcpy)

### Supported State Events

* **Gsi Listener**
  * Offers a base event `OnPostReceived` for all incoming GSI payloads

* **Map State**

  * `MapState` events
* **Player State**

  * `PlayerState` events
* **Round State**

  * `RoundState` events

These events allow real-time reactions to gameplay changes such as round transitions, player updates, and map changes.

---

## 🧩 Client Services

### 🟦 SteamService

Provides access to Steam and CS2 client data.

#### Methods

* `GetSteamPath()`
* `TryGetGamePath(int gameId, out string path)`
* `GetGamePath(int gameId)`
* `GetUserConfig(long? userId)`
* `GetGameLaunchOptions(int gameId)`
* `GetCurrentUserId()`

---

### 🖥️ ConsoleService (CS2 Console Integration)

Interact directly with the CS2 console and listen to runtime events.

#### Events

* `OnLogReceived`
* `OnPlayerConnected`
* `OnMapJoined`
* `OnGlobalChatMessageReceived`
* `OnTeamChatMessageReceived`
* `OnUiStateChanged`
* `OnAddonProgress`
* `OnAddonFinished`
* `OnServerJoining`
* `OnServerConnected`
* `OnServerDisconnected`

This service enables advanced automation, monitoring, and addon integration.

---

## ⚙️ Valve Config Reader / Writer

A flexible configuration system built around **`JsonDocument`-style APIs**.

### Supported Formats

* `.vcfg`
* `.cfg`
* `.vdf`
* `.acf`

### Capabilities

* Read and parse Valve configuration formats
* Modify values using structured JSON-style access
* Write configurations back to disk safely
* Designed for tooling, modding, and automation
