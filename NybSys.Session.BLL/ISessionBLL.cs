using NybSys.Session.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Session.BLL
{
    public interface ISessionBLL
    {
        Task<SessionDTO> AddSession(SessionDTO session);
        Task<SessionDTO> UpdateSession(SessionDTO session);
        Task<SessionDTO> UpdateSession(Guid token);
        Task<SessionDTO> GetSession(Expression<Func<SessionDTO, bool>> predicate);
        Task<List<SessionDTO>> GetAllSession(Expression<Func<SessionDTO, bool>> predicate);
        Task<bool> RemoveSession(Expression<Func<SessionDTO, bool>> predicate);
        Task<bool> VerifySession(Expression<Func<SessionDTO, bool>> predicate);
        Task<bool> VerifySession(Guid token);
        Task KillSession(Guid token);
        Task KillAllSession(string userName);
    }
}
