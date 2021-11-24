using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Domain.Events
{
    public class PostsActions
    {
        private readonly UserManager<User> _userManager;
        private UserContext _context;

        public PostsActions(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Post> GetPosts(string userName)
        {
            var result = _context.Post.Where(p => p.UserName == userName);
            return result.ToList();
        }

        public async Task<Post> AddPost(string filling, string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (CanCreatePost(filling))
            {
                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString("N"),
                    UserName = user.UserName,
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Filling = filling,
                    User = user
                };
                _context.Post.Add(post);
                _context.SaveChanges();
                return post;
            }
            return null;
        }

        public async Task<string> DeletePost(string postId, string userName)
        {
           if (PostExists(postId))
           {
                var post = _context.Post.Single(x => x.UserName == userName && x.PostId == postId);
                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
                return "This post is deleted";
           }
            return "This post doesn`t exist";
        }

        public bool CanCreatePost(string filling)
        {
            if (string.IsNullOrEmpty(filling))
            {
                return false;
            }
            return true;
        }

        public bool PostExists(string postId)
        {
            Post post = _context.Post.FirstOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return false;
            }
            return true;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Post.ToList();
        }

        public List<Post> GetAllSubPosts(string eMail)
        {
            /*User user = _context.UsersDB.FirstOrDefault(u => u.EMail == eMail);
            List<Post> posts = new List<Post>();
            List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.AuthUser == user.UserName).ToList();
            foreach (var sub in subscriptions)
            {
                List<Post> authPosts = _context.Post.Where(p => p.UserName == sub.SubUser).ToList();
                posts.AddRange(authPosts);
            }
            posts.Sort((ps1, ps2) => DateTime.Compare(ps1.Date, ps2.Date));
            return posts;*/
            return null;
        }
    }
}
