using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.BOT.Commands
{
    public class Combine : Command
    {
        public override string Name => "Combine";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "1) Длительность комбайна 20 торговых дней с даты оплаты\n2) Достижение необходимого профита(500$ Net, чистой прибыли)\n3) Соблюдение рисков(50$ на день, 250$ на месяц)\n4) Максимальный объем позиции в одной сделке 500 акций\n5) Минимальное количество торговых дней 15\n6) Результат минимум 7 - ми дней должен быть больше +10$ (Net)");
        }
    }
}