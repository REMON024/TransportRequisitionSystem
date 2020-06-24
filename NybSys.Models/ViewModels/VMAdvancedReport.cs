using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
  public  class VMAdvancedReport
    {
        public int EmployeeID { get; set; }
        public int DriverID { get; set; }
        public int VehicleID { get; set; }

        public string EmployeeName { get; set; }
        public string DriverName { get; set; }
        public string VehicleName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
