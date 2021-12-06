using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public class PostsRepository : GenericRepository<Post>, IPostsRepository
    {
        public PostsRepository(UserContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context, userManager, signInManager)
        {
        }
        public async Task<List<Post>> GetAuthPosts(string userName)
        {
            List<Post> result = await _context.Post
                .Where(p => p.UserName == userName)
                .ToListAsync();
            return result;
        }

        public async Task<List<Post>> GetSubPosts(string userName)
        {
            List<Post> posts = new List<Post>();
            List<Subscription> subscriptions = await _context.Subscriptions.Where(s => s.AuthUser == userName).ToListAsync();
            foreach (var sub in subscriptions)
            {
                List<Post> authPosts = await _context.Post.Where(p => p.UserName == sub.SubUser).ToListAsync();
                posts.AddRange(authPosts);
            }
            return posts;
        }

        public async Task<List<Post>> GetUserPosts(string userName)
        {
            var result = _context.Post
                .Where(p => p.UserName == userName);
            return await result.ToListAsync();
        }

        public async Task RemoveUserPosts(string userName)
        {
            List<Post> posts = await _context.Post.Where(p => p.UserName == userName).ToListAsync();
            _context.Post.RemoveRange(posts);
            await _context.SaveChangesAsync();
        }
    }
}
