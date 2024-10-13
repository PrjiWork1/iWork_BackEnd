using IWork.Domain.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class ItemAdvertisement
    {
        public ItemAdvertisement(string name, decimal price, Guid dynamicAdvertisementId)
        {
            ValidateAndSetValues(name, price, dynamicAdvertisementId);
        }

        public Guid Id { get; set; }
        public Guid DynamicAdvertisementId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }

        public DynamicAdvertisement DynamicAdvertisement { get; set; }

        private void ValidateAndSetValues(string name, decimal price, Guid dynamicAdvertisementId)
        {
            ValidateName(name);
            ValidatePrice(price);
            ValidateDynamicAdvertisementId(dynamicAdvertisementId);

            Name = name;
            Price = price;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid name. Name is required!");
            if (name.Length > 50)
                DomainExceptionValidations.ExceptionHandler(true, "Too long name.");
        }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidations.ExceptionHandler(price <= 0, "Invalid Price. Price must be greater than zero!");
        }

        private void ValidateDynamicAdvertisementId(Guid dynamicAdvertisementId)
        {
            DomainExceptionValidations.ExceptionHandler(dynamicAdvertisementId == Guid.Empty, "Invalid DynamicAdvertisementId. DynamicAdvertisementId is required!");
        }
    }

}

