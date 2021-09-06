using GrigoriiBot.BOT.Models;
using GrigoriiBot.Models.BOT;
using GrigoriiBot.Models.BOT.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GrigoriiBot.BOT.Work.Scheduler
{
    public class SchedulerManager
    {
        public static void AddToSchedule(BotUser user, DateTime timeToSend)
        {
            using(BotContext db = new BotContext())
            {
                db.Users.Attach(user);
                db.Scheduler.Add(new Models.Scheduler() { SendTime = timeToSend, UserToSend = user });
                db.SaveChanges();
            }
        }

        public static async Task SendMessages()
        {
            using (BotContext db = new BotContext())
            {
                var Client = await Bot.Get();
                List<Models.Scheduler> Messages = db.Scheduler.Include("UserToSend").Where(i => i.SendTime < DateTime.Now).ToList();
                foreach(var el in Messages)
                {
                    await Client.SendTextMessageAsync(el.UserToSend.TId,"message");
                }
            }
        }
    }
}