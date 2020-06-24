using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class LogType
    {
        public LogType()
        {
            LogMain = new HashSet<LogMain>();
        }

        public int LogTypeId { get; set; }
        public string LogTypeName { get; set; }

        public ICollection<LogMain> LogMain { get; set; }
    }
}
