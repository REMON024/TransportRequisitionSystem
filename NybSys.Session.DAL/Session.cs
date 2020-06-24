using System;

namespace NybSys.Session.DAL
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Duration { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
