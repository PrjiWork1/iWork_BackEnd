using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Update(Guid id);
        Task<bool> Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<bool> Save();

    }
}
