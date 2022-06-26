using System;

namespace Telegram.Bot
{
    internal class Program
    {
        static void Main()
        {
            BotClient botClient = new BotClient("Token");
            botClient.Start();
        }
    }
}
