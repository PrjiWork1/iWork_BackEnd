using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.AdvertisementCommands
{
    public class DynamicAdvertisementAddCommand : IRequest<bool>
    {
        public DynamicAdvertisementAddCommand(string title, string description, string urlBanner, 
            AdvertisementType type, bool iWorkPro, string userId, Guid categoryId, DateTime createdAt,
            AdvertisementStatus status, ICollection<ItemAdvertisementRequest> itemAdvertisements, bool isActive)
        {
            Title = title;
            Description = description;
            UrlBanner = urlBanner;
            Type = type;
            IWorkPro = iWorkPro;
            UserId = userId;
            CategoryId = categoryId;
            CreatedAt = createdAt;
            Status = status;
            this.itemAdvertisements = itemAdvertisements;
            IsActive = isActive;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlBanner { get; set; }
        public AdvertisementType Type { get; set; }
        public bool IWorkPro { get; set; }
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public AdvertisementStatus Status { get; set; }
        public ICollection<ItemAdvertisementRequest> itemAdvertisements { get; set; }
        public bool IsActive { get; set; }
    }
}
