using NybSys.Models.DTO;
using NybSys.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NybSys.TMS.BLL
{
    public interface IRequisitionBLL
    {
        Task SaveAsync(Requisitions obj);      
        Task <Requisitions> GetByID(long id);
        Task<List<Requisitions>> GetFilteredAsync(Expression<Func<Requisitions, bool>> predicate);
    }
}
