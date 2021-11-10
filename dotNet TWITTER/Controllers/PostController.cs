using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class PostController : Controller
    {
        private UserContext _context;

        public PostController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("ShowAllPosts")]
        public ActionResult ShowAllPosts()
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.GetAllPosts());
        }

        [Authorize]
        [HttpPost("PostCreation")]
        public ActionResult CreatePost(string filling)
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.AddPost(filling, User.Identity.Name));
        }

        [Authorize]
        [HttpGet("ShowAuthUserPosts")]
        public ActionResult ShowAuthUserPosts()
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.GetPosts(User.Identity.Name));
        }

        [Authorize]
        [HttpGet("ShowSubPosts")]
        public ActionResult ShowSubPosts()
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.GetAllSubPosts(User.Identity.Name));
        }

        [Authorize]
        [HttpDelete("DeleteAuthUserPost")]
        public ActionResult DeleteAuthUserPost(int id)
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.DeletePost(id, User.Identity.Name));
        }
    }
}

