using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity;
using dotNet_TWITTER.Infrastructure.Repository;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class SubscriptionsActions
    {
        private readonly ISubRepository _subRepository;
        public SubscriptionsActions(ISubRepository subRepository)
        {
            _subRepository = subRepository;
        }

        public async Task<Subscription> AddSubscription(string userId, string userName, string subUserName)
        {
            if (await _subRepository.SubUserExists(subUserName) & _subRepository.SubscriptionExists(userName, subUserName))
            {
                Subscription subscription =
                new Subscription
                {
                    SubscriptionId = Guid.NewGuid().ToString("N"),
                    userId = userId,
                    AuthUser = userName,
                    SubUser = subUserName
                };
                await _subRepository.Add(subscription);
                return subscription;
            }
            return null;
        }

        public async Task<List<Subscription>> ShowUserSubscriptions(string userName)
        {
            return await _subRepository.GetAuthSubscriptions(userName);
        }

        public async Task<string> RemoveSubscription(string userName, string subUserName)
        {
            await _subRepository.RemoveSub(userName, subUserName);
            return "Subscription was removed";
        }
    }
}
