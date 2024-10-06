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
            bool iWorkPro, string userId, Guid categoryId, bool isActive, DateTime createdAt, decimal price, int stock) : 
            base(title, description, urlBanner, type, iWorkPro, userId, categoryId, isActive, createdAt)
        {
            ValidatePrice(price);
            ValidateStock(stock);

            Price = price;
            Stock = stock;
        }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidations.ExceptionHandler(price <= 0, "Invalid Price. Price must be greater than zero!");
        }

        private void ValidateStock(int stock)
        {
            DomainExceptionValidations.ExceptionHandler(stock < 0, "Invalid Stock. Stock must be greater than or equal to zero!");
        }
    }
}
