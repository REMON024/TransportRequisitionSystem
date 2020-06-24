using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface ISecurityManager
    {
        Task KillSessionASync(Guid tokenIdentifier);
        Task KillAllSessionAsync(string userName);
        Task<bool> VerifyTokenAsync(Guid tokenIdentifier);
        Task<IActionResult> LoginAsync(ApiCommonMessage message);
        Task<IActionResult> LogoutAsync(ApiCommonMessage message);
        Task<IActionResult> GetSessionLogsByFilter(ApiCommonMessage message);
        Task<IActionResult> CancelSession(ApiCommonMessage message);
    }
}
