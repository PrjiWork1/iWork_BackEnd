using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.Data.Context;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace IWork.Service.Tests.Services
{
    public class UserServiceTests
    {
        private readonly DataContext _context;
        private readonly UserService _service;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UserServiceTests")
                .Options;

            _context = new DataContext(options);

            _userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _roleManagerMock = new Mock<RoleManager<Role>>(
                new Mock<IRoleStore<Role>>().Object, null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();

            _service = new UserService(_context, _userManagerMock.Object, _roleManagerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowExceptionForInvalidData()
        {
            // Arrange
            var request = new RegisterRequest
            {
                CompleteName = "",
                UserName = "UserName",
                Email = "invalid-email",
                Password = "short",
                ConfirmPassword = "short",
                Role = "InvalidRole",
                BirthDate = new DateTime(1990, 1, 1),
                CPF = "123.456.789-00",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid data" }));

            // Act
            Func<Task> act = async () => await _service.RegisterAsync(request);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid data");
        }


        [Fact]
        public async Task LoginAsync_ShouldThrowExceptionForInvalidCredentials()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "InvalidPassword"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await _service.LoginAsync(request);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUserByEmail()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                CompleteName = "Complete Name",
                UserName = "UserName",
                Email = email,
                IsActive = true
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _service.GetByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
            _userManagerMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(x => x.GetRolesAsync(It.IsAny<User>()), Times.Once);
        }
    }
}