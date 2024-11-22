using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Handlers.AdvertisementHandler;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Service.Interfaces;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Handlers
{
    public class GetByIdAdvertisementQueryHandlerTests
    {
        private readonly Mock<IAdvertisementService> _advertisementServiceMock;
        private readonly GetByIdAdvertisementQueryHandler _handler;

        public GetByIdAdvertisementQueryHandlerTests()
        {
            _advertisementServiceMock = new Mock<IAdvertisementService>();
            _handler = new GetByIdAdvertisementQueryHandler(_advertisementServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAdvertisementById()
        {
            
            var advertisement = new Advertisement(
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
                Id = Guid.NewGuid(),
                User = new User { UserName = "User1", CompleteName = "CompleteName1", Email = "Email1" },
                Category = new Category { Description = "Category1" },
                Items = new List<ItemAdvertisement>
                {
                    new ItemAdvertisement("Item1", 50)
                }
            };

            _advertisementServiceMock.Setup(x => x.GetAdvertisementById(advertisement.Id)).ReturnsAsync(advertisement);

            var query = new GetByIdAdvertisementQuery(advertisement.Id);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Id.Should().Be(advertisement.Id);
            result.Title.Should().Be("Ad1");
            _advertisementServiceMock.Verify(x => x.GetAdvertisementById(advertisement.Id), Times.Once);
        }

        [Fact]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            
            Func<Task> act = async () => await _handler.Handle(null, CancellationToken.None);

            
            act.Should().ThrowAsync<ArgumentNullException>().WithMessage("*request*");
        }

        [Fact]
        public void Handle_ShouldThrowArgumentException_WhenAdvertisementIdIsInvalid()
        {
            
            var query = new GetByIdAdvertisementQuery(Guid.Empty);

            
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            
            act.Should().ThrowAsync<ArgumentException>().WithMessage("*AdvertisementId*");
        }
    }
}