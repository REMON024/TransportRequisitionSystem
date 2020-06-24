using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Generic.BLL
{
    public interface IGenericBLL
    {
        Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, new();
        TEntity Insert<TEntity>(TEntity entity) where TEntity : class, new();
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, new();
        TEntity Delete<TEntity>(TEntity entity) where TEntity : class, new();
        TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new();
        Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new();


        Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new();
        Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new();



        Task<List<TResult>> GetByFilterAsync<TEntity,TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : class, new() where TResult : class;
        Task<List<TResult>> GetByFilterAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new() where TResult : class;


        List<TEntity> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new();
        List<TResult> GetByFilter<TEntity,TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : class, new() where TResult : class;


        void SaveChanges();
        Task SaveChangesAsync();
    }
}
