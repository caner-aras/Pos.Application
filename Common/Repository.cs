using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Add(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().Add((TEntity)entities);
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(params TEntity[] entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().Remove((TEntity)entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public TEntity Include(Expression<Func<TEntity, bool>> expression, string[] include)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
