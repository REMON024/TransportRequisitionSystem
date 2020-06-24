using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IMailManager
    {
        Task SendEmail(string subject, string messages, bool isBcc);
        Task SendEmail(string subject, string messages);
        Task SendEmail(string subject, string messages, string ccAddress, string fileLocation);
        Task SendEmail(string subject, string messages, string ccAddress);

    }
}