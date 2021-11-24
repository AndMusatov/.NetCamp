using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace dotNet_TWITTER.Controllers
{
    [ApiController]
    [Route("account")]
    public class GoogleAuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private UserContext _context;
        public GoogleAuthController(UserContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Route("google-login")]
        public ActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Route("signin-google")]
        public async Task<ActionResult> GoogleResponse()
        {
            var authenticateResult = await this.HttpContext.AuthenticateAsync();
            //ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            //User user = await _userManager.FindByEmailAsync(loginInfo.AuthenticationProperties.GetTokenValue(EmailTokenProvider));
            var result = await HttpContext.AuthenticateAsync();
            //User user = _userManager.GetUserAsync(result);
            //await _signInManager.SignInAsync(result, false);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Value
                });
            return Json(authenticateResult);
        }

        [HttpPost]
        [Route("google-register")]
        public async Task<IActionResult> GoogleRegistration(GoogleRegisterModel googleRegisterModel)
        {
            /*if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_userManager, _signInManager);
                await authActions.Registration(googleRegisterModel, User.FindFirstValue(ClaimTypes.Email));
                return Ok("Registration is ok");
            }*/

            return Ok("Wrong model");
        }
    }
}
