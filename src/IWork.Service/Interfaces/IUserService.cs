using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task<bool> AssignRoleAsync(string userId, string role);
    }

}
