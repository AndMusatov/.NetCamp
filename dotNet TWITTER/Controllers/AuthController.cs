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

namespace dotNet_TWITTER.Controllers
{
    public class AuthController : Controller
    {
        private UserContext _context;
        public AuthController(UserContext context)
        {
            _context = context;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_context);
                await authActions.Registration(model);
                User user = await authActions.Login(model.Email, model.Password);
                await Authenticate(user);
                return Ok(model);
            }
            return Ok("model isn`t valid");
        }

        [HttpPost("AuthLogin")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_context);
                User user = await authActions.Login(model);
                await Authenticate(user);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Json(_context.UsersDB.ToList());
        }

        [Authorize]
        [HttpGet("LoginUserEMail")]
        public IActionResult GetLoginUsername()
        {
            return Content(User.FindFirstValue(ClaimTypes.Email));
        }

        [Authorize]
        [HttpDelete("DeleteLoginUser")]
        public IActionResult DeleteUser()
        {
            AuthActions authActions = new AuthActions(_context);
            return Ok(authActions.DeleteUser(User.FindFirstValue(ClaimTypes.Email)));
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EMail),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
               ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
