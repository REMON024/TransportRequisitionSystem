using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class AccessRight
    {
        public AccessRight()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string AccessRightName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }
        public string AccessControlJson { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
