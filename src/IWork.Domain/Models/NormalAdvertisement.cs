using IWork.Domain.Models.Enums;
using IWork.Domain.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class NormalAdvertisement : Advertisement
    {
        public NormalAdvertisement(string title, string description, string urlBanner, AdvertisementType type, 
            bool iWorkPro, string userId, Guid categoryId, bool isActive, DateTime createdAt, decimal price, 
            AdvertisementStatus status) : 
            base(title, description, urlBanner, type, iWorkPro, userId, categoryId, isActive, createdAt, status)
        {
            ValidatePrice(price);

            Price = price;
        }

        public decimal Price { get; set; }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidations.ExceptionHandler(price <= 0, "Invalid Price. Price must be greater than zero!");
        }
    }
}
