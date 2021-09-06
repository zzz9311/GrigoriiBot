using GrigoriiBot.BOT.Models;
using GrigoriiBot.Models.BOT.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;

namespace GrigoriiBot.BOT.Work
{
    public class AccountManager
    {
        public static AccountsModel Get(BotUser user)
        {
            using(BotContext db = new BotContext())
            {
                db.Users.Attach(user);
                AccountsModel Account = db.Accounts.Where(i=>!i.Used).First();
                if(Account is null)
                {
                    return Account;
                }
                Account.GetDate = DateTime.Now;
                Account.Used = true;
                Account.User = user;
                Account.PassingRequest = false; 
                db.SaveChanges();
                return Account;
            }
        }

        public static async Task Send(AccountsModel account, BotUser user, TelegramBotClient client)
        {
            if(user is null)
            {
                return;
            }

            if(account is null)
            {
                await client.SendTextMessageAsync(user.TId, "Аккаунты закончились, напишите администратору для выдачи вам аккаунта лично");
                await client.SendTextMessageAsync(322044387, "Аккаунты закончились, напишите администратору для выдачи вам аккаунта");
                return;
            }

            await client.SendTextMessageAsync(user.TId, $"Логин: {account.Login}\nПароль: {account.Password}");
        }

        public static void AddAccounts(List<AccountsModel> accounts)
        {
            if(accounts.Count==0)
            {
                return;
            }
            using(BotContext db = new BotContext())
            {
                db.Accounts.AddRange(accounts);
                db.SaveChanges();
            }
        }

        public static List<AccountsModel> GetAll(bool taken = false)
        {
            using(BotContext db = new BotContext())
            {
                return db.Accounts.Where(i=>i.Used == taken).ToList();
            }
        }

        public static List<AccountsModel> GetAll()
        {
            using (BotContext db = new BotContext())
            {
                return db.Accounts.ToList();
            }
        }
    }
}