using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Domain.Events;

namespace dotNet_TWITTER.Controllers
{
    public class PostLikesController : ControllerBase
    {
        private LikesActions likesActions = new LikesActions();
        [HttpPost("AddPostLikes")]
        public ActionResult AddPostLike(int id)
        {
            return Ok(likesActions.AddLike(id));
        }
        [HttpGet("ShowPostLikes")]
        public ActionResult ShowPostLikes(int id)
        {
            return Ok(likesActions.PostLikesQuantity(id));
        }
    }
}
