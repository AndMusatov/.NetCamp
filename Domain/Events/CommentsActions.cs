using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Domain.Events
{
    public class CommentsActions
    {
        UserContext _context;
        public CommentsActions(UserContext context)
        {
            _context = context;
        }

        public List<Comment> GetComments(int postId)
        {
            var result = _context.Comment.Where(p => p.PostId == postId);
            return result.ToList();
        }

        public string AddComment(int postId, string commentStr, string userName)
        {
            if (CommentsInputCheck(postId, commentStr))
            {
                List<Comment> comments = new List<Comment>();
                Post post = _context.Post.FirstOrDefault(p => p.PostId == postId);
                _context.Comment.Add(new Comment
                {
                    PostId = postId,
                    UserName = userName,
                    CommentFilling = commentStr
                }
                );
                _context.SaveChanges();
                return "Input is Ok";
            }
            return "Input is wrong";
        }

        public bool CommentsInputCheck(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return false;
            }
            if (_context.Post.Count() <= postId)
            {
                return false;
            }
            return true;
        }

        public string RemoveComment(string userName, int commentId, int postId)
        {
            var comment = _context.Comment.Single(x => x.UserName == userName && x.PostId == postId && x.CommentId == commentId);
            _context.Comment.Remove(comment);
            _context.SaveChanges();
            return "Cooment was removed";
        }
    }
}
