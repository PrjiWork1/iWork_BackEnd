using IWork.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Commands.AdvertisementCommands
{
    public class UpdateAdvertisementStatusCommand : IRequest<bool>
    {
        public UpdateAdvertisementStatusCommand(Guid id, UpdateAdvertisementStatusRequest request)
        {
            Id = id;
            AdvertisementStatus = request;
        }

        public Guid Id { get; set; }
        public UpdateAdvertisementStatusRequest AdvertisementStatus { get; set; }
    }
}
