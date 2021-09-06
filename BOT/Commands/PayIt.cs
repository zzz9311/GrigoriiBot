using GrigoriiBot.Models.BOT.Work;
using GrigoriiBot.Models2.BOT2.Work;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GrigoriiBot.BOT.Commands
{
    public class PayIt : Command
    {
        public override string Name => "PayIt";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            //await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id);
            long TId = message.CallbackQuery.From.Id;
            decimal Price = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("Cost"));
            GetUrlPay get = new GetUrlPay();
            int UserCode = GetPaymentCode.GetCode(TId);
            string Url = get.GetPayUrl(Price, UserCode);
            var inlineSelect = new InlineKeyboardMarkup(new[]
            {
                                            new[]
                                            {
                                                InlineKeyboardButton.WithUrl("💳 ОПЛАТИТЬ",Url),
                                            },
                                            new InlineKeyboardButton[]
                                            {
                                                InlineKeyboardButton.WithCallbackData("🔎 Проверить оплату",$"Проверить оплату")
                                            }
            });
            await client.SendTextMessageAsync(TId, $"Чтобы получить аккаунт вам нужно оплатить {Price} руб.",replyMarkup:inlineSelect);
        }
    }
}