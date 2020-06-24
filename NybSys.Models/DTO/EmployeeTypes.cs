using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class EmployeeTypes
    {
        public EmployeeTypes()
        {
            Employees = new HashSet<Employees>();
        }

        public int EmpTypeId { get; set; }
        public int CategoryId { get; set; }
        public string EmpTypeName { get; set; }
        public int Status { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
