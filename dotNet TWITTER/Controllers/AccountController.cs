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
    public class AccountController : Controller
    {
        private UserContext _context;
        public AccountController(UserContext context)
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
                return Ok(model);
            }
            return Ok("model isn`t valid");
        }

        [HttpPost("AuthLogin")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AuthActions authActions = new AuthActions(_context);
                User user = await authActions.Login(model);
                await Authenticate(user); // аутентификация
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.UsersDB);
        }

        [Authorize]
        [HttpGet("LoginUserName")]
        public IActionResult GetLoginUsername()
        {
            return Content(User.Identity.Name);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimTypes.Email, user.EMail)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
               ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
