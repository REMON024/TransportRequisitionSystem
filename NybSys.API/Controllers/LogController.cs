using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Helper;
using NybSys.API.Manager;
using NybSys.Models.ViewModels;

namespace NybSys.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [NybSysAuthorizeWithDB]
    [SessionCheck]
    public class LogController : ControllerBase
    {
        private readonly ILogManager _logManager;


        public LogController(ILogManager logManager)
        {
            _logManager = logManager;
        }

        /// <summary>
        /// Get Audit Log List by Filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("get-auditlog-by-filter")]
        public async Task<IActionResult> GetAuditLogByFilter([FromBody]ApiCommonMessage message)
        {
            return await _logManager.GetAuditLogByFilter(message);
        }
    }
}