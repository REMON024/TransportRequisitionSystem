using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NybSys.Session.BLL
{
    public interface IRedisSessionBLL
    {
        Task AddSession(Guid sessionId, string data, int time);
        Task UpdateOrRefreshSession(Guid sessionId);
        Task<string> GetSession(Guid sessionId);
        Task RemoveSession(Guid sessionId);
        Task<bool> VerifiedSession(Guid sessionId);
        Task KilledSession(Guid sessionId);
        Task KilledAllSession(List<Guid> lstSessionId);
    }
}
