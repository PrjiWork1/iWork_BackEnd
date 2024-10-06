using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.CategoryCommands
{
    public class CategoryDeleteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public CategoryDeleteCommand(Guid id)
        {
            Id = id;
        }
    }
}
