using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingBot.Models.BOT.Qiwi
{
    public class features
    {
        public bool chequeReady { get; set; }

        public bool bankDocumentReady { get; set; }

        public bool regularPaymentEnabled { get; set; }

        public bool bankDocumentAvailable { get; set; }

        public bool repeatPaymentEnabled { get; set; }

        public bool favoritePaymentEnabled { get; set; }

        public bool chatAvailable { get; set; }

        public bool greetingCardAttached { get; set; }
    }
}