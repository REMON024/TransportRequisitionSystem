using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Generic.BLL;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;

namespace NybSys.API.Manager
{
    public class DriverManager : IDriverManager
    {
        private readonly IGenericBLL _genericBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        public DriverManager(IGenericBLL genericBLL,IAuditLogBLL auditLogBLL)
        {
            _genericBLL = genericBLL;
            _auditLogBLL = auditLogBLL;
        }

        public virtual async Task<IActionResult> GetAllDriver(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetAllDriver), "Get all driver", Common.Enums.Action.Other, message);
                List<DriverInfos> lstDriverInfo = new List<DriverInfos>();
                lstDriverInfo = await _genericBLL.GetByFilterAsync<DriverInfos,DriverInfos>(predicate: p => p.DriverInfoId > 0,selector: u=> new DriverInfos {
                    EmployeeName=u.Employee.FirstName+""+u.Employee.Lastname,
                    DriverInfoId=u.DriverInfoId,
                    DutyEnd=u.DutyEnd,
                    DutyStart=u.DutyStart,
                    DrivingLicenceNo=u.DrivingLicenceNo,
                    LicenceExpireDate=u.LicenceExpireDate,
                    OtherInfo=u.OtherInfo,
                    EmployeeId=u.EmployeeId



                },orderBy: x => x.OrderByDescending(p => p.DriverInfoId));
                return Build.SuccessMessage(lstDriverInfo);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetAllDriver", 0, "DriverManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetDriverByFilter(ApiCommonMessage message)
        {
            VMQueryObject vmQueryObject = new VMQueryObject();
            try
            {
                vmQueryObject = message.GetRequestObject<VMQueryObject>();
                await _auditLogBLL.SaveLog(nameof(GetDriverByFilter), "Search driver", Common.Enums.Action.View, message);
                List<DriverInfos> lstDriverInfo = new List<DriverInfos>();
                lstDriverInfo = await _genericBLL.GetByFilterAsync<DriverInfos>(p => p.DriverInfoId > 0);
                return Build.SuccessMessage(lstDriverInfo);
                
            }
            catch (Exception ex)
            {
               await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, vmQueryObject, "GetDriverByFilter", 0, "DriverManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetDriverInfoByID(ApiCommonMessage message)
        {
            DriverInfos driverInfos = new DriverInfos();
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetDriverInfoByID), "Search driver by ID", Common.Enums.Action.View, message);
                driverInfos = message.GetRequestObject<DriverInfos>();
                var result =await _genericBLL.GetByFilterAsync<DriverInfos,DriverInfos>(predicate: p => p.DriverInfoId == driverInfos.DriverInfoId,selector:u=> new DriverInfos {
                    DriverInfoId=u.DriverInfoId,
                    DrivingLicenceNo=u.DrivingLicenceNo,
                    DutyEnd=u.DutyEnd,
                    DutyStart=u.DutyStart,
                    LicenceExpireDate=u.LicenceExpireDate,
                    OtherInfo=u.OtherInfo,
                    EmployeeId=u.EmployeeId,
                    EmployeeName=u.Employee.FirstName+ ""+u.Employee.Lastname,
                    


                });

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, driverInfos, "GetDriverInfoByID", 0, "DriverManager");
                return Build.ExceptionMessage(ex);
            }


        }

        public virtual async Task<IActionResult> SaveDriverAsync(ApiCommonMessage message)
        {
            DriverInfos driverInfos = new DriverInfos();
            try
            {
                await _auditLogBLL.SaveLog(nameof(SaveDriverAsync), "Save driver", Common.Enums.Action.Insert, message);
                var result = new DriverInfos();
                driverInfos = message.GetRequestObject<DriverInfos>();
                if (driverInfos.DriverInfoId > 0)
                {
                    result = _genericBLL.Update<DriverInfos>(driverInfos);
                }
                else
                {
                    result = await _genericBLL.InsertAsync<DriverInfos>(driverInfos);
                }

                await _genericBLL.SaveChangesAsync();
                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, driverInfos, "GetDriverInfoByID", 0, "DriverManager");
                return Build.ExceptionMessage(ex);
            }
        }
    }
}
