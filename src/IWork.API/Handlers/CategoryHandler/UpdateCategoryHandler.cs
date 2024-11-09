using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class UpdateCategoryHandler : IRequestHandler<CategoryUpdateCommand, bool>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryHandler(ICategoryService categoryService)
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
