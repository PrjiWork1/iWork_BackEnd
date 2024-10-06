using IWork.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.UserCommands
{
    public class LoginCommand : IRequest<string>
    {
        public LoginCommand(LoginRequest request) { Email = request.Email; Password = request.Password; }
        public string Email { get; }
        public string Password { get; }
    }
}
