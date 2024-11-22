using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.CategoryHandler;
using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Domain.Validations;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.API.Tests.Handlers.CategoryHandler
{
    public class CreateCategoryHandlerTest
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTest()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _handler = new CreateCategoryHandler(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateCategorySuccessfully()
        {
            
            var command = new CategoryAddCommand("Valid Description", true);
            _categoryServiceMock.Setup(x => x.Add(It.IsAny<Category>())).ReturnsAsync(true);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeTrue();
            _categoryServiceMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenServiceFailsToAddCategory()
        {
            
            var command = new CategoryAddCommand("Valid Description", true);
            _categoryServiceMock.Setup(x => x.Add(It.IsAny<Category>())).ReturnsAsync(false);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeFalse();
            _categoryServiceMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public void Handle_ShouldThrowExceptionForTooLongDescription()
        {
            
            var command = new CategoryAddCommand("DescriptionTestLongerThan30CharactersUnitTestForDomainException", true);

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Description is too long. Maximum length is 30 characters.");
        }

        [Fact]
        public void Handle_ShouldThrowExceptionForTooShortDescription()
        {
            
            var command = new CategoryAddCommand("TV", true);

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Description is too short. Minimum length is 4 characters.");
        }

        [Fact]
        public void Handle_ShouldThrowExceptionForEmptyDescription()
        {
            
            var command = new CategoryAddCommand("", true);

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            act.Should().ThrowAsync<DomainExceptionValidations>().WithMessage("Invalid Description. Description is required!");
        }
    }
}