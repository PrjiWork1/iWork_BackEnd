using IWork.Domain.Models.Enums;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public abstract class Advertisement
    {
        public Advertisement(string title, string description, string urlBanner,
           AdvertisementType type, bool iWorkPro, string userId, Guid categoryId,
           bool isActive, DateTime createdAt, AdvertisementStatus status)
        {
            ValidateAndSetValues(title, description, urlBanner, type, iWorkPro, userId,
                categoryId, isActive, createdAt, status);
        }

        public Guid Id { get;  set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlBanner { get; set; }
        public AdvertisementType Type { get; set; }
        public bool IWorkPro { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal AdvertisementRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AdvertisementStatus Status { get; set; }
        public bool IsActive { get; set; }

        private void ValidateAndSetValues(string title, string description, string urlBanner,
            AdvertisementType type, bool iWorkPro, string userId, Guid categoryId, bool isActive, 
            DateTime createdAt, AdvertisementStatus status)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            validateUrlBanner(urlBanner);
            ValidateUserId(userId);
            ValidateCategoryId(categoryId);

            Title = title;
            Description = description;
            UrlBanner = urlBanner;
            Type = type;
            IWorkPro = iWorkPro;
            UserId = userId;
            CategoryId = categoryId;
            CreatedAt = createdAt;
            Status = status;
            IsActive = true;

            AdvertisementRate = CalculateAdvertisementRate();
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid title. Title is required!");
            if (title.Length > 100)
                DomainExceptionValidations.ExceptionHandler(true, "Title is too long. Maximum length is 100 characters.");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid description. Description is required!");
            if (description.Length > 200)
                DomainExceptionValidations.ExceptionHandler(true, "Description is too long. Maximum length is 200 characters.");
        }

        private void validateUrlBanner(string urlBanner)
        {
            if (string.IsNullOrEmpty(urlBanner))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid title. Title is required!");
        }

        private void ValidateUserId(string userId)
        {
            DomainExceptionValidations.ExceptionHandler(userId == string.Empty, "Invalid UserId. UserId is required!");
        }

        private void ValidateCategoryId(Guid categoryId)
        {
            DomainExceptionValidations.ExceptionHandler(categoryId == Guid.Empty, "Invalid CategoryId. CategoryId is required!");
        }

        public decimal CalculateAdvertisementRate()
        {
            switch (Type)
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
