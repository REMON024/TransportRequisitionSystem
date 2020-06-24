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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpPost("GetFilteredEmployee")]
        public async Task<IActionResult> GetFiteredRequisition([FromBody] ApiCommonMessage message)
        {
            return await _employeeManager.GetEmployeeByFilter(message);
        }

        [HttpPost("getAllEmployee")]
        public async Task<IActionResult> GetAllEmployee([FromBody] ApiCommonMessage message)
        {
            return await _employeeManager.GetEmployeeByFilter(message);
        }


        [HttpPost("getEmployeeById")]
        public async Task<IActionResult> GetEmployeeById([FromBody] ApiCommonMessage message)
        {
            return await _employeeManager.GetEmployeeByid(message);
        }

        [HttpPost("getAllEmployeeUsingVM")]
        public async Task<IActionResult> getAllEmployeeUsingVM([FromBody] ApiCommonMessage message)
        {
            return await _employeeManager.GetEmployeeByFilterUsingVM(message);
        }
    }
}