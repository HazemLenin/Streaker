using Microsoft.EntityFrameworkCore;
using Streaker.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseDomain
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int take,
            int step,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (take > 0)
                query = query.Skip((step - 1) * take).Take(take);

            return query;
        }

        public T? GetById(string id, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return query.FirstOrDefault(e => e.Id == id);
        }

        public string Add(T entity)
        {
            _dbSet.Add(entity);
            
            // Assuming the entity has an ID property
            return entity.Id ?? "";
        }

        public void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            }
        }

        public int Save() => _dbContext.SaveChanges();
    }
}
