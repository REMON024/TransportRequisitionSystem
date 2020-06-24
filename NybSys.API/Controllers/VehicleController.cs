using Microsoft.AspNetCore.Mvc;
using NybSys.API.Manager;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NybSys.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleTypeManager _vehicleTypeManager;

        public VehicleController(IVehicleTypeManager vehicleTypeManager)
        {
            _vehicleTypeManager = vehicleTypeManager;
        }

        [HttpPost("get-vehicle-type")]
        public async Task<IActionResult> GetVehicleType([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.GetVehicleType(message);
        }
        [HttpPost("save/docs")]
        public async Task<IActionResult> InsertVehicleDetails([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.InsertVehicleDetails(message);
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllVehicle([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.GetAllVehicle(message);
        }
        [HttpPost("GetVehicleByID")]
        public async Task<IActionResult> GetVehicleByID([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.GetVehicleByID(message);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateVehicle([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.UpdateVehicle(message);
        }

        [HttpPost("GetVehicleByType")]
        public async Task<IActionResult> GetVehicleByType([FromBody] ApiCommonMessage message)
        {
            return await _vehicleTypeManager.GetVehicleByType(message);
        }
    }
}
