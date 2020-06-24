using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NybSys.Auth.BLL
{
    public interface IAuthenticationBLL
    {
        Task<string> Login(string userName, string password);
        Task Logout(Guid sessionId);
    }
}
