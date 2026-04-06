using Inventory.Application.Interfaces.IRepository.Common;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

        public async Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public Task SaveAsync() => _dbcontext.SaveChangesAsync();

        public async Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate,
                                     params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbcontext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }
        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
