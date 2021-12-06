using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context, userManager, signInManager)
        {
        }

        public async Task<Object> RegisterUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task SignInUser(User user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<Object> PasswordSignInUser(string user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }
}
