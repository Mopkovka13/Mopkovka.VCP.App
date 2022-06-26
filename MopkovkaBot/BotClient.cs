using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot
{
    internal class BotClient
    {
        private string _token;
        private TelegramBotClient _botClient;

        public BotClient(string token)
        {
            _token = token;
            _botClient = new TelegramBotClient(_token) { Timeout = TimeSpan.FromSeconds(10)};
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            chooseMenu(messageText, chatId, botClient, cancellationToken);


        }
        static ReplyKeyboardMarkup GetMenu()
        {

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "One", "Two" },
                new KeyboardButton[] { "Three", "Four" },
            })
            {
                ResizeKeyboard = true
            };
            return replyKeyboardMarkup;
        }
        async static void chooseMenu(string message, long chatId, ITelegramBotClient botClient,CancellationToken cancellationToken)
        {
            if (message == "Привет")
            {
                Message response = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Hello, World!",
                cancellationToken: cancellationToken);
            }
            else if (message == "Стикер")
            {
                Message response2 = await botClient.SendStickerAsync(
                chatId: chatId,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
                cancellationToken: cancellationToken);
            }
            else if (message == "Картинка")
            {
                Message response4 = await botClient.SendPhotoAsync(
                chatId: chatId,
                photo: "https://catchsuccess.ru/wp-content/uploads/4/a/d/4ad8fae2e57ce31b8ec6de6801924309.jpeg",
                caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
            }
            else if (message == "Музыка")
            {
                Message response3 = await botClient.SendAudioAsync(
                chatId: chatId,
                audio: "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3",
                /*
                performer: "Joel Thomas Hunger",
                title: "Fun Guitar and Ukulele",
                duration: 91, // in seconds
                */
                cancellationToken: cancellationToken);
            }
            else
            {
                Message response = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ты что блядь меню не видишь ?",
                replyMarkup: GetMenu(),
                cancellationToken: cancellationToken);
            }

        }
        public void Start()
        {
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken: cts.Token);

            Console.ReadLine();
        }
        
    }
}
