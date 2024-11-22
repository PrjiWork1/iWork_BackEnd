using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Service.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IWork.Service.Tests.Services
{
    public class AdvertisementServiceTests
    {
        private readonly DataContext _context;
        private readonly AdvertisementService _service;

        public AdvertisementServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "AdvertisementServiceTests")
                .Options;

            _context = new DataContext(options);
            _service = new AdvertisementService(_context);
        }

        [Fact]
        public async Task GetAdvertisementById_ShouldReturnNullWhenAdvertisementNotFound()
        {
            // Arrange
            var advertisementId = Guid.NewGuid();

            // Act
            var result = await _service.GetAdvertisementById(advertisementId);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task GetAllAdvertisements_ShouldReturnEmptyListWhenNoAdvertisementsAvailable()
        {
            // Act
            var result = await _service.GetAllAdvertisements(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}