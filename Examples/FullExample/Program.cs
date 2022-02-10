using System;
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
            // Sending an alert event example
            await lumia.SendAlert(LumiaAlertValues.TWITCH_FOLLOWER);

            // Sending a command; with a callback to get the result for this call
            await lumia.SendCommand("red");

            RGB rgb = new RGB();
            rgb.r = 255;
            rgb.g = 0;
            rgb.b = 255;

            await lumia.SendColor(rgb, 60, 1000, 0, false, false, null);

            // Sending a brightness
            await lumia.SendBrightness(100, 0, false);

            // Sending a TTS message
            await lumia.SendTts("This SDK is the best", 100, "");

            // Sending a Chat bot message
            await lumia.SendChatbot(Platforms.TWITCH, "This SDK is the best");

            // Sending a raw event example
            ILumiaSendPack pack = new ILumiaSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaCommandTypes>(LumiaCommandTypes.ALERT);
            pack.params_ = new LumiaPackParams();
            pack.params_.value = LumiaUtils.getTypeValue<LumiaAlertValues>(LumiaAlertValues.TWITCH_FOLLOWER);
            await lumia.Send(pack);
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
                switch (LumiaUtils.getTypeValueFromString<LumiaEventTypes>("LumiaEventTypes", data["type"].Value<string>()))
                {
                    case LumiaEventTypes.STATES:
                        Console.WriteLine("States have been updated:  " + data.ToString());
                        break;

                    case LumiaEventTypes.COMMAND:
                        Console.WriteLine("A Chat Command is being triggered:  " + data.ToString());
                        break;

                    case LumiaEventTypes.CHAT:
                        Console.WriteLine("New chat message:  " + data.ToString());
                        break;

                    case LumiaEventTypes.ALERT:
                        Console.WriteLine("New alert:  " + data.ToString());
                        break;
                }
            };

            var r = await lumia.GetInfo();

            Console.WriteLine("get info result : " + r.ToString());

            await testSends(lumia);

        }
        public static void Main(string[] args)
        {
            run().GetAwaiter().GetResult();
        }
    }
}
