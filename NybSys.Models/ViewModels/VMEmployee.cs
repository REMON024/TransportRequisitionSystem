using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
   public class VMEmployee
    {
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }

        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }

        public string HOD { get; set; }

        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }




    }
}
