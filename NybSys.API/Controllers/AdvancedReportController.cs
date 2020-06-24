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
    public class AdvancedReportController : ControllerBase
    {

        private readonly IAdvancedReportSearchManager _advancedReportSearchManager;

        public AdvancedReportController(IAdvancedReportSearchManager advancedReportSearchManager)
        {
            _advancedReportSearchManager = advancedReportSearchManager;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
       

        [HttpPost("AdvancedReportSearch")]
        public async Task<IActionResult> AdvancedReportSearch([FromBody] ApiCommonMessage message)
        {
            return await _advancedReportSearchManager.GetAdvancedReportByFilter(message);
        }

        
    }
}