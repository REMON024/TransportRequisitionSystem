using NybSys.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
    public class VMVehicle
    {
        public Vehicles Vehicle { get; set; }

        public List<VehicleDocuments> lstVehicleDocument { get; set; }

        public VMVehicle()
        {
            Vehicle = new Vehicles();
            lstVehicleDocument = new List<VehicleDocuments>();
        }
    }
}
