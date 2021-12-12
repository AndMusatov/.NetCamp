using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostsRepository _postsRepository;

        public PostController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        [Authorize]
        [HttpPost("PostCreation")]
        public async Task<ActionResult> CreatePost(string filling)
        {
            PostsActions postsActions = new PostsActions(_postsRepository);
            Post post = await postsActions.AddPost(filling, User.FindFirstValue(ClaimTypes.Name), User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (post == null)
            {
                return BadRequest();
            }
            return Ok(post);
        }

        [Authorize]
        [HttpGet("ShowAuthUserPosts")]
        public async Task<ActionResult> GetAuthUserPosts([FromQuery] PostsParameters postsParameters)
        {
            PostsActions postsActions = new PostsActions(_postsRepository);
            return Ok(await postsActions.GetAuthUserPosts(User.FindFirstValue(ClaimTypes.Name), postsParameters));
        }

        [Authorize]
        [HttpGet("ShowSubPosts")]
        public async Task<ActionResult> GetSubPosts([FromQuery] PostsParameters postsParameters)
        {
            PostsActions postsActions = new PostsActions(_postsRepository);
            return Ok(await postsActions.GetSubPosts(User.FindFirstValue(ClaimTypes.Name), postsParameters));
        }

        [Authorize]
        [HttpDelete("DeleteAuthUserPost")]
        public async Task<ActionResult> RemoveAuthUserPost(string postId)
        {
            PostsActions postsActions = new PostsActions(_postsRepository);
            return Ok(await postsActions.DeletePost(postId));
        }
    }
}
