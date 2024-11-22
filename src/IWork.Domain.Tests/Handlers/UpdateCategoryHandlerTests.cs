using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.CategoryHandler;
using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Domain.Requests;
using IWork.Domain.Validations;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class UpdateCategoryHandlerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly UpdateCategoryHandler _handler;

        public UpdateCategoryHandlerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _handler = new UpdateCategoryHandler(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateCategorySuccessfully()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Description = "Old Description",
                IsActive = false
            };

            var request = new CategoryUpdateCommand(categoryId, new CategoryRequest("New Description", true));

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);
            _categoryServiceMock.Setup(x => x.Update(category.Id)).ReturnsAsync(true);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeTrue();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Update(category.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenCategoryNotFound()
        {
            
            var categoryId = Guid.NewGuid();
            var request = new CategoryUpdateCommand(categoryId, new CategoryRequest("New Description", true));

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync((Category)null);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeFalse();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Update(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenUpdateFails()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Description = "Old Description",
                IsActive = false
            };

            var request = new CategoryUpdateCommand(categoryId, new CategoryRequest("New Description", true));

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);
            _categoryServiceMock.Setup(x => x.Update(category.Id)).ReturnsAsync(false);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeFalse();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Update(category.Id), Times.Once);
        }
    }
}