using IWork.Domain.Models.Enums;
using IWork.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWork.Domain.Requests;

namespace IWork.Domain.Commands.HiringAdvertisementCommands
{
    public class HiringAdvertisementAddCommand : IRequest<bool>
    {
        public HiringAdvertisementAddCommand(Guid advertisementId, string contractorId, 
            string advertiserId, HiringStatus hiringStatus, 
            AdvertisementTemplate advertisementTemplate, AdvertisementType advertisementType, 
            ICollection<HiringItemAdvertisementRequest> items, decimal price, int quantity, decimal totalAmount, bool isActive)
        {
            AdvertisementId = advertisementId;
            ContractorId = contractorId;
            AdvertiserId = advertiserId;
            HiringStatus = hiringStatus;
            AdvertisementTemplate = advertisementTemplate;
            AdvertisementType = advertisementType;
            Items = items;
            Price = price;
            Quantity = quantity;
            TotalAmount = totalAmount;
            IsActive = isActive;
        }

     
        public Guid AdvertisementId { get; set; }
        public string ContractorId { get; set; }
        public string AdvertiserId { get; set; }
        public HiringStatus HiringStatus { get; set; }
        public AdvertisementTemplate AdvertisementTemplate { get; set; }
        public AdvertisementType AdvertisementType { get; set; }
        public ICollection<HiringItemAdvertisementRequest> Items { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
