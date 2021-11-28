using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost("CancellOperations")]
        public IActionResult CancelOperation()
        {
            DataGenerator.CancellGeneration();
            return Ok("Canceled");
        }
    }
}
