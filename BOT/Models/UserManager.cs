using GrigoriiBot.BOT.Models;
using GrigoriiBot.Models.BOT.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace GrigoriiBot.Models.BOT.Models
{
    public class UserManager : IUserManager
    {
        public List<BotUser> GetAllUsers(bool banned = false)
        {
            if (banned)
            {
                using (BotContext db = new BotContext())
                {
                    return db.Users.AsNoTracking().ToList();
                }
            }
            else
            {
                using (BotContext db = new BotContext())
                {
                    return db.Users.Where(b => !b.Banned).AsNoTracking().ToList();
                }
            }
        }

        public (bool, string) EditUser(BotUser user)
        {
            BotUser _User = GetUserByTID(user.TId);
            if (_User != null)
            {
                if ((_User.Name == null || user.Name.Equals(_User.Name)) && user.TId == _User.TId && user.FirstDate.Date.Equals(_User.FirstDate.Date))
                {
                    using (BotContext db = new BotContext())
                    {
                        user.Id = _User.Id;
                        user.FirstDate = _User.FirstDate;
                        user.LastSent = _User.LastSent;
                        
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return (true, "Пользователь изменен!");
                    }
                }
                else
                {
                    return (false, "Нельзя менять неизменяемые параметры!");
                }
            }
            return (false, "Неизвестная ошибка, попробуйте позже!");
        }

        public BotUser GetUserByTID(long tid)
        {
            using (BotContext db = new BotContext())
            {
                BotUser _User = db.Users.AsNoTracking().Where(i => i.TId == tid).FirstOrDefault();
                return _User;
            }
        }

        public bool IsUserBanned(long tid)
        {
            using (BotContext db = new BotContext())
            {
                BotUser _User = db.Users.Where(t => t.TId == tid).FirstOrDefault();
                return _User == null ? false : _User.Banned;
            }
        }


        public void SetStatusToUser(long tid, string status)
        {
            BotUser User = GetUserByTID(tid);
            if(User!=null)
            {
                using(BotContext db = new BotContext())
                {
                    db.Users.Attach(User);
                    User.Status = status;
                    db.SaveChanges();
                }
            }
        }

        public void SetStatusToUser(BotUser user, string status)
        {
            if (user != null)
            {
                using (BotContext db = new BotContext())
                {
                    db.Users.Attach(user);
                    user.Status = status;
                    db.SaveChanges();
                }
            }
        }
    }


    interface IUserManager
    {
        BotUser GetUserByTID(long tid);
        bool IsUserBanned(long tid);
        List<BotUser> GetAllUsers(bool banned = false);
        (bool, string) EditUser(BotUser user);
        void SetStatusToUser(long tid, string status);
        void SetStatusToUser(BotUser user, string status);
    }
}