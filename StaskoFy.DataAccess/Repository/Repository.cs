using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StaskoFyDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(StaskoFyDbContext _context)
        {
            this.context = _context;
            this.dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await this.dbSet.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IQueryable<T> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            this.dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            this.dbSet.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
