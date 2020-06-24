using System;
using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public class Actions
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
        public int ControllerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }

        public Controllers Controller { get; set; }
        
    }
}
