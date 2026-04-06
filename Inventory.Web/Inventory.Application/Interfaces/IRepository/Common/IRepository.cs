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
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes);
        T? GetByIdIncluding(int id, params Expression<Func<T, object>>[] includes);
        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate,
                             params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
