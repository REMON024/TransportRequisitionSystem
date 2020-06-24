using System;

namespace NybSys.Models.ViewModels
{
    public class VmAuditLogFilter : Pagination
    {
        public string Username { get; set; }
        public int Action { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
