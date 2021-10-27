using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class MainPostController : Controller
    {
        private MainPostsActions postsActions = new MainPostsActions();

        [HttpPost("PostCreation")]
        public ActionResult CreatePost(string filling)
        {
            postsActions.AddPost(filling);
            return Ok();
        }

        [HttpGet("ShowPost")]
        public ActionResult ShowPost(int id)
        {
            Post post = postsActions.GetPost(id);
            return Ok(post);
        }

        [HttpDelete("DeletePost")]
        public ActionResult DeletePost(int id)
        {
            postsActions.DeletePost(id);
            return Ok();
        }
    }
}

