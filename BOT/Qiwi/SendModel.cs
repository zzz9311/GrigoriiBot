using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingBot.Models.BOT.Qiwi
{
    public class SendModel
    {
        public string id { get; set; }
        public sum sum { get; set; }
        public paymentMethod paymentMethod { get; set; }
        public string comment { get; set; }
        public fields fields { get; set; }
    }
    public class sum
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
    public class paymentMethod
    {
        public string type { get; set; }
        public string accountId { get; set; }
    }
    public class fields
    {
        public string account { get; set; }
    }


}