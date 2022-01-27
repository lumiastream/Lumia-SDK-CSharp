﻿
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace lumia
{
    class LumiaSdkExample1
    {
        private static string token = "4enrnh84scr54d36kyy2"; // Game token that you have retrieved from https://lumiastream.com/games/submit
        private static string name = ""; // Name of the game that will be prompted for the user to approve

        public static async Task testSends(LumiaSdk lumia)
        {
            // Sending an alert event example
            await lumia.sendAlert(LumiaSDKAlertValues.TWITCH_FOLLOWER);

            // Sending a command; with a callback to get the result for this call
            await lumia.sendCommand("red", null, null);

            RGB rgb = new RGB();
            rgb.r = 255;
            rgb.g = 0;
            rgb.b = 255;

            await lumia.sendColor(rgb, 60, 1000);

            // Sending a brightness
            await lumia.sendBrightness(100);

            // Sending a TTS message
            await lumia.sendTts("This SDK is the best");

            // Sending a Chat bot message
            await lumia.sendChatbot(Platforms.TWITCH, "This SDK is the best");

            // Sending a raw event example
            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.ALERT;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = LumiaSDKAlertValues.TWITCH_FOLLOWER;
            await lumia.send(pack);
        }
        public static async Task run()
        {
            LumiaSdk lumia = new LumiaSdk();
            await lumia.init(token, name);

            lumia.error += (string r) =>
            {
                Console.WriteLine("error : " + r);
            };

            lumia.closed += (string r) =>
            {
                Console.WriteLine("closed : " + r);
            };


            lumia.events += (JObject data) =>
            {
                Console.WriteLine("Event data : " + data.ToString());


                // here we give the context as we know it's an SDK Eent types
                switch (LumiaUtils.getTypeValueFromString<LumiaSdkEventTypes>("LumiaSdkEventTypes", data["type"].Value<string>()))
                {
                    case LumiaSdkEventTypes.STATES:
                        Console.WriteLine("States have been updated:  " + data.ToString());
                        break;

                    case LumiaSdkEventTypes.COMMAND:
                        Console.WriteLine("A Chat Command is being triggered:  " + data.ToString());
                        break;

                    case LumiaSdkEventTypes.CHAT:
                        Console.WriteLine("New chat message:  " + data.ToString());
                        break;

                    case LumiaSdkEventTypes.ALERT:
                        Console.WriteLine("New alert:  " + data.ToString());
                        break;
                }
            };

            var r = await lumia.getInfo();

            Console.WriteLine("get info result : " + r.ToString());

            await testSends(lumia);

        }
        static void Main(string[] args)
        {
            run().GetAwaiter().GetResult();
        }
    }
}