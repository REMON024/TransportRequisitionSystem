using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
    public class VMAuditLog
    {
        public string UserId { get; set; }
        public Common.Enums.Action Action { get; set; }
        public string CalledFunction { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogTime { get; set; }
        public string LogMessage { get; set; }
    }
}
