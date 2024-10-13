using IWork.Domain.Models.Enums;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.NormalAdvertisementCommands
{
    public class NormalAdvertisementAddCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlBanner { get; set; }
        public AdvertisementType Type { get; set; }
        public bool IWorkPro { get; set; }
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public NormalAdvertisementAddCommand(string title, string description, string urlBanner,
           AdvertisementType type, bool iWorkPro, string userId, Guid categoryId,
           DateTime createdAt, decimal price, bool isActive)
        {
            Title = title;
            Description = description;
            UrlBanner = urlBanner;
            Type = type;
            IWorkPro = iWorkPro;
            UserId = userId;
            CategoryId = categoryId;
            CreatedAt = createdAt;
            Price = price;
            IsActive = isActive;
        }
    }
}
