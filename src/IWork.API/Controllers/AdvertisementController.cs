using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AdvertisementService _advertisementService;

        public AdvertisementController(IMediator mediator, AdvertisementService advertisementService)
        {
            _mediator = mediator;
            _advertisementService = advertisementService;
        }

        [HttpGet("GetAllAdvertisements")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var advertisements = _advertisementService.GetAlldvertisements();
            if (advertisements == null)
            {
               return NotFound();
            }
            return Ok(advertisements);
        }

        [HttpGet("GetAdvertisementById{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var advertisement = _advertisementService.GetAdvertisementById(Id);
            if (advertisement == null) return NotFound();
            return Ok(advertisement);
        }

        [HttpPost("CreateNormalAdvertisement")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post([FromBody] NormalAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }
    }
}
