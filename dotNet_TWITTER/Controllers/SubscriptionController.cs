using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private UserContext _context;
        private readonly UserManager<User> _userManager;
        public SubscriptionController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("UserSubscribe")]
        public async Task<ActionResult> Subscribe(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context, _userManager);
            return Ok(await subscriptionsActions.AddSubscription(User.FindFirstValue(ClaimTypes.NameIdentifier), subUserName));
        }

        [HttpGet("ShowSubscriptions")]
        public ActionResult ShowSubscriptions()
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context, _userManager);
            return Ok(subscriptionsActions.ShowUserSubscriptions(User.FindFirstValue(ClaimTypes.Name)));
        }

        [HttpDelete("RemoveSubscription")]
        public async Task<ActionResult> RemoveSubscription(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context, _userManager);
            return Ok(await subscriptionsActions.RemoveSubscription(User.FindFirstValue(ClaimTypes.Name), subUserName));
        }
    }
}
