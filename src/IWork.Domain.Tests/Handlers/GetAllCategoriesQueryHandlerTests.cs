using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllCategoriesQueryHandlerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly GetAllCategoriesQueryHandler _handler;

        public GetAllCategoriesQueryHandlerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _handler = new GetAllCategoriesQueryHandler(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCategoriesSuccessfully()
        {
            
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Description = "Category1", IsActive = true },
                new Category { Id = Guid.NewGuid(), Description = "Category2", IsActive = false }
            };

            _categoryServiceMock.Setup(x => x.GetAll()).ReturnsAsync(categories);

            var query = new GetAllCategoriesQuery();

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Description.Should().Be("Category1");
            _categoryServiceMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListWhenNoCategoriesAvailable()
        {
            
            _categoryServiceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Category>());

            var query = new GetAllCategoriesQuery();

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _categoryServiceMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListWhenServiceReturnsNull()
        {
            
            _categoryServiceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Category>());

            var query = new GetAllCategoriesQuery();

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _categoryServiceMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}