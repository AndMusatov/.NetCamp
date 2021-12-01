using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public class PostsRepository : GenericRepository<Post>, IPostsRepository
    {
        public PostsRepository(UserContext context, UserManager<User> userManager) : base(context, userManager)
        {
        }
        public IEnumerable<Post> GetAuthPosts(string userName, PostsParameters postsParameters)
        {
            var result = _context.Post
                .Where(p => p.UserName == userName)
                .Skip((postsParameters.PageNumber - 1) * postsParameters.PageSize)
                .Take(postsParameters.PageSize);
            return result.ToList();
        }
    }
}
