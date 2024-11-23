using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.UserHandler;
using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class RegisterHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly RegisterHandler _handler;

        public RegisterHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new RegisterHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRegisterUserSuccessfully()
        {
            var request = new RegisterRequest
            {
                CompleteName = "Complete Name",
                UserName = "UserName",
                Email = "test@example.com",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!",
                Role = "User",
                BirthDate = new DateTime(1990, 1, 1),
                CPF = "123.456.789-00",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            var user = new UserViewModel(
                Guid.NewGuid(),
                request.CompleteName,
                request.UserName,
                request.Email,
                request.Role,
                request.IsActive
            );

            _userServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterRequest>())).ReturnsAsync(user);

            var command = new RegisterCommand(request);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Email.Should().Be(request.Email);
            _userServiceMock.Verify(x => x.RegisterAsync(It.IsAny<RegisterRequest>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNullWhenServiceFails()
        {
            var request = new RegisterRequest
            {
                CompleteName = "Complete Name",
                UserName = "UserName",
                Email = "test@example.com",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!",
                Role = "User",
                BirthDate = new DateTime(1990, 1, 1),
                CPF = "123.456.789-00",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            _userServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterRequest>())).ReturnsAsync((UserViewModel)null);

            var command = new RegisterCommand(request);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeNull();
            _userServiceMock.Verify(x => x.RegisterAsync(It.IsAny<RegisterRequest>()), Times.Once);
        }
    }
}