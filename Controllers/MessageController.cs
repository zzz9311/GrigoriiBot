using GrigoriiBot.BOT.Commands;
using GrigoriiBot.BOT.Models;
using GrigoriiBot.Models.BOT;
using GrigoriiBot.Models.BOT.Contexts;
using GrigoriiBot.Models.BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GrigoriiBot.Controllers
{
    public class MessageController : ApiController
    {
        private readonly UserManager _UserManager;
        public MessageController()
        {
            _UserManager = new UserManager();
        }


        [HttpPost]
        [Route("secondmessage/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null)
            {
                return Ok();
            }
            BotUser user = new BotUser();
            TelegramBotClient client = await Bot.Get();
            IReadOnlyList<Command> CommandsList = Bot.Commands;
            var Data = await GetInformationFromUpdate(update, client);
            long TId = Data.Item2;
            string Text = Data.Item1;

            user = _UserManager.GetUserByTID(TId);
            if (user != null && user.Banned)
            {
                await client.SendTextMessageAsync(TId, "Вы забанены!");
                return Ok();
            }



            try
            {
                foreach (var el in CommandsList)
                {
                    if (el.Contains(Text))
                    {
                        await el.ExecuteAsync(update, client);
                        return Ok();
                    }
                }

                using (BotContext db = new BotContext())
                {
                    if (user == null)
                    {
                        return Ok();
                    }
                    db.Users.Attach(user);
                    switch (user.Status)
                    {
                        
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(322044387, ex.ToString());
            }
            return Ok();
        }

        public static async Task<(string, long)> GetInformationFromUpdate(Update updatee, TelegramBotClient client)
        {
            Message message;
            var update = updatee;
            string Text = "";
            long TId = 0;
            try
            {
                if (update.CallbackQuery?.Data != null || update.Message?.Text != null)
                {
                    Text = update.CallbackQuery == null ? update.Message.Text : update.CallbackQuery.Data;
                }

                if (update.CallbackQuery != null)
                {
                    TId = update.CallbackQuery.From.Id;
                }
                else if (update.Message != null)
                {
                    message = update.Message;
                    TId = message.From.Id;
                }
                Console.WriteLine(Text);
                return (Text, TId);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(322044387, ex.ToString());
                return ("", 0);
            }
        }
    }
}