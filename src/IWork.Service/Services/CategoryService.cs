using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Domain.Models.IdentityEntities;
using IWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IWork.Service.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(DataContext context) : base(context)
        {
        }
    }
}
