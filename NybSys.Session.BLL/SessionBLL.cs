using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NybSys.Common.Constants;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Extension;
using NybSys.Session.DAL;

namespace NybSys.Session.BLL
{
    public class SessionBLL : ISessionBLL, IRedisSessionBLL
    {
        private readonly SessionContext _context;
        private readonly IDistributedCache _distributedCache;

        public SessionBLL(
            SessionContext context,
            IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }

        public async Task<SessionDTO> AddSession(SessionDTO session)
        {
            await _context.Session.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public virtual async Task AddSession(Guid sessionId, string data, int time)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(time));

            await _distributedCache.SetStringAsync(sessionId.ToString(), data, options);
        }

        public async Task<List<SessionDTO>> GetAllSession(Expression<Func<SessionDTO, bool>> predicate)
        {
            return await _context.Session.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<SessionDTO> GetSession(Expression<Func<SessionDTO, bool>> predicate)
        {
            return await _context.Session.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<string> GetSession(Guid sessionId)
        {
            return await _distributedCache.GetStringAsync(sessionId.ToString());
        }

        public virtual async Task KillAllSession(string userName)
        {
            List<SessionDTO> lstSession = await _context.Session.Where(p => p.UserId.EqualsWithLower(userName)).ToListAsync();

            if (lstSession.Count > 0)
            {
                await RemoveSession(lstSession);
            }
        }

        public virtual async Task KilledSession(Guid sessionId)
        {
            await RemoveSession(sessionId);
        }

        public virtual async Task KillSession(Guid token)
        {
            SessionDTO session = await _context.Session.FirstOrDefaultAsync(p => p.Id.Equals(token));

            if (session != null)
                await RemoveSession(session);
        }

        public async Task<bool> RemoveSession(Expression<Func<SessionDTO, bool>> predicate)
        {
            SessionDTO session = await GetSession(predicate);

            if (session != null)
            {
                _context.Session.Remove(session);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new NotFoundException(ErrorMessages.SESSION_NOT_FOUND);
            }
        }

        public virtual async Task RemoveSession(Guid sessionId)
        {
            await _distributedCache.RemoveAsync(sessionId.ToString());
        }

        public async Task<SessionDTO> UpdateSession(SessionDTO session)
        {
            _context.Session.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<SessionDTO> UpdateSession(Guid token)
        {
            SessionDTO session = await GetSession(s => s.Id.Equals(token));

            if (session != null)
            {
                session.LastUpdate = DateTime.Now;
                await UpdateSession(session);
                return session;
            }
            else
            {
                throw new NotFoundException(ErrorMessages.SESSION_NOT_FOUND);
            }
        }

        public async Task<bool> VerifySession(Expression<Func<SessionDTO, bool>> predicate)
        {
            return await _context.Session.AnyAsync(predicate);
        }

        public async Task<bool> VerifySession(Guid token)
        {
            SessionDTO session = await GetSession(s => s.Id.Equals(token));

            if (session != null)
            {
                if (session.LastUpdate.Value.AddMinutes(session.Duration) < DateTime.Now)
                {
                    return false;
                }

                session.LastUpdate = DateTime.Now;
                await UpdateSession(session);
                return true;
            }
            else
            {
                throw new Exception();
            }
        }

        public virtual async Task<bool> VerifiedSession(Guid sessionId)
        {
            var session = await GetSession(sessionId);

            if (session != null)
            {
                await UpdateOrRefreshSession(sessionId);
                return true;
            }

            return false;
        }

        private async Task RemoveSession(SessionDTO session)
        {
            _context.Remove(session);
            await _context.SaveChangesAsync();
        }

        private async Task RemoveSession(List<SessionDTO> sessions)
        {
            _context.RemoveRange(sessions);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateOrRefreshSession(Guid sessionId)
        {
            await _distributedCache.RefreshAsync(sessionId.ToString());
        }

        public virtual async Task KilledAllSession(List<Guid> lstSessionId)
        {
            foreach (Guid sessionId in lstSessionId)
            {
                await RemoveSession(sessionId);
            }
        }
    }
}
