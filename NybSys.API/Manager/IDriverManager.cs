using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IDriverManager
    {
        Task<IActionResult> SaveDriverAsync(ApiCommonMessage message);

        Task<IActionResult> GetDriverByFilter(ApiCommonMessage message);

        Task<IActionResult> GetAllDriver(ApiCommonMessage message);
        Task<IActionResult> GetDriverInfoByID(ApiCommonMessage message);
    }
}
