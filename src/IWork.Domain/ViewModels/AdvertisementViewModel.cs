using IWork.Domain.Models.Enums;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.ViewModels
{
    public class AdvertisementViewModel
    {
        public AdvertisementViewModel(Guid id, string title, string description, string urlBanner, 
            AdvertisementType type, bool iWorkPro, string userId, string userName, string completeName, 
            Guid categoryId, string categoryDescription, decimal advertisementRate, DateTime createdAt, 
            decimal price, bool isActive)
        {
            Id = id;
            Title = title;
            Description = description;
            UrlBanner = urlBanner;
            Type = type;
            IWorkPro = iWorkPro;
            UserId = userId;
            UserName = userName;
            CompleteName = completeName;
            CategoryId = categoryId;
            CategoryDescription = categoryDescription;
            AdvertisementRate = advertisementRate;
            CreatedAt = createdAt;
            Price = price;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlBanner { get; set; }
        public AdvertisementType Type { get; set; }
        public bool IWorkPro { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public decimal AdvertisementRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
