using System;

namespace NybSys.Models.ViewModels
{
    public class VmSessionFilter : Pagination
    {
        public bool? IsLoggedIn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string UserId { get; set; }
    }
}
