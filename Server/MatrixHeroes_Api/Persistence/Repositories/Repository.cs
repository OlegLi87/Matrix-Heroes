using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MatrixHeroes_Api.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext DbContext;
        public Repository(DbContext context) => DbContext = context;

        public IEnumerable<T> Get(Expression<Func<T, bool>> pred = null)
        {
            IQueryable<T> data = DbContext.Set<T>();
            if (pred != null)
                data = data.Where(pred);
            return data;
        }
        public async Task<T> GetByIdAsync(Guid id) => await DbContext.Set<T>().FindAsync(id);

        public async Task<T> GetByNameAsync(string name)
                => await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Name == name);

        public async Task AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbContext.Set<T>().AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }
    }
}