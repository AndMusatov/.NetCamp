using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using dotNet_TWITTER.Applications.Common.Models;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;

namespace dotNet_TWITTER.Controllers
{
    [ApiController]
    [Route("account")]
    public class GoogleAuthController : Controller
    {
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
            return Ok(claims);
        }

        /*[HttpGet]
        [Route("google-register")]
        public async Task<ActionResult> GoogleRegistration(GoogleRegisterModel googleRegisterModel)
        {

            return Json();
        }*/

        /*private async Task Authenticate()
        {
            /*var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = new List<Claim>();
            claims.Add(new Claim(OpenIdConnectConstants.Claims.Email);

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
               ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }*/
    }
}
