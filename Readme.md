# The official Lumia Stream SDK for C#

This repository is for the Lumia Stream SDK releases and documentation.

Developers can use the Lumia Stream SDK to extend and control the Lumia Stream desktop app, enabling them to control smart lights, MIDI, DMX, OSC, OBS, TTS and so much more to create a custom and unique lighting experience.

## Table of Contents

<!-- toc -->

- [The official Lumia Stream SDK for C](#the-official-lumia-stream-sdk-for-c)
  - [Table of Contents](#table-of-contents)
- [Installation](#installation)
    - [Self Build](#self-build)
    - [NuGet Gallery](#nuget-gallery)
- [Run the SDK](#run-the-sdk)
- [Sample](#sample)
- [Events](#events)
    - [States](#states)
    - [Chat Command](#chat-command)
    - [Chat](#chat)
    - [Alert](#alert)
- [Control](#control)
    - [Get Settings](#get-settings)
    - [Send Command](#send-command)
    - [Send Color](#send-color)
    - [Send Brightness](#send-brightness)
    - [Send TTS](#send-tts)
    - [Send Chat bot](#send-chat-bot)
    - [Send Chat Command](#send-chat-command)
    - [Send Chat](#send-chat)
    - [Send Alert](#send-alert)
  - [Resources](#resources)
  - [Let's link](#lets-link)

<!-- tocstop -->

# Installation

### Self Build ###

You should add your lumia-sdk.dll (e.g. `/path/to/lumia-sdk/bin/Debug/lumia-sdk.dll`) to the library references of your project.

If you would like to use that dll in your [Unity] project, you should add it to any folder of your project (e.g. `Assets/Plugins`) in the **Unity Editor**.

### NuGet Gallery ###

lumia-sdk is available on the [NuGet Gallery], as a **prerelease** version.

- [NuGet Gallery: lumia-sdk]

You can add lumia-sdk to your project with the NuGet Package Manager, by using the following command in the Package Manager Console.

    PM> Install-Package lumia-sdk -Pre

# Run the SDK

We've also included an example for using the SDK.

To run the example head to [examples](https://github.com/lumiastream/Lumia-SDK-CSharp/tree/main/examples) and you will see the `Progam.cs` file there.

Make sure you replace your token with the token that you will find in Lumia Stream's settings. Or if you're making a game, you should use the Game Token that you can create from [here](https://lumiastream.com/games/submit)
Here is how to [find your API token](https://dev.lumiastream.com/docs/get-a-token)

After you have your token and replaced it inside of the `Progam.cs`, you can now run the Program:

This will first initialize the sdk to create the connection. Then it will listen in on events that are coming through.

# Sample

The following snippet shows a valid sdk example

```c#
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Lumia;

namespace Examples
{
	class Program
	{
		private static string token = ""; // Game token that you have retrieved from https://lumiastream.com/games/submit
		private static string name = ""; // Name of the game that will be prompted for the user to approve

		public static async Task testSends(LumiaSdk lumia)
		{
			// Sending a command; with a callback to get the result for this call
			await lumia.SendCommand("red", null, null);

			RGB rgb = new RGB();
			rgb.r = 255;
			rgb.g = 0;
			rgb.b = 255;

			await lumia.SendColor(rgb, 60, 1000);

			// Sending a brightness
			await lumia.SendBrightness(100);

			// Sending a TTS message
			await lumia.SendTts("This SDK is the best");

			// Sending a Chat bot message
			await lumia.SendChatbot(Platforms.TWITCH, "This SDK is the best");
		}

		public static async Task run()
		{
			LumiaSdk lumia = new LumiaSdk();
			await lumia.init(token, name);

			lumia.error += (string r) =>
			{};

			lumia.closed += (string r) =>
			{};


			lumia.events += (JObject data) =>
			{
							// here we give the context as we know it's an SDK Eent types
							switch (LumiaUtils.getTypeValueFromString<LumiaSdkEventTypes>("LumiaSdkEventTypes", data["type"].Value<string>()))
				{
					case LumiaSdkEventTypes.STATES:
						break;

					case LumiaSdkEventTypes.COMMAND:
						break;

					case LumiaSdkEventTypes.CHAT:
						break;

					case LumiaSdkEventTypes.ALERT:
						break;
				}
			};

			var r = await lumia.GetInfo();
			await testSends(lumia);
		}
		public static void Main(string[] args)
		{
			run().GetAwaiter().GetResult();
		}
	}
}
```

# Events

Events are broadcasted by Lumia Stream to each connected client when an action occurs inside Lumia Stream.

These events range from the app state being changed, raw chat messages, chat commands, alerts and much more.

An event message will contain at least the following base fields:

-   `type` _LumiaSdkEventTypes_: the type of event.
-   `origin` _EventOrigins (optional)_: where the event originated from. i.e: twitch for example
-   `data` _ILumiaSdkEventStateBody | ILumiaSdkEventChatCommandBody | ILumiaSdkEventChatBody | ILumiaSdkEventAlertBody | ILumiaSdkEventStateBody (optional)_: the body of the event

Additional fields may be present in the event message depending on the event type.

### States

States indicate the current status the Lumia Stream is in

**Example Response:**

```json
{
    "origin": null,
    "type": "states",
    "data": { "on": 1, "streamMode": 1, "fuze": 0 }
}
```

---

### Chat Command

Lumia Stream has been prompted to trigger a command

**Example Response:**

```json
{
    "origin": "twitch",
    "type": "command",
    "data": { "username": "lumiastream", "command": "red" },
    "raw": { "username": "lumiastream", "command": "red" }
}
```

---

### Chat

A raw chat message that has been sent

**Example Response (From Twitch):**

```json
{
    "type": "chat",
    "data": {
        "channel": "#lumiastream",
        "message": "Wow",
        "username": "lumiastream",
        "userId": "163366031",
        "userColor": "#8A2BE2",
        "userColorRgb": "138,43,226",
        "platform": "twitch",
        "badgesRaw": "broadcaster/1,subscriber/0",
        "hasEmotes": false,
        "emotes": "",
        "rawMessageWithoutEmotes": "Wow",
        "emotesRaw": "",
        "user": {
            "badge-info": [],
            "badges": [],
            "client-nonce": "",
            "color": "#8A2BE2",
            "display-name": "lumiastream",
            "emotes": null,
            "first-msg": false,
            "flags": null,
            "id": "188ebc3d-e6e2-4b36-a125-0c4f0c0f54fd",
            "mod": false,
            "room-id": "163366031",
            "subscriber": true,
            "turbo": false,
            "user-id": "163366031",
            "user-type": null,
            "emotes-raw": null,
            "badge-info-raw": "subscriber/21",
            "badges-raw": "broadcaster/1,subscriber/0",
            "username": "lumiastream",
            "message-type": "chat",
            "isSelf": false,
            "vip": false,
            "tier3": true,
            "tier2": true,
            "tier1": true,
            "follower": false
        }
    }
}
```

---

### Alert

A raw alert has been sent

**Example Response (From Twitch Follow):**

```json
{
    "origin": "twitch",
    "type": "alert",
    "event": "twitch-follower",
    "data": { "userId": "12345", "username": "lumiastream" },
    "raw": { "userId": "12345", "username": "lumiastream" }
}
```

# Control

You can control Lumia Stream as well through the SDK in a variety of ways.

You will have the ability to send a command, an alert, a chat bot message, tts, a direct color, as well as even triggering certain lights.

### Get Settings

There are a few reasons why you may want to receive settings from Lumia Stream.
These settings will include all of the lights that are connected to Lumia, the current state of the app, whether the user is premium or not and more

**Example:**

```c#
const info = await sdk.GetInfo();
```

---

### Send Command

The simplest way to use Lumia is first setting up a command inside of Lumia Stream and then recalling it with the SDK

**Example:**

```c#
await lumia.SendCommand("red", null, null);
```

---

### Send Color

Sending a color gives you the ability to set your lights to whatever color you choose along with the brightness and the duration. The duration is so that Lumia can revert back to defaul after the determined time

**Example:**

```c#
RGB rgb = new RGB();
rgb.r = 255;
rgb.g = 0;
rgb.b = 255;

await lumia.SendColor(rgb, 60, 1000);
```

---

Using the same sendColor method you can also choose which lights receive the color change

**Example:**

```c#
RGB rgb = new RGB();
rgb.r = 255;
rgb.g = 0;
rgb.b = 255;
List lights;

await lumia.SendColor(rgb, 60, 1000, null, null, null, lights);
```

---

### Send Brightness

Sending brightness alone will keep all of your lights at their current state while only updating the brightness value

**Example:**

```c#
await lumia.SendBrightness(100);
```

---

### Send TTS

Sending TTS messages will give you the ability to use Lumia's TTS by just caling the sendTts method

**Example:**

```c#
await lumia.SendTts("This SDK is the best");
```

---

### Send Chat bot

Sending a Chat bot messages will allow you to send messages to chat through Lumia's Chat bot

**Example:**

```c#
await lumia.SendChatbot(Platforms.TWITCH, "This SDK is the best");
```

---

### Send Chat Command

Lumia Stream has been prompted to trigger a command

**Example Response:**

```json
{
    "origin": "twitch",
    "type": "command",
    "data": { "username": "lumiastream", "command": "red" },
    "raw": { "username": "lumiastream", "command": "red" }
}
```

---

### Send Chat

A raw chat message that has been sent

**Example Response (From Twitch):**

```json
{
    "type": "chat",
    "data": {
        "channel": "#lumiastream",
        "message": "Wow",
        "username": "lumiastream",
        "userId": "163366031",
        "userColor": "#8A2BE2",
        "userColorRgb": "138,43,226",
        "platform": "twitch",
        "badgesRaw": "broadcaster/1,subscriber/0",
        "hasEmotes": false,
        "emotes": "",
        "rawMessageWithoutEmotes": "Wow",
        "emotesRaw": "",
        "user": {
            "badge-info": [],
            "badges": [],
            "client-nonce": "",
            "color": "#8A2BE2",
            "display-name": "lumiastream",
            "emotes": null,
            "first-msg": false,
            "flags": null,
            "id": "188ebc3d-e6e2-4b36-a125-0c4f0c0f54fd",
            "mod": false,
            "room-id": "163366031",
            "subscriber": true,
            "turbo": false,
            "user-id": "163366031",
            "user-type": null,
            "emotes-raw": null,
            "badge-info-raw": "subscriber/21",
            "badges-raw": "broadcaster/1,subscriber/0",
            "username": "lumiastream",
            "message-type": "chat",
            "isSelf": false,
            "vip": false,
            "tier3": true,
            "tier2": true,
            "tier1": true,
            "follower": false
        }
    }
}
```

---

### Send Alert

Send a mock alert

**Example:**

```c#
await sdk.SendAlert({ alert: LumiaSDKAlertValues.TWITCH_FOLLOWER });
```

---

## Resources

-   [Download the latest Lumia Stream SDK release from nuget](https://www.nuget.org/packages/lumia-sdk/)
-   [Read the full API reference](https://dev.lumiastream.com)
-   [Browse some examples](https://github.com/lumiastream/Lumia-SDK-CSharp/tree/development/examples)

## Let's link

Reach out to us on Discord to show off what you're working on, or to just lounge around and speak about ideas [Link](https://discord.gg/R8rCaKb)
