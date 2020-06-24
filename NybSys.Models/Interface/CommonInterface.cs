using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NybSys.Models.Interface
{
    public interface CommonInterface
    {

        Task SaveAsync<T>(T obj);
        Task UpdateAsync<T>(T obj);
        Task<T> GetByID<T>(long id);
        Task<List<T>> GetFilteredAsync<T>(Expression<Func<T, bool>> predicate);
    }
}