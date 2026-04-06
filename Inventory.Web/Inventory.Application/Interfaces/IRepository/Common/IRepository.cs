using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IRepository.Common
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetQueryable();
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate,
                             params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
