using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class GetByIdAdvertisementQueryHandler : IRequestHandler<GetByIdAdvertisementQuery, AdvertisementViewModel>
    {
        private readonly IAdvertisementService _advertisementService;

        public GetByIdAdvertisementQueryHandler(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<AdvertisementViewModel> Handle(GetByIdAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.GetAdvertisementById(request.Id);

            var advertisement = new AdvertisementViewModel(
               result.Id,
               result.Title,
               result.Description,
               result.UrlBanner,
               result.Type,
               result.IWorkPro,
               result.UserId,
               result.UserName,
               result.CompleteName,
               result.CategoryId,
               result.CategoryDescription,
               result.AdvertisementRate,
               result.CreatedAt,
               result.Price,
               result.Status,
               result.NumberOfSales,
               result.itemAdvertisements?.Select(item => new ItemAdvertisementViewModel(
                   item.Name,
                   item.Price
               )).ToList(),
               result.IsActive
           );

            return advertisement;
        }
    }
}
