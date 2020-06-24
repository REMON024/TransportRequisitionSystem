using Microsoft.Extensions.Caching.Distributed;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Extension;
using NybSys.Common.Utility;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;
using NybSys.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Auth.BLL
{
    public class AccessRightBLL : IAccessRightBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        public AccessRightBLL(
            IUnitOfWork unitOfWork,
            IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }

        public IRepository<Controllers> ControllerRepository
        {
            get
            {
                return _unitOfWork.Repository<Controllers>();
            }
        }

        public IRepository<Actions> ActionRepository
        {
            get
            {
                return _unitOfWork.Repository<Actions>();
            }
        }

        public IRepository<AccessRight> AccessRightRepository
        {
            get
            {
                return _unitOfWork.Repository<AccessRight>();
            }
        }

        public virtual async Task<Actions> CreateAction(Actions action)
        {
            await ActionRepository.InsertAsync(action);
            await _unitOfWork.SaveAsync();
            return action;
        }

        public virtual async Task<Controllers> CreateController(Controllers controller)
        {
            await ControllerRepository.InsertAsync(controller);
            await _unitOfWork.SaveAsync();
            return controller;
        }

        public virtual async Task<List<Actions>> GetActionByController(string controllerName)
        {
            List<Actions> lstAction = (await ActionRepository.GetAsync(predicate: a => a.Controller.ControllerName.EqualsWithLower(controllerName)
                                                                    && a.Status.Equals((int)Common.Enums.Enums.Status.Active))).ToList();

            if(lstAction.Count > 0)
            {
                return lstAction;
            }
            throw new NotFoundException(Common.Constants.ErrorMessages.NO_ACTION_FOUND);
        }

        public virtual async Task<List<Actions>> GetActionList(Expression<Func<Actions, bool>> predicate)
        {
            List<Actions> lstAction =  (await ActionRepository.GetAsync(predicate: predicate)).ToList();

            if (lstAction.Count > 0)
            {
                return lstAction;
            }
            throw new NotFoundException(Common.Constants.ErrorMessages.NO_ACTION_FOUND);
        }

        public virtual async Task<List<VmController>> GetAllAccessRight(Expression<Func<Controllers, bool>> predicate)
        {
            var lstAccessRight = (await ControllerRepository.GetAsync<VmController>(predicate: predicate,
                                                                                   selector: c => new VmController()
                                                                                   {
                                                                                       ControllerName = c.ControllerName,
                                                                                       Title = c.Title,
                                                                                       Actions = c.Actions.Select(a => new VMAction()
                                                                                       {
                                                                                           ActionName = a.ActionName,
                                                                                           Title = a.Title
                                                                                       }).ToList()
                                                                                   })).ToList();
            if(lstAccessRight.Count > 0)
            {
                return lstAccessRight;
            }
            throw new NotFoundException(Common.Constants.ErrorMessages.NO_ACCESS_RIGHT_FOUND);
        }

        public virtual async Task<Controllers> GetControllerByName(string controllerName)
        {
            Controllers controller = await ControllerRepository.GetFirstOrDefaultAsync(predicate: c => c.ControllerName.EqualsWithLower(controllerName));

            if(controller == null)
            {
                throw new NotFoundException(Common.Constants.ErrorMessages.NO_CONTROLLER_FOUND);
            }
            return controller;
        }

        public virtual async Task<List<Controllers>> GetControllers(Expression<Func<Controllers, bool>> predicate)
        {
            List<Controllers> lstController = (await ControllerRepository.GetAsync(predicate: predicate)).ToList();

            if(lstController.Count > 0)
            {
                return lstController;
            }
            throw new NotFoundException(Common.Constants.ErrorMessages.NO_CONTROLLER_FOUND);
        }

        private async Task SaveAccessControll(VMAccessRights accessRights)
        {
            var accessRightStr = JSONConvert.ConvertString(accessRights.RightLists);
            await _distributedCache.SetStringAsync(accessRights.RoleName, accessRightStr);
        }

        private async Task UpdateAccessControll(VMAccessRights accessRights)
        {
            
            if(await VerifyAccessRight(accessRights.RoleName))
            {
                await SaveAccessControll(accessRights);
            } else
            {
                throw new BadRequestException(Common.Constants.ErrorMessages.NO_ACCESS_RIGHT_FOUND);
            }
        }

        private async Task<bool> VerifyAccessRight(string roleName)
        {
            var result = await _distributedCache.GetStringAsync(roleName);
            return result != null ? true : false;
        }

        public virtual async Task<Actions> UpdateAction(Actions action)
        {
            List<object> avoidProperty = new List<object> { action.CreatedBy, action.CreatedDate };

            ActionRepository.Update(action, avoidProperty.ToArray());
            await _unitOfWork.SaveAsync();
            return action;
        }

        public virtual async Task<Controllers> UpdateController(Controllers controller)
        {
            List<object> avoidProperty = new List<object> { controller.CreatedBy, controller.CreatedDate };

            ControllerRepository.Update(controller, avoidProperty.ToArray());
            await _unitOfWork.SaveAsync();

            return controller;
        }

        public virtual async Task<bool> VerifyAccessControlByRoleName(string roleName, string controllerName, string actionName)
        {
            List<VmController> lstController = await GetAccessListByRoleName(roleName);

            bool verifyResult = lstController.Any(p => p.ControllerName.EqualsWithLower(controllerName)
                                                    && p.Actions.Any(a => a.ActionName.EqualsWithLower(actionName)));

            return verifyResult;
        }

        public virtual async Task<List<VmController>> GetAccessListByRoleName(string roleName)
        {
            if(await VerifyAccessRight(roleName))
            {
                var accessRightStr = await _distributedCache.GetStringAsync(roleName);
                List<VmController> lstController = JSONConvert.Convert<List<VmController>>(accessRightStr);
                return lstController;
            }

            throw new NotFoundException(Common.Constants.ErrorMessages.NO_ACCESS_RIGHT_FOUND);
        }

        private async Task<AccessRight> SaveAccessRight(AccessRight accessRight)
        {
            bool result = await VerifyAccessRight(a => a.AccessRightName.EqualsWithLower(accessRight.AccessRightName));

            if (!result)
            {
                await AccessRightRepository.InsertAsync(accessRight);
                await _unitOfWork.SaveAsync();
                return accessRight;
            }

            throw new BadRequestException(Common.Constants.ErrorMessages.ACCESS_RIGHT_ALREADY_EXIST);
        }

        private async Task<AccessRight> UpdateAccessRight(AccessRight accessRight)
        {
            var avoidProperties = new List<object>()
            {
                accessRight.CreatedBy,
                accessRight.CreatedDate
            };

            //AccessRightRepository.Update(accessRight, avoidProperties.ToArray());
            await _unitOfWork.SaveAsync();
            return accessRight;
        }

        public virtual async Task<IEnumerable<VmAccessListDropDwon>> GetAccessRightForDropDown()
        {
            return await AccessRightRepository.GetAsync(predicate: a => a.Status.Equals((int)Common.Enums.Enums.Status.Active),
                                                                              selector: a => new VmAccessListDropDwon()
                                                                              {
                                                                                  AccessRightId = a.Id,
                                                                                  AccessRightName = a.AccessRightName
                                                                              });
        }

        private async Task<bool> VerifyAccessRight(Expression<Func<AccessRight, bool>> predicate)
        {
            return await AccessRightRepository.IsExistAsync(predicate);
        }

        public virtual async Task<IPagedList<VmRole>> GetRoleList(VmRoleFilter filter)
        {
            IPagedList<VmRole> lstRole = await AccessRightRepository.GetPagedListAsync<VmRole>(predicate: p => p.Status == (int)Common.Enums.Enums.Status.Active,
                                                                                   orderBy: r => r.OrderByDescending(p => p.CreatedDate),
                                                                                   pageIndex: filter.PageIndex + 1,
                                                                                   pageSize: filter.PageSize,
                                                                                   selector: r => new VmRole()
                                                                                   {
                                                                                       Id = r.Id,
                                                                                       RoleName = r.AccessRightName
                                                                                   });

            if(lstRole.TotalCount > 0)
            {
                return lstRole;
            }
            else
            {
                throw new NotFoundException(Common.Constants.ErrorMessages.NO_ACCESS_RIGHT_FOUND);
            }
        }

        public virtual async Task<AccessRight> GetAccessRight(Expression<Func<AccessRight, bool>> predicate)
        {
            AccessRight accessRight = await AccessRightRepository.GetFirstOrDefaultAsync(predicate: predicate);

            return accessRight;
        }
       

        public virtual async Task<string> SaveOrUpdateAccessRight(VMAccessRights accessRights)
        {
            AccessRight accessRight = await GetAccessRight(p => p.AccessRightName.EqualsWithLower(accessRights.RoleName));
            if (accessRights.Id == 0 && accessRight==null)
            {
                

                if (accessRight == null)
                {
                    accessRight = new AccessRight()
                    {
                        AccessRightName = accessRights.RoleName,
                        CreatedBy = "Super Admin",
                        CreatedDate = DateTime.Now,
                        Status = 2,
                        AccessControlJson = accessRights.AccessControlJson
                        
                    };

                    await SaveAccessRight(accessRight);
                    await SaveAccessControll(accessRights);

                    return Common.Constants.SuccessMessages.SUCCESSFULLY_CREATE_ACCESS_CONTROLL;
                }
                else
                {
                    throw new BadRequestException(Common.Constants.ErrorMessages.ACCESS_RIGHT_ALREADY_EXIST);
                }
            }
            else
            {
                //AccessRight accessRight = await GetAccessRight(p => p.Id == accessRights.Id);

                if (accessRight != null)
                {
                    accessRight.AccessRightName = accessRights.RoleName;
                    accessRight.Status = accessRights.Status;
                    accessRight.ModifiedBy = accessRights.UserName;
                    accessRight.ModifiedDate = DateTime.Now;
                    accessRight.AccessControlJson = accessRights.AccessControlJson;

                   
                    var result =await UpdateAccessRight(accessRight);

                    await UpdateAccessControll(accessRights);

                    return Common.Constants.SuccessMessages.SUCCESSFULLY_UPDATE_ACCESS_CONTROLL;
                }
                else
                {
                    throw new BadRequestException(Common.Constants.ErrorMessages.NO_ACCESS_RIGHT_FOUND);
                }
            }
        }
    }
}
