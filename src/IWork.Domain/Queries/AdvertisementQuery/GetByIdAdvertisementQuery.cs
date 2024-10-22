using IWork.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Queries.AdvertisementQuery
{
    public class GetByIdAdvertisementQuery : IRequest<AdvertisementViewModel>
    {
        public GetByIdAdvertisementQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
