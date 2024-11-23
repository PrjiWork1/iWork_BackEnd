using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.UserHandler;
using IWork.Domain.Commands.UserCommands;
using IWork.Domain.Requests;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly LoginHandler _handler;

        public LoginHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new LoginHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldLoginSuccessfully()
        {
            
            var email = "test@example.com";
            var password = "ValidPassword123!";
            var token = "valid_token";

            _userServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequest>())).ReturnsAsync(token);

            var command = new LoginCommand(new LoginRequest { Email = email, Password = password });

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().Be(token);
            _userServiceMock.Verify(x => x.LoginAsync(It.IsAny<LoginRequest>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNullForInvalidCredentials()
        {
            
            var email = "test@example.com";
            var password = "InvalidPassword";
            var token = (string)null;

            _userServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequest>())).ReturnsAsync(token);

            var command = new LoginCommand(new LoginRequest { Email = email, Password = password });

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeNull();
            _userServiceMock.Verify(x => x.LoginAsync(It.IsAny<LoginRequest>()), Times.Once);
        }
    }
}