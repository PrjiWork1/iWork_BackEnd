using IWork.Domain.Queries.CategoryQuery;
using IWork.Domain.ViewModels;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryViewModel>
    {
        private readonly CategoryService _categoryService;
        public GetByIdCategoryQueryHandler(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryViewModel> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetById(request.Id);

            var category = new CategoryViewModel
              (
                result.Id,
                result.Description,
                result.IsActive
              );

            return category;
        }
    }
}
