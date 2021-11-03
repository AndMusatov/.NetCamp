using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class MainPostController : Controller
    {
        private UserContext _context;

        public MainPostController(UserContext context)
        {
            _context = context;
        }

        [HttpPost("PostCreation")]
        public ActionResult CreatePost(string filling)
        {
            MainPostsActions postsActions = new MainPostsActions(_context);
            return Ok(postsActions.AddPost(filling, User.Identity.Name));
        }

        [HttpGet("ShowPost")]
        public ActionResult ShowPost(int id)
        {
            MainPostsActions postsActions = new MainPostsActions(_context);
            Post post = postsActions.GetPost(id);
            return Ok(post);
        }

        [HttpDelete("DeletePost")]
        public ActionResult DeletePost(int id)
        {
            MainPostsActions postsActions = new MainPostsActions(_context);
            postsActions.DeletePost(id);
            return Ok();
        }
    }
}

