using NybSys.Common.ExceptionHandle;
using NybSys.Repository;
using NybSys.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Generic.BLL
{
    public class GenericBLL : IGenericBLL
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            return _unitOfWork.Repository<TEntity>();
        }

        public virtual  List<TEntity> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
        {
            var result = (Repository<TEntity>().Get(predicate: predicate)).ToList();

            if(result.Count > 0)
            {
                return result;
            }

            throw new NotFoundException(string.Format("There was no {0} found", nameof(TEntity)));
        }

        public virtual List<TResult> GetByFilter<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : class, new() where TResult : class
        {
            var result = Repository<TEntity>().Get<TResult>(predicate: predicate, selector: selector).ToList();

            if (result.Count > 0)
            {
                return result;
            }

            throw new NotFoundException(string.Format("There was no {0} found", nameof(TEntity)));

        }


        public virtual async Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
        {
            var result = (await Repository<TEntity>().GetAsync(predicate: predicate)).ToList();

            if (result.Count > 0)
            {
                return result;
            }

            return new List<TEntity>();
        }

        public async Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new()
        {
            var result = (await Repository<TEntity>().GetAsync(predicate: predicate, orderBy: orderBy)).ToList();

            if (result.Count > 0)
            {
                return result;
            }

            return new List<TEntity>();
        }


        public virtual async Task<List<TResult>> GetByFilterAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : class, new() where TResult : class
        {
            var result = (await Repository<TEntity>().GetAsync<TResult>(predicate: predicate, selector: selector)).ToList();

            if (result.Count > 0)
            {
                return result;
            }

            throw new NotFoundException(string.Format("Item couldn't found", nameof(TEntity)));
        }
        public virtual async Task<List<TResult>> GetByFilterAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new() where TResult : class
        {
            var result = (await Repository<TEntity>().GetAsync<TResult>(selector: selector, predicate: predicate)).ToList();

            if (result.Count > 0)
            {
                return result;
            }

            throw new NotFoundException(string.Format("Item couldn't found", nameof(TEntity)));
        }

        public virtual TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
        {
            var entity = Repository<TEntity>().GetFirstOrDefault(predicate: predicate);

            if (entity != null)
            {
                return entity;
            }

            throw new NotFoundException(string.Format("Item couldn't found", nameof(TEntity)));
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
        {
            var entity = await Repository<TEntity>().GetFirstOrDefaultAsync(predicate: predicate);

            if (entity != null)
            {
                return entity;
            }

            else
            {
                return entity;
            }
        }


        public virtual TEntity Insert<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Repository<TEntity>().Insert(entity);
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, new()
        {
            await Repository<TEntity>().InsertAsync(entity);
            return entity;
        }

        public virtual TEntity Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Repository<TEntity>().Update(entity);
            return entity;
        }

        public void SaveChanges()
        {
            _unitOfWork.Save();
        }

        public virtual async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveAsync();
        }

        public TEntity Delete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Repository<TEntity>().Delete(entity);
            return entity;
        }

      
    }
}
