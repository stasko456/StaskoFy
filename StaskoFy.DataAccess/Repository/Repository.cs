using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Guid? id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
