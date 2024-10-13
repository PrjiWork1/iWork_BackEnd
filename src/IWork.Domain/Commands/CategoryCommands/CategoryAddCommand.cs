using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.CategoryCommands
{
    public class CategoryAddCommand : IRequest<bool>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CategoryAddCommand(string description, bool isActive)
        {
            Description = description;
            IsActive = isActive;
        }
    }
}
