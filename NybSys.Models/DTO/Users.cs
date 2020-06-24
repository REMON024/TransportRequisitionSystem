using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Users
    {
        public Users()
        {
            SessionLog = new HashSet<SessionLog>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public long? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int AccessRightId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }

        public AccessRight AccessRight { get; set; }
        public Employees Employee { get; set; }
        public ICollection<SessionLog> SessionLog { get; set; }
    }
}
