using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public abstract class RepositoryBaseWithoutKey<TEntity> : IRepositoryWithoutKey<TEntity>
        where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> Dbset;

        protected RepositoryBaseWithoutKey(ApplicationDbContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            DataContext = dataContext;
            this.Dbset = this.DataContext.Set<TEntity>();
        }

        protected ApplicationDbContext DataContext { get; }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.Dbset.Add(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.Dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
            {
                throw new ArgumentNullException(nameof(where));
            }
            IEnumerable<TEntity> objects = this.Dbset.Where<TEntity>(where).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                this.Dbset.Remove(obj);
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return this.Dbset;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
            {
                throw new ArgumentNullException(nameof(where));
            }
            return this.Dbset.Where(where);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
