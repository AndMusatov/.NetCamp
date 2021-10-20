using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Controllers
{
    public class PostCommentsController : ControllerBase
    {
        private CommentsActions commentsActions;
        [HttpPost("PostCommentCreation")]
        public ActionResult CreatePost(int id, string filling, CommentsActions commentsActions)
        {
            this.commentsActions = commentsActions;
            return Ok(commentsActions.AddComment(id, filling));
        }

        [HttpGet("ShowComment")]
        public ActionResult ShowComment(int idPost, int idComment, CommentsActions commentsActions)
        {
            this.commentsActions = commentsActions;
            return Ok(commentsActions.GetComment(idPost, idComment));
        }

        [HttpGet("ShowComments")]
        public ActionResult ShowComments(int idPost, CommentsActions commentsActions)
        {
            this.commentsActions = commentsActions;
            return Ok(commentsActions.GetComments(idPost));
        }
    }
}
