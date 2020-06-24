using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NybSys.API.Helper;
using NybSys.AuditLog.BLL;
using NybSys.Common.Enums;
using NybSys.Generic.BLL;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;
using NybSys.TMS.BLL;

namespace NybSys.API.Manager
{
    public class RequisitionManager : IRequisitionManager
    {
        public string url = "http://172.16.16.91:83/#/dashboard/tms/requisitionapproval/";
        private IOptions<Models.ViewModels.SmtpMailServer> _config;
        private readonly IGenericBLL _genericBLL;
        private readonly IAuditLogBLL _auditLogBLL;
        private readonly IMailManager _mailManager;
        public RequisitionManager(IGenericBLL genericBLL, IAuditLogBLL auditLogBLL, IMailManager mailManager, IOptions<Models.ViewModels.SmtpMailServer> config)
        {
            _genericBLL = genericBLL;
            _auditLogBLL = auditLogBLL;
            _mailManager = mailManager;
            _config = config;
        }

        public virtual async Task<IActionResult> GetRequisitionByFilter(ApiCommonMessage message)
        {
            int pageCount = 0;
            IEnumerable<Requisitions> items = new List<Requisitions>();
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetRequisitionByFilter), "Get Requisition by filter", Common.Enums.Action.View, message);
                VMQueryObject vmQueryObject = message.GetRequestObject<VMQueryObject>();
                List<Requisitions> lstRequisition = new List<Requisitions>();
                if (vmQueryObject.EmployeeID > 0)
                {

                    lstRequisition = await _genericBLL.GetByFilterAsync<Requisitions, Requisitions>(predicate: p => p.EmployeeId == vmQueryObject.EmployeeID, selector: d => new Requisitions
                    {

                        VehicleTypeName = d.Vehicle.VehicleType.TypeName,
                        ApprovedBy = d.ApprovedBy,
                        RequisitionId = d.RequisitionId,
                        RequisitionDate = d.RequisitionDate,
                        EmployeeId = d.EmployeeId,
                        PalcetoVisit = d.PalcetoVisit,
                        PurposeofVisit = d.PurposeofVisit,
                        FromTime = d.FromTime,
                        ToTime = d.ToTime,
                        ProjectName = d.ProjectName,
                        VehicleTypeID = d.VehicleTypeID,
                        DriverID = d.DriverID,
                        IsSelfDrive = d.IsSelfDrive,
                        SelefDriverName = d.SelefDriverName,
                        NumberofPassenger = d.NumberofPassenger,
                        VehicleID = d.VehicleID,
                        ApprovedDate = d.ApprovedDate,
                        RequisitionStatus = d.RequisitionStatus,
                        CretedBy = d.CretedBy,
                        CreatedDate = d.CreatedDate,
                        Status = d.Status,
                        Note = d.Note,
                        VehicleName = d.Vehicle.Name


                    },
                    orderBy: x => x.OrderByDescending(p => p.RequisitionDate));
                }
                else
                {
                    vmQueryObject.FromDate = DateTime.Parse(vmQueryObject.FromDate.ToString("yyyy-MM-dd 00:00:00"));
                    vmQueryObject.ToDate = DateTime.Parse(vmQueryObject.ToDate.ToString("yyyy-MM-dd 23:59:59"));
                    lstRequisition = await _genericBLL.GetByFilterAsync<Requisitions, Requisitions>(p => p.RequisitionDate >= vmQueryObject.FromDate && p.RequisitionDate <= vmQueryObject.ToDate, selector: d => new Requisitions
                    {

                        VehicleTypeName = d.Vehicle.VehicleType.TypeName,
                        ApprovedBy = d.ApprovedBy,
                        RequisitionId = d.RequisitionId,
                        RequisitionDate = d.RequisitionDate,
                        EmployeeId = d.EmployeeId,
                        PalcetoVisit = d.PalcetoVisit,
                        PurposeofVisit = d.PurposeofVisit,
                        FromTime = d.FromTime,
                        ToTime = d.ToTime,
                        ProjectName = d.ProjectName,
                        VehicleTypeID = d.VehicleTypeID,
                        DriverID = d.DriverID,
                        IsSelfDrive = d.IsSelfDrive,
                        SelefDriverName = d.SelefDriverName,
                        NumberofPassenger = d.NumberofPassenger,
                        VehicleID = d.VehicleID,
                        ApprovedDate = d.ApprovedDate,
                        RequisitionStatus = d.RequisitionStatus,
                        CretedBy = d.CretedBy,
                        CreatedDate = d.CreatedDate,
                        Status = d.Status,
                        VehicleName = d.Vehicle.Name,
                        Note = d.Note


                    }, orderBy: x => x.OrderByDescending(p => p.RequisitionDate));
                }

