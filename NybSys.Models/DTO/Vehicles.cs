using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Vehicles
    {
        public Vehicles()
        {
            Requisitions = new HashSet<Requisitions>();
            TravelDetails = new HashSet<TravelDetails>();
            VehicleDocuments = new HashSet<VehicleDocuments>();
        }

        public int VehicleID { get; set; }
        public int VehicleTypeID { get; set; }
        public string Name { get; set; }
        public string RegistrationNo { get; set; }
        public string EngineNumber { get; set; }
        public string ChasisNumber { get; set; }
        public int Capacity { get; set; }
        public string OtherInfo { get; set; }
        public string Color { get; set; }

        public VehicleTypes VehicleType { get; set; }
        public ICollection<Requisitions> Requisitions { get; set; }
        public ICollection<TravelDetails> TravelDetails { get; set; }
        public ICollection<VehicleDocuments> VehicleDocuments { get; set; }
    }
}
