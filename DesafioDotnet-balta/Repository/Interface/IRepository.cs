using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<T>
    {
      
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        Task Commit();
    }
}
