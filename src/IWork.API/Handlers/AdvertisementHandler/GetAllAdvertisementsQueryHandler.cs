using IWork.Domain.DTO;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.Validations;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.AdvertisementHandler
{
    public class GetAllAdvertisementsQueryHandler : IRequestHandler<GetAllAdvertisementsQuery, List<AdvertisementViewModel>>
    {
        private readonly IAdvertisementService _advertisementService;

        public GetAllAdvertisementsQueryHandler(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
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
                a.IWorkPro,
                a.UserId,
                a.UserName,
                a.CompleteName,
                a.CategoryId,
                a.CategoryDescription,
                a.AdvertisementRate,
                a.CreatedAt,
                a.Price,
                a.Status,
                a.NumberOfSales,
                a.itemAdvertisements?.Select(item => new ItemAdvertisementViewModel(
                    item.Name,
                    item.Price
                )).ToList(),
                a.IsActive
            )).ToList() ?? new List<AdvertisementViewModel>();

            return advertisement;

        }
    }
}
