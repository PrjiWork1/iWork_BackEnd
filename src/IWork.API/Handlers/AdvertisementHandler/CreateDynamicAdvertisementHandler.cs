using IWork.Domain.Commands.AdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class CreateDynamicAdvertisementHandler : IRequestHandler<DynamicAdvertisementAddCommand, bool>
    {
        private readonly DynamicAdvertisementService _advertisementService;
        public CreateDynamicAdvertisementHandler(DynamicAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }
        public async Task<bool> Handle(DynamicAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            var dynamicAdvertisement = new DynamicAdvertisement(
                request.Title,
                request.Description,
                request.UrlBanner,
                request.Type,
                request.IWorkPro,
                request.UserId,
                request.CategoryId,
                request.IsActive,
                DateTime.UtcNow,
                request.Status
            );

            if (request.itemAdvertisements != null)
            {
                foreach (var item in request.itemAdvertisements)
                {
                    var itemAdvertisement = new ItemAdvertisement(item.Name, item.Price);
                    
                    dynamicAdvertisement.Items.Add(itemAdvertisement);
                }
            }

          
        

        var result = await _advertisementService.Add(dynamicAdvertisement);

            if (result) return true;

            return false;
        }
    }
}
