using IWork.Domain.Commands.UploadCommands;
using IWork.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null) return BadRequest("File not found.");

            var cmd = new UploadAddCommand(file);
            var response = await _mediator.Send(cmd);
            return Ok(new { path = response });
        }

    }
}
