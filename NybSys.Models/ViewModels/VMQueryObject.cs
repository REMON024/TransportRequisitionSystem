using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
  public  class VMQueryObject
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Status { get; set; }

        public int EmployeeID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
