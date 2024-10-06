using IWork.Data.Context;
using IWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly DataContext _context;

        public BaseService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(T entity)
        {
            var save = _context.Add(entity);
            if (save == null) return false;
            await Save();
            return true;
        }
        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            var isActiveProperty = entity.GetType().GetProperty("IsActive");
            if (isActiveProperty == null)
            {
                throw new InvalidOperationException("A propriedade 'IsActive' não foi encontrada na entidade.");
            }

            
            isActiveProperty.SetValue(entity, false);

            
            _context.Set<T>().Update(entity);

            
            return await Save();
        }
   
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()
                         .Where(entity => EF.Property<bool>(entity, "IsActive"))
                         .ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


        public async Task<bool> Update(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Update(entity);
            return await Save();
        }
    }
}
