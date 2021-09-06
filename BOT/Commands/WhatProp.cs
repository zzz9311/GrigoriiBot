using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.BOT.Commands
{
    public class WhatProp : Command
    {
        public override string Name => "WhatProp";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Проп-трейдинг (Proprietary trading) – частное лицо или компания, которые обучают с нуля или привлекают успешных трейдеров для управления их капиталом на бирже. Полученный доход от торговли делится между компанией и трейдером.\nБолее подробно о плюсах пропа можно ознакомится здесь(ссылка на сайт)");
        }
    }
}