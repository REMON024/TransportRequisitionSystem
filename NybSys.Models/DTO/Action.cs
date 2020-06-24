using System.Collections.Generic;

namespace NybSys.Models.DTO
{
    public partial class Action
    {
        public Action()
        {
            LogMain = new HashSet<LogMain>();
        }

        public int ActionId { get; set; }
        public string ActionName { get; set; }

        public ICollection<LogMain> LogMain { get; set; }
    }
}
