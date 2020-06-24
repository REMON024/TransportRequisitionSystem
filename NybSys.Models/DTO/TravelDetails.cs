using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class TravelDetails
    {
        public int TravelDetailId { get; set; }
        public int RequisitionId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public decimal MileageStart { get; set; }
        public decimal MileageEnd { get; set; }
        public decimal FuelInLiter { get; set; }
        public decimal Amount { get; set; }
        public string ReceiptNo { get; set; }
        public string FilledBy { get; set; }
        public string TravelStartTime { get; set; }
        public string TravelEndTime { get; set; }
        public string VisitingPlace { get; set; }

        public DriverInfos Driver { get; set; }
        public Requisitions Requisition { get; set; }
        public Vehicles Vehicle { get; set; }
    }
}
