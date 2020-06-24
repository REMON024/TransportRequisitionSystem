using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.Generic.BLL;
using NybSys.Models.ViewModels;
using NybSys.Models.DTO;
using NybSys.API.Helper;
using NybSys.TMS.BLL;
using NybSys.Common.Enums;
using NybSys.AuditLog.BLL;

namespace NybSys.API.Manager
{
    public class TravelDetailManager : ITravelDetailsManager
    {
        private readonly ITravelDetailsBLL _travelDetailsBLL;
        private readonly IGenericBLL _genericBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        public TravelDetailManager(IGenericBLL genericBLL, ITravelDetailsBLL travelDetailsBLL,IAuditLogBLL auditLogBLL)
        {
            _genericBLL = genericBLL;
            _auditLogBLL = auditLogBLL;
            _travelDetailsBLL = travelDetailsBLL;
        }

        public virtual async Task<IActionResult> GetTravelDetailsByIdAsync(ApiCommonMessage message)
        {


            try
            {
                await _auditLogBLL.SaveLog(nameof(GetTravelDetailsByIdAsync), "Get travel details by id", Common.Enums.Action.View, message);
               Requisitions requisitions  = message.GetRequestObject<Requisitions>();

                var result = await _genericBLL.GetByFilterAsync<TravelDetails>(p => p.RequisitionId == requisitions.RequisitionId);
                if(result.Count>0)
                {
                    TravelDetails travelDetails = await _genericBLL.GetFirstOrDefaultAsync<TravelDetails>(p => p.RequisitionId == requisitions.RequisitionId);

                    return Build.SuccessMessage(travelDetails);
                }

                else
                {
                    return new NotFoundObjectResult("Travel Details Couldn't Found");
                }
                

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetTravelDetailsByIdAsync", 0, "TravelDetailManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public async Task<IActionResult> SaveOrUpdateTravelDetailsAsync(ApiCommonMessage message)
        {

            try
            {
                await _auditLogBLL.SaveLog(nameof(SaveOrUpdateTravelDetailsAsync), "Save or update travel details", Common.Enums.Action.Insert, message);
                Requisitions requisitions = new Requisitions();
                TravelDetails travelDetails = message.GetRequestObject<TravelDetails>();
                if (travelDetails.RequisitionId>0)
                {
                    requisitions = _genericBLL.GetByFilter<Requisitions>(predicate: r => r.RequisitionId == travelDetails.RequisitionId).FirstOrDefault();
                }
                if (requisitions.RequisitionStatus!=(int)Common.Enums.Enums.RequisitionStatus.Approved)
                {
                    return Build.ExceptionMessage(null);
                }

                var result = new TravelDetails();
                if (travelDetails.TravelDetailId > 0)
                {
                    result = _genericBLL.Update<TravelDetails>(travelDetails);
                }
                else
                {
                    result =  _genericBLL.Insert<TravelDetails>(travelDetails);
                }
              

                if (requisitions != null)
                {
                    requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Closed;
                    _genericBLL.Update<Requisitions>(requisitions);
                }

                await _genericBLL.SaveChangesAsync();

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "SaveOrUpdateTravelDetailsAsync", 0, "TravelDetailManager");
                return Build.ExceptionMessage(ex);
            }
        }




    }
}
