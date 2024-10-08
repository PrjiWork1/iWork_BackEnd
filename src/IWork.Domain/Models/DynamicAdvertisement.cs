﻿using IWork.Domain.Models.Enums;
using IWork.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class DynamicAdvertisement : Advertisement
    {
        public DynamicAdvertisement(string title, string description, string urlBanner,
            AdvertisementType type, bool iWorkPro, string userId, Guid categoryId, bool isActive, DateTime createdAt)
            : base(title, description, urlBanner, type, iWorkPro, userId, categoryId, isActive, createdAt)
        {
            ValidateItems();
        }

        public ICollection<ItemAdvertisement> Items { get; set; }

        private void ValidateItems()
        {
            if (Items == null || Items.Count == 0)
            {
                DomainExceptionValidations.ExceptionHandler(true, "Invalid Items. Dynamic Advertisement must have at least one item.");
            }

            var duplicateItems = Items
                .GroupBy(i => i.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            if (duplicateItems.Any())
            {
                var duplicateNames = string.Join(", ", duplicateItems);
                DomainExceptionValidations.ExceptionHandler(true, $"Duplicate item names are not allowed: {duplicateNames}");
            }
        }
    }

}
