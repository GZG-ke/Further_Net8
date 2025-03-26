using System.Linq.Expressions;
using SqlSugar;

namespace Further_Net8_Servive.Base
{
    public interface IBaseServices<TEntity, TVo> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        Task<long> Add(TEntity entity);

        Task<List<long>> AddSplit(TEntity entity);

        Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null);

        Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);

        Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null);
    }
}