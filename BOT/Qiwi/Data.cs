using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingBot.Models.BOT.Qiwi
{
    public class Data
    {
        public string account { get; set; }

        public string comment { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public commission commission { get; set; }

        public summ summ { get; set; }

        public Decimal currencyRate { get; set; }

        public DateTime date { get; set; }

        public string error { get; set; }

        public int errorCode { get; set; }

        public long personId { get; set; }

        public provider provider { get; set; }

        public source source { get; set; }

        public string statusText { get; set; }

        public total total { get; set; }

        public string trmTxnId { get; set; }

        public long txnId { get; set; }

        public features features { get; set; }

        public view view { get; set; }
    }
}