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
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverManager _driverManager;

        public DriverController(IDriverManager driverManager)
        {
            _driverManager = driverManager;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("SaveDriver")]
        public async Task<IActionResult> SaveRequisition([FromBody] ApiCommonMessage message)
        {

            return await _driverManager.SaveDriverAsync(message);
        }

        [HttpPost("GetDriverInfo")]
        public async Task<IActionResult> GetAllDriver([FromBody] ApiCommonMessage message)
        {
            return await _driverManager.GetAllDriver(message);
        }


        [HttpPost("GetFilteredDriver")]
        public async Task<IActionResult> GetFiteredRequisition([FromBody] ApiCommonMessage message)
        {
            return await _driverManager.GetDriverByFilter(message);
        }

     [HttpPost("GetDriverInfoByID")]
        public async Task<IActionResult> GetDriverInfoByID([FromBody] ApiCommonMessage message)
        {
            return await _driverManager.GetDriverInfoByID(message);
        }
    }
}