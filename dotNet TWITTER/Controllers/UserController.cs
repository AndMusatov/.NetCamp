using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_TWITTER.Controllers
{
    public class UserController : Controller
    {
        private UserActions userActions = new UserActions();
        [HttpGet("GetUser")]
        public ActionResult GetUser(int id)
        {
            return Ok(userActions.GetUser(id));
        }

        [HttpPost("OldRegistration")]
        public ActionResult Registration(string userName, string password, string mailAdress)
        {
            return Ok(userActions.NewUser(userName, password, mailAdress));
        }

        [HttpGet("OldGetAllUsers")]
        public ActionResult GetAllUsers()
        {
            return Ok(userActions.GetAllUsers());
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            return Ok(userActions.DeleteUser()); 
        }
    }
}
