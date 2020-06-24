using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface ILogManager
    {
        Task<IActionResult> GetAuditLogByFilter(ApiCommonMessage message);
    }
}
