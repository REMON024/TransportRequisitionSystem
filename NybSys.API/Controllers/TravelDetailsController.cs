using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Manager;
using NybSys.Models.ViewModels;
namespace NybSys.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TravelDetailsController
    {
        private readonly ITravelDetailsManager _travelDetailsManager;

        public TravelDetailsController(ITravelDetailsManager travelDetailsManager)
        {
            _travelDetailsManager = travelDetailsManager;


        }

        [HttpPost("SaveOrUpdateTravelDetails")]
        public async Task<IActionResult> SaveOrUpdateTravelDetails([FromBody] ApiCommonMessage message)
        {
            return await _travelDetailsManager.SaveOrUpdateTravelDetailsAsync(message);
        }

        [HttpPost("GetTravelDetailsById")]
        public async Task<IActionResult> GetTravelDetailsById([FromBody] ApiCommonMessage message)
        {
            return await _travelDetailsManager.GetTravelDetailsByIdAsync(message);
        }
    }
}
