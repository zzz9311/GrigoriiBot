
using System.Configuration;

namespace GrigoriiBot.Models.BOT
{
    public class AppSettings
    {
        public static string Url { get; set; } = ConfigurationManager.AppSettings.Get("Host");

        public static string Name { get; set; } = "";

        public static string Key { get; set; } = ConfigurationManager.AppSettings.Get("BotToken");
    }
}