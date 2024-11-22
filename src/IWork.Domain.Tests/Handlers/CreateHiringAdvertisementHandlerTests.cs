using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.HiringAdvertisementHandler;
using IWork.Domain.Commands.HiringAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Requests;
using IWork.Domain.Validations;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class CreateHiringAdvertisementHandlerTests
    {
        private readonly Mock<IHiringAdvertisementService> _advertisementServiceMock;
        private readonly CreateHiringAdvertisementHandler _handler;

        public CreateHiringAdvertisementHandlerTests()
        {
            _advertisementServiceMock = new Mock<IHiringAdvertisementService>();
            _handler = new CreateHiringAdvertisementHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNormalHiringAdvertisementSuccessfully()
        {
            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                AdvertisementTemplate.Normal,
                AdvertisementType.Gold,
                null,
                5000.0m,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<HiringAdvertisement>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<HiringAdvertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreateDynamicHiringAdvertisementSuccessfully()
        {
            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                AdvertisementTemplate.Dynamic,
                AdvertisementType.Gold,
                new List<HiringItemAdvertisementRequest>
                {
                    new HiringItemAdvertisementRequest("Item1", 100),
                    new HiringItemAdvertisementRequest("Item2", 200)
                },
                0,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<HiringAdvertisement>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);


            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<HiringAdvertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreateDynamicHiringAdvertisementWithNullItems()
        {

            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                AdvertisementTemplate.Dynamic,
                AdvertisementType.Gold,
                null,
                0,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<HiringAdvertisement>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);


            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<HiringAdvertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalseWhenServiceFailsToAddAdvertisement()
        {

            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                AdvertisementTemplate.Normal,
                AdvertisementType.Gold,
                null,
                5000.0m,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<HiringAdvertisement>())).ReturnsAsync(false);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<HiringAdvertisement>()), Times.Once);
        }
    }
}