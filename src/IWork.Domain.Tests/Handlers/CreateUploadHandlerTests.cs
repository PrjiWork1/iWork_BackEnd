using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.UploadHandler;
using IWork.Domain.Commands.UploadCommands;
using IWork.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class CreateUploadHandlerTests
    {
        private readonly Mock<IBlobService> _blobServiceMock;
        private readonly CreateUploadHandler _handler;

        public CreateUploadHandlerTests()
        {
            _blobServiceMock = new Mock<IBlobService>();
            _handler = new CreateUploadHandler(_blobServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUploadFileSuccessfully()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.ContentType).Returns("text/plain");

            var uri = new Uri("http://localhost/test.txt");
            _blobServiceMock.Setup(x => x.UploadFileBlobAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(uri);

            var command = new UploadAddCommand(fileMock.Object);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(uri.AbsoluteUri);
            _blobServiceMock.Verify(x => x.UploadFileBlobAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionWhenFileIsNull()
        {

            var command = new UploadAddCommand(null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("File not found. (Parameter 'File')");
        }
    }
}