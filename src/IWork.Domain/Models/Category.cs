using IWork.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class Category
    {
        public Category() { }   
        public Category(string description, bool isActive)
        {
            ValidateDescription(description);
            
            Description = description;
            IsActive = isActive;

        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Advertisement> Advertisement { get; set; }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid Description. Description is required!");

            if (description.Length > 30)
            {
                throw new ArgumentException("Description is too long. Maximum length is 30 characters.", nameof(description));
            }

            if (description.Length < 3)
            {
                throw new ArgumentException("Description is too short. Minimum length is 4 characters.", nameof(description));
            }
        }
    }
}
