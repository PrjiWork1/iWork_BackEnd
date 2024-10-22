using IWork.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Queries.AdvertisementQuery
{
    public class GetAllAdvertisementsQuery : IRequest<List<AdvertisementViewModel>>
    {
        public GetAllAdvertisementsQuery(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; set; }
    }
}
