using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Identity;

namespace dotNet_TWITTER.Domain.Events
{
    public class CommentsActions
    {
        UserContext _context;
        UserManager<User> _userManager;

        public CommentsActions(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Comment> GetComments(string postId)
        {
            var result = _context.Comment.Where(p => p.PostId == postId);
            return result.ToList();
        }

        public async Task<string> AddComment(string postId, string commentStr, string userId)
        {
            if (CommentsInputCheck(commentStr))
            {
                User user = await _userManager.FindByIdAsync(userId);
                List<Comment> comments = new List<Comment>();
                Post post = _context.Post.FirstOrDefault(p => p.PostId == postId);
                _context.Comment.Add(new Comment
                {
                    CommentId = Guid.NewGuid().ToString("N"),
                    PostId = postId,
                    UserName = user.UserName,
                    CommentFilling = commentStr
                }
                );
                await _context.SaveChangesAsync();
                return "Input is Ok";
            }
            return "Input is wrong";
        }

        public bool CommentsInputCheck(string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return false;
            }
            return true;
        }

        public async Task<string> RemoveComment(string userId, string commentId, string postId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            var comment = _context.Comment.Single(x => x.UserName == user.UserName && x.PostId == postId && x.CommentId == commentId);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return "Cooment was removed";
        }
    }
}
