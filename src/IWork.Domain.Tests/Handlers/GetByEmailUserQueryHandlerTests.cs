using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.UserHandler;
using IWork.Domain.Queries.UserQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class GetByEmailUserQueryHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly GetByEmailUserQueryHandler _handler;

        public GetByEmailUserQueryHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new GetByEmailUserQueryHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUserByEmail()
        {
            
            var email = "test@example.com";
            var user = new UserViewModel(
                Guid.NewGuid(),
                "Complete Name",
                "UserName",
                email,
                "User",
                true
            );

            _userServiceMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            var query = new GetByEmailUserQuery(email);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
            _userServiceMock.Verify(x => x.GetByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNullWhenUserNotFound()
        {
            
            var email = "test@example.com";

            _userServiceMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((UserViewModel)null);

            var query = new GetByEmailUserQuery(email);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().BeNull();
            _userServiceMock.Verify(x => x.GetByEmailAsync(email), Times.Once);
        }
    }
}