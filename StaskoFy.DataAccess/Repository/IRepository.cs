using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAttached();

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);
    }
}
