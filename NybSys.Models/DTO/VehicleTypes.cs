using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class VehicleTypes
    {
        public VehicleTypes()
        {
            Vehicles = new HashSet<Vehicles>();
        }

        public int VehicleTypeID { get; set; }
        public string TypeName { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
