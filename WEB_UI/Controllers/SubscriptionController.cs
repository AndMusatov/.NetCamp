using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubRepository _subRepository;
        public SubscriptionController(ISubRepository subRepository)
        {
            _subRepository = subRepository;
        }

        [HttpPost("UserSubscribe")]
        public async Task<ActionResult> Subscribe(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_subRepository);
            Subscription subscription = await subscriptionsActions.AddSubscription(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Name), subUserName);
            if (subscription == null)
            {
                return BadRequest();
            }
            return Ok(subscription);
        }

        [HttpGet("ShowSubscriptions")]
        public async Task<ActionResult> ShowSubscriptions()
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_subRepository);
            return Ok(await subscriptionsActions.ShowUserSubscriptions(User.FindFirstValue(ClaimTypes.Name)));
        }

        [HttpDelete("RemoveSubscription")]
        public async Task<ActionResult> RemoveSubscription(string subUserName)
        {
            SubscriptionsActions subscriptionsActions = new SubscriptionsActions(_subRepository);
            return Ok(await subscriptionsActions.RemoveSubscription(User.FindFirstValue(ClaimTypes.Name), subUserName));
        }
    }
}
