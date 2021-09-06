using GrigoriiBot.BOT.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;

namespace GrigoriiBot.Models.BOT
{
    public class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> CommandsList;

        public static IReadOnlyList<Command> Commands { get => CommandsList.AsReadOnly(); }

        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }

            client = new TelegramBotClient(AppSettings.Key);
            CommandsList = new List<Command>();
            CommandsList.Add(new HelloCommand());
            CommandsList.Add(new PayIt());
            CommandsList.Add(new Combine());
            CommandsList.Add(new WhatProp());
            CommandsList.Add(new PropRules());
            CommandsList.Add(new GetAccount());
            CommandsList.Add(new CheckPayments());
            string url = string.Format(AppSettings.Url, "secondmessage/update");
            await client.SetWebhookAsync(url);
            return client;
        }
    }
}