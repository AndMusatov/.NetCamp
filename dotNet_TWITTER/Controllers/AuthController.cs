using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace dotNet_TWITTER.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_userManager, _signInManager);
                var result = await authActions.Registration(model);
                return Json(result);
            }
            return Ok("Wrong input");
        }

        [HttpPost("AuthLogin")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_userManager, _signInManager);
                var result = await authActions.Login(model);
                return Json(result);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("LoginUserEMail")]
        public IActionResult GetLoginUsername()
        {
            return Content(User.FindFirstValue(ClaimTypes.Email));
        }

        [Authorize]
        [HttpDelete("DeleteLoginedUser")]
        public async Task<IActionResult> DeleteUser()
        {
            AuthActions authActions = new AuthActions(_userManager, _signInManager);
            return Json(await authActions.DeleteUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
