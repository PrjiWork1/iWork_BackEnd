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
        public HiringAdvertisement(Guid advertisementId, string contractorId, string advertiserId, HiringStatus hiringStatus, 
            AdvertisementTemplate advertisementTemplate, AdvertisementType advertisementType, 
            decimal price, decimal advertisementRate, int quantity, 
            decimal totalAmount, bool isActive)
        {
            AdvertisementId = advertisementId;
            ContractorId = contractorId;
            AdvertiserId = advertiserId;
            HiringStatus = hiringStatus;
            AdvertisementTemplate = advertisementTemplate;
            AdvertisementType = advertisementType;
            Items = new List<HiringItemAdvertisement>();
            Price = price;
            AdvertisementRate = CalculateAdvertisementRate();
            Quantity = quantity;
            TotalAmount = totalAmount;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string ContractorId { get; set; }
        public string AdvertiserId { get; set; }
        public DateTime ContractDate { get; set; } = DateTime.UtcNow;
        public HiringStatus HiringStatus { get; set; }
        public AdvertisementTemplate AdvertisementTemplate { get; set; }
        public AdvertisementType AdvertisementType { get; set; }
        public virtual ICollection<HiringItemAdvertisement> Items { get; set; }
        public decimal Price { get; set; }
        public decimal AdvertisementRate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }


        public decimal CalculateTotalWithRate()
        {
            decimal baseTotal = 0;

            // Verifica o tipo do anúncio e calcula o valor total básico
            if (AdvertisementTemplate == AdvertisementTemplate.Normal)
            {
                // Para Normal, multiplica preço unitário pela quantidade
                baseTotal = Price * Quantity;
            }
            else if (AdvertisementTemplate == AdvertisementTemplate.Dynamic && Items != null)
            {
                // Para Dynamic, percorre manualmente cada item e acumula o valor total
                foreach (var item in Items)
                {
                    baseTotal += item.Price * item.Quantity;
                }
            }
            else
            {
                baseTotal = 0;
            }

            // Calcula o total final com a taxa
            decimal rate = CalculateAdvertisementRate();
            decimal totalWithRate = baseTotal + (baseTotal * rate);

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
