using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public class SubRepository : GenericRepository<Subscription>, ISubRepository
    {
        public SubRepository(UserContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context, userManager, signInManager)
        {

        }

        public async Task<List<Subscription>> GetAuthSubscriptions(string userName)
        {
            var result = await _context.Subscriptions.Where(s => s.AuthUser == userName).ToListAsync();
            return result;
        }
        public async Task RemoveSub(string userName, string subUserName)
        {
            var sub = _context.Subscriptions.FirstOrDefault(x => x.AuthUser == userName && x.SubUser == subUserName);
            _context.Subscriptions.Remove(sub);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> SubUserExists(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return false;
            return true;
        }
        public bool SubscriptionExists(string userName, string subUserName)
        {
            Subscription subscription = _context.Subscriptions.FirstOrDefault(s => s.AuthUser == userName && s.SubUser == subUserName);
            if (subscription != null)
                return false;
            return true;
        }
    }
}
