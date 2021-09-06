using GrigoriiBot.BOT.Models;
using GrigoriiBot.BOT.Work;
using GrigoriiBot.BOT.Work.Scheduler;
using GrigoriiBot.Models.BOT;
using GrigoriiBot.Models.BOT.Contexts;
using MeetingBot.Models.BOT.Qiwi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.BOT.Commands
{
    public class CheckPayments : Command
    {
        public override string Name => "Проверить оплату";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id);
            List<string> Data = message.CallbackQuery.Data.Split(';').ToList();
            long TId = message.CallbackQuery.From.Id;
            decimal MoneyToAdd = 0;
            int UserCode;
            BotUser User = _UserManager.GetUserByTID(TId);
            using (BotContext db = new BotContext())
            {
                Payments pay = db.Payments.Where(t => t.TId == TId).FirstOrDefault();
                if (pay == null)
                {
                    return;
                }
                UserCode = pay.Code;
            }
            string str1 = "";
            string Phone = ConfigurationManager.AppSettings.Get("Phone");
            string requestUriString = $"https://edge.qiwi.com/payment-history/v2/persons/{Phone}/payments?operation=IN&rows=50"; //CHANGE PHONE
            string str2 = ConfigurationManager.AppSettings.Get("QiwiToken");
            WebRequest webRequest = WebRequest.Create(requestUriString);
            webRequest.Headers.Add("Authorization", "Bearer " + str2);
            webRequest.ContentType = "application/json";
            WebResponse response = webRequest.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                    str1 = streamReader.ReadLine();
            }
            response.Close();
            foreach (var data in JsonConvert.DeserializeObject<deserjs>(str1).data)
            {
                if (data.comment != null && data.comment.ToString().Equals(UserCode.ToString()))
                {
                    using (BotContext db = new BotContext())
                    {
                        List<Payments> payments = db.Payments.ToList();
                        foreach (Payments el in payments.Where(i => i.Code == UserCode))
                        {
                            db.Payments.Remove(el);
                            MoneyToAdd += data.total.amount;
                        }

                        if (MoneyToAdd == 1000)
                        {
                            AccountsModel Account = AccountManager.Get(User);
                            await AccountManager.Send(Account, User, client);
                            SchedulerManager.AddToSchedule(User, DateTime.Now.AddDays(30));
                        }

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}