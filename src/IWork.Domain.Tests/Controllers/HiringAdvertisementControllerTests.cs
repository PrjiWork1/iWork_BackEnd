using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.API.Controllers;
using IWork.Domain.Commands.HiringAdvertisementCommands;
using IWork.Domain.Models.Enums;
using IWork.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IWork.Domain.Tests.Controllers
{
    public class HiringAdvertisementControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly HiringAdvertisementController _controller;

        public HiringAdvertisementControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new HiringAdvertisementController(_mediatorMock.Object);
            SetUserRole("Admin");
        }

        // Método auxiliar para definir o contexto do usuário autenticado
        private void SetUserRole(string role)
        {
            var user = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role) }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAtRoute_WhenResponseIsTrue()
        {
            
            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                new AdvertisementTemplate(),
                AdvertisementType.Gold,
                new List<HiringItemAdvertisementRequest>(),
                5000.0m,
                true
            );

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<HiringAdvertisementAddCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            
            var response = await _controller.Post(command);

            
            response.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenResponseIsFalse()
        {
            
            var command = new HiringAdvertisementAddCommand(
                Guid.NewGuid(),
                "contractorId",
                "advertiserId",
                new AdvertisementTemplate(),
                AdvertisementType.Silver,
                new List<HiringItemAdvertisementRequest>(),
                5000.0m,
                true
            );

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<HiringAdvertisementAddCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            
            var response = await _controller.Post(command);

            
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}