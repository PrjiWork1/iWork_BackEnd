using IWork.Domain.Commands.HiringAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Service.Interfaces;
using MediatR;

namespace IWork.API.Handlers.HiringAdvertisementHandler
{
    public class CreateHiringAdvertisementHandler : IRequestHandler<HiringAdvertisementAddCommand, bool>
    {
        private readonly IHiringAdvertisementService _service;
        public CreateHiringAdvertisementHandler(IHiringAdvertisementService advertisementService)
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
                    request.PreferenceId,
                    request.AdvertisementTemplate,
                    request.AdvertisementType,
                    request.HiringStatus,
                    request.Description,
                    request.Price,
                    0,
                    0, 
                    request.IsActive
                );

                hiringAdvertisement.TotalAmount = hiringAdvertisement.CalculateTotal();

            }
            else
            {
                hiringAdvertisement = new HiringAdvertisement(
                    request.AdvertisementId,
                    request.ContractorId,
                    request.AdvertiserId,
                    request.PreferenceId,   
                    request.AdvertisementTemplate,
                    request.AdvertisementType,
                    request.HiringStatus,   
                    request.Description,
                    0,
                    0,
                    0, 
                    request.IsActive
                );

                if (request.Items != null)
                {
                    foreach (var item in request.Items)
                    {
                        var hiringItem = new HiringItemAdvertisement(item.Name, item.Price);
                        ValidateItems(hiringAdvertisement, hiringItem);
                        hiringAdvertisement.Items.Add(hiringItem); 
                    }

                    
                    hiringAdvertisement.TotalAmount = hiringAdvertisement.CalculateTotal();
                }
            }

            var result = await _service.Add(hiringAdvertisement);

            if (result) return true;

            return false;
        }

        public void ValidateItems(HiringAdvertisement hiringAdvertisement, HiringItemAdvertisement item)
        {
            if (hiringAdvertisement.Items.Any(i => i.Name == item.Name && i.Price == item.Price))
            {
                throw new Exception("O item já existe na contratação do anúncio.");
            }
        }
    }
}
