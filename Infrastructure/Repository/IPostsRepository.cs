using dotNet_TWITTER.Applications.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public interface IPostsRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetAuthPosts(string userName);
        Task<List<Post>> GetUserPosts(string userName);
        Task<List<Post>> GetSubPosts(string userName);
        Task RemoveUserPosts(string userName);

    }
}
