using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrigoriiBot.BOT.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }
        public BotUser User { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime GetDate { get; set; }
        public bool Used { get; set; }
        public bool PassingRequest { get; set; }
    }
}