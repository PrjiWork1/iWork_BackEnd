using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllAdvertisements")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(bool isAdmin)
        { 
            var query = new GetAllAdvertisementsQuery(isAdmin);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("GetAdvertisementById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var query = new GetByIdAdvertisementQuery(Id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("CreateNormalAdvertisement")]
        [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        public async Task<IActionResult> AddNormalAdvertisement([FromBody] NormalAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPost("CreateDynamicAdvertisement")]
        [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        public async Task<IActionResult> AddDynamicAdvertisement([FromBody] DynamicAdvertisementAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPut("UpdateStatusAdvertisement{Id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateStatusAdvertisement(Guid Id, AdvertisementStatusRequest command)
        {
            var advertisement = new UpdateAdvertisementStatusCommand(Id, command);
            var response  = await _mediator.Send(advertisement);
            if (!response) return NotFound("Anúncio não encontrado");
            return Ok(response);
        }

        [HttpPut("UpdateNumberOfSalesAdvertisement{Id}")]
        //[Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateNumberOfSalesAdvertisement(Guid Id, AdvertisementNumberOfSalesRequest command)
        {
            var advertisement = new UpdateAdvertisementNumberOfSalesCommand(Id, command);
            var response = await _mediator.Send(advertisement);
            if (!response) return NotFound("Anúncio não encontrado");
            return Ok(response);
        }
    }
}
