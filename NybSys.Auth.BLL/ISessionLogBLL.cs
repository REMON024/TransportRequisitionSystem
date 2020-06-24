using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;

namespace NybSys.Auth.BLL
{
    public interface ISessionLogBLL
    {
        Task<SessionLog> CreateSessionLog(SessionLog session);
        Task<SessionLog> UpdateSessionLog(SessionLog session);
        Task<SessionLog> GetSessionLog(Guid sessionId);
        Task<SessionLog> GetSessionLog(Expression<Func<SessionLog,bool>> predicate);
        Task<IEnumerable<Guid>> GetSessionId(Expression<Func<SessionLog,bool>> predicate);
        Task LogoutAsync(Guid sessionId);
        Task LogoutAsync(string userName);
        Task<List<SessionLog>> GetSessionLogs(Expression<Func<SessionLog,bool>> predicate);
        Task<IPagedList<VmSessionLog>> GetSessionLogsByFilter(VmSessionFilter filter);
    }
}