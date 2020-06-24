using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Extension;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;
using NybSys.UnitOfWork;

namespace NybSys.Auth.BLL
{
    public class SessionLogBLL : ISessionLogBLL
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionLogBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<SessionLog> CreateSessionLog(SessionLog session)
        {
            await GetUnitOfWork().InsertAsync(session);
            await _unitOfWork.SaveAsync();

            return session;
        }

        public virtual async Task<SessionLog> GetSessionLog(Guid sessionId)
        {
            return await GetUnitOfWork().GetFirstOrDefaultAsync(predicate: p => p.SessionId.Equals(sessionId),disableTracking: false);
        }

        public virtual async Task<SessionLog> GetSessionLog(Expression<Func<SessionLog, bool>> predicate)
        {
            return await GetUnitOfWork().GetFirstOrDefaultAsync(predicate: predicate);
        }

        public virtual async Task<IEnumerable<Guid>> GetSessionId(Expression<Func<SessionLog, bool>> predicate)
        {
            return (await GetUnitOfWork().GetAsync(predicate: predicate)).Select(p => p.SessionId);
        }

        public virtual async Task<List<SessionLog>> GetSessionLogs(Expression<Func<SessionLog, bool>> predicate)
        {
            return (await GetUnitOfWork().GetAsync(predicate : predicate)).ToList();
        }

        public virtual async Task<IPagedList<VmSessionLog>> GetSessionLogsByFilter(VmSessionFilter filter)
        {
            IPagedList<VmSessionLog> lstSessionLog = await GetUnitOfWork().GetPagedListAsync<VmSessionLog>(
                                             predicate: u => (string.IsNullOrEmpty(filter.UserId) ? true : u.UserId.EqualsWithLower(filter.UserId)) &&
                                                             (filter.IsLoggedIn == null ? true : u.IsLoggedIn.Equals(filter.IsLoggedIn)) &&
                                                             (u.LoginDate.Date >= filter.FromDate.GetLocalZoneDate()
                                                             && u.LoginDate.Date <= filter.ToDate.GetLocalZoneDate()),
                                             pageIndex: (filter.PageIndex - 1), pageSize: filter.PageSize,
                                             orderBy: source => source.OrderByDescending(u => u.LoginDate),
                                             selector: u => new VmSessionLog()
                                             {
                                                 IsLoggedIn = u.IsLoggedIn,
                                                 UserId = u.UserId,
                                                 LoginDate = u.LoginDate,
                                                 Browser = u.Browser,
                                                 Device = u.Device,
                                                 Id = u.Id,
                                                 Ipaddress = u.Ipaddress,
                                                 LogoutDate = u.LogoutDate,
                                                 OS = u.OS,
                                                 SessionId = u.SessionId
                                             });

            if (lstSessionLog.TotalCount > 0)
            {
                return lstSessionLog;
            }

            throw new NotFoundException(Common.Constants.ErrorMessages.NO_LOG_FOUND);
        }


        public virtual async Task LogoutAsync(Guid sessionId)
        {
            SessionLog sessionLog = await GetSessionLog(sessionId);
            sessionLog.IsLoggedIn = false;
            sessionLog.LogoutDate = DateTime.Now;

            await UpdateSessionLog(sessionLog);            
        }

        public virtual async Task LogoutAsync(string userName)
        {
            List<SessionLog> lstSessionLog = await GetSessionLogs(p => p.UserId.Equals(userName) && p.IsLoggedIn);
            lstSessionLog.ForEach(item => {
                item.IsLoggedIn  = false;
                item.LogoutDate = DateTime.Now;
            });

            GetUnitOfWork().Update(lstSessionLog);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<SessionLog> UpdateSessionLog(SessionLog session)
        {
            GetUnitOfWork().Update(session);
            await _unitOfWork.SaveAsync();
            return session;
        }

        private IRepository<SessionLog> GetUnitOfWork()
        {
            return _unitOfWork.Repository<SessionLog>();
        }
    }
}