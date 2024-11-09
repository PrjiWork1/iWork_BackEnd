using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class CreateNormalAdvertisementHandler : IRequestHandler<NormalAdvertisementAddCommand, bool>
    {
        private readonly IAdvertisementService _advertisementService;
        public CreateNormalAdvertisementHandler(IAdvertisementService service)
        {
            _advertisementService = service;
        }
        public async Task<bool> Handle(NormalAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            var normalAdvertisement = new Advertisement(
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
                request.Price
            );

            normalAdvertisement.Items = new List<ItemAdvertisement>();

            var result = await _advertisementService.Add(normalAdvertisement);

            if (result) return true;

            return false;
        }
    }
}
