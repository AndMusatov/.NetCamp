using dotNet_TWITTER.Applications.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task RemovePostComments(string postId);
        Task<List<Comment>> GetPostComments(string postId);
        bool PostForCommentExists(string postId);
    }
}
