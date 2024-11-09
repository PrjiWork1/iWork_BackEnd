using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class UpdateAdvertisementStatusHandler : IRequestHandler<UpdateAdvertisementStatusCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;
        public UpdateAdvertisementStatusHandler(IAdvertisementService service)
        {
            _advertisementService = service;
        }

        public async Task<bool> Handle(UpdateAdvertisementStatusCommand request, CancellationToken cancellationToken)
        {
            var advertisement = await _advertisementService.GetById(request.Id);
            if (advertisement == null) return false;
            advertisement.Status = request.AdvertisementStatus.Status;
            var result = await _advertisementService.Update(advertisement.Id);
            if (result) return true;
            return false;
        }
    }
}
