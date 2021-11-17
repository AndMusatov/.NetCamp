using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotNet_TWITTER.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private UserContext _context;
        public SubscriptionController(UserContext context)
        {
            _context = context;
        }

        [HttpPost("UserSubscribe")]
        public ActionResult Subscribe(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context);
            return Ok(subscriptionsActions.AddSubscription(User.FindFirstValue(ClaimTypes.Email), subUserName));
        }

        [HttpGet("ShowSubscriptions")]
        public ActionResult ShowSubscribes()
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context);
            return Ok(subscriptionsActions.ShowUserSubscriptions(User.FindFirstValue(ClaimTypes.Email)));
        }

        [HttpDelete("RemoveSubscription")]
        public ActionResult RemoveSubscription(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_context);
            return Ok(subscriptionsActions.RemoveSubscription(User.FindFirstValue(ClaimTypes.Email), subUserName));
        }
    }
}
