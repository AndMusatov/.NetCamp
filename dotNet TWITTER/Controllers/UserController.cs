using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Controllers
{
    public class UserController : Controller
    {
        private UserActions userActions;
        [HttpGet("GetUser")]
        public IActionResult GetUser(int id, UserActions userActions)
        {
            this.userActions = userActions;
            return Ok(userActions.GetUser(id));
        }

        [HttpPost("Registration")]
        public IActionResult Registration(string userName, string password, string mailAdress, UserActions userActions)
        {
            this.userActions = userActions;
            return Ok(userActions.NewUser(userName, password, mailAdress));
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers(UserActions userActions)
        {
            this.userActions = userActions;
            return Ok(userActions.GetAllUsers());
        }
    }
}
