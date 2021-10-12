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
    public class PostCommentsController : ControllerBase
    {
        [HttpPost("PostCommentCreation")]
        public ActionResult CreatePost(int id, string filling)
        {
            return Ok(IPostDataBase.AddComment(id, filling));
        }

        [HttpGet("ShowComment")]
        public ActionResult Index(int idPost, int idComment)
        {
            return Ok(IPostDataBase.SendComment(idPost, idComment));
        }
    }
}
