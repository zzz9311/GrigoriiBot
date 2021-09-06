using Qiwi.BillPayments.Model.Out;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;


namespace GrigoriiBot.Models2.BOT2.Work
{
    public class GetUrlPay
    {
        public string GetPayUrl(decimal cost,int comment)
        {

            string str1 = "";
            string GuId = Guid.NewGuid().ToString();
            string requestUriString = $"https://api.qiwi.com/partner/bill/v1/bills/{GuId}";
            string str2 = ConfigurationManager.AppSettings.Get("QiwiSecretToken"); //code
            var webRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            webRequest.Headers.Add("Authorization", "Bearer " + str2 + " ");
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";
            webRequest.Method = "PUT";
            string stringtime = DateTime.Now.AddDays(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "+03:00";
            stringtime = stringtime.Replace(' ', 'T');
            Money m = new Money();
            m.amount = new amount();
            m.amount.currency = "RUB";
            m.amount.value = cost;
            m.comment = comment.ToString();
            m.expirationDateTime = stringtime;



            string json;
            json = new JavaScriptSerializer().Serialize(m);
            byte[] array = System.Text.Encoding.UTF8.GetBytes(json);


            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(array, 0, array.Length);
            }


            WebResponse response = webRequest.GetResponse();
            using (HttpWebResponse s = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var reader = new StreamReader(s.GetResponseStream()))
                {
                    str1 = reader.ReadToEnd();
                }
            }
            response.Close();
            BillResponse responsee = new JavaScriptSerializer().Deserialize<BillResponse>(str1);
            string url = responsee.PayUrl.ToString();
            return url;
        }
        public class Money
        {
            public amount amount { get; set; }
            public string comment { get; set; }
            public string expirationDateTime { get; set; }

        }
        public class amount
        {
            public string currency { get; set; }
            public decimal value { get; set; }
        }
    }
}
