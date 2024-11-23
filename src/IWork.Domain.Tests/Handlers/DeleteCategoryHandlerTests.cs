using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.CategoryHandler;
using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class DeleteCategoryHandlerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly DeleteCategoryHandler _handler;

        public DeleteCategoryHandlerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _handler = new DeleteCategoryHandler(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCategorySuccessfully()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Description = "Category1" };

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);
            _categoryServiceMock.Setup(x => x.Delete(categoryId)).ReturnsAsync(true);

            var request = new CategoryDeleteCommand(categoryId);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeTrue();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Delete(categoryId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenCategoryNotFound()
        {
            
            var categoryId = Guid.NewGuid();

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync((Category)null);

            var request = new CategoryDeleteCommand(categoryId);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeFalse();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenDeleteFails()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Description = "Category1" };

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);
            _categoryServiceMock.Setup(x => x.Delete(categoryId)).ReturnsAsync(false);

            var request = new CategoryDeleteCommand(categoryId);

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            result.Should().BeFalse();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
            _categoryServiceMock.Verify(x => x.Delete(categoryId), Times.Once);
        }
    }
}