using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class CategoryService : BaseService<Category>
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Category> GetNormalAdvertisements()
        {
            IQueryable<Category> entity = _context.Category
                .AsNoTracking()
                .Include(n => n.NormalAdvertisements);

            return entity.ToList();
        }

        public List<Category> GetNormalAdvertisementsById(Guid categoryId)
        {
            IQueryable<Category> entity = _context.Category
                 .AsNoTracking()
                 .Include(n => n.NormalAdvertisements)
                 .Where(c => c.Id == categoryId);

            return entity.ToList();
        }
    }
}
