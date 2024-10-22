using IWork.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Queries.CategoryQuery
{
    public class GetByIdCategoryQuery : IRequest<CategoryViewModel>
    {
        public GetByIdCategoryQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
