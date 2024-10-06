using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Requests;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(IMediator mediator, IUserService userService)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RegisterCommand(request);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : Unauthorized();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new LoginCommand(request);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : Unauthorized();

        }
    }
}
