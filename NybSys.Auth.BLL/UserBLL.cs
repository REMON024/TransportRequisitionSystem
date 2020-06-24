using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Extension;
using NybSys.Common.Utility;
using NybSys.Models.DTO;
using NybSys.Repository;
using NybSys.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NybSys.Models.ViewModels;

namespace NybSys.Auth.BLL {
    public class UserBLL : IUserBLL {

        private readonly IUnitOfWork _unitOfWork;

        public UserBLL (IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public virtual async Task AddUserAccessRightAsync(Users user, string role)
        {
            AccessRight accessRight = await GetAccessRightRepository().GetFirstOrDefaultAsync(predicate: p => p.AccessRightName.ToLower().Equals(role.ToLower()));

            if(accessRight != null)
            {
                user.AccessRightId = accessRight.Id;
                GetUserRepository().Update(user);
                await _unitOfWork.SaveAsync();
            }else
            {
                throw new NotFoundException($"{role} is not exist");
            }
        }

        public virtual async Task ChangePassword(Users user, string oldPassword, string newPassword)
        {
            EncryptionService encryptionService = new EncryptionService();

            string existingPassword = encryptionService.Decrypt(user.Password);

            if(oldPassword.Equals(existingPassword))
            {
                user.Password = encryptionService.Encrypt(newPassword);

                GetUserRepository().Update(user);

                await _unitOfWork.SaveAsync();
            } else {
                throw new BadRequestException(Common.Constants.ErrorMessages.OLDPASSWORD_NOT_MATCH);
            }
            
        }

        public virtual async Task<bool> CheckPasswordAsync(Users user, string password)
        {
            EncryptionService encryptionService = new EncryptionService();

            string existingPassword = encryptionService.Decrypt(user.Password);

            if(password.Equals(existingPassword))
            {
                return await Task.FromResult<bool>(true);
            } else {
                return await Task.FromResult<bool>(false);
            }
        }

        public virtual async Task CreateUserAsync(Users user)
        {
            if(await VerifyUser(user))
            {
                EncryptionService encryptionService = new EncryptionService();

                user.Password = encryptionService.Encrypt(user.Password);

                await GetUserRepository().InsertAsync(user);
                await _unitOfWork.SaveAsync();
            }
        }

        public virtual async Task DeleteUserAsync(Users user)
        {
            GetUserRepository().Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<Users> FindByNameAsync(string userName)
        {
            Users user =  await GetUserRepository().GetFirstOrDefaultAsync(predicate: u => u.Username.ToLower().Equals(userName.ToLower()), include: u => u.Include(a => a.AccessRight));

            if (user != null)
            {
                return user;
            }

            throw new BadRequestException(Common.Constants.ErrorMessages.USER_NOT_EXIST);
        }

        public virtual async Task<Users> FindUser(Expression<Func<Users, bool>> predicate)
        {
            return await GetUserRepository().GetFirstOrDefaultAsync(predicate: predicate);
        }

        public Task<string> GeneratePasswordResetTokenAsync(Users user)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AccessRight> GetUserAccessRight(Users user)
        {
            return await GetAccessRightRepository().GetFirstOrDefaultAsync(predicate: a => a.Id.Equals(user.AccessRightId));
        }

        public virtual async Task<Users> GetUserByEmail(string email)
        {
            return await GetUserRepository().GetFirstOrDefaultAsync(predicate: u => u.Email.EqualsWithLower(email));
        }

        public virtual async Task<Users> GetUserByPhoneNumber(string phoneNumber)
        {


            return await GetUserRepository().GetFirstOrDefaultAsync(predicate: u => u.PhoneNumber.Equals(phoneNumber));
        }

        public virtual async Task<string> GetUserEmailAsync(string userName)
        {
            Users user = await FindByNameAsync(userName);
            return user.Email;
        }

        public virtual async Task<string> GetUserPhoneNumber(string userName)
        {
            Users user = await FindByNameAsync(userName);

            if(user != null) {
                return user.PhoneNumber;
            }

            throw new BadRequestException(Common.Constants.ErrorMessages.USER_NOT_EXIST);
        }

        public virtual async Task<IEnumerable<Users>> GetUsers(Expression<Func<Users, bool>> predicate)
        {
            return await GetUserRepository().GetAsync(predicate : predicate);
        }

        public virtual async Task ResetPasswordAsync(string userName, string password)
        {
            Users user = await FindByNameAsync(userName);

            EncryptionService encryptionService = new EncryptionService();
            user.Password = encryptionService.Encrypt(password);

            GetUserRepository().Update(user);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task SetUserEmail(Users user, string email)
        {
            user.Email = email;
            GetUserRepository().Update(user);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task SetUserPhoneNumber(Users user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            GetUserRepository().Update(user);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task UpdatePassword(Users user, string password)
        {
            EncryptionService encryptionService = new EncryptionService();
            user.Password = encryptionService.Encrypt(password);
            GetUserRepository().Update(user);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task UpdateUserAsync(Users user)
        {
            GetUserRepository().Update(user, new { user.Password }, new { user.CreatedBy }, new { user.CreatedDate });
            await _unitOfWork.SaveAsync();
        }

        private IRepository<Users> GetUserRepository()
        {
            return _unitOfWork.Repository<Users>();
        }

        private IRepository<AccessRight> GetAccessRightRepository()
        {
            return _unitOfWork.Repository<AccessRight>();
        }

        public virtual async Task<bool> VerifyUser(Users user)
        {
            IEnumerable<Users> lstUser = await GetUsers(u => u.Username.EqualsWithLower(user.Username)
                                                        || u.Email.EqualsWithLower(user.Email)
                                                        || u.PhoneNumber.Equals(user.PhoneNumber));
            if(lstUser.Count() > 0)
            {
                if(lstUser.Any(u => u.Username.EqualsWithLower(user.Username)))
                {
                    throw new BadRequestException($"{user.Username} is already exist");
                }

                if(lstUser.Any(u => u.Email.EqualsWithLower(user.Email)))
                {
                    throw new BadRequestException($"{user.Email} is already exist");
                }

                if(lstUser.Any(u => u.PhoneNumber.Equals(user.PhoneNumber)))
                {
                    throw new BadRequestException($"{user.PhoneNumber} is already exist");
                }

                throw new Exception(Common.Constants.ErrorMessages.SERVER_PROBLEM);
            }else{
                return true;
            }
        }

        public async Task<IPagedList<VmUser>> GetUsersByFilter(VmUserFilter filter)
        {
            try
            {
                IPagedList<VmUser> lstUser = await GetUserRepository().GetPagedListAsync<VmUser>(
                                             predicate: u => (string.IsNullOrEmpty(filter.Username) ? true : u.Username.EqualsWithLower(filter.Username)) &&
                                                             (string.IsNullOrEmpty(filter.Name) ? true : u.Name.Contains(filter.Name)) &&
                                                             filter.Status.Equals(0) ? true : u.Status.Equals(filter.Status),
                                             pageIndex: (filter.PageIndex - 1), pageSize: filter.PageSize,
                                             orderBy: source => source.OrderByDescending(u => u.CreatedDate),
                                             selector: u => new VmUser()
                                             {
                                                 Username = u.Username,
                                                 Name = u.Name,
                                                 AccessRight  =u.AccessRight.AccessRightName,
                                                 Status = u.Status,
                                                 Email = u.Email,
                                                 MobileNo = u.PhoneNumber
                                             });
                if(lstUser.TotalCount > 0)
                {
                    return lstUser;
                }

                throw new NotFoundException(Common.Constants.ErrorMessages.NO_USER_FOUND);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}