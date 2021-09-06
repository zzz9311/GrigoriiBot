using GrigoriiBot.Models.BOT.Models;
using GrigoriiBot.BOT.Models;
using GrigoriiBot.Models.BOT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GrigoriiBot.Models.BOT.Contexts
{
    public class BotContext:DbContext
    {
        public BotContext() : base("DefaultConnection") { }
        public DbSet<BotUser> Users { get; set; }
        public DbSet<MessagesModel> Messages { get; set; }
        public DbSet<SendMessageModel> SentMessages { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<AccountsModel> Accounts { get; set; }
        public DbSet<Scheduler> Scheduler { get; set; }
    }
}