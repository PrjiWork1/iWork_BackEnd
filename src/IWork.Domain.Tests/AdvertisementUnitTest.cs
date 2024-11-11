using FluentAssertions;
using IWork.Domain.Models.Enums;
using IWork.Domain.Models;
using IWork.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Tests
{
    public class AdvertisementUnitTest
    {
        [Fact]
        public void WhenTitleIsEmpty_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "", // Title
                "Valid description",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid title. Title is required!");
        }

        [Fact]
        public void WhenTitleIsTooLong_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "TitleTestLongerThan100CharactersUnitTestForDomainExceptionToTestTitleLengthValidationInAdvertisementClassWithFluentAssertions\r\n",
                "Valid description",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Title is too long. Maximum length is 100 characters.");
        }

        [Fact]
        public void WhenDescriptionIsEmpty_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "", // Description is empty
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid description. Description is required!");
        }

        [Fact]
        public void WhenDescriptionIsTooLong_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "DescriptionTestLongerThan200CharactersUnitTestForDomainExceptionToTestDescriptionLengthValidationInAdvertisementClassWithFluentAssertionsToEnsureTheValidationWorksProperlyAndCoversEdgeCasesAndHandlesAllConditionsCorrectly",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Description is too long. Maximum length is 200 characters.");
        }

        [Fact]
        public void WhenUrlBannerIsEmpty_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "Valid description",
                "", // UrlBanner is empty
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid urlBanner. urlBanner is required!"); 
        }

        [Fact]
        public void WhenUserIdIsEmpty_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "Valid description",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.Empty.ToString(), // UserId is empty
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid UserId. UserId is required!");
        }

        [Fact]
        public void WhenCategoryIdIsEmpty_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "Valid description",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.Empty, // CategoryId is empty
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                100m
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid CategoryId. CategoryId is required!");
        }


        [Fact]
        public void WhenPriceIsNegative_ShouldThrowDomainException()
        {
            Action advertisement = () => new Advertisement(
                "Valid Title",
                "Valid description",
                "http://valid-url.com",
                AdvertisementType.Silver,
                Guid.NewGuid().ToString(),
                Guid.NewGuid(),
                true,
                DateTime.Now,
                AdvertisementStatus.NotApproved,
                0,
                -10m // Negative price
            );

            advertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid Price. Price must be greater than zero!");
        }

    }
}
