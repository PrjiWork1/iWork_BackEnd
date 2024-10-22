using IWork.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.CategoryCommands
{
    public class CategoryUpdateCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public CategoryRequest CategoryRequest { get; set; }
        public CategoryUpdateCommand(Guid id, CategoryRequest categoryRequest)
        {
            Id = id;
            CategoryRequest = categoryRequest;
        }
    }
}
