using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NybSys.Common.Constants;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Utility;
using NybSys.Models.DTO;
using NybSys.Session.BLL;
using NybSys.Session.DAL;
using UAParser;
using static NybSys.Common.Enums.Enums;

namespace NybSys.Auth.BLL
{
    public class AuthenticationBLL : IAuthenticationBLL
    {
        private readonly IUserBLL _userManager;
        private readonly IConfiguration _config;
        private readonly ISessionLogBLL _sessionLogBLL;
        private readonly IRedisSessionBLL _redisSessionBLL;
        private readonly ISessionBLL _sessionBLL;
        private readonly IHttpContextAccessor _accessor;

        public AuthenticationBLL(
            IUserBLL userManager,
            IConfiguration config,
            ISessionLogBLL sessionLogBLL,
            ISessionBLL sessionBLL,
            IHttpContextAccessor accessor,
            IRedisSessionBLL redisSessionBLL)
        {
            _userManager = userManager;
            _config = config;
            _sessionLogBLL = sessionLogBLL;
            _sessionBLL = sessionBLL;
            _accessor = accessor;
            _redisSessionBLL = redisSessionBLL;
        }

        public virtual async Task<string> Login(string userName, string password)
        {
            Users user = await _userManager.FindByNameAsync(userName);

            if(user == null || user.Status != (int)Common.Enums.Enums.Status.Active)
            {
                throw new BadRequestException(Common.Constants.ErrorMessages.USERNAME_OR_PASSWORD_INCORRECT);
            }

            if(await _userManager.CheckPasswordAsync(user, password))
            {
                Guid Jti = Guid.NewGuid();

                await _sessionLogBLL.CreateSessionLog(GetSessionLog(user, Jti));
                //await _sessionBLL.AddSession(GetSession(user, Jti));
                await _redisSessionBLL.AddSession(Jti, user.Username, Convert.ToInt32(_config["JWT:SessionExpired"]));

                return TokenBuild(user , Jti);

            }else
            {
                throw new BadRequestException(Common.Constants.ErrorMessages.USERNAME_OR_PASSWORD_INCORRECT);
            }
        }

        public virtual async Task Logout(Guid sessionId)
        {
            //await _sessionBLL.RemoveSession(s => s.Id.Equals(sessionId));
            await _redisSessionBLL.RemoveSession(sessionId);
            await _sessionLogBLL.LogoutAsync(sessionId);
        }

        private string TokenBuild(Users user, Guid jti)
        {
            TokenBuilder tokenBuilder = new TokenBuilder();

            string token = tokenBuilder.AddIssuer(_config["JWT:Issuer"].ToString())
                                        .AddAudience(_config["JWT:Audience"].ToString())
                                        .AddUniqueUser(user.Username)
                                        .AddRoleClaim(user.AccessRight.AccessRightName)
                                        .AddClaim(JwtClaims.Name, user.Name)
                                        .AddClaim(JwtClaims.AccessRight, user.AccessRight.AccessRightName)
                                        .AddTokenCredential(_config["JWT:TokenKey"].ToString(), TokenAlgorithm.HmacSha256)
                                        .AddTokenExpiration(TimeSpan.FromMinutes(Convert.ToInt32(_config["JWT:TokenExpired"])))
                                        .AddClaim(JwtClaims.Email, user.Email)
                                        .AddJti(jti)
                                        .BuildToken();
            return token ?? string.Empty;
        }

        private SessionDTO GetSession(Users user, Guid guid)
        {
            return new SessionDTO()
            {
                UserId = user.Username,
                Id = guid,
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,
                Duration = Convert.ToInt32(_config["JWT:SessionExpired"].ToString())
            };
        }

        private SessionLog GetSessionLog(Users user, Guid guid)
        {
            VmBrowserInfo browserInfo = GetBrowserInfo();

            return new SessionLog()
            {
                UserId = user.Username,
                IsLoggedIn = true,
                LoginDate = DateTime.Now,
                SessionId = guid,
                Ipaddress = GetRequestedIpAddress(),
                Browser = browserInfo.BrowserName,
                Device  = browserInfo.DeviceName,
                OS = browserInfo.OSName
            };
        }

        private string GetRequestedIpAddress()
        {
            return _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        private VmBrowserInfo GetBrowserInfo()
        {
            if (_accessor.HttpContext.Request.Headers.ContainsKey("User-Agent"))
            {
                var str = _accessor.HttpContext.Request.Headers["User-Agent"].ToString();

                var uaParser = Parser.GetDefault();

                ClientInfo clientInfo = uaParser.Parse(str);

                return Common.AppUtils.GetBrowserInfo(clientInfo);
            }
            else
            {
                return new VmBrowserInfo();
            }
        }
    }
}