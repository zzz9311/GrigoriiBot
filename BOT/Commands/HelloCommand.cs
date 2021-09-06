 using GrigoriiBot.Models.BOT.Contexts;
using GrigoriiBot.Models.BOT.Models;
using GrigoriiBot.BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GrigoriiBot.BOT.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/Start";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            BotUser User;
            using (BotContext db = new BotContext())
            {
                User = db.Users.Where(i => i.TId == message.Message.From.Id).FirstOrDefault();
                if (User == null)
                {
                    User = new BotUser();
                    User.FirstDate = DateTime.Now;
                    User.Name = message.Message.From.Username;
                    User.TId = message.Message.From.Id;
                    User.Status = "";
                    User.LastSent = DateTime.Now;
                    db.Users.Add(User);
                    db.SaveChanges();
                }
                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
                {
                         new InlineKeyboardButton[]
                         {
                            InlineKeyboardButton.WithCallbackData("Что такое проп",$"WhatProp")
                         },
                         new InlineKeyboardButton[]
                         {
                            InlineKeyboardButton.WithCallbackData("Условия пропа",$"PropRules")
                         },
                         new InlineKeyboardButton[]
                         {
                            InlineKeyboardButton.WithCallbackData("Условия прохождения трейдерского комбайна",$"Combine")
                         },
                         new InlineKeyboardButton[]
                         {
                            InlineKeyboardButton.WithCallbackData("Оплатить участие",$"PayIt")
                         }
                });
                await client.SendTextMessageAsync(User.TId, "*Пройди трейдерский комбайн и получи 20000$ в управление*", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,replyMarkup: keyboard);
            }
        }
    }
}