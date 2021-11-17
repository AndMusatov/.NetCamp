using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            return Ok(postsActions.AddPost(filling, User.FindFirstValue(ClaimTypes.Email)));
        }

        [Authorize]
        [HttpGet("ShowAuthUserPosts")]
        public ActionResult ShowAuthUserPosts()
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.GetPosts(User.FindFirstValue(ClaimTypes.Email)));
        }

        [Authorize]
        [HttpGet("ShowSubPosts")]
        public ActionResult ShowSubPosts()
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.GetAllSubPosts(User.FindFirstValue(ClaimTypes.Email)));
        }

        [Authorize]
        [HttpDelete("DeleteAuthUserPost")]
        public ActionResult DeleteAuthUserPost(int id)
        {
            PostsActions postsActions = new PostsActions(_context);
            return Ok(postsActions.DeletePost(id, User.FindFirstValue(ClaimTypes.Email)));
        }
    }
}

