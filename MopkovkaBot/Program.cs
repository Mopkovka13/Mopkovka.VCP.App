using System;

namespace Telegram.Bot
{
    internal class Program
    {
        static void Main()
        {
            BotClient botClient = new BotClient("5245125821:AAH2zl3rPhKDLD_1ljtmoLI6z1MEx3TTVv0");
            botClient.Start();
        }
    }
}
