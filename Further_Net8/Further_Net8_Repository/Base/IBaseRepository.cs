﻿using System.Linq.Expressions;
using SqlSugar;

namespace Further_Net8_Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        Task<long> Add(TEntity entity);

        Task<List<long>> AddSplit(TEntity entity);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();

        Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);

        Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>> whereExpression = null);
    }
}