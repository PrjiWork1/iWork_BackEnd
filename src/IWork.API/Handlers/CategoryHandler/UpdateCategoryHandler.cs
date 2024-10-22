using IWork.Domain.Commands.CategoryCommands;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class UpdateCategoryHandler : IRequestHandler<CategoryUpdateCommand, bool>
    {
        private readonly CategoryService _categoryService;

        public UpdateCategoryHandler(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<bool> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetById(request.Id);
            if (category == null) return false;
            category.Description = request.CategoryRequest.Description;
            category.IsActive = request.CategoryRequest.IsActive;
            var result = await _categoryService.Update(category.Id);
            if (result) return true;
            return false;
        }
    }
}
