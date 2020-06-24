namespace NybSys.Models.ViewModels
{
    public class VmUserFilter : Pagination
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Status { get; set; }
    }
}
