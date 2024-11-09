using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class GetAllAdvertisementsQueryHandler : IRequestHandler<GetAllAdvertisementsQuery, List<AdvertisementViewModel>>
    {
        private readonly IAdvertisementService _advertisementService;
        public GetAllAdvertisementsQueryHandler(IAdvertisementService service)
        {
            _advertisementService = service;
        }

        public async Task<List<AdvertisementViewModel>> Handle(GetAllAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.GetAllAdvertisements(request.IsAdmin);

            var advertisement = result?.Select(a => new AdvertisementViewModel(
                a.Id,
                a.Title,
                a.Description,
                a.UrlBanner,
                a.Type,
                a.UserId,
                a.User.UserName,
                a.User.CompleteName,
                a.User.Email,
                a.CategoryId,
                a.Category.Description,
                a.AdvertisementRate,
                a.CreatedAt,
                a.Price,
                a.Status,
                a.NumberOfSales,
                a.Items?.Select(item => new ItemAdvertisementViewModel(
                    item.Name,
                    item.Price
                )).ToList(),
                a.IsActive
            )).ToList() ?? new List<AdvertisementViewModel>();

            return advertisement;
        }
    }
}
