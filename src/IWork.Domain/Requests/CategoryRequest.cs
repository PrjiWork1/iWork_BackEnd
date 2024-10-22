using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Requests
{
    public class CategoryRequest
    {
        public CategoryRequest(string description, bool isActive)
        {
           Description = description;
            IsActive = isActive;
        }

        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
