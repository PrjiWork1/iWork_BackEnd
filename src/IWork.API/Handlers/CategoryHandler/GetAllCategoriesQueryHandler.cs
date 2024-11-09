using IWork.Domain.Models;
using IWork.Domain.Queries.CategoryQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result =  await _categoryService.GetAll();

            var categories = result.Select(c => new CategoryViewModel(
                c.Id,
                c.Description,
                c.IsActive
            )).ToList();

            return categories;
        }
    }
}