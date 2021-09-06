using GrigoriiBot.BOT.Models;
using GrigoriiBot.BOT.Work;
using GrigoriiBot.BOT.Work.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.BOT.Commands
{
    public class GetAccount : Command
    {
        public override string Name => "/get";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var User = _UserManager.GetUserByTID(message.Message.From.Id);
            AccountsModel Account = AccountManager.Get(User);
            await AccountManager.Send(Account, User, client);
            SchedulerManager.AddToSchedule(User, DateTime.Now.AddDays(30));
        }
    }
}