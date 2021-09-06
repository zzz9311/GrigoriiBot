using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrigoriiBot.BOT.Models
{
    public class SendMessageModel
    {
        public int Id { get; set; }
        public long TId { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public DateTime SentTime { get; set; }
    }
}