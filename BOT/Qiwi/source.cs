using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingBot.Models.BOT.Qiwi
{
    public class source
    {
        public string description { get; set; }

        public List<extras> extras { get; set; }

        public int id { get; set; }

        public string keys { get; set; }

        public string logoUrl { get; set; }

        public string longName { get; set; }

        public string shortName { get; set; }

        public string siteUrl { get; set; }
    }
}