using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class CreateCategoryHandler : IRequestHandler<CategoryAddCommand, bool>
    {
        private readonly CategoryService _categoryService;

        public CreateCategoryHandler(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<bool> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Description, request.IsActive);

            var result = await _categoryService.Add(category);
            
            if (result) return true;

            return false;
        }
    }
}
