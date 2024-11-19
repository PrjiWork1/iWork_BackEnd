using IWork.Domain.Models.Enums;
using IWork.Domain.Validations;
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
            string preferenceId, AdvertisementTemplate advertisementTemplate, AdvertisementType advertisementType, 
           HiringStatus hiringStatus, string description, decimal price, decimal advertisementRate,
            decimal totalAmount, bool isActive)
        {
            ValidateAndSetValues(advertisementId, contractorId, advertiserId, preferenceId, advertisementTemplate,
                advertisementType, hiringStatus, description, price, advertisementRate, totalAmount, isActive);

            Items = new List<HiringItemAdvertisement>();
        }

        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string ContractorId { get; set; }
        public string AdvertiserId { get; set; }
        public string PreferenceId { get; set; }
        public DateTime ContractDate { get; set; } = DateTime.UtcNow;
        public AdvertisementTemplate AdvertisementTemplate { get; set; }
        public AdvertisementType AdvertisementType { get; set; }
        public HiringStatus HiringStatus { get; set; }
        public string Description { get; set; }
        public virtual ICollection<HiringItemAdvertisement> Items { get; set; }
        public decimal Price { get; set; }
        public decimal AdvertisementRate { get; set; }

        //public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }

        private void ValidateAndSetValues(Guid advertisementId, string contractorId, string advertiserId,
           string preferenceId, AdvertisementTemplate advertisementTemplate, AdvertisementType advertisementType,
            HiringStatus hiringStatus, string description,decimal price, decimal advertisementRate,
            decimal totalAmount, bool isActive)
        {
            ValidateAdvertisementId(advertisementId);
            ValidateContractorId(contractorId);
            ValidateAdvertiserId(advertiserId);
            ValidatePreferenceId(preferenceId);
            ValidateDescription(description);
            ValidatePrice(price);
            ValidateTotalAmount(totalAmount);

            AdvertisementId = advertisementId;
            ContractorId = contractorId;
            AdvertiserId = advertiserId;
            PreferenceId = preferenceId;
            AdvertisementTemplate = advertisementTemplate;
            AdvertisementType = advertisementType;
            HiringStatus = hiringStatus;
            Description = description;
            Price = price;
            AdvertisementRate = CalculateAdvertisementRate();
            TotalAmount = totalAmount;
            IsActive = isActive;
        }

        private void ValidateAdvertisementId(Guid advertisementId)
        {
            DomainExceptionValidations.ExceptionHandler(advertisementId == Guid.Empty, "Invalid AdvertisementId. AdvertisementId is required!");
        }

        private void ValidateContractorId(string contractorId)
        {
            DomainExceptionValidations.ExceptionHandler(contractorId == string.Empty, "Invalid ContractorId. ContractorId is required!");
        }

        private void ValidateAdvertiserId(string advertiserId)
        {
            DomainExceptionValidations.ExceptionHandler(advertiserId == string.Empty, "Invalid AdvertiserId. AdvertiserId is required!");
        }
        private void ValidatePreferenceId(string preferenceId)
        {
            DomainExceptionValidations.ExceptionHandler(preferenceId == String.Empty, "Invalid PreferenceId. PreferenceId is required!");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid description. Description is required!");
            if (description.Length > 200)
                DomainExceptionValidations.ExceptionHandler(true, "Description is too long. Maximum length is 200 characters.");
        }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidations.ExceptionHandler(price < 0, "Invalid Price. Price must be greater than zero!");
        }

        private void ValidateTotalAmount(decimal total)
        {
            DomainExceptionValidations.ExceptionHandler(total < 0, "Invalid TotalAmount. Price must be greater than zero!");
        }

        public decimal CalculateTotal()
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

            return baseTotal;
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
