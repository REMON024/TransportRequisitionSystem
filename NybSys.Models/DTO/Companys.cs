using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Companys
    {
        public Companys()
        {
            Departments = new HashSet<Departments>();
        }

        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Departments> Departments { get; set; }
    }
}
