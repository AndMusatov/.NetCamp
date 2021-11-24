using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Domain.Events;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace dotNet_TWITTER.Controllers
{
    public class GenerateDataController : Controller
    {
        private UserContext _context;
        private readonly UserManager<User> _userManager;
        public GenerateDataController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("DataGenerator")]
        public async Task<IActionResult> GenerateData(int quantityOfUsers, int quantityOfPostsForOneUser)
        {
            DataGenerator dataGenerator = new DataGenerator(_context, _userManager);
            return Ok(await dataGenerator.Generate(quantityOfUsers, quantityOfPostsForOneUser));
        }
    }
}
