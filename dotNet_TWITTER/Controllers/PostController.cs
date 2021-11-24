using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class PostController : Controller
    {
        private UserContext _context;
        private readonly UserManager<User> _userManager;

        public PostController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("ShowAllPosts")]
        public ActionResult ShowAllPosts()
        {
            PostsActions postsActions = new PostsActions(_context, _userManager);
            return Ok(postsActions.GetAllPosts());
        }

        [Authorize]
        [HttpPost("PostCreation")]
        public async Task<ActionResult> CreatePost(string filling)
        {
            PostsActions postsActions = new PostsActions(_context, _userManager);
            return Ok(await postsActions.AddPost(filling, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize]
        [HttpGet("ShowAuthUserPosts")]
        public ActionResult ShowAuthUserPosts()
        {
            PostsActions postsActions = new PostsActions(_context, _userManager);
            return Ok(postsActions.GetPosts(User.FindFirstValue(ClaimTypes.Name)));
        }

        [Authorize]
        [HttpGet("ShowSubPosts")]
        public ActionResult ShowSubPosts()
        {
            PostsActions postsActions = new PostsActions(_context, _userManager);
            return Ok(postsActions.GetAllSubPosts(User.FindFirstValue(ClaimTypes.Email)));
        }

        [Authorize]
        [HttpDelete("DeleteAuthUserPost")]
        public async Task<ActionResult> DeleteAuthUserPost(string postId)
        {
            PostsActions postsActions = new PostsActions(_context, _userManager);
            return Ok(await postsActions.DeletePost(postId, User.FindFirstValue(ClaimTypes.Name)));
        }
    }
}

