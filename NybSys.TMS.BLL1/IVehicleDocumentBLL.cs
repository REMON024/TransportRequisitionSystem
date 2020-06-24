using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NybSys.TMS.BLL
{
  public  interface IVehicleDocumentBLL
    {
        Task<Vehicles> SaveVehicleDocument(Models.ViewModels.VMVehicle vMVehicle);
    }
}
