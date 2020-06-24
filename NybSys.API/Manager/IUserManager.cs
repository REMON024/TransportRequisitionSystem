using Microsoft.AspNetCore.Mvc;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IUserManager
    {
        Task<IActionResult> CreateUserAsync(ApiCommonMessage message);
        Task<IActionResult> UpdateUserAsync(ApiCommonMessage message);
        Task<IActionResult> ChangePassword(ApiCommonMessage message);
        Task<IActionResult> ResetPassword(ApiCommonMessage message);
        Task<IActionResult> GetUser(ApiCommonMessage message);
        Task<IActionResult> GetUsersByFilter(ApiCommonMessage message);

    }
}
