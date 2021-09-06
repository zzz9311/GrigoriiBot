using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.BOT.Commands
{
    public class PropRules : Command
    {
        public override string Name => "PropRules";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "После прохождения челленджа трейдер попадает на 1 уровень пропа и получает 20 000$ в управление.\nС подробными условиями данного уровня и условиями перехода с уровня на уровень, можно познакомиться здесь(ссылка на презентацию)");
        }
    }
}