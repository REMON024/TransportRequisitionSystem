using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Auth.BLL;
using NybSys.Common.Utility;
using NybSys.Common.Extension;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Session.BLL;
using System.Linq;

namespace NybSys.API.Manager
{
    public class SecurityManager : ISecurityManager, IUserManager, IAccessManager
    {
        private readonly IAuthenticationBLL _authenticationBLL;
        private readonly ISessionLogBLL _sessionLogBLL;
        private readonly ISessionBLL _sessionBLL;
        private readonly IRedisSessionBLL _redisSessionBLL;
        private readonly IUserBLL _userBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        private readonly IAccessRightBLL _accessRightBLL;

        public SecurityManager
            (
                IAuthenticationBLL authenticationBLL,
                ISessionLogBLL sessionLogBLL,
                ISessionBLL sessionBLL,
                IUserBLL userBLL,
                IAuditLogBLL auditLogBLL,
                IRedisSessionBLL redisSessionBLL,
                IAccessRightBLL accessRightBLL
            )
        {
            _authenticationBLL = authenticationBLL;
            _sessionLogBLL = sessionLogBLL;
            _sessionBLL = sessionBLL;
            _userBLL = userBLL;
            _auditLogBLL = auditLogBLL;
            _redisSessionBLL = redisSessionBLL;
            _accessRightBLL = accessRightBLL;
        }

        public virtual async Task<IActionResult> CancelSession(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(CancelSession), "Session Cancel", Common.Enums.Action.Delete, message);

            try
            {
                string sessionId = message.GetRequestObject<string>();
                await KillSessionASync(new Guid(sessionId));
                return Build.SuccessMessage(Common.Constants.SuccessMessages.SUCCESSFULLY_CANCEL_SESSION);

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "CancelSession", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> ChangePassword(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(ChangePassword), "Change Password", Common.Enums.Action.Update, message);

            try
            {
                VmChangePassword changePassword = message.GetRequestObject<VmChangePassword>();

                Users user = await _userBLL.FindByNameAsync(changePassword.Username);

                await _userBLL.ChangePassword(user, changePassword.OldPassword, changePassword.NewPassword);

                return Build.SuccessMessage(Common.Constants.SuccessMessages.SUCCESSFULLY_CHANGE_PASSWORD);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "ChangePassword", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> CreateUserAsync(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(CreateUserAsync), "Create User", Common.Enums.Action.Insert, message);

            try
            {
                Users user = message.GetRequestObject<Users>();
                user.CreatedBy = message.UserName;
                user.CreatedDate = DateTime.Now;

                await _userBLL.CreateUserAsync(user);

                return Build.SuccessMessage(user);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "CreateUserAsync", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetAccessControlByRole(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetAccessControlByRole), "Get Access list by role", Common.Enums.Action.View, message);

            try
            {
                string roleName = message.GetRequestObject<string>();
                return Build.SuccessMessage(await _accessRightBLL.GetAccessListByRoleName(roleName));
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetAccessControlByRole", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetAllAccessList(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetAllAccessList), "Get All access list", Common.Enums.Action.View, message);

            try
            {
                return Build.SuccessMessage(await _accessRightBLL.GetAllAccessRight(p => true));

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetAllAccessList", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetAllRole(ApiCommonMessage message)
        {
            try
            {
                return Build.SuccessMessage(await _accessRightBLL.GetAccessRightForDropDown());

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetAllRole", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        

        public virtual async Task<IActionResult> GetSessionLogsByFilter(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetSessionLogsByFilter), "Get session log by filter", Common.Enums.Action.View, message);

            try
            {
                VmSessionFilter filter = message.GetRequestObject<VmSessionFilter>();

                var result = await _sessionLogBLL.GetSessionLogsByFilter(filter);

                return Build.SuccessMessage(result);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetSessionLogsByFilter", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetUser(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetUser), string.Format("Get user for {0}",message.UserName), Common.Enums.Action.View, message);

            try
            {
                string userName = message.GetRequestObject<string>();

                Users user = await _userBLL.FindByNameAsync(userName);

                return Build.SuccessMessage(user);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetUser", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetUsersByFilter(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetUsersByFilter), "Get users by filter", Common.Enums.Action.View, message);

            try
            {
                VmUserFilter filter = message.GetRequestObject<VmUserFilter>();

                return Build.SuccessMessage(await _userBLL.GetUsersByFilter(filter));

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetUsersByFilter", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task KillAllSessionAsync(string userName)
        {
            try
            {
                //await _sessionBLL.KillAllSession(userName);

                var lstSessionId = await _sessionLogBLL.GetSessionId(p => p.UserId.EqualsWithLower(userName)
                                                                            && p.IsLoggedIn);

                await _redisSessionBLL.KilledAllSession(lstSessionId.ToList());

                await _sessionLogBLL.LogoutAsync(userName);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual async Task KillSessionASync(Guid tokenIdentifier)
        {
            try
            {
                await _authenticationBLL.Logout(tokenIdentifier);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual async Task<IActionResult> LoginAsync(ApiCommonMessage message)
        {
            
            try
            {
                VmLogin login = message.GetRequestObject<VmLogin>();

                message.UserName = login.Username;

                await _auditLogBLL.SaveLog(nameof(LoginAsync), "Login into system", Common.Enums.Action.Other, message);

                //login.Password = ClientEncryptionDecryption.DecryptStringAES(login.Password);

                string token = await _authenticationBLL.Login(login.Username, ClientEncryptionDecryption.DecryptStringAES(login.Password));

                return Build.SuccessMessage(new
                {
                    Token = Common.Constants.HttpHeaders.AuthenticationSchema + token
                });

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "LoginAsync", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> LogoutAsync(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(LogoutAsync), "Logout from system", Common.Enums.Action.Other, message);

            try
            {
                await KillSessionASync(message.SessionId);
                return Build.SuccessMessage(Common.Constants.SuccessMessages.SUCCESSFULLY_LOGOUT);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "LogoutAsync", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> ResetPassword(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(ResetPassword), "Password reset", Common.Enums.Action.Update, message);

            try
            {
                VmResetPassword resetPassword = message.GetRequestObject<VmResetPassword>();

                await _userBLL.ResetPasswordAsync(resetPassword.Username, ClientEncryptionDecryption.DecryptStringAES(resetPassword.Password));

                await KillAllSessionAsync(resetPassword.Username);

                return Build.SuccessMessage(Common.Constants.SuccessMessages.SUCCESSFULLY_RESET_PASSWORD);

            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "ResetPassword", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> SaveOrUpdateAccessControl(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(SaveOrUpdateAccessControl), "Save Or Update access control", Common.Enums.Action.View, message);

            try
            {
                VMAccessRights accessRights = message.GetRequestObject<VMAccessRights>();
                accessRights.AccessControlJson= JSONConvert.ConvertString(accessRights.RightLists);

                return Build.SuccessMessage(await _accessRightBLL.SaveOrUpdateAccessRight(accessRights));
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "SaveOrUpdateAccessControl", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> SaveUpdateAction(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(SaveUpdateAction), "Save Or Update action", Common.Enums.Action.View, message);

            try
            {
                Actions action = message.GetRequestObject<Actions>();

                if(action.Id == 0)
                {
                    action.CreatedBy = message.UserName;
                    action.CreatedDate = DateTime.Now;
                    await _accessRightBLL.CreateAction(action);
                }
                {
                    action.ModifiedBy = message.UserName;
                    action.ModifiedDate = DateTime.Now;
                    await _accessRightBLL.UpdateAction(action);
                }

                return Build.SuccessMessage(action);


            }catch(Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "SaveUpdateAction", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> SaveUpdateController(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(SaveUpdateController), "Save Or Update Controller", Common.Enums.Action.View, message);

            try
            {
                NybSys.Models.DTO.Controllers controller = message.GetRequestObject<NybSys.Models.DTO.Controllers>();

                if (controller.Id == 0)
                {
                    controller.CreatedBy = message.UserName;
                    controller.CreatedDate = DateTime.Now;
                    await _accessRightBLL.CreateController(controller);
                }
                {
                    controller.ModifiedBy = message.UserName;
                    controller.ModifiedDate = DateTime.Now;
                    await _accessRightBLL.UpdateController(controller);
                }

                return Build.SuccessMessage(controller);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "SaveUpdateController", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> UpdateUserAsync(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(UpdateUserAsync), "Update user data", Common.Enums.Action.Update, message);

            try
            {
                Users user = message.GetRequestObject<Users>();
                user.ModifiedBy = message.UserName;
                user.ModifiedDate = DateTime.Now;

                await _userBLL.UpdateUserAsync(user);
                await KillAllSessionAsync(user.Username);

                return Build.SuccessMessage(user);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "UpdateUserAsync", 0, "SecurityManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<bool> VerifyAccess(string roleName, string controllerName, string actionName)
        {
            try
            {
                return await _accessRightBLL.VerifyAccessControlByRoleName(roleName, controllerName, actionName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> VerifyTokenAsync(Guid tokenIdentifier)
        {
            try
            {
                bool isVerify = await _redisSessionBLL.VerifiedSession(tokenIdentifier);

                if(isVerify)
                {
                    return isVerify;
                }
                else
                {
                    await _sessionLogBLL.LogoutAsync(tokenIdentifier);
                    return isVerify;
                }

            }catch(Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
