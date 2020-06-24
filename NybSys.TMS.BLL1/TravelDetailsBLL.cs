using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NybSys.Common.ExceptionHandle;
using NybSys.Models.DTO;
using NybSys.Repository;
using NybSys.UnitOfWork;

namespace NybSys.TMS.BLL
{
    public class TravelDetailsBLL : ITravelDetailsBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        public TravelDetailsBLL(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<TravelDetails> GetByID(int id)
        {
            TravelDetails travelDetails = await GetTravelDetailsRepository().GetFirstOrDefaultAsync(predicate: u => u.RequisitionId.Equals(id));

            if (travelDetails != null)
            {
                return travelDetails;
            }
            throw new NotFoundException(Common.Constants.ErrorMessages.REQUISITION_NOT_EXIST);
        }




        private IRepository<TravelDetails> GetTravelDetailsRepository()
        {
            return _unitOfWork.Repository<TravelDetails>();
        }
    }


}
