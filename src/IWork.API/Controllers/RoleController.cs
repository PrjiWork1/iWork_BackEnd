using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("CreateRole")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole(RoleViewModel RoleDto)
        {
            try
            {
                var retorno = await _roleManager.CreateAsync(new Role { Name = RoleDto.Name, IsActive = RoleDto.IsActive });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }

        [HttpPut("UpdateUserRole")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRoleViewModel updateUserDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateUserDto.Email);

                if (user != null)
                {
                    if (updateUserDto.Delete)
                        await _userManager.RemoveFromRoleAsync(user, updateUserDto.Role);
                    else
                        await _userManager.AddToRoleAsync(user, updateUserDto.Role);
                }
                else
                {
                    return Ok("Usuário não encontrado");
                }

                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
    }
}
