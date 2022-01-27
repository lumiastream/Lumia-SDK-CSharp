using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Lumia;

namespace Examples
{
	class SimpleListen
	{
		private static string token = ""; // Game token that you have retrieved from https://lumiastream.com/games/submit
		private static string name = ""; // Name of the game that will be prompted for the user to approve

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
		}
		public static void Main(string[] args)
		{
			run().GetAwaiter().GetResult();
		}
	}
}
