using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class VehicleDocuments
    {
        public int VehicleDocumentId { get; set; }
        public int VehicleId { get; set; }
        public string DocumentName { get; set; }
        public DateTime ValidDate { get; set; }
        public DateTime RenewalDate { get; set; }

        public Vehicles Vehicle { get; set; }
    }
}
