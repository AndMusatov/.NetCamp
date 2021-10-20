using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Controllers
{
    public class LoginController : Controller
    {
        private LoginActions loginActions;

        [HttpPost("Login")]
        public IActionResult Login(LoginActions loginActions, string userName, string password)
        {
            this.loginActions = loginActions;
            return Ok(loginActions.Login(userName, password));
        }

        [HttpGet("LoginStatus")]
        public IActionResult CheckLoginStatus(LoginActions loginActions)
        {
            this.loginActions = loginActions;
            return Ok(loginActions.CheckLoginStatus());
        }
    }
}
