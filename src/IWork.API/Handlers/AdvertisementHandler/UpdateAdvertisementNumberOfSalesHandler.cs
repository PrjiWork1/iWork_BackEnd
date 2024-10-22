using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class UpdateAdvertisementNumberOfSalesHandler : IRequestHandler<UpdateAdvertisementNumberOfSalesCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;
        public UpdateAdvertisementNumberOfSalesHandler(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<bool> Handle(UpdateAdvertisementNumberOfSalesCommand request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.UpdateAdvertisementNumberOfSales(request.Id, request.AdvertisementNumberOfSales.NumberOfSales);

            if (result) return true;

            return false;
        }
    }
}
