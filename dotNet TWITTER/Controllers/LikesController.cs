using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikesController : ControllerBase
    {
        [HttpPost("AddPostLikes")]
        public ActionResult AddPostLike(int id)
        {
            return Ok(IPostDataBase.AddLike(id));
        }
        [HttpGet("ShowPostLikes")]
        public ActionResult ShowPostLikes(int id)
        {
            int result = IPostDataBase.PostLikesQuantity(id);
            if (result == 0)
            {
                return Ok("Wrong id Input");
            }
            return Ok(IPostDataBase.PostLikesQuantity(id));
        }
    }
}
