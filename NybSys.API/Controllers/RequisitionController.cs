using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Manager;
using NybSys.Models.ViewModels;

namespace NybSys.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequisitionController : ControllerBase
    {
        private readonly IRequisitionManager _requisitionManager;

        public RequisitionController(IRequisitionManager requisitionManager)
        {
            _requisitionManager = requisitionManager;
        }



        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("SaveRequisition")]
        public async Task<IActionResult> SaveRequisition([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.SaveRequisitionAsync(message);
        }
        [HttpPost("ApproveRequisition")]
        public async Task<IActionResult> ApproveRequisition([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.ApproveRequisition(message);
        }


        [HttpPost("RejectRequisition")]
        public async Task<IActionResult> RejectRequisition([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.RejectRequisition(message);
        }

        [HttpPost("CancelRequisition")]
        public async Task<IActionResult> CancelRequisition([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.CancelRequisition(message);
        }

        [HttpPost("GetFilteredRequisition")]
        public async Task<IActionResult> GetFiteredRequisition([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.GetRequisitionByFilter(message);
        }

        [HttpPost("GetRequisitionByID")]
        public async Task<IActionResult> GetRequisitionByID([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.GetRequisitionByID(message);
        }

        [HttpPost("CheckDriverAvailability")]
        public async Task<IActionResult> CheckDriverAvailability([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.CheckDriverAvailability(message);
        }

        [HttpPost("CheckVehicleAvailability")]
        public async Task<IActionResult> CheckVehicleAvailability([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.CheckVehicleAvailability(message);
        }


        [HttpPost("DriverSchedule")]
        public async Task<IActionResult> DriverSchedule([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.GetDriverScheduale(message);
        }

        [HttpPost("VehicleSchedule")]
        public async Task<IActionResult> VehicleSchedule([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.GetVehicleScheduale(message);
        }

        [HttpPost("GetRequisitionByEmployee")]
        public async Task<IActionResult> GetRequisitionByEmployee([FromBody] ApiCommonMessage message)
        {
            return await _requisitionManager.GetRequisitionByEmployee(message);
        }



    }
}