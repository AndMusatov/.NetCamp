using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Controllers
{
    [ApiController]
    [Route("account")]
    public class GoogleAuthController : Controller
    {
        protected IUserRepository _userRepository;
        protected IPostsRepository _postsRepository;
        public GoogleAuthController(IUserRepository userRepository, IPostsRepository postsRepository)
        {
            _userRepository = userRepository;
            _postsRepository = postsRepository;
        }
        [HttpGet]
        [Route("google-login")]
        public ActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Route("signin-google")]
        public async Task<ActionResult> GoogleResponse()
        {
            AuthActions authActions = new AuthActions(_userRepository, _postsRepository);
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return BadRequest();
            User user = await authActions.GetUserByEmail(result.Principal.FindFirst(ClaimTypes.Email).Value);
            if (user == null)
            {
                return Json(await authActions.GoogleRegister(
                    result.Principal.FindFirst(ClaimTypes.Email).Value, result.Principal.FindFirst(ClaimTypes.Email).Value));
            }
            await authActions.GoogleLogin(result.Principal.FindFirst(ClaimTypes.Email).Value);
            return Ok(result.Principal.FindFirst(ClaimTypes.Email).Value);
        }
    }
}
