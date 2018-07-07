using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EnsureThat;
using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public abstract class RepositoryBaseWithoutKey<TEntity> : IRepositoryWithoutKey<TEntity>
        where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> Dbset;
        // TODO: пофиксить варнинг Ensure.That<T>()

        protected RepositoryBaseWithoutKey(ApplicationDbContext dataContext)
        {
            Ensure.That(dataContext, "dataContext").IsNotNull();
            DataContext = dataContext;
            this.Dbset = this.DataContext.Set<TEntity>();
        }

        protected ApplicationDbContext DataContext { get; }

        public virtual TEntity Add(TEntity entity)
        {
            Ensure.That(entity, "entity").IsNotNull();
            this.Dbset.Add(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Ensure.That(entity, "entity").IsNotNull();
            this.Dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            Ensure.That(where, "where").IsNotNull();
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
            Ensure.That(where, "where").IsNotNull();
            return this.Dbset.Where(where);
        }

        public virtual void Update(TEntity entity)
        {
            Ensure.That(entity, "entity").IsNotNull();
            this.Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
