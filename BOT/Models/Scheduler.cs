using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrigoriiBot.BOT.Models
{
    public class Scheduler
    {
        public int Id { get; set; }
        public DateTime SendTime { get; set; }
        public BotUser UserToSend { get; set; }
    }
}