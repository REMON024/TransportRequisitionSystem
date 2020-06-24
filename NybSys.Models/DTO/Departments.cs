using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
            Units = new HashSet<Units>();
        }

        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public short Status { get; set; }

        public Companys Company { get; set; }
        public ICollection<Employees> Employees { get; set; }
        public ICollection<Units> Units { get; set; }
    }
}
