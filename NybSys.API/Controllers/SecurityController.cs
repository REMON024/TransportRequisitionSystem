using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Helper;
using NybSys.API.Manager;
using NybSys.Models.ViewModels;

namespace NybSys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NybSysAuthorizeWithDB]
    [SessionCheck]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityManager _securityManager;
        private readonly IUserManager _userManager;
        private readonly IAccessManager _accessManager;

        public SecurityController
            (
                ISecurityManager securityManager,
                IUserManager userManager,
                IAccessManager accessManager
            )
        {
            _securityManager = securityManager;
            _userManager = userManager;
            _accessManager = accessManager;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] ApiCommonMessage message)
        {
            return await _userManager.CreateUserAsync(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] ApiCommonMessage message)
        {
            return await _userManager.UpdateUserAsync(message);
        }

        [HttpPost("get-user")]
        public async Task<IActionResult> GetUser([FromBody] ApiCommonMessage message)
        {
            return await _userManager.GetUser(message);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ApiCommonMessage message)
        {
            return await _userManager.ChangePassword(message);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ApiCommonMessage message)
        {
            return await _userManager.ResetPassword(message);
        }

        /// <summary>
        /// Login into system
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [NybSysAllowAnonymous]
        [SessionCheckByPass]
        public async Task<IActionResult> Login([FromBody] ApiCommonMessage message)
        {
            return await _securityManager.LoginAsync(message);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] ApiCommonMessage message)
        {
            return await _securityManager.LogoutAsync(message);
        }

        [HttpPost("get-session-by-filter")]
        public async Task<IActionResult> GetSessionLogByFilter([FromBody] ApiCommonMessage message)
        {
            return await _securityManager.GetSessionLogsByFilter(message);
        }

        [HttpPost("get-users-by-filter")]
        public async Task<IActionResult> GetUsersByFilter([FromBody] ApiCommonMessage message)
        {
            return await _userManager.GetUsersByFilter(message);
        }

        [HttpPost("cancel-session")]
        public async Task<IActionResult> CancelSession([FromBody] ApiCommonMessage message)
        {
            return await _securityManager.CancelSession(message);
        }

        [NybSysAllowAnonymous]
        [SessionCheckByPass]
        [HttpPost("save-update-access-control")]
        public async Task<IActionResult> SaveOrUpdateAccessControl([FromBody] ApiCommonMessage message)
        {
            return await _accessManager.SaveOrUpdateAccessControl(message);
        }

        [NybSysAllowAnonymous]
        [SessionCheckByPass]
        [HttpPost("get-all-access-control")]
        public async Task<IActionResult> GetAllAccessControl([FromBody] ApiCommonMessage message)
        {
            return await _accessManager.GetAllAccessList(message);
        }

        [HttpPost("get-access-control-by-role")]
        [NybSysAllowAnonymous]
        [SessionCheckByPass]
        public async Task<IActionResult> GetAccessControlByRole([FromBody] ApiCommonMessage message)
        {
            return await _accessManager.GetAccessControlByRole(message);
        }

        [HttpPost("get-all-access-role-name")]
        [NybSysAllowAnonymous]
        [SessionCheckByPass]
        public async Task<IActionResult> GetAllRoleName([FromBody] ApiCommonMessage message)
        {
           var result= await _accessManager.GetAllRole(message);
            return result;
        }

    }
}