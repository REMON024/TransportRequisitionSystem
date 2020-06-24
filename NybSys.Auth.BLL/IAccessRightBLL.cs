using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NybSys.Auth.BLL
{
    public interface IAccessRightBLL
    {
        Task<Actions> CreateAction(Actions action);
        Task<Actions> UpdateAction(Actions action);
        Task<List<Actions>> GetActionByController(string controllerName);
        Task<List<Actions>> GetActionList(Expression<Func<Actions, bool>> predicate);

        Task<Controllers> CreateController(Controllers controller);
        Task<Controllers> UpdateController(Controllers controller);
        Task<Controllers> GetControllerByName(string controllerName);
        Task<List<Controllers>> GetControllers(Expression<Func<Controllers, bool>> predicate);

        Task<List<VmController>> GetAllAccessRight(Expression<Func<Controllers, bool>> predicate);

        Task<bool> VerifyAccessControlByRoleName(string roleName, string controllerName, string actionName);

        Task<List<VmController>> GetAccessListByRoleName(string roleName);
        Task<string> SaveOrUpdateAccessRight(VMAccessRights accessRight);
        Task<AccessRight> GetAccessRight(Expression<Func<AccessRight, bool>> predicate);
        

        Task<IPagedList<VmRole>> GetRoleList(VmRoleFilter filter);
        Task<IEnumerable<VmAccessListDropDwon>> GetAccessRightForDropDown();

    }
}
