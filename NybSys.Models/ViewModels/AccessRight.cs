using System.Collections.Generic;

namespace NybSys.Models.ViewModels
{
    public class VMAccessRights
    {
        public VMAccessRights()
        {
            RightLists = new List<VmController>();
        }

        public int Id { get; set; }

        public string RoleName { get; set; }
        public List<VmController> RightLists { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public string AccessControlJson { get; set; }
    }

    public class VmController
    {
        public VmController()
        {
            Actions = new HashSet<VMAction>();
        }
       
        public string ControllerName { get; set; }
        public string Title { get; set; }

        public ICollection<VMAction> Actions { get; set; }
    }

    public class VMAction
    {
        
        public string ActionName { get; set; }
        public string Title { get; set; }
    }
}
