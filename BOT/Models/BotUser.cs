using GrigoriiBot.BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrigoriiBot.BOT.Models
{
    public class BotUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public long TId { get; set; }
        public bool Banned { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastSent { get; set; }
    }
}