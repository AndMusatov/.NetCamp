using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class PostsActions
    {
        //private readonly UserManager<User> _userManager;
        //private UserContext _context;
        private readonly IGenericRepository<Post> _genericRepository;

        public PostsActions(IGenericRepository<Post> genericRepository)
        {
            //_context = context;
            //_userManager = userManager;
            _genericRepository = genericRepository;
        }

        public async Task<Post> AddPost(string filling, string userName,string userId)
        {
            if (CanCreatePost(filling))
            {
                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString("N"),
                    UserName = userName,
                    UserId = userId,
                    Date = DateTime.Now,
                    Filling = filling,
                    User = await _genericRepository.FindById(userId)
                };
                _genericRepository.Add(post);
                return post;
            }
            return null;
        }

        /*public async Task<string> DeletePost(string postId, string userName)
        {
           if (PostExists(postId))
           {
                var post = _context.Post.Single(x => x.UserName == userName && x.PostId == postId);
                _genericRepository.Remove(post);
                return "This post is deleted";
           }
            return "This post doesn`t exist";
        }*/

        public bool CanCreatePost(string filling)
        {
            if (string.IsNullOrEmpty(filling))
            {
                return false;
            }
            return true;
        }

        /*public bool PostExists(string postId)
        {
            Post post = _context.Post.FirstOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return false;
            }
            return true;
        }*/

        /*public async Task<List<Post>> GetAllSubPosts(string userName, PostsParameters postsParameters)
        {
            List<Post> posts = new List<Post>();
            List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.AuthUser == userName).ToList();
            foreach (var sub in subscriptions)
            {
                List<Post> authPosts = _context.Post.Where(p => p.UserName == sub.SubUser).ToList();
                posts.AddRange(authPosts);
            }
            posts.Sort((ps1, ps2) => DateTime.Compare(ps1.Date, ps2.Date));
            var result = _context.Post
                .Skip((postsParameters.PageNumber - 1) * postsParameters.PageSize)
                .Take(postsParameters.PageSize);
            return result.ToList();
        }*/
    }
}
