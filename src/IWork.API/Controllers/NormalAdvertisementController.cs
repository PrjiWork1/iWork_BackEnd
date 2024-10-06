using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Commands.NormalAdvertisementCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalAdvertisementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NormalAdvertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post([FromBody] NormalAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }
    }
}
