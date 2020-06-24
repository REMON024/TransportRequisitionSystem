using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public  interface IVehicleTypeManager
    {
        Task<IActionResult> GetVehicleType(ApiCommonMessage message);
        Task<IActionResult> Save(ApiCommonMessage message);
        Task<IActionResult> InsertVehicleDetails(ApiCommonMessage message);
        Task<IActionResult> GetAllVehicle(ApiCommonMessage message);
        Task<IActionResult> GetVehicleByID(ApiCommonMessage message);
        Task<IActionResult> UpdateVehicle(ApiCommonMessage message);
        Task<IActionResult> GetVehicleByType(ApiCommonMessage message);
    }
}
