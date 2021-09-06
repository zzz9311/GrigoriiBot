using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingBot.Models.BOT.Qiwi
{
    public class deserjs
    {
        public List<Data> data { get; set; }

        public DateTime? nextTxnDate { get; set; }

        public long? nextTxnId { get; set; }
    }
}