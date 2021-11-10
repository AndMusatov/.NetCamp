using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Domain.Events
{
    public class SubscriptionsActions
    {
        private UserContext _context;
        public SubscriptionsActions(UserContext context)
        {
            _context = context;
        }

        public string AddSubscription(string authUserName, string subUserName)
        {
            User user = _context.UsersDB.FirstOrDefault(u => u.UserName == authUserName);
            List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.AuthUser == authUserName).ToList();
            if (subscriptions.Find(s => s.SubUser == subUserName) != null)
            {
                return "You are alredy subscriped";
            }
            _context.Subscriptions.Add(
                new Subscription
                {
                    UserId = user.UserId,
                    AuthUser = user.UserName,
                    SubUser = subUserName
                }
                );
            _context.SaveChanges();
            return "Subscriped";
        }

        public List<Subscription> ShowUserSubscriptions(string userName)
        {
            return _context.Subscriptions.Where(s => s.AuthUser == userName).ToList();
        }

        public string RemoveSubscription(string authUserName, string subUserName)
        {
            var sub = _context.Subscriptions.FirstOrDefault(x => x.AuthUser == authUserName && x.SubUser == subUserName);
            _context.Subscriptions.Remove(sub);
            _context.SaveChanges();
            return "Subscription was removed";
        }
    }
}
