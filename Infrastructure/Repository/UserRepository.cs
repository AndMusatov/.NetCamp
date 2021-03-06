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

        public async Task RemoveUser(User user)
        {
            List<Post> posts = _context.Post.Where(p => p.UserName == user.UserName).ToList();
            _context.Post.RemoveRange(posts);
            List<Comment> comments = _context.Comment.Where(c => c.UserName == user.UserName).ToList();
            _context.Comment.RemoveRange(comments);
            List<Subscription> Subscriptions = _context.Subscriptions.Where(s => s.AuthUser == user.UserName).ToList();
            _context.Subscriptions.RemoveRange(Subscriptions);
            await _userManager.DeleteAsync(user);
        }

        public async Task<Object> RegisterUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<Object> GoogleRegisterUser(User user)
        {
            return await _userManager.CreateAsync(user);
        }
        public async Task SignInUser(User user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<Object> PasswordSignInUser(string user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            return user;
        }
    }
}
