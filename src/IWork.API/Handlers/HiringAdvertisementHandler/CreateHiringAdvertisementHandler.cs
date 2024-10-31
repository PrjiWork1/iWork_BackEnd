using IWork.Domain.Commands.HiringAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.HiringAdvertisementHandler
{
    public class CreateHiringAdvertisementHandler : IRequestHandler<HiringAdvertisementAddCommand, bool>
    {
        private readonly HiringAdvertisementService _service;
        public CreateHiringAdvertisementHandler(HiringAdvertisementService advertisementService)
        {
            _service = advertisementService;
        }

        public async Task<bool> Handle(HiringAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            HiringAdvertisement hiringAdvertisement;

            if (request.AdvertisementTemplate == AdvertisementTemplate.Normal)
            {

                hiringAdvertisement = new HiringAdvertisement(
                    request.AdvertisementId,
                    request.ContractorId,
                    request.AdvertiserId,
                    request.HiringStatus,
                    request.AdvertisementTemplate,
                    request.AdvertisementType,
                    request.Price,
                    0,
                    request.Quantity,
                    0, 
                    request.IsActive
                );

                // Calcula o total para um anúncio Normal
                hiringAdvertisement.TotalAmount = hiringAdvertisement.CalculateTotalWithRate();

            }
            else
            {
                hiringAdvertisement = new HiringAdvertisement(
                    request.AdvertisementId,
                    request.ContractorId,
                    request.AdvertiserId,
                    request.HiringStatus,
                    request.AdvertisementTemplate,
                    request.AdvertisementType,
                    0, 
                    0,
                    0,
                    0, 
                    request.IsActive
                );

                if (request.Items != null)
                {
                    foreach (var item in request.Items)
                    {
                        var hiringItem = new HiringItemAdvertisement(item.Name, item.Quantity, item.Price);
                        hiringAdvertisement.Items.Add(hiringItem); 
                    }

                    
                    hiringAdvertisement.TotalAmount = hiringAdvertisement.CalculateTotalWithRate();
                }
            }

            var result = await _service.Add(hiringAdvertisement);

            if (result) return true;

            return false;
        }

    }
}
