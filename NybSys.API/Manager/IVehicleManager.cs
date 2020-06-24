using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
   public interface IVehicleManager
    {
        Task<IActionResult> SaveVehicleAsync(ApiCommonMessage message);

        
    }
}
