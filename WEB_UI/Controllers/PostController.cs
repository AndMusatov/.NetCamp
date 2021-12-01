using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.WEB_UI.Controllers
{
    public class PostController : Controller
    {
        //private UserContext _context;
        //private readonly UserManager<User> _userManager;
        private readonly IPostsRepository _postsRepository;
        private IGenericRepository<Post> _genericRepository;

        public PostController(IPostsRepository postsRepository, IGenericRepository<Post> genericRepository)
        {
            //_context = context;
            //_userManager = userManager;
            _postsRepository = postsRepository;
            _genericRepository = genericRepository;
        }

        [Authorize]
        [HttpPost("PostCreation")]
        public async Task<ActionResult> CreatePost(string filling)
        {
            PostsActions postsActions = new PostsActions(_genericRepository);
            return Ok(await postsActions.AddPost(filling, User.FindFirstValue(ClaimTypes.Name), User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize]
        [HttpGet("ShowAuthUserPosts")]
        public ActionResult ShowAuthUserPosts([FromQuery] PostsParameters postsParameters)
        {
            return Ok(_postsRepository.GetAuthPosts(User.FindFirstValue(ClaimTypes.Name), postsParameters));
        }

        /*[Authorize]
        [HttpGet("ShowSubPosts")]
        public ActionResult ShowSubPosts([FromQuery] PostsParameters postsParameters)
        {
            PostsActions postsActions = new PostsActions(_context, _userManager, _genericRepository);
            return Ok(postsActions.GetAllSubPosts(User.FindFirstValue(ClaimTypes.Name), postsParameters));
        }*/

        /*[Authorize]
        [HttpDelete("DeleteAuthUserPost")]
        public async Task<ActionResult> DeleteAuthUserPost(string postId)
        {
            PostsActions postsActions = new PostsActions(_context, _userManager, _genericRepository);
            return Ok(await postsActions.DeletePost(postId, User.FindFirstValue(ClaimTypes.Name)));
        }*/
    }
}
