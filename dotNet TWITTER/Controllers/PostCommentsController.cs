using Microsoft.AspNetCore.Mvc;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using dotNet_TWITTER.Applications.Data;
using System.Security.Claims;

namespace dotNet_TWITTER.Controllers
{
    public class PostCommentsController : ControllerBase
    {
        UserContext _context;
        public PostCommentsController(UserContext userContext)
        {
            _context = userContext;
        }

        [Authorize]
        [HttpPost("PostCommentCreation")]
        public ActionResult CreatePostComment(int id, string filling)
        {
            CommentsActions commentsActions = new CommentsActions(_context);
            return Ok(commentsActions.AddComment(id, filling, User.FindFirstValue(ClaimTypes.Email)));
        }

        [Authorize]
        [HttpDelete("PostCommentDelete")]
        public ActionResult DeletePostComment(int postId, int commentId)
        {
            CommentsActions commentsActions = new CommentsActions(_context);
            return Ok(commentsActions.RemoveComment(User.FindFirstValue(ClaimTypes.Email), postId, commentId));
        }

        [HttpGet("ShowPostComments")]
        public ActionResult ShowPostComments(int idPost)
        {
            CommentsActions commentsActions = new CommentsActions(_context);
            return Ok(commentsActions.GetComments(idPost));
        }
    }
}
