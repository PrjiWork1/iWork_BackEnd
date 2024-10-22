using IWork.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.AdvertisementCommands
{
    public class UpdateAdvertisementNumberOfSalesCommand : IRequest<bool>
    {
        public UpdateAdvertisementNumberOfSalesCommand(Guid id, AdvertisementNumberOfSalesRequest advertisementNumberOfSales)
        {
            Id = id;
            AdvertisementNumberOfSales = advertisementNumberOfSales;
        }

        public Guid Id { get; set; }
        public AdvertisementNumberOfSalesRequest AdvertisementNumberOfSales { get; set; }
    }
}
