using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Domain.Events
{
    public class LikesActions
    {
        UserContext _context;
        public LikesActions(UserContext context)
        {
            _context = context;
        }

        public string LikeAction(string postId, string authUserName)
        {
            Post post = _context.Post.FirstOrDefault(p => p.PostId == postId);
            if(post.Likes.Contains(authUserName))
            {
                _context.Post.FirstOrDefault(p => p.PostId == postId).Likes.Remove(post.UserName);
                _context.SaveChanges();
                return "Like Removed";
            }
            _context.Post.FirstOrDefault(p => p.PostId == postId).Likes.Add(authUserName);
            _context.SaveChanges();
            return "Like add";
        }

        public int PostLikesQuantity(string postId)
        {
            return _context.Post.FirstOrDefault(p => p.PostId == postId).Likes.Count();
        }
    }
}

