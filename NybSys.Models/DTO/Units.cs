using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Units
    {
        public Units()
        {
            Employees = new HashSet<Employees>();
        }

        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public short Status { get; set; }

        public Departments Department { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
