using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Module
    {
        public Module()
        {
            LogMain = new HashSet<LogMain>();
        }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }

        public ICollection<LogMain> LogMain { get; set; }
    }
}
