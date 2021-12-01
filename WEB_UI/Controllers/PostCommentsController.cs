using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotNet_TWITTER.Applications.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using dotNet_TWITTER.Applications.Common.Models;
using System.Threading.Tasks;
using dotNet_TWITTER.Infrastructure.Services;

namespace dotNet_TWITTER.Controllers
{
    public class PostCommentsController : ControllerBase
    {
        UserContext _context;
        private readonly UserManager<User> _userManager;
        public PostCommentsController(UserContext userContext, UserManager<User> userManager)
        {
            _context = userContext;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("PostCommentCreation")]
        public async Task<ActionResult> CreatePostComment(string postId, string filling)
        {
            CommentsActions commentsActions = new CommentsActions(_context, _userManager);
            return Ok(await commentsActions.AddComment(postId, filling, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize]
        [HttpDelete("PostCommentDelete")]
        public async Task<ActionResult> DeletePostComment(string postId, string commentId)
        {
            CommentsActions commentsActions = new CommentsActions(_context, _userManager);
            return Ok(await commentsActions.RemoveComment(User.FindFirstValue(ClaimTypes.Email), postId, commentId));
        }

        [HttpGet("ShowPostComments")]
        public ActionResult ShowPostComments(string postId)
        {
            CommentsActions commentsActions = new CommentsActions(_context, _userManager);
            return Ok(commentsActions.GetComments(postId));
        }
    }
}
