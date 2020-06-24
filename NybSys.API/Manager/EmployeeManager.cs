using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Generic.BLL;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;

namespace NybSys.API.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private IOptions<Models.ViewModels.SmtpMailServer> _config;
        private readonly IGenericBLL _genericBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        public EmployeeManager(IGenericBLL genericBLL,IAuditLogBLL auditLogBLL, IOptions<Models.ViewModels.SmtpMailServer> config)
        {
            _genericBLL = genericBLL;
            _auditLogBLL = auditLogBLL;
            _config = config;
        }

        public virtual async Task<IActionResult> GetEmployeeByFilter(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetEmployeeByFilter), "Search employee", Common.Enums.Action.View, message);
            VMQueryObject vmQueryObject = message.GetRequestObject<VMQueryObject>();
            List<Employees> lstEmployee = new List<Employees>();
            try
            {
                lstEmployee = await _genericBLL.GetByFilterAsync<Employees>(p => p.EmployeeId > 0);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, vmQueryObject, "GetEmployeeByFilter", 0, "EmployeeManager");
                throw ex;
            }
               
            return Build.SuccessMessage(lstEmployee);
        }

        public virtual async Task<IActionResult> GetEmployeeByFilterUsingVM(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetEmployeeByFilter), "Search employee", Common.Enums.Action.View, message);
            VMQueryObject vmQueryObject = message.GetRequestObject<VMQueryObject>();
            List<VMEmployee> lstEmployee = new List<VMEmployee>();
            try
            {
                lstEmployee = await _genericBLL.GetByFilterAsync<Employees, VMEmployee>(predicate: p => p.EmployeeId > 0, selector: u => new VMEmployee {
                    EmployeeId = u.EmployeeId,
                    FirstName = u.FirstName,
                    Lastname = u.Lastname,
                    MiddleName = u.MiddleName,
                    DepartmentId = u.DepartmentId,
                    DesignationId = u.DesignationId,
                    Department = u.Department.DepartmentName,
                    Designation = u.Designation.DesignationName,
                    HOD = _config.Value.HOD
                    

                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Build.SuccessMessage(lstEmployee);
        }

        public virtual async Task<IActionResult> GetEmployeeByid(ApiCommonMessage message)
        {
            await _auditLogBLL.SaveLog(nameof(GetEmployeeByFilter), "Search employee", Common.Enums.Action.View, message);
            VMQueryObject vmQueryObject = message.GetRequestObject<VMQueryObject>();
            List<VMEmployee> lstEmployee = new List<VMEmployee>();
            try
            {
                lstEmployee = await _genericBLL.GetByFilterAsync<Employees, VMEmployee>(predicate: p => p.EmployeeId ==vmQueryObject.EmployeeID, selector: u => new VMEmployee
                {
                    EmployeeId = u.EmployeeId,
                    FirstName = u.FirstName,
                    Lastname = u.Lastname,
                    MiddleName = u.MiddleName,
                    DepartmentId = u.DepartmentId,
                    DesignationId = u.DesignationId,
                    Department = u.Department.DepartmentName,
                    Designation = u.Designation.DesignationName,
                    EmployeeName= u.FirstName+""+ u.Lastname,
                    HOD = _config.Value.HOD


                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Build.SuccessMessage(lstEmployee);
        }
    }
}
