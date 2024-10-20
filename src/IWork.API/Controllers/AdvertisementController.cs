using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
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
            var advertisements = _advertisementService.GetAllAdvertisements();

            if (advertisements == null)
            {
               return NotFound("Nenhum anúncio cadastrado");
            }

            return Ok(advertisements);
        }

        [HttpGet("GetAdvertisementById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var advertisement = _advertisementService.GetAdvertisementById(Id);
            if (advertisement == null) return NotFound();
            return Ok(advertisement);
        }

        [HttpPost("CreateNormalAdvertisement")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddNormalAdvertisement([FromBody] NormalAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPost("CreateDynamicAdvertisement")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddDynamicAdvertisement([FromBody] DynamicAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPut("UpdateStatusAdvertisement{Id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateStatusAdvertisement(Guid Id, UpdateAdvertisementStatusRequest command)
        {
            var advertisement = new UpdateAdvertisementStatusCommand(Id, command);
            var response  = await _mediator.Send(advertisement);
            if (!response) return NotFound("Anúncio não encontrado");
            return Ok(response);
        }
    }
}
