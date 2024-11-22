using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.AdvertisementHandler;
using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Requests;
using IWork.Domain.Validations;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class UpdateAdvertisementStatusHandlerTests
    {
        private readonly Mock<IAdvertisementService> _advertisementServiceMock;
        private readonly UpdateAdvertisementStatusHandler _handler;

        public UpdateAdvertisementStatusHandlerTests()
        {
            _advertisementServiceMock = new Mock<IAdvertisementService>();
            _handler = new UpdateAdvertisementStatusHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateStatusSuccessfully()
        {
            var advertisementId = Guid.NewGuid();
            var advertisement = new Advertisement(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                true,
                DateTime.UtcNow,
                AdvertisementStatus.UnderReview,
                5,
                100m
            );

            var request = new UpdateAdvertisementStatusCommand(advertisementId, new AdvertisementStatusRequest(AdvertisementStatus.Approved));

            _advertisementServiceMock.Setup(x => x.GetById(advertisementId)).ReturnsAsync(advertisement);
            _advertisementServiceMock.Setup(x => x.Update(advertisement.Id)).ReturnsAsync(true);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.GetById(advertisementId), Times.Once);
            _advertisementServiceMock.Verify(x => x.Update(advertisement.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenAdvertisementNotFound()
        {
            var advertisementId = Guid.NewGuid();
            var request = new UpdateAdvertisementStatusCommand(advertisementId, new AdvertisementStatusRequest(AdvertisementStatus.Approved));

            _advertisementServiceMock.Setup(x => x.GetById(advertisementId)).ReturnsAsync((Advertisement)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.Should().BeFalse();
            _advertisementServiceMock.Verify(x => x.GetById(advertisementId), Times.Once);
            _advertisementServiceMock.Verify(x => x.Update(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenUpdateFails()
        {
            var advertisementId = Guid.NewGuid();
            var advertisement = new Advertisement(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                true,
                DateTime.UtcNow,
                AdvertisementStatus.UnderReview,
                5,
                100m
            );

            var request = new UpdateAdvertisementStatusCommand(advertisementId, new AdvertisementStatusRequest(AdvertisementStatus.Approved));

            _advertisementServiceMock.Setup(x => x.GetById(advertisementId)).ReturnsAsync(advertisement);
            _advertisementServiceMock.Setup(x => x.Update(advertisement.Id)).ReturnsAsync(false);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.Should().BeFalse();
            _advertisementServiceMock.Verify(x => x.GetById(advertisementId), Times.Once);
            _advertisementServiceMock.Verify(x => x.Update(advertisement.Id), Times.Once);
        }
    }
}