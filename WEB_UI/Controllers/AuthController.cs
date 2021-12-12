using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostsRepository _postRepository;
        public AuthController(IUserRepository userRepository, IPostsRepository postsRepository)
        {
            _userRepository = userRepository;
            _postRepository = postsRepository;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_userRepository, _postRepository);
                var result = await authActions.Registration(model);
                return Ok(result);
            }
            return BadRequest(model);
        }

        [HttpPost("AuthLogin")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.PasswordSignInUser(model.UserName, model.Password);
                return Ok(result);
            }
            return BadRequest(model);
        }

        [Authorize]
        [HttpGet("LoginUserEMail")]
        public IActionResult GetLoginUsername()
        {
            return Ok(User.FindFirstValue(ClaimTypes.Email));
        }

        [Authorize]
        [HttpDelete("DeleteLoginedUser")]
        public async Task<IActionResult> DeleteUser()
        {
            AuthActions authActions = new AuthActions(_userRepository, _postRepository);
            return Ok(await authActions.DeleteUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
