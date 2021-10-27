using Microsoft.AspNetCore.Mvc;
using dotNet_TWITTER.Domain.Events;

namespace dotNet_TWITTER.Controllers
{
    public class PostCommentsController : ControllerBase
    {
        private CommentsActions commentsActions = new CommentsActions();
        [HttpPost("PostCommentCreation")]
        public ActionResult CreatePost(int id, string filling)
        {
            return Ok(commentsActions.AddComment(id, filling));
        }

        [HttpGet("ShowComment")]
        public ActionResult ShowComment(int idPost, int idComment)
        {
            return Ok(commentsActions.GetComment(idPost, idComment));
        }

        [HttpGet("ShowComments")]
        public ActionResult ShowComments(int idPost)
        {
            return Ok(commentsActions.GetComments(idPost));
        }
    }
}
