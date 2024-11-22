using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Controllers;
using IWork.Domain.Commands.UploadCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IWork.API.Tests.Controllers
{
    public class UploadControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly UploadController _uploadController;

        public UploadControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _uploadController = new UploadController(_mockMediator.Object);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldReturnBadRequest_WhenFileIsNull()
        {
            
            var result = await _uploadController.UploadProfilePicture(null);

            
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestResult.Value.Should().Be("File not found.");
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldReturnOkResult_WhenFileIsUploaded()
        {
            
            var mockFile = new Mock<IFormFile>();
            var fileName = "test.jpg";
            mockFile.Setup(f => f.FileName).Returns(fileName);
            _mockMediator.Setup(m => m.Send(It.IsAny<UploadAddCommand>(), default)).ReturnsAsync("path/to/file");

            
            var result = await _uploadController.UploadProfilePicture(mockFile.Object);

            
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().BeEquivalentTo(new { path = "path/to/file" });
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            
            var mockFile = new Mock<IFormFile>();
            var fileName = "test.jpg";
            mockFile.Setup(f => f.FileName).Returns(fileName);
            _mockMediator.Setup(m => m.Send(It.IsAny<UploadAddCommand>(), default)).ThrowsAsync(new System.Exception("Test Exception"));

            
            Func<Task> act = async () => await _uploadController.UploadProfilePicture(mockFile.Object);

            
            await act.Should().ThrowAsync<System.Exception>().WithMessage("Test Exception");
        }
    }
}