using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNet_TWITTER.Applications.Common.ToDoItems.Queries;
    using dotNet_TWITTER.Data;
using Microsoft.Extensions.Logging;

namespace dotNet_TWITTER.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public TaskController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet("/{Id}")]
        public async Task<IActionResult> GetDatabyId(int Id)
        {
            var result = await _mediatr.Send(new GetData.Query(Id));
            return result != null ? Ok(result) : NotFound();
        }
        [HttpPost("")]
        public async Task<IActionResult> GetDatabyId(AddData.Command command) => Ok(await _mediatr.Send(command));
    }
}
