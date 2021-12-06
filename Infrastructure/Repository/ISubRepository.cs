using dotNet_TWITTER.Applications.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public interface ISubRepository : IGenericRepository<Subscription>
    {
        Task<List<Subscription>> GetAuthSubscriptions(string userName);
        Task RemoveSub(string userName, string subUserName);
        Task<bool> SubUserExists(string userName);
        bool SubscriptionExists(string userName, string subUserName);
    }
}
