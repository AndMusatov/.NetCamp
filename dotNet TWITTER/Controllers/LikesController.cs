using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Domain.Events;
using dotNet_TWITTER.Applications.Data;
using Microsoft.AspNetCore.Authorization;

namespace dotNet_TWITTER.Controllers
{
    public class PostLikesController : ControllerBase
    {
        UserContext _context;
        public PostLikesController(UserContext userContext)
        {
            _context = userContext;
        }

        [Authorize]
        [HttpPost("LikeAction")]
        public ActionResult AddPostLike(int id)
        {
            LikesActions likesActions = new LikesActions(_context);
            return Ok(likesActions.LikeAction(id, User.Identity.Name));
        }
        [HttpGet("ShowPostLikes")]
        public ActionResult ShowPostLikes(int id)
        {
            LikesActions likesActions = new LikesActions(_context);
            return Ok(likesActions.PostLikesQuantity(id));
        }
    }
}
