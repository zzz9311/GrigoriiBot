using GrigoriiBot.Models.BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GrigoriiBot.BOT.Commands
{
    public abstract class Command
    {
        public UserManager _UserManager;
        public Command()
        {
            _UserManager = new UserManager();
        }
        public abstract string Name { get; }

        public abstract Task ExecuteAsync(Update message, TelegramBotClient client);

        public bool Contains(string message)
        {
            return message.ToLower().Contains(Name.ToLower());
        }
    }
}