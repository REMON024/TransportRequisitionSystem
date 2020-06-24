using System;

namespace NybSys.Models.DTO
{
    public partial class LogMain
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public int? ModuleId { get; set; }
        public string FormName { get; set; }
        public string CalledFunction { get; set; }
        public int? ActionId { get; set; }
        public string LogDescription { get; set; }
        public string LogMessage { get; set; }
        public DateTime? LogTime { get; set; }
        public int? LogTypeId { get; set; }
        public int? Status { get; set; }

        public Action Action { get; set; }
        public LogType LogType { get; set; }
        public Module Module { get; set; }
    }
}
