using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Requests;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.UserHandler
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserService _userService;
        public LoginHandler(IUserService userService) { _userService = userService; }
        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var request = new LoginRequest { Email = command.Email, Password = command.Password };
            return await _userService.LoginAsync(request);
        }

    }
}
