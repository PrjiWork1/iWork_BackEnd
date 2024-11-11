using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Controllers;
using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Models.Enums;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Controllers
{
    public class AdvertisementControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AdvertisementController _controller;

        public AdvertisementControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AdvertisementController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAdvertisements_ShouldReturnOk_WhenResultIsNotNull()
        {
            // Arrange
            var result = new List<AdvertisementViewModel>
            {
                new AdvertisementViewModel(Guid.NewGuid(), "Ad1", "Description1", "Category1", AdvertisementType.Silver,
                    "Location1", "Contact1", "Email1", "Phone1", Guid.NewGuid(), "Image1", 100.0m, DateTime.Now,
                    10.0m, AdvertisementStatus.UnderReview, 0, new List<ItemAdvertisementViewModel>(), true)
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllAdvertisementsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetAll(false);

            // Assert
            response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(result);
        }

        [Fact]
    public async Task GetAllAdvertisements_ShouldReturnNoContent_WhenResultIsEmpty()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllAdvertisementsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AdvertisementViewModel>());

        // Act
        var response = await _controller.GetAll(false);

        // Assert
        response.Should().BeOfType<NoContentResult>();
    }

        [Fact]
        public async Task GetAdvertisementById_ShouldReturnOk_WhenResultIsNotNull()
        {
            // Arrange
            var advertisementId = Guid.NewGuid();
            var result = new AdvertisementViewModel(advertisementId, "Ad1", "Description1", "Category1", AdvertisementType.Silver,
                "Location1", "Contact1", "Email1", "Phone1", Guid.NewGuid(), "Image1", 100.0m, DateTime.Now,
                10.0m, AdvertisementStatus.UnderReview, 0, new List<ItemAdvertisementViewModel>(), true);

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetByIdAdvertisementQuery>(q => q.Id == advertisementId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetById(advertisementId);

            // Assert
            response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetAdvertisementById_ShouldReturnNoContent_WhenResultIsNull()
        {
            // Arrange
            var advertisementId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<GetByIdAdvertisementQuery>(q => q.Id == advertisementId), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AdvertisementViewModel)null);

            // Act
            var response = await _controller.GetById(advertisementId);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task AddNormalAdvertisement_ShouldReturnCreatedAtRoute_WhenResponseIsTrue()
        {
            // Arrange
            var command = new NormalAdvertisementAddCommand("title", "description", "category", AdvertisementType.Diamond,
                "location", Guid.NewGuid(), DateTime.Now, AdvertisementStatus.Approved, 100.0m, true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<NormalAdvertisementAddCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.AddNormalAdvertisement(command);

            // Assert
            response.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddNormalAdvertisement_ShouldReturnBadRequest_WhenResponseIsFalse()
        {
            // Arrange
            var command = new NormalAdvertisementAddCommand("title", "description", "category", AdvertisementType.Silver,
                "location", Guid.NewGuid(), DateTime.Now, AdvertisementStatus.Approved, 100.0m, true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<NormalAdvertisementAddCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.AddNormalAdvertisement(command);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateStatusAdvertisement_ShouldReturnOk_WhenResponseIsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AdvertisementStatusRequest(AdvertisementStatus.Approved);

            _mediatorMock
                .Setup(m => m.Send(It.Is<UpdateAdvertisementStatusCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.UpdateStatusAdvertisement(id, command);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateStatusAdvertisement_ShouldReturnNotFound_WhenResponseIsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AdvertisementStatusRequest(AdvertisementStatus.UnderReview);

            _mediatorMock
                .Setup(m => m.Send(It.Is<UpdateAdvertisementStatusCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.UpdateStatusAdvertisement(id, command);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Anúncio não encontrado");
        }
    }
}
