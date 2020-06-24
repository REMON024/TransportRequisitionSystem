using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
    public class VmRequsationFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DriverID { get; set; }
        public int VehicleID { get; set; }
    }
}
