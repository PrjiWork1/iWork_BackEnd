using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.AdvertisementHandler;
using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class CreateNormalAdvertisementHandlerTests
    {
        private readonly Mock<IAdvertisementService> _advertisementServiceMock;
        private readonly CreateNormalAdvertisementHandler _handler;

        public CreateNormalAdvertisementHandlerTests()
        {
            _advertisementServiceMock = new Mock<IAdvertisementService>();
            _handler = new CreateNormalAdvertisementHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAdvertisementSuccessfully()
        {
            
            var command = new NormalAdvertisementAddCommand(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                100m,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<Advertisement>())).ReturnsAsync(true);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseForInvalidAdvertisementData()
        {
            
            var command = new NormalAdvertisementAddCommand(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                100m,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<Advertisement>())).ReturnsAsync(false);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeFalse();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionForNullAdvertisementData()
        {
            
            NormalAdvertisementAddCommand command = null;

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}