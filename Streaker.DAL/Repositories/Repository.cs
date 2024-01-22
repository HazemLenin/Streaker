using Microsoft.EntityFrameworkCore;
using Streaker.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseDomain
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private static readonly char[] separator = [','];

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            int? take,
            int? step,
            string? includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (take > 0)
                query = query.Skip((step - 1) * take ?? 0).Take(take ?? 0);

            return query;
        }

        public async Task<T?> GetByIdAsync(string id, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Created = DateTime.UtcNow;

            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.Created = DateTime.UtcNow;

            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            entity.Updated = DateTime.UtcNow;
            _dbSet.Update(entity);
            _dbSet.Entry(entity).Property(e => e.Created).IsModified = false;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.Updated = DateTime.UtcNow;
            
            _dbSet.UpdateRange(entities);
            
            foreach (var entity in entities)
                _dbSet.Entry(entity).Property(e => e.Created).IsModified = false;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            }
        }

        public async Task<bool> CheckExistsAsync(string id) => await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
    }
}
