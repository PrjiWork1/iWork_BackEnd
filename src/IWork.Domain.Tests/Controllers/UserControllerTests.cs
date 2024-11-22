using System;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Controllers;
using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Queries.UserQuery;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMediator> _mockMediator;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMediator = new Mock<IMediator>();
            _userController = new UserController(_mockMediator.Object, _mockUserService.Object);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnOk_WhenUserExists()
        {
            
            var email = "test@example.com";
            var userViewModel = new UserViewModel(Guid.NewGuid(), "Test User", "testuser", email, "User", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<GetByEmailUserQuery>(), default)).ReturnsAsync(userViewModel);

            
            var result = await _userController.GetUserByEmail(email);

            
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(userViewModel);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnNoContent_WhenUserDoesNotExist()
        {
            
            var email = "test@example.com";
            _mockMediator.Setup(m => m.Send(It.IsAny<GetByEmailUserQuery>(), default)).ReturnsAsync((UserViewModel)null);

            
            var result = await _userController.GetUserByEmail(email);

            
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            
            var request = new RegisterRequest
            {
                CompleteName = "Test User",
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                Role = "User",
                BirthDate = DateTime.Now.AddYears(-20),
                CPF = "123.456.789-00",
                PhoneNumber = "1234567890",
                IsActive = true
            };
            var userViewModel = new UserViewModel(Guid.NewGuid(), request.CompleteName, request.UserName, request.Email, request.Role, request.IsActive);
            _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), default)).ReturnsAsync(userViewModel);

            
            var result = await _userController.Register(request);

            
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(userViewModel);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            
            var request = new RegisterRequest();
            _userController.ModelState.AddModelError("Email", "Required");

            
            var result = await _userController.Register(request);

            
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
            var token = "fake-jwt-token";
            _mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), default)).ReturnsAsync(token);

            
            var result = await _userController.Login(request);

            
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(token);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            
            var request = new LoginRequest();
            _userController.ModelState.AddModelError("Email", "Required");

            
            var result = await _userController.Login(request);

            
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}