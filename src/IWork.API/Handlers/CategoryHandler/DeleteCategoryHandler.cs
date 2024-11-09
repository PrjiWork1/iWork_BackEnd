using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.CategoryHandler
{
    public class DeleteCategoryHandler : IRequestHandler<CategoryDeleteCommand, bool>
    {
        private readonly ICategoryService _categoryService;
        public DeleteCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            var book = await _categoryService.GetById(request.Id);
            if (book == null) return false;
            var result = await _categoryService.Delete(book.Id);
            if (result) return true;
            return false;
        }
    }
}
