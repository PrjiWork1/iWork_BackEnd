using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Models;
using IWork.Domain.Validations;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class CreateDynamicAdvertisementHandler : IRequestHandler<DynamicAdvertisementAddCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;
        public CreateDynamicAdvertisementHandler(IAdvertisementService service)
        {
            _advertisementService = service;
        }
        public async Task<bool> Handle(DynamicAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            var dynamicAdvertisement = new Advertisement(
                 request.Title,
                 request.Description,
                 request.UrlBanner,
                 request.Type,
                 request.UserId,
                 request.CategoryId,
                 request.IsActive,
                 DateTime.UtcNow,
                 request.Status,
                 0,
                 0
             );

            if (request.itemAdvertisements != null)
            {
                foreach (var item in request.itemAdvertisements)
                {
                    var itemAdvertisement = new ItemAdvertisement(item.Name, item.Price);

                    ValidateItems(dynamicAdvertisement, itemAdvertisement);

                    dynamicAdvertisement.Items.Add(itemAdvertisement);
                }
            }

            var result = await _advertisementService.Add(dynamicAdvertisement);

            if (result) return true;

            return false;
        }

        public void ValidateItems(Advertisement advertisement, ItemAdvertisement item)
        {
           if (advertisement.Items.Any(i => i.Name == item.Name && i.Price == item.Price))
           {
                throw new Exception("Item already exists in the advertisement.");
           }
        }
    }
}
