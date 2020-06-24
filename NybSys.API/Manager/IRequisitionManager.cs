using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IRequisitionManager
    {

        Task<IActionResult> SaveRequisitionAsync(ApiCommonMessage message);

        Task<IActionResult> GetRequisitionByFilter(ApiCommonMessage message);
        Task<IActionResult> ApproveRequisition(ApiCommonMessage message);
        Task<IActionResult> RejectRequisition(ApiCommonMessage message);
        Task<IActionResult> CancelRequisition(ApiCommonMessage message);


        Task<IActionResult> GetRequisitionByID(ApiCommonMessage message);
        Task<IActionResult> CheckDriverAvailability(ApiCommonMessage message);

        Task<IActionResult> CheckVehicleAvailability(ApiCommonMessage message);
        Task<IActionResult> GetDriverScheduale(ApiCommonMessage message);
        Task<IActionResult> GetVehicleScheduale(ApiCommonMessage message);
        Task<IActionResult> GetRequisitionByEmployee(ApiCommonMessage message);



    }
}
