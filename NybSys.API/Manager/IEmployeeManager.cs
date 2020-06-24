using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IEmployeeManager
    {
        Task<IActionResult> GetEmployeeByFilter(ApiCommonMessage message);

        Task<IActionResult> GetEmployeeByFilterUsingVM(ApiCommonMessage message);

        Task<IActionResult> GetEmployeeByid(ApiCommonMessage message);




    }
}
