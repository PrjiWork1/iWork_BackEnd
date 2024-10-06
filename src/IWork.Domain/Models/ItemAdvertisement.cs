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
        public ItemAdvertisement(string name, decimal price, int stock, Guid dynamicAdvertisementId)
        {
            ValidateAndSetValues(name, price, stock, dynamicAdvertisementId);
        }

        public Guid Id { get; set; }
        public Guid DynamicAdvertisementId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public DynamicAdvertisement DynamicAdvertisement { get; set; }

        private void ValidateAndSetValues(string name, decimal price, int stock, Guid dynamicAdvertisementId)
        {
            ValidateName(name);
            ValidatePrice(price);
            ValidateStock(stock);
            ValidateDynamicAdvertisementId(dynamicAdvertisementId);

            Name = name;
            Price = price;
            Stock = stock;
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

        private void ValidateStock(int stock)
        {
            DomainExceptionValidations.ExceptionHandler(stock < 0, "Invalid Stock. Stock must be greater than or equal to zero!");
        }


        private void ValidateDynamicAdvertisementId(Guid dynamicAdvertisementId)
        {
            DomainExceptionValidations.ExceptionHandler(dynamicAdvertisementId == Guid.Empty, "Invalid DynamicAdvertisementId. DynamicAdvertisementId is required!");
        }
    }

}

