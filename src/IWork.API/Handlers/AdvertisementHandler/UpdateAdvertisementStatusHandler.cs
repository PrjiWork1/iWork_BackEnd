using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class UpdateAdvertisementStatusHandler : IRequestHandler<UpdateAdvertisementStatusCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;

        public UpdateAdvertisementStatusHandler(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<bool> Handle(UpdateAdvertisementStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.UpdateAdvertisementStatus(request.Id, request.AdvertisementStatus.Status);

            if (result) return true;

            return false;
        }
    }
}
