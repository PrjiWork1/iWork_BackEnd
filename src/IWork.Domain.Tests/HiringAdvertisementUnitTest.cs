using FluentAssertions;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Tests
{
    public class HiringAdvertisementUnitTest
    {
        [Fact]
        public void WhenAdvertisementIdIsEmpty_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.Empty, 
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenAdvertisementIdIsEmpty_ShouldThrowDomainException",
                100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid AdvertisementId. AdvertisementId is required!");
        }

        [Fact]
        public void WhenContractorIdIsEmpty_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                "",
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenContractorIdIsEmpty_ShouldThrowDomainException",
                100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid ContractorId. ContractorId is required!");
        }

        [Fact]
        public void WhenAdvertiserIdIsEmpty_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                "",
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenAdvertiserIdIsEmpty_ShouldThrowDomainException",
                100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid AdvertiserId. AdvertiserId is required!");
        }

        [Fact]
        public void WhenPreferenceIdIsEmpty_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "",
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenPreferenceIdIsEmpty_ShouldThrowDomainException",
                100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid PreferenceId. PreferenceId is required!");
        }

        [Fact]
        public void WhenDescriptionIsEmpty_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "",
                100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid description. Description is required!");
        }

        [Fact]
        public void WhenDescriptionIsTooLong_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
               Guid.NewGuid(),
               Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString(),
               AdvertisementTemplate.Normal,
               AdvertisementType.Silver,
               HiringStatus.Failure,
               "DescriptionTestLongerThan200CharactersUnitTestForDomainExceptionToTestDescriptionLengthValidationInHiringAdvertisementClassWithFluentAssertionsToEnsureTheValidationWorksProperlyAndCoversEdgeCasesAndHandlesAllConditionsCorrectly",
               100m,
               0.0999m,
               110m,
               true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Description is too long. Maximum length is 200 characters.");
        }

        [Fact]
        public void WhenPriceIsNegative_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenPriceIsNegative_ShouldThrowDomainException",
                -100m,
                0.0999m,
                110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid Price. Price must be greater than zero!");
        }

        [Fact]
        public void WhenTotalAmountIsNegative_ShouldThrowDomainException()
        {
            Action hiringAdvertisement = () => new HiringAdvertisement(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                AdvertisementTemplate.Normal,
                AdvertisementType.Silver,
                HiringStatus.Failure,
                "WhenTotalAmountIsNegative_ShouldThrowDomainException",
                100m,
                0.0999m,
                -110m,
                true
            );

            hiringAdvertisement.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid TotalAmount. Price must be greater than zero!");
        }

    }
}