                if (lstRequisition.Count > 0)
                {
                    items = lstRequisition.OrderByDescending(p => p.RequisitionDate).Skip((vmQueryObject.PageIndex - 1) * vmQueryObject.PageSize).Take(vmQueryObject.PageSize).ToList();
                    pageCount = lstRequisition.Count;
                    return Build.SuccessMessage(new { items, pageCount });
                }
                return Build.SuccessMessage(new { items, pageCount });
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetRequisitionByFilter", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> SaveRequisitionAsync(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(SaveRequisitionAsync), "Save requisition", Common.Enums.Action.Insert, message);
                string filter = string.Format("{0}='{1}'", "UserID", message.UserName);
                Users users = _genericBLL.GetByFilter<Users>(predicate: u => u.Username == message.UserName).FirstOrDefault();

                Requisitions requisitions = message.GetRequestObject<Requisitions>();
                var result = new Requisitions();
                if (requisitions.RequisitionId > 0)
                {
                    Requisitions req = _genericBLL.GetByFilter<Requisitions>(x => x.RequisitionId == requisitions.RequisitionId).FirstOrDefault();
                    if (req != null && req.RequisitionStatus == (int)Common.Enums.Enums.RequisitionStatus.Submitted)
                    {
                        result = _genericBLL.Update<Requisitions>(requisitions);
                        await _genericBLL.SaveChangesAsync();
                    }

                }
                else
                {
                    requisitions.RequisitionDate = DateTime.Now;
                    requisitions.EmployeeId = (users != null) ? users.EmployeeId ?? 0 : 0;
                    requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Submitted;
                    requisitions.Status = (int)Enums.Status.Active;
                    result = await _genericBLL.InsertAsync<Requisitions>(requisitions);
                    await _genericBLL.SaveChangesAsync();
                    url += requisitions.RequisitionId.ToString();



                    var empinfo = await _genericBLL.GetByFilterAsync<Employees, VMEmployee>(predicate: u => u.EmployeeId == result.EmployeeId, selector: p => new VMEmployee
                    {
                        EmployeeName = p.FirstName + "" + p.Lastname,
                        Department = p.Department.DepartmentName,
                        Designation = p.Designation.DesignationName,
                        EmployeeId = p.EmployeeId
                    });

                    VMForMailTemplete vMForMailTemplete = setMailValue(message);

                    vMForMailTemplete.EmployeeName = empinfo.Where(u => u.EmployeeId == result.EmployeeId).FirstOrDefault().EmployeeName;
                    vMForMailTemplete.Designation = empinfo.Where(u => u.EmployeeId == result.EmployeeId).FirstOrDefault().Designation;
                    vMForMailTemplete.Department = empinfo.Where(u => u.EmployeeId == result.EmployeeId).FirstOrDefault().Department;
                    vMForMailTemplete.VehicleName = _genericBLL.GetFirstOrDefault<Vehicles>(p => p.VehicleID == result.VehicleID).Name;

                    vMForMailTemplete.RequisitionDate = _genericBLL.GetFirstOrDefault<Requisitions>(u => u.RequisitionId == requisitions.RequisitionId).RequisitionDate;
                    string mailBody = string.Empty;
                    //string rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    //rootDir = rootDir + "\\EmailTemplate\\RequisitionMail.html";
                    string test = ".//EmailTemplate//RequisitionMail.html";
                   // rootDir = rootDir.Replace("file:\\", "");
                    using (StreamReader SourceReader = System.IO.File.OpenText(test))
                    {
                        mailBody = SourceReader.ReadToEnd();
                    }
                    mailBody = mailBody.Replace("dtRequestDate", vMForMailTemplete.RequisitionDate.ToString(Common.Constants.AppConstant.DISPLAY_DATE_FORMAT));
                    mailBody = mailBody.Replace("dtFromTime", vMForMailTemplete.FromTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtToTime", vMForMailTemplete.ToTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtVehicleNeed", vMForMailTemplete.VehicleName);
                    mailBody = mailBody.Replace("dtPlaceOfVisit", vMForMailTemplete.PalcetoVisit);
                    mailBody = mailBody.Replace("dtPurposeOfVisit", vMForMailTemplete.PurposeofVisit);
                    mailBody = mailBody.Replace("dtProjectName", vMForMailTemplete.ProjectName);
                    mailBody = mailBody.Replace("dtRequestor", vMForMailTemplete.EmployeeName);
                    mailBody = mailBody.Replace("dtDesignation", vMForMailTemplete.Designation);
                    mailBody = mailBody.Replace("dtDepartment", vMForMailTemplete.Department);
                    mailBody = mailBody.Replace("dtHod", _config.Value.HOD);
                    mailBody = mailBody.Replace("dturl", url);
                    Thread thread = new Thread(() => _mailManager.SendEmail("Requisition Registration Notification", mailBody));
                    thread.Start();

                    
                }

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "SaveRequisitionAsync", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> ApproveRequisition(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(ApproveRequisition), "Approve requisition", Common.Enums.Action.Update, message);
                Requisitions requisitions = message.GetRequestObject<Requisitions>();
                Requisitions req = _genericBLL.GetByFilter<Requisitions>(x => x.RequisitionId == requisitions.RequisitionId).FirstOrDefault();
                if (req != null && req.RequisitionStatus == (int)Common.Enums.Enums.RequisitionStatus.Submitted)
                {
                    requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Approved;
                    requisitions.ApprovedBy = message.UserName.ToString();
                    requisitions.ApprovedDate = DateTime.Now;
                    var result = _genericBLL.Update(requisitions);
                    await _genericBLL.SaveChangesAsync();




                    VMForMailTemplete vMForMailTemplete = setMailValue(message);
                    var empid = _genericBLL.GetFirstOrDefault<DriverInfos>(u => u.DriverInfoId == result.DriverID);
                    
                    
                    vMForMailTemplete.DriverName = _genericBLL.GetFirstOrDefault<Employees>(u => u.EmployeeId == empid.EmployeeId).FirstName + "" + _genericBLL.GetFirstOrDefault<Employees>(u => u.EmployeeId == empid.EmployeeId).Lastname;

                    string EmployeeEmail = _genericBLL.GetFirstOrDefault<Employees>(u => u.EmployeeId == result.EmployeeId).Email;

                    string DriverEmail = _genericBLL.GetFirstOrDefault<Employees>(u => u.EmployeeId == empid.EmployeeId).Email;
                    string mailBody = string.Empty;
                    //string rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    //rootDir = rootDir + "\\EmailTemplate\\RequisitionApproveMail.html";
                    //rootDir = rootDir.Replace("file:\\", "");
                    string test = ".//EmailTemplate//RequisitionApproveMail.html";
                    using (StreamReader SourceReader = System.IO.File.OpenText(test))
                    {
                        mailBody = SourceReader.ReadToEnd();
                    }
                    mailBody = mailBody.Replace("dtRequestDate", vMForMailTemplete.RequisitionDate.ToString(Common.Constants.AppConstant.DISPLAY_DATE_FORMAT));
                    mailBody = mailBody.Replace("dtFromTime", vMForMailTemplete.FromTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtToTime", vMForMailTemplete.ToTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtVehicleNeed", vMForMailTemplete.VehicleName);
                    mailBody = mailBody.Replace("dtPlaceOfVisit", vMForMailTemplete.PalcetoVisit);
                    mailBody = mailBody.Replace("dtPurposeOfVisit", vMForMailTemplete.PurposeofVisit);
                    mailBody = mailBody.Replace("dtProjectName", vMForMailTemplete.ProjectName);
                    mailBody = mailBody.Replace("dtRequestor", vMForMailTemplete.EmployeeName);
                    mailBody = mailBody.Replace("dtDesignation", vMForMailTemplete.Designation);
                    mailBody = mailBody.Replace("dtDepartment", vMForMailTemplete.Department);
                    mailBody = mailBody.Replace("dtDriver", vMForMailTemplete.DriverName);

                    mailBody = mailBody.Replace("dtHod", _config.Value.HOD);
                    mailBody = mailBody.Replace("dNote", vMForMailTemplete.ApproveOrRejectNote);
                    Thread thread = new Thread(() => _mailManager.SendEmail("Requisition Approval Notification", mailBody, "salimullah.iu@gmail.com" + "," + "remon024@gmail.com"+","+ DriverEmail+","+ EmployeeEmail));
                    thread.Start();
                    return Build.SuccessMessage(result);
                }
                return Build.ExceptionMessage(null);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "ApproveRequisition", 0, "RequisitionManager");

                return Build.ExceptionMessage(ex);
            }
        }




        public virtual async Task<IActionResult> RejectRequisition(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(RejectRequisition), "Reject requisition", Common.Enums.Action.Update, message);
                Requisitions requisitions = message.GetRequestObject<Requisitions>();
                Requisitions req = _genericBLL.GetByFilter<Requisitions>(x => x.RequisitionId == requisitions.RequisitionId).FirstOrDefault();
                if (req != null && req.RequisitionStatus == (int)Common.Enums.Enums.RequisitionStatus.Submitted)
                {
                    requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Rejected;
                    var result = _genericBLL.Update(requisitions);
                    await _genericBLL.SaveChangesAsync();

                    VMForMailTemplete vMForMailTemplete = setMailValue(message);
                    

                    

                    string EmployeeEmail = _genericBLL.GetFirstOrDefault<Employees>(u => u.EmployeeId == result.EmployeeId).Email;

                    
                    string mailBody = string.Empty;
                    //string rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    //rootDir = rootDir + "\\EmailTemplate\\RequisitionRejectMail.html";
                    //rootDir = rootDir.Replace("file:\\", "");
                    string test = ".//EmailTemplate//RequisitionRejectMail.html";
                    using (StreamReader SourceReader = System.IO.File.OpenText(test))
                    {
                        mailBody = SourceReader.ReadToEnd();
                    }
                    mailBody = mailBody.Replace("dtRequestDate", vMForMailTemplete.RequisitionDate.ToString(Common.Constants.AppConstant.DISPLAY_DATE_FORMAT));
                    mailBody = mailBody.Replace("dtFromTime", vMForMailTemplete.FromTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtToTime", vMForMailTemplete.ToTime.ToString("yyyy-MM-dd hh:mm"));
                    mailBody = mailBody.Replace("dtVehicleNeed", vMForMailTemplete.VehicleName);
                    mailBody = mailBody.Replace("dtPlaceOfVisit", vMForMailTemplete.PalcetoVisit);
                    mailBody = mailBody.Replace("dtPurposeOfVisit", vMForMailTemplete.PurposeofVisit);
                    mailBody = mailBody.Replace("dtProjectName", vMForMailTemplete.ProjectName);
                    mailBody = mailBody.Replace("dtRequestor", vMForMailTemplete.EmployeeName);
                    mailBody = mailBody.Replace("dtDesignation", vMForMailTemplete.Designation);
                    mailBody = mailBody.Replace("dtDepartment", vMForMailTemplete.Department);
                    mailBody = mailBody.Replace("dtHod", _config.Value.HOD);
                    mailBody = mailBody.Replace("dNote", vMForMailTemplete.DriverName);
                    Thread thread = new Thread(() => _mailManager.SendEmail("Requisition Reject Notification", mailBody,  EmployeeEmail));
                    thread.Start();
                    
                    return Build.SuccessMessage(result);
                }
                return Build.ExceptionMessage(null);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "RejectRequisition", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetRequisitionByID(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetRequisitionByID), "Get requisition by id", Common.Enums.Action.View, message);
                Requisitions requisitions = message.GetRequestObject<Requisitions>();
                requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Approved;
                var result = await _genericBLL.GetByFilterAsync<Requisitions>(predicate: p => p.RequisitionId == requisitions.RequisitionId);

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetRequisitionByID", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> CheckDriverAvailability(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(CheckDriverAvailability), "Check available driver", Common.Enums.Action.View, message);
                Requisitions requisitions = message.GetRequestObject<Requisitions>();

                var result = await _genericBLL.GetByFilterAsync<Requisitions>(predicate:
                p => ((p.FromTime <= requisitions.FromTime && p.ToTime >= requisitions.FromTime) ||
                    (p.FromTime <= requisitions.ToTime && p.ToTime >= requisitions.ToTime) ||
                     (p.FromTime >= requisitions.FromTime && p.ToTime <= requisitions.ToTime) ||
                     (p.FromTime <= requisitions.FromTime && p.ToTime >= requisitions.ToTime)) &&
           (p.RequisitionStatus == (int)Enums.RequisitionStatus.Approved && p.DriverID == requisitions.DriverID));



                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "CheckDriverAvailability", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> CheckVehicleAvailability(ApiCommonMessage message)
        {
            Requisitions requisitions = new Requisitions();
            try
            {
                await _auditLogBLL.SaveLog(nameof(CheckVehicleAvailability), "Check available vehicle", Common.Enums.Action.View, message);

                requisitions = message.GetRequestObject<Requisitions>();
                var result = await _genericBLL.GetByFilterAsync<Requisitions>(predicate:
                p => ((p.FromTime <= requisitions.FromTime && p.ToTime >= requisitions.FromTime) ||
                    (p.FromTime <= requisitions.ToTime && p.ToTime >= requisitions.ToTime) ||
                     (p.FromTime >= requisitions.FromTime && p.ToTime <= requisitions.ToTime) ||
                     (p.FromTime <= requisitions.FromTime && p.ToTime >= requisitions.ToTime)) &&
            (p.RequisitionStatus == (int)Enums.RequisitionStatus.Approved && p.VehicleID == requisitions.VehicleID));


                return Build.SuccessMessage(result);

            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "CheckVehicleAvailability", 0, "RequisitionManager");
                return Build.ExceptionMessage(ex);
            }
        }



        public VMForMailTemplete setMailValue(ApiCommonMessage message)
        {
            VMForMailTemplete vMForMailTemplete = message.GetRequestObject<VMForMailTemplete>();


            return vMForMailTemplete;
        }

        public virtual async Task<IActionResult> GetDriverScheduale(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(GetDriverScheduale), "Get Driver Scheduale", Common.Enums.Action.View, message);
                VmRequsationFilter filter = message.GetRequestObject<VmRequsationFilter>();


                List<Requisitions> lstrequisition = await _genericBLL.GetByFilterAsync<Requisitions,Requisitions>(p => p.DriverID == filter.DriverID && (filter.FromDate.Date <= p.FromTime.Date && filter.ToDate.Date >= p.ToTime.Date && filter.FromDate.Date <= p.ToTime.Date && filter.ToDate.Date >= p.FromTime.Date) && (p.RequisitionStatus==2),selector: x=> new Requisitions {

                    FromTime=x.FromTime,
                    ToTime=x.ToTime,
                    DriverID=x.DriverID,
                    VehicleID=x.VehicleID,
                    VehicleName=x.Vehicle.Name,
                    DriverName=x.driverInfos.Employee.FirstName+""+ x.driverInfos.Employee.Lastname


                });

                List<Requisitions> lstrequisitionNew = new List<Requisitions>();

                foreach (var item in lstrequisition)
                {
                    var _FromTime = item.FromTime;
                    var _ToTime = item.ToTime;

                    // both date are not equal
                    if (_FromTime.Date != _ToTime.Date)
                    {
                        // looping until from date smaller than to date
                        while (_FromTime.Date < _ToTime.Date)
                        {
                            // add fromdate 1 day
                            var FromTime = _FromTime.AddDays(1);

                            //  increased 1 day from is equal to todate
                            if (FromTime.Date == _ToTime.Date)
                            {
                                lstrequisitionNew.Add(new Requisitions()
                                {
                                    FromTime = _FromTime.AddDays(1).Date,
                                    ToTime = item.ToTime
                                });
                                item.ToTime = _FromTime.Date
                                                    .AddHours(23)
                                                    .AddMinutes(59);

                            }
                            else
                            {
                                lstrequisitionNew.Add(new Requisitions()
                                {
                                    FromTime = _FromTime.Date.AddDays(1),
                                    ToTime = _FromTime.Date.AddDays(1)
                                                            .AddHours(23)
                                                            .AddMinutes(59)
                                });
                            }

                            _FromTime = _FromTime.AddDays(1);
                        }
                    }
                }

                lstrequisition.AddRange(lstrequisitionNew);

                var result = lstrequisition.GroupBy(x => x.FromTime.Date,
                    (key, x) => new GanttChartVM
                    {
                        Date = key,
                        lstSchedule = x.Select(y =>
                        new RequsationChartView
                        {
                            DriverID = y.DriverID,
                            VehicleID = y.VehicleID,
                            FromTime = y.FromTime,
                            ToTime = y.ToTime,
                            DriverName=y.DriverName,
                            VehicleName=y.VehicleName
                        }).ToList()
                    });

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetDriverScheduale", 0, "Get Driver Scheduale");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetVehicleScheduale(ApiCommonMessage message)
        {

            try
            {
                await _auditLogBLL.SaveLog(nameof(GetVehicleScheduale), "Get Vehicle Scheduale", Common.Enums.Action.View, message);
                VmRequsationFilter filter = message.GetRequestObject<VmRequsationFilter>();

               
                List<Requisitions> lstrequisition = await _genericBLL.GetByFilterAsync<Requisitions,Requisitions>(p => p.VehicleID == filter.VehicleID && (filter.FromDate.Date<=p.FromTime.Date && filter.ToDate.Date>=p.ToTime.Date && filter.FromDate.Date<=p.ToTime.Date && filter.ToDate.Date>=p.FromTime.Date) && p.RequisitionStatus==2, selector: x => new Requisitions
                {

                    FromTime = x.FromTime,
                    ToTime = x.ToTime,
                    DriverID = x.DriverID,
                    VehicleID = x.VehicleID,
                    VehicleName = x.Vehicle.Name,
                    DriverName = x.driverInfos.Employee.FirstName + "" + x.driverInfos.Employee.Lastname


                });

                List<Requisitions> lstrequisitionNew = new List<Requisitions>();

                foreach (var item in lstrequisition)
                {
                    var _FromTime = item.FromTime;
                    var _ToTime = item.ToTime;

                    // both date are not equal
                    if (_FromTime.Date != _ToTime.Date)
                    {
                        // looping until from date smaller than to date
                        while (_FromTime.Date < _ToTime.Date)
                        {
                            // add fromdate 1 day
                            var FromTime = _FromTime.AddDays(1);

                            //  increased 1 day from is equal to todate
                            if (FromTime.Date == _ToTime.Date)
                            {
                                lstrequisitionNew.Add(new Requisitions()
                                {
                                    FromTime = _FromTime.AddDays(1).Date,
                                    ToTime = item.ToTime
                                });
                                item.ToTime = _FromTime.Date
                                                    .AddHours(23)
                                                    .AddMinutes(59);

                            } else
                            {
                                lstrequisitionNew.Add(new Requisitions()
                                {
                                    FromTime = _FromTime.Date.AddDays(1),
                                    ToTime = _FromTime.Date.AddDays(1)
                                                            .AddHours(23)
                                                            .AddMinutes(59)
                                });
                            }

                            _FromTime = _FromTime.AddDays(1);
                        }
                    }
                }

                lstrequisition.AddRange(lstrequisitionNew);

                var result = lstrequisition.GroupBy(x => x.FromTime.Date,
                    (key, x) => new GanttChartVM { Date = key,
                        lstSchedule = x.Select(y =>
                        new RequsationChartView { DriverID = y.DriverID,
                                                 VehicleID = y.VehicleID,
                                                FromTime = y.FromTime,
                                                ToTime = y.ToTime,
                                                DriverName = y.DriverName,
                                                VehicleName = y.VehicleName
                        }).ToList()});

                return Build.SuccessMessage(result);
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetVehicleScheduale", 0, "Get Vehicle Scheduale");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> CancelRequisition(ApiCommonMessage message)
        {
            try
            {
                await _auditLogBLL.SaveLog(nameof(CancelRequisition), "Cancel requisition", Common.Enums.Action.Update, message);
                Requisitions requisitions = message.GetRequestObject<Requisitions>();
                Requisitions req = _genericBLL.GetByFilter<Requisitions>(x => x.RequisitionId == requisitions.RequisitionId).FirstOrDefault();
                if (req != null && req.RequisitionStatus == (int)Common.Enums.Enums.RequisitionStatus.Submitted)
                {
                    requisitions.RequisitionStatus = (int)Enums.RequisitionStatus.Cancel;
                    var result = _genericBLL.Update(requisitions);
                    await _genericBLL.SaveChangesAsync();
                    return Build.SuccessMessage(result);
                }
                else
                {
                    return Build.ExceptionMessage(null);

                }
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "CancelRequisition", 0, "Cancel Requisition");
                return Build.ExceptionMessage(ex);
            }
        }

        public virtual async Task<IActionResult> GetRequisitionByEmployee(ApiCommonMessage message)
        {

            try
            {
                await _auditLogBLL.SaveLog(nameof(GetRequisitionByEmployee), "Get Requisition By Employee", Common.Enums.Action.View, message);
                var EmployeeId = await _genericBLL.GetFirstOrDefaultAsync<Users>(p => p.Username == message.UserName);

                List<Requisitions> lstreq = new List<Requisitions>();

                lstreq = await _genericBLL.GetByFilterAsync<Requisitions>(p => p.EmployeeId == EmployeeId.EmployeeId);


                if (lstreq.Count > 0)
                {
                    return Build.SuccessMessage(lstreq);
                }

                else
                {
                    return Build.SuccessMessage("You have no Requisitions");
                }
            }
            catch (Exception ex)
            {
                await _auditLogBLL.ExceptionLogEntry(message, ex.ToString(), ex.Message, message, "GetRequisitionByEmployee", 0, "Get Requisition By Employee");

                throw ex;
            }




            throw new NotImplementedException();
        }

      

       
    }



   
}
