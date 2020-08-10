using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        T Include(Expression<Func<T, bool>> expression, string[] include);

        void Add(T entity);
        void Add(params T[] entities);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}