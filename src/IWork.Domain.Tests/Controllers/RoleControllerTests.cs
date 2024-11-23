using System;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Controllers;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly RoleController _roleController;

        public RoleControllerTests()
        {
            var roleStore = new Mock<IRoleStore<Role>>();
            _mockRoleManager = new Mock<RoleManager<Role>>(roleStore.Object, null, null, null, null);
            var userStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            _roleController = new RoleController(_mockRoleManager.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnOkResult_WhenRoleIsCreated()
        {
            
            var roleViewModel = new RoleViewModel("Admin", true);
            _mockRoleManager.Setup(r => r.CreateAsync(It.IsAny<Role>())).ReturnsAsync(IdentityResult.Success);

            
            var result = await _roleController.CreateRole(roleViewModel);

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            
            var roleViewModel = new RoleViewModel("Admin", true);
            _mockRoleManager.Setup(r => r.CreateAsync(It.IsAny<Role>())).ThrowsAsync(new Exception("Test Exception"));

            
            var result = await _roleController.CreateRole(roleViewModel);

            
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().Be("ERROR Test Exception");
        }

        [Fact]
        public async Task UpdateUserRoles_ShouldReturnOkResult_WhenUserIsFoundAndRoleUpdated()
        {
            
            var updateUserRoleViewModel = new UpdateUserRoleViewModel("test@example.com", "Admin", false);
            var user = new User { Email = "test@example.com" };
            _mockUserManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            
            var result = await _roleController.UpdateUserRoles(updateUserRoleViewModel);

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().Be("Sucesso");
        }

        [Fact]
        public async Task UpdateUserRoles_ShouldReturnOkResult_WhenUserIsNotFound()
        {
            
            var updateUserRoleViewModel = new UpdateUserRoleViewModel("test@example.com", "Admin", false);
            _mockUserManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            
            var result = await _roleController.UpdateUserRoles(updateUserRoleViewModel);

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().Be("Usuário não encontrado");
        }

        [Fact]
        public async Task UpdateUserRoles_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            
            var updateUserRoleViewModel = new UpdateUserRoleViewModel("test@example.com", "Admin", false);
            _mockUserManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Test Exception"));

            
            var result = await _roleController.UpdateUserRoles(updateUserRoleViewModel);

            
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().Be("ERROR Test Exception");
        }
    }
}



















































































































