using IWork.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class HiringAdvertisement
    {
        public HiringAdvertisement(Guid advertisementId, string contractorId, string advertiserId, 
            AdvertisementTemplate advertisementTemplate, AdvertisementType advertisementType, 
            decimal price, decimal advertisementRate,
            decimal totalAmount, bool isActive)
        {
            AdvertisementId = advertisementId;
            ContractorId = contractorId;
            AdvertiserId = advertiserId;
            AdvertisementTemplate = advertisementTemplate;
            AdvertisementType = advertisementType;
            Items = new List<HiringItemAdvertisement>();
            Price = price;
            AdvertisementRate = CalculateAdvertisementRate();
            TotalAmount = totalAmount;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string ContractorId { get; set; }
        public string AdvertiserId { get; set; }
        public DateTime ContractDate { get; set; } = DateTime.UtcNow;
        public AdvertisementTemplate AdvertisementTemplate { get; set; }
        public AdvertisementType AdvertisementType { get; set; }
        public virtual ICollection<HiringItemAdvertisement> Items { get; set; }
        public decimal Price { get; set; }
        public decimal AdvertisementRate { get; set; }
        //public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }


        public decimal CalculateTotalWithRate()
        {
            decimal baseTotal = 0;

            if (AdvertisementTemplate == AdvertisementTemplate.Normal)
            {
                baseTotal = Price;
            }
            else if (AdvertisementTemplate == AdvertisementTemplate.Dynamic && Items != null)
            {
                foreach (var item in Items)
                {
                    baseTotal += item.Price;
                }
            }

            decimal rate = CalculateAdvertisementRate(); 
            decimal totalWithRate = baseTotal * (1 + rate);

            return totalWithRate;
        }



        public decimal CalculateAdvertisementRate()
        {
            switch (AdvertisementType)
            {
                case AdvertisementType.Silver:
                    return 0.0999m; // 9.99%
                case AdvertisementType.Gold:
                    return 0.1199m; // 11.99%
                case AdvertisementType.Diamond:
                    return 0.1299m; // 12.99%
                default:
                    return 0.0999m; // Valor padrão
            }
        }
    }
}
