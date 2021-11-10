using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Domain.Events
{
    public class PostsActions
    {
        private UserContext _context;

        public PostsActions(UserContext context)
        {
            _context = context;
        }

        public List<Post> GetPosts(string userName)
        {
            var result = _context.Post.Where(p => p.UserName == userName);
            return result.ToList();
        }

        public string AddPost(string filling, string userName)
        {
            if (CanCreatePost(filling))
            {
                User user = _context.UsersDB.FirstOrDefault(u => u.UserName == userName);
                _context.Post.Add(
                new Post
                {
                    UserName = user.UserName,
                    UserId = user.UserId,
                    Date = DateTime.Now,
                    Filling = filling, 
                    User = user
                }
                );
                _context.SaveChanges();
                return ("Input is Ok");
            }
            return "Input is wrong";
        }

        public string DeletePost(int postId, string userName)
        {
            if (PostExists(postId))
            {
                var post = _context.Post.Single(x => x.UserName == userName && x.PostId == postId);
                _context.Post.Remove(post);
                _context.SaveChanges();
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

        public bool PostExists(int postId)
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

        public List<Post> GetAllSubPosts(string authUserName)
        {
            List<Post> posts = new List<Post>();
            List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.AuthUser == authUserName).ToList();
            foreach (var sub in subscriptions)
            {
                List<Post> authPosts = _context.Post.Where(p => p.UserName == sub.SubUser).ToList();
                posts.AddRange(authPosts);
            }
            return posts;
        }
    }
}
