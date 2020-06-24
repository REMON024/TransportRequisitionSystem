using System;
using System.Collections.Generic;


namespace NybSys.Models.DTO
{
    public partial class Requisitions
    {
        public Requisitions()
        {
            TravelDetails = new HashSet<TravelDetails>();
        }

        public int RequisitionId { get; set; }
        public DateTime RequisitionDate { get; set; }
        public long EmployeeId { get; set; }
        public string PalcetoVisit { get; set; }
        public string PurposeofVisit { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string ProjectName { get; set; }
        public int VehicleTypeID { get; set; }
        public int? DriverID { get; set; }
        public int IsSelfDrive { get; set; }
        public string SelefDriverName { get; set; }
        public int NumberofPassenger { get; set; }
        public int VehicleID { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int RequisitionStatus { get; set; }
        public string CretedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }

        public string Note { get; set; }
        public string ApproveOrRejectNote { get; set; }

        public string VehicleTypeName { get; set; }
        public string VehicleName { get; set; }
        public string DriverName { get; set; }


        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DriverInfos driverInfos { get; set; }
        public Employees employees { get; set; }
        public Vehicles Vehicle { get; set; }
        public ICollection<TravelDetails> TravelDetails { get; set; }
    }
}
