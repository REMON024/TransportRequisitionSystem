using System;
using System.Collections.Generic;
using System.Text;
using NybSys.Models.DTO;
using System.Threading.Tasks;

namespace NybSys.TMS.BLL
{
   public interface ITravelDetailsBLL
    {
        Task<TravelDetails> GetByID(int id);
        
    }
}
