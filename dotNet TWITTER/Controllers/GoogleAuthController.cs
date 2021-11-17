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


namespace dotNet_TWITTER.Controllers
{
    [ApiController]
    [Route("account")]
    public class GoogleAuthController : Controller
    {
        private UserContext _context;
        public GoogleAuthController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("google-login")]
        public ActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Route("google-response")]
        public async Task<ActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Value
                });
            return Json(claims);
        }

        [HttpPost]
        [Route("google-register")]
        public async Task<IActionResult> GoogleRegistration(GoogleRegisterModel googleRegisterModel)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_context);
                await authActions.Registration(googleRegisterModel, User.FindFirstValue(ClaimTypes.Email));
                return Ok("Registration is ok");
            }

            return Ok("Wrong model");
        }
    }
}
