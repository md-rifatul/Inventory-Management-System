using Inventory.Application.Interfaces.IRepository.Common;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.Infrastructure.Repository.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
            _dbSet = _dbcontext.Set<T>();
        }

        // Standard CRUD
        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public void Save() => _dbcontext.SaveChanges();

        // 1. Get All with Includes
        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return query.AsNoTracking().ToList(); // AsNoTracking improves performance for Read-Only
        }

        // 2. Get By Id with Includes
        public T? GetByIdIncluding(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            // Note: This assumes your PK is named "Id". 
            // If it's "ProductId" or "CategoryId", you'll need to adjust the property name.
            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        // 3. Search/Filter with Includes
        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return query.Where(predicate).ToList();
        }
        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}