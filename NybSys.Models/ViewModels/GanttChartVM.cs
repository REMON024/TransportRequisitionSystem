using NybSys.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
    public class GanttChartVM
    {
        public DateTime Date { get; set; }
        public List<RequsationChartView> lstSchedule { get; set; }
    }

    public class RequsationChartView
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int VehicleID { get; set; }
        public int? DriverID { get; set; }
        public string DriverName { get; set; }
        public string VehicleName { get; set; }

    }

}
