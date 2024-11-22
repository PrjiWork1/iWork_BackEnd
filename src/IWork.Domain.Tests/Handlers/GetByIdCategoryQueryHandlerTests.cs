using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.CategoryHandler;
using IWork.Domain.Models;
using IWork.Domain.Queries.CategoryQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class GetByIdCategoryQueryHandlerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly GetByIdCategoryQueryHandler _handler;

        public GetByIdCategoryQueryHandlerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _handler = new GetByIdCategoryQueryHandler(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCategoryById()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Description = "Category1",
                IsActive = true
            };

            _categoryServiceMock.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);

            var query = new GetByIdCategoryQuery(categoryId);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Id.Should().Be(categoryId);
            result.Description.Should().Be("Category1");
            result.IsActive.Should().BeTrue();
            _categoryServiceMock.Verify(x => x.GetById(categoryId), Times.Once);
        }
    }
}