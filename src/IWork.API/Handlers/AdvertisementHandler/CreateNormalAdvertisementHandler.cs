using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.NormalAdvertisementHandler
{
    public class CreateNormalAdvertisementHandler : IRequestHandler<NormalAdvertisementAddCommand, bool>
    {
        private readonly NormalAdvertisementService _advertisementService;

        public CreateNormalAdvertisementHandler(NormalAdvertisementService normalAdvertisementService)
        {
            _advertisementService = normalAdvertisementService;
        }

        public async Task<bool> Handle(NormalAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            var normalAdvertisement = new NormalAdvertisement(request.Title, request.Description, request.UrlBanner,
            request.Type, request.IWorkPro, request.UserId, request.CategoryId, request.IsActive, DateTime.UtcNow,
            request.Price);

            var result = await _advertisementService.Add(normalAdvertisement);

            if (result) return true;

            return false;
        }
    }
}
