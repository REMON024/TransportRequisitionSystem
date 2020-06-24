using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Models.ViewModels;

namespace NybSys.API.Manager
{
    public class LogManager : ILogManager
    {
        private readonly IAuditLogBLL _auditLogBLL;

        public LogManager(IAuditLogBLL auditLogBLL)
        {
            _auditLogBLL = auditLogBLL;
        }

        public virtual async Task<IActionResult> GetAuditLogByFilter(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetAuditLogByFilter), "Get Audit by filter", Common.Enums.Action.View, message);

            try
            {
                VmAuditLogFilter filter = message.GetRequestObject<VmAuditLogFilter>();

                var result = await _auditLogBLL.GetLogByFilter(filter);

                return Build.SuccessMessage(new
                {
                    result.TotalCount,
                    result.Items
                });
            }
            catch(Exception ex)
            {
                return Build.ExceptionMessage(ex);
            }
        }
    }
}
