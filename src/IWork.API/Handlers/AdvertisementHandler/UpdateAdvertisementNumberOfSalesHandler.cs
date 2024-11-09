using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class UpdateAdvertisementNumberOfSalesHandler : IRequestHandler<UpdateAdvertisementNumberOfSalesCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;
        public UpdateAdvertisementNumberOfSalesHandler(IAdvertisementService service)
        {
            _advertisementService = service;
        }
        public async Task<bool> Handle(UpdateAdvertisementNumberOfSalesCommand request, CancellationToken cancellationToken)
        {
            var advertisement = await _advertisementService.GetById(request.Id);
            if (advertisement == null) return false;
            advertisement.NumberOfSales = request.AdvertisementNumberOfSales.NumberOfSales;
            var result = await _advertisementService.Update(advertisement.Id);
            if (result) return true;
            return false;
        }
    }
}
