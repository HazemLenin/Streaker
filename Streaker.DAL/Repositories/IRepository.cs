using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? take = 0, int? step = 0, string? includeProperties = "");
        Task<T?> GetByIdAsync(string id, string? includeProperties = "");
        Task<string> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(string id);
        Task DeleteRangeAsync(IEnumerable<string> ids);
        Task<bool> CheckExistsAsync(string id);
    }
}
