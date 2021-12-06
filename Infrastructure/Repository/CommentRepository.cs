using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(UserContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context, userManager, signInManager)
        {
        }
        public async Task RemovePostComments(string postId)
        {
            List<Comment> comments = await _context.Comment.Where(c => c.PostId == postId).ToListAsync();
            _context.Comment.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetPostComments(string postId)
        {
            var result = await _context.Comment.Where(c => c.PostId == postId).ToListAsync();
            return result;
        }

        public bool PostForCommentExists(string postId)
        {
            if(_context.Post.FirstOrDefault(p => p.PostId == postId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
