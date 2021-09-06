using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrigoriiBot.Models.BOT.Contexts;

namespace GrigoriiBot.Models.BOT.Work
{
    public class GetPaymentCode
    {
        public static int GetCode(long id)
        {
            int Code;
            using (BotContext db = new BotContext())
            {
                Payments pay = db.Payments.Where(t => t.TId == id).FirstOrDefault();
                if (pay == null)
                {
                    Random rnd = new Random();
                    pay = new Payments();
                    pay.Code = rnd.Next(0, 1000000);
                    Code = pay.Code;
                    pay.TId = id;
                    db.Payments.Add(pay);
                    db.SaveChanges();
                }
                else
                {
                    Code = pay.Code;
                }
            }
            return Code;
        }
    }
}