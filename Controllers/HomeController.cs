using GrigoriiBot.BOT.Models;
using GrigoriiBot.BOT.Work;
using GrigoriiBot.BOT.Work.Scheduler;
using GrigoriiBot.Models.BOT;
using GrigoriiBot.Models.BOT.Contexts;
using GrigoriiBot.Models.BOT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrigoriiBot.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager _UserManager;
        public HomeController()
        {
            _UserManager = new UserManager();
        }

        public ActionResult Index()
        {
            List<BotUser> Users = _UserManager.GetAllUsers(banned: true);
            return View(Users);
        }

        private string BuildResultString((bool, string) tuple)
        {
            string Color = tuple.Item1 ? "Green" : "Red";
            string ResultString = $"<p class=\"font-weight-bold\" style=\"color: {Color}\">{tuple.Item2}</p>";
            return ResultString;
        }

        [HttpGet]
        public ActionResult EditUser(int tUserId)
        {
            BotUser _User = _UserManager.GetUserByTID(tUserId);
            if (_User == null)
            {
                return HttpNotFound();
            }
            return View(_User);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public string EditUser(BotUser user)
        {
            var Result = _UserManager.EditUser(user);
            string ResultString = BuildResultString(Result);
            return ResultString;
        }
        [HttpGet]
        public ActionResult SendMessages()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> SendMessages(string message)
        {
            List<BotUser> Users = _UserManager.GetAllUsers(false);
            var Client = await Bot.Get();
            int Counter = 0;
            foreach (var el in Users)
            {
                try
                {
                    await Client.SendTextMessageAsync(el.TId, message);
                    Counter++;
                }
                catch (Exception ex)
                {

                }
                await Task.Delay(35);
            }
            return BuildResultString((true, $"Было отправлено {Counter} сообщений"));
        }

        [AllowAnonymous]
        public async Task Scheduler()
        {
            await SchedulerManager.SendMessages();
        }

        [HttpGet]
        public ActionResult AddAccounts()
        {
            return View();
        }

        [HttpPost]
        public string AddAccounts(string accounts)
        {
            List<AccountsModel> Accounts = new List<AccountsModel>();
            foreach (var el in accounts.Split('\n'))
            {
                var Login = el.Split(':')[0];
                var Pass = el.Split(':')[1];
                Accounts.Add(new AccountsModel() { GetDate = DateTime.MaxValue, Login = Login, Used = false, User = null, Password = Pass, PassingRequest = false });
            }
            AccountManager.AddAccounts(Accounts);
            return BuildResultString((true, $"{Accounts.Count} Аккаунтов было добавлено"));
        }

        [HttpGet]
        public ActionResult TakenAccounts()
        {
            List<AccountsModel> Accounts = AccountManager.GetAll(taken: true);
            return View("AllAccounts", Accounts);
        }

        [HttpGet]
        public ActionResult AllAccounts()
        {
            List<AccountsModel> Accounts = AccountManager.GetAll();
            return View("AllAccounts", Accounts);
        }

        [HttpGet]
        public ActionResult NotTakenAccounts()
        {
            List<AccountsModel> Accounts = AccountManager.GetAll(taken: false);
            return View("AllAccounts", Accounts);
        }

        [HttpGet]
        public ActionResult Account(int id)
        {
            AccountsModel Account = AccountManager.Get(id);
            if (Account is null)
            {
                return HttpNotFound();
            }
            return View(Account);
        }

        [HttpPost]
        public string Account(AccountsModel model)
        {
            using (BotContext db = new BotContext())
            {
                try
                {
                    var Account = db.Accounts.Where(i => i.Id == model.Id).FirstOrDefault();
                    Account.Login = model.Login;
                    Account.PassingRequest = model.PassingRequest;
                    Account.Password = model.Password;
                    Account.Used = model.Used;
                    Account.GetDate = model.GetDate.Year == DateTime.MinValue.Year ? DateTime.MaxValue : model.GetDate;
                    if(model.User!=null)
                    {
                        if(model.User.TId!=0)
                        {
                            var User = _UserManager.GetUserByTID(model.User.TId);
                            if (User != null)
                            {
                                db.Users.Attach(User);
                                Account.User = User;
                            }
                        }
                        else
                        {
                            Account.User = null;
                        }
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "OK";
        }
    }
}