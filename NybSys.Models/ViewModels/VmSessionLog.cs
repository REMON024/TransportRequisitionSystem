using System;

namespace NybSys.Models.ViewModels
{
    public class VmSessionLog
    {
        public long Id { get; set; }
        public Guid SessionId { get; set; }
        public string UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Ipaddress { get; set; }
        public string Device { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
    }
}
