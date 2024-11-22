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
    public class BaseServiceTests
    {
        private readonly DataContext _context;
        private readonly BaseService<Advertisement> _service;

        public BaseServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "BaseServiceTests")
                .Options;

            _context = new DataContext(options);
            _service = new BaseService<Advertisement>(_context);
        }

        [Fact]
        public async Task Add_ShouldAddEntitySuccessfully()
        {
            // Arrange
            var advertisement = new Advertisement(
                "Title",
                "Description",
                "UrlBanner",
                AdvertisementType.Gold,
                "UserId",
                Guid.NewGuid(),
                true,
                DateTime.UtcNow,
                AdvertisementStatus.Approved,
                5,
                100m
            );

            // Act
            var result = await _service.Add(advertisement);

            // Assert
            result.Should().BeTrue();
            _context.Advertisement.Should().Contain(advertisement);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var advertisements = new List<Advertisement>
            {
                new Advertisement(
                    "Title1",
                    "Description1",
                    "UrlBanner1",
                    AdvertisementType.Gold,
                    "UserId1",
                    Guid.NewGuid(),
                    true,
                    DateTime.UtcNow,
                    AdvertisementStatus.Approved,
                    5,
                    100m
                ),
                new Advertisement(
                    "Title2",
                    "Description2",
                    "UrlBanner2",
                    AdvertisementType.Gold,
                    "UserId2",
                    Guid.NewGuid(),
                    true,
                    DateTime.UtcNow,
                    AdvertisementStatus.Approved,
                    5,
                    100m
                )
            };

            _context.Advertisement.AddRange(advertisements);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntityById()
        {
            // Arrange
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
                AdvertisementStatus.Approved,
                5,
                100m
            )
            {
                Id = advertisementId
            };

            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetById(advertisementId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(advertisementId);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntitySuccessfully()
        {
            // Arrange
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
                AdvertisementStatus.Approved,
                5,
                100m
            )
            {
                Id = advertisementId
            };

            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();

            advertisement.Title = "Updated Title";

            // Act
            var result = await _service.Update(advertisementId);

            // Assert
            result.Should().BeTrue();
            var updatedAdvertisement = await _service.GetById(advertisementId);
            updatedAdvertisement.Title.Should().Be("Updated Title");
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntitySuccessfully()
        {
            // Arrange
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
                AdvertisementStatus.Approved,
                5,
                100m
            )
            {
                Id = advertisementId
            };

            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.Delete(advertisementId);

            // Assert
            result.Should().BeTrue();
            var deletedAdvertisement = await _service.GetById(advertisementId);
            deletedAdvertisement.Should().NotBeNull();
            deletedAdvertisement.IsActive.Should().BeFalse();
        }
    }
}