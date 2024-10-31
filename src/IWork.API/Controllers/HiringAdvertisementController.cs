using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Commands.HiringAdvertisementCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringAdvertisementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HiringAdvertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateHiringAdvertisement")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] HiringAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }
    }
}
