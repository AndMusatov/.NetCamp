using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotNet_TWITTER.Applications.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using dotNet_TWITTER.Applications.Common.Models;
using System.Threading.Tasks;
using dotNet_TWITTER.Infrastructure.Services;
using dotNet_TWITTER.Infrastructure.Repository;
using System.Collections.Generic;

namespace dotNet_TWITTER.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost("PostCommentCreation")]
        public async Task<ActionResult> CreatePostComment(string postId, string filling)
        {
            CommentsActions commentsActions = new CommentsActions(_commentRepository);
            Comment comment = await commentsActions.AddComment(postId, filling, User.FindFirstValue(ClaimTypes.Name));
            if (comment == null)
            {
                return BadRequest();
            }
            return Ok(comment);
        }

        [Authorize]
        [HttpDelete("PostCommentDelete")]
        public async Task<ActionResult> DeletePostComment(string commentId)
        {
            CommentsActions commentsActions = new CommentsActions(_commentRepository);
            return Ok(await commentsActions.RemoveComment(commentId));
        }

        
        [HttpGet("ShowPostComments")]
        public async Task<ActionResult> GetPostComments(string postId)
        {
            CommentsActions commentsActions = new CommentsActions(_commentRepository);
            List<Comment> comments = await commentsActions.GetComments(postId);
            if (comments == null)
            {
                return BadRequest();
            }
            return Ok(comments);
        }
    }
}
