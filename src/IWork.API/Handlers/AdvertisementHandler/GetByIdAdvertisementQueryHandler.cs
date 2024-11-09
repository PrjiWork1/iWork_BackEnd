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
        public GetByIdAdvertisementQueryHandler(IAdvertisementService service)
        {
            _advertisementService = service;
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
               result.UserId,
               result.User.UserName,
               result.User.CompleteName,
               result.User.Email,
               result.CategoryId,
               result.Category.Description,
               result.AdvertisementRate,
               result.CreatedAt,
               result.Price,
               result.Status,
               result.NumberOfSales,
               result.Items?.Select(item => new ItemAdvertisementViewModel(
                   item.Name,
                   item.Price
               )).ToList(),
               result.IsActive
           );

            return advertisement;
        }
    }
}
