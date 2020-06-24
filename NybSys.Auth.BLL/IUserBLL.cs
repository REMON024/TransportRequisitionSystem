using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Auth.BLL
{
    public interface IUserBLL
    {
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(Users user);
        Task<Users> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(Users user, string password);
        Task UpdatePassword(Users user, string password);
        Task ChangePassword(Users user, string oldPassword, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(Users user);
        Task ResetPasswordAsync(string userName, string password);
        Task AddUserAccessRightAsync(Users user, string role);
        Task<AccessRight> GetUserAccessRight(Users user);
        Task<string> GetUserEmailAsync(string userName);
        Task<string> GetUserPhoneNumber(string userName);
        Task SetUserEmail(Users user, string email);
        Task SetUserPhoneNumber(Users user, string phoneNumber);
        Task<Users> GetUserByEmail(string email);
        Task<Users> GetUserByPhoneNumber(string phoneNumber);
        Task<Users> FindUser(Expression<Func<Users, bool>> predicate);
        Task<IEnumerable<Users>> GetUsers(Expression<Func<Users, bool>> predicate);
        Task<bool> VerifyUser(Users user);
        Task<IPagedList<VmUser>> GetUsersByFilter(VmUserFilter filter);
    }
}
