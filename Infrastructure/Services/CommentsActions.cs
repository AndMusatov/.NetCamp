using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class CommentsActions
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsActions(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetComments(string postId)
        {
            if (_commentRepository.PostForCommentExists(postId))
            {
                return await _commentRepository.GetPostComments(postId);
            }
            return null;
        }

        public async Task<Comment> AddComment(string postId, string commentStr, string userName)
        {
            if (CommentsInputCheck(commentStr) & _commentRepository.PostForCommentExists(postId))
            {
                Comment comment = new Comment
                {
                    CommentId = Guid.NewGuid().ToString("N"),
                    PostId = postId,
                    UserName = userName,
                    CommentFilling = commentStr
                };
                await _commentRepository.Add(comment);
                return comment;
            }
            return null;
        }

        public bool CommentsInputCheck(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveComment(string commentId)
        {
            var comment = await _commentRepository.GetById(commentId);
            if (comment != null)
            {
                await _commentRepository.Remove(comment);
                return true;
            }
            return false;
        }
    }
}
