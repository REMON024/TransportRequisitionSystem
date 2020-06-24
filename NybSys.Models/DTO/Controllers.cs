using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public class Controllers
    {
        public Controllers()
        {
            Actions = new HashSet<Actions>();
        }

        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }

        public ICollection<Actions> Actions { get; set; }
        

    }
}
