using System;
using System.Threading.Tasks;
using Lumia;

namespace Examples
{
	class SimpleSend
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

			// Sending a command
			await lumia.SendCommand("red");

			RGB rgb = new RGB();
			rgb.r = 255;
			rgb.g = 0;
			rgb.b = 255;

			// You can also send a raw color
			//await lumia.SendColor(rgb, 60, 1000, 0, false, false, null);
		}
		public static void Main(string[] args)
		{
			run().GetAwaiter().GetResult();
		}
	}
}
