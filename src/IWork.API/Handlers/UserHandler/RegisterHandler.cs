using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.UserHandler
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, UserViewModel>
    {
        private readonly IUserService _userService;
        public RegisterHandler(IUserService userService) { _userService = userService; }
        public async Task<UserViewModel> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var request = new Domain.Requests.RegisterRequest
            {
                CompleteName = command.CompleteName,
                Email = command.Email,
                Password = command.Password,
                ConfirmPassword = command.ConfirmPassword,
                Role = command.Role,
                BirthDate = command.BirthDate,
                CPF = command.CPF,
                PhoneNumber = command.PhoneNumber,
                UserName = command.UserName,
                IsActive = command.IsActive,
            };
            return await _userService.RegisterAsync(request);
        }
    }
}
