using NybSys.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NybSys.AuditLog.BLL
{
    public interface IAuditLogBLL
    {
        Task SaveLog(
            string CalledFunction, 
            string LogDescription, 
            Common.Enums.Action action,
            ApiCommonMessage data, 
            Common.Enums.LogType logType = Common.Enums.LogType.SystemLog, 
            Common.Enums.Module module = Common.Enums.Module.Web);

        Task<(IEnumerable<VMAuditLog> Items, int TotalCount)> GetLogByFilter(VmAuditLogFilter filter);
        Task ExceptionLogEntry(
            ApiCommonMessage message, 
            string exception, 
            string excptnMsg,
            object InputObject, string methodName, int logPriority, string FormName = "");
    }
}
