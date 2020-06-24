using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NybSys.Common.ExceptionHandle;
using NybSys.Models.DTO;
using NybSys.Models.Interface;
using NybSys.Repository;
using NybSys.UnitOfWork;

namespace NybSys.TMS.BLL
{
    public class RequisitionServiceBLL : IRequisitionBLL
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequisitionServiceBLL(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public virtual async Task SaveAsync(Requisitions obj)
        {
            if (obj.RequisitionId > 0)
            {
                GetRequisitionRepository().Insert(obj);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                GetRequisitionRepository().Update(obj);
                await _unitOfWork.SaveAsync();
            }
        }


        public virtual async Task<Requisitions> GetByID(long id)
        {
            Requisitions requisitions = await GetRequisitionRepository().GetFirstOrDefaultAsync(predicate: u => u.RequisitionId.Equals(id));

            if (requisitions != null)
            {
                return requisitions;
            }
            throw new BadRequestException(Common.Constants.ErrorMessages.REQUISITION_NOT_EXIST);
        }

        public virtual async Task<List<Requisitions>> GetFilteredAsync(Expression<Func<Requisitions, bool>> predicate)
        {
            return (await GetRequisitionRepository().GetAsync(predicate: predicate)).ToList();

   
            throw new BadRequestException(Common.Constants.ErrorMessages.REQUISITION_NOT_EXIST);
        }

        private IRepository<Requisitions> GetRequisitionRepository()
        {
            return _unitOfWork.Repository<Requisitions>();
        }

    }
}
