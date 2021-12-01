using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class SubscriptionsActions
    {
        private UserContext _context;
        private readonly UserManager<User> _userManager;
        public SubscriptionsActions(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> AddSubscription(string userId, string subUserName)
        {
            User user = await _userManager.FindByIdAsync(userId);
            User userSub = await _userManager.FindByNameAsync(subUserName);
            List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.AuthUser == user.UserName).ToList();
            if (subscriptions.Find(s => s.SubUser == subUserName) != null)
            {
                return "You are alredy subscriped";
            }
            _context.Subscriptions.Add(
                new Subscription
                {
                    SubscriptionId = Guid.NewGuid().ToString("N"),
                    userId = user.Id,
                    AuthUser = user.UserName,
                    SubUser = subUserName,
                    user = user
                }
                );
            await _context.SaveChangesAsync();
            return "Subscriped";
        }

        public List<Subscription> ShowUserSubscriptions(string userName)
        {
            return _context.Subscriptions.Where(s => s.AuthUser == userName).ToList();
        }

        public async Task<string> RemoveSubscription(string userName, string subUserName)
        {
            var sub = _context.Subscriptions.FirstOrDefault(x => x.AuthUser == userName && x.SubUser == subUserName);
            _context.Subscriptions.Remove(sub);
            await _context.SaveChangesAsync();
            return "Subscription was removed";
        }
    }
}
