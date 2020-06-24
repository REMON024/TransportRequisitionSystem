using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class DriverInfos
    {
        public DriverInfos()
        {
            TravelDetails = new HashSet<TravelDetails>();
            requisitions = new HashSet<Requisitions>();
        }

        public int DriverInfoId { get; set; }
        public long EmployeeId { get; set; }
        public string DrivingLicenceNo { get; set; }
        public DateTime LicenceExpireDate { get; set; }
        public string DutyStart { get; set; }
        public string DutyEnd { get; set; }
        public string OtherInfo { get; set; }
        public string EmployeeName { get; set; }

        public Employees Employee { get; set; }
        public ICollection<Requisitions> requisitions { get; set; }

        public ICollection<TravelDetails> TravelDetails { get; set; }
    }
}
