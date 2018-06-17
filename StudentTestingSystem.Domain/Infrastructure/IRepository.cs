﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentTestingSystem.Domain.Infrastructure
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        TEntity GetById(TKey id);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        void Update(TEntity entity);
    }

    public interface IRepositoryWithoutKey<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        void Update(TEntity entity);
    }
}