using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Domain.Events;

namespace dotNet_TWITTER.Controllers
{
    public class LoginController : Controller
    {
        private LoginActions loginActions = new LoginActions();

        [HttpPost("Login")]
        public ActionResult Login(string userName, string password)
        {
            return Ok(loginActions.Login(userName, password));
        }

        [HttpGet("LoginStatus")]
        public ActionResult CheckLoginStatus()
        {
            return Ok(loginActions.CheckLoginStatus());
        }
    }
}
