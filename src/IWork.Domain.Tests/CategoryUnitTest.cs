using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IWork.Domain.Models;
using IWork.Domain.Validations;

namespace IWork.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact]
        public void WhenCategoryName_LenghtMoreThan_DomainException()
        {
            Action category = () => new Category("DescriptionTestLongerThan20CharactersUnitTestForDomainException", true);
            category.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Description is too long. Maximum length is 20 characters.");
        }

        [Fact]
        public void WhenCategoryName_IsEmpty_DomainException()
        {
            Action category = () => new Category("", true);
            category.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Invalid Description. Description is required!");
        }

        [Fact]
        public void WhenCategoryName_LenghtLessThan_DomainException()
        {
            Action category = () => new Category("TV", true);
            category.Should().Throw<DomainExceptionValidations>()
                .WithMessage("Description is too short. Minimum length is 4 characters.");
        }
    }
}
