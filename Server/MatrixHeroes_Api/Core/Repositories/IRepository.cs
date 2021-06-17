using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models.Domain;

namespace MatrixHeroes_Api.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByNameAsync(string name);
        IEnumerable<T> Get(Expression<Func<T, bool>> pred = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}