using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IWork.API.Controllers;
using MediatR;
using IWork.Domain.Commands;
using IWork.Domain.Queries;
using FluentAssertions;
using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Requests;
using IWork.Domain.Queries.CategoryQuery;
using IWork.Domain.ViewModels;

namespace IWork.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CategoryController(_mediatorMock.Object);
            SetUserRole("Admin");
        }

        private void SetUserRole(string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, role)
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenCategoriesExist()
        {
            
            var categories = new List<CategoryViewModel> 
            { 
                new CategoryViewModel (Guid.NewGuid(), "Category1", true ), 
                new CategoryViewModel(Guid.NewGuid(), "Category2", true) 
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCategoriesQuery>(), default!))
                .ReturnsAsync(categories);

            
            var result = await _controller.GetAll();

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoCategoriesExist()
        {
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCategoriesQuery>(), default))
                .ReturnsAsync((List<CategoryViewModel>?)null);

            
            var result = await _controller.GetAll();

            
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenCategoryExists()
        {
            
            var categoryId = Guid.NewGuid();
            var category = new CategoryViewModel(categoryId, "Category1", true);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdCategoryQuery>(), default!))
                .ReturnsAsync(category);

            
            var result = await _controller.GetById(categoryId);

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GetById_ShouldReturnNoContent_WhenCategoryDoesNotExist()
        {
            
            var categoryId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdCategoryQuery>(), default!))
                .ReturnsAsync((CategoryViewModel?)null);

            
            var result = await _controller.GetById(categoryId);

            
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Post_ShouldReturnCreated_WhenCategoryIsCreated()
        {
            
            var command = new CategoryAddCommand("NewCategory", true);
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(true);

            
            var result = await _controller.Post(command);

            
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenCategoryCreationFails()
        {
            
            var command = new CategoryAddCommand("NewCategory", true);
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(false);

            
            var result = await _controller.Post(command);

            
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Put_ShouldReturnOk_WhenCategoryIsUpdated()
        {
            
            var categoryId = Guid.NewGuid();
            var request = new CategoryRequest("UpdatedCategory", true);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CategoryUpdateCommand>(), default))
                .ReturnsAsync(true);

            
            var result = await _controller.Put(categoryId, request);

            
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenCategoryUpdateFails()
        {
            
            var categoryId = Guid.NewGuid();
            var request = new CategoryRequest("UpdatedCategory", true);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CategoryUpdateCommand>(), default))
                .ReturnsAsync(false);

            
            var result = await _controller.Put(categoryId, request);

            
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenCategoryIsDeleted()
        {
            
            var categoryId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CategoryDeleteCommand>(), default))
                .ReturnsAsync(true);

            
            var result = await _controller.Delete(categoryId);

            
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenCategoryDeletionFails()
        {
            
            var categoryId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CategoryDeleteCommand>(), default))
                .ReturnsAsync(false);

            
            var result = await _controller.Delete(categoryId);

            
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
