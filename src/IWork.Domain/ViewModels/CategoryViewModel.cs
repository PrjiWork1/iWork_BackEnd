using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(Guid id, string description, bool isActive)
        {
            Id = id;
            Description = description;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
