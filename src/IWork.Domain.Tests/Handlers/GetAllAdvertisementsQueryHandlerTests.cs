using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.AdvertisementHandler;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class GetAllAdvertisementsQueryHandlerTests
    {
        private readonly Mock<IAdvertisementService> _advertisementServiceMock;
        private readonly GetAllAdvertisementsQueryHandler _handler;

        public GetAllAdvertisementsQueryHandlerTests()
        {
            _advertisementServiceMock = new Mock<IAdvertisementService>();
            _handler = new GetAllAdvertisementsQueryHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAdvertisementsForAdmin()
        {
            
            var advertisements = new List<Advertisement>
            {
                new Advertisement(
                    "Ad1",
                    "Description1",
                    "Url1",
                    AdvertisementType.Gold,
                    "UserId1",
                    Guid.NewGuid(),
                    true,
                    DateTime.UtcNow,
                    AdvertisementStatus.Approved,
                    10,
                    100m
                )
                {
                    User = new User { UserName = "User1", CompleteName = "CompleteName1", Email = "Email1" },
                    Category = new Category { Description = "Category1" },
                    Items = new List<ItemAdvertisement>
                    {
                        new ItemAdvertisement("Item1", 50)
                    }
                }
            };

            _advertisementServiceMock.Setup(x => x.GetAllAdvertisements(true)).ReturnsAsync(advertisements);

            var query = new GetAllAdvertisementsQuery(true);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Title.Should().Be("Ad1");
            _advertisementServiceMock.Verify(x => x.GetAllAdvertisements(true), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnAdvertisementsForNonAdmin()
        {
            
            var advertisements = new List<Advertisement>
            {
                new Advertisement(
                    "Ad2",
                    "Description2",
                    "Url2",
                    AdvertisementType.Silver,
                    "UserId2",
                    Guid.NewGuid(),
                    true,
                    DateTime.UtcNow,
                    AdvertisementStatus.UnderReview,
                    5,
                    200m
                )
                {
                    User = new User { UserName = "User2", CompleteName = "CompleteName2", Email = "Email2" },
                    Category = new Category { Description = "Category2" },
                    Items = new List<ItemAdvertisement>
                    {
                        new ItemAdvertisement("Item2", 100)
                    }
                }
            };

            _advertisementServiceMock.Setup(x => x.GetAllAdvertisements(false)).ReturnsAsync(advertisements);

            var query = new GetAllAdvertisementsQuery(false);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Title.Should().Be("Ad2");
            _advertisementServiceMock.Verify(x => x.GetAllAdvertisements(false), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListWhenNoAdvertisementsAvailable()
        {
            
            _advertisementServiceMock.Setup(x => x.GetAllAdvertisements(It.IsAny<bool>())).ReturnsAsync(new List<Advertisement>());

            var query = new GetAllAdvertisementsQuery(true);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _advertisementServiceMock.Verify(x => x.GetAllAdvertisements(It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListWhenServiceReturnsNull()
        {
            
            _advertisementServiceMock.Setup(x => x.GetAllAdvertisements(It.IsAny<bool>())).ReturnsAsync((List<Advertisement>)null);

            var query = new GetAllAdvertisementsQuery(true);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _advertisementServiceMock.Verify(x => x.GetAllAdvertisements(It.IsAny<bool>()), Times.Once);
        }
    }
}