using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.UserCommands
{
    public class RegisterCommand : IRequest<UserViewModel>
    {
        public RegisterCommand(RegisterRequest request)
        {
            CompleteName = request.CompleteName;
            Email = request.Email;
            Password = request.Password;
            ConfirmPassword = request.ConfirmPassword;
            Role = request.Role;
            BirthDate = request.BirthDate;
            CPF = request.CPF;
            PhoneNumber = request.PhoneNumber;
            UserName = request.UserName;
            IsActive = request.IsActive;
        }

        public string CompleteName { get; }
        public string UserName { get; }
        public string Email { get; }
        public string Password { get; }
        public DateTime BirthDate { get; }
        public string CPF { get; }
        public string PhoneNumber { get; }
        public string ConfirmPassword { get; }
        public string Role { get; }
        public bool IsActive { get; }
    }
}
