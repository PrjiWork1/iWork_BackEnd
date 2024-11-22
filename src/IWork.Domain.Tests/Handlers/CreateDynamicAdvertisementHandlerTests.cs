using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CreateDynamicAdvertisementHandlerTests
    {
        private readonly Mock<IAdvertisementService> _advertisementServiceMock;
        private readonly CreateDynamicAdvertisementHandler _handler;

        public CreateDynamicAdvertisementHandlerTests()
        {
            _advertisementServiceMock = new Mock<IAdvertisementService>();
            _handler = new CreateDynamicAdvertisementHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAdvertisementSuccessfully()
        {
            
            var command = new DynamicAdvertisementAddCommand(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                new List<ItemAdvertisementRequest>
                {
                    new ItemAdvertisementRequest("Item1", 100),
                    new ItemAdvertisementRequest("Item2", 200)
                },
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<Advertisement>())).ReturnsAsync(true);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionForDuplicateItems()
        {
            
            var command = new DynamicAdvertisementAddCommand(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                new List<ItemAdvertisementRequest>
                {
                    new ItemAdvertisementRequest("Item1", 100),
                    new ItemAdvertisementRequest("Item1", 100)
                },
                true
            );

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            await act.Should().ThrowAsync<Exception>().WithMessage("Item already exists in the advertisement.");
        }

        [Fact]
        public async Task Handle_ShouldCreateAdvertisementWithNullItems()
        {
            
            var command = new DynamicAdvertisementAddCommand(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                null,
                true
            );

            _advertisementServiceMock.Setup(x => x.Add(It.IsAny<Advertisement>())).ReturnsAsync(true);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().BeTrue();
            _advertisementServiceMock.Verify(x => x.Add(It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionForInvalidAdvertisementData()
        {
            
            var command = new DynamicAdvertisementAddCommand(
                "",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                new List<ItemAdvertisementRequest>
                {
                    new ItemAdvertisementRequest("Item1", 100)
                },
                true
            );

            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            await act.Should().ThrowAsync<DomainExceptionValidations>().WithMessage("Invalid title. Title is required!");
        }
    }
}