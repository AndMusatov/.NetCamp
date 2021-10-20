using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Controllers
{
    public class PostLikesController : ControllerBase
    {
        private LikesActions likesActions;
        [HttpPost("AddPostLikes")]
        public ActionResult AddPostLike(int id, LikesActions likesActions)
        {
            this.likesActions = likesActions;
            return Ok(likesActions.AddLike(id));
        }
        [HttpGet("ShowPostLikes")]
        public ActionResult ShowPostLikes(int id, LikesActions likesActions)
        {
            this.likesActions = likesActions;
            return Ok(likesActions.PostLikesQuantity(id));
        }
    }
}
