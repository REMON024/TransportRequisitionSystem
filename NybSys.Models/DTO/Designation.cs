using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.DTO
{
   public class Designation
    {
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
