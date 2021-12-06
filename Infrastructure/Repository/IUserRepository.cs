using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<Object> RegisterUser(User user, string password);
        Task SignInUser(User user);
        Task<Object> PasswordSignInUser(string user, string password);
    }
}
