using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
  public class VMAdvancedReportSearchResult
    {
        public int RequisitionId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime RequisitionDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string PlaceToVisit { get; set; }
        public string PurposeOfVisit { get; set; }
        public string ProjectName { get; set; }
        public string VehicleName { get; set; }
        public string DriverName { get; set; }
        public decimal Amount { get; set; }
        public int status { get; set; }
        public int VehicleId { get; set; }
    }
}
