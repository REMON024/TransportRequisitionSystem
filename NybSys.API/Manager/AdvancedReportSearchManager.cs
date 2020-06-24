using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using NybSys.Generic.BLL;
using NybSys.API.Helper;
using NybSys.Models.DTO;
using NybSys.TMS.BLL;

namespace NybSys.API.Manager
{
    public class AdvancedReportSearchManager : IAdvancedReportSearchManager
    {
        public DateTime DEfaultToTime;

        private readonly IGenericBLL _genericBLL;
        public AdvancedReportSearchManager(IGenericBLL genericBLL)
        {
            _genericBLL = genericBLL;
        }

        public virtual async Task<IActionResult> GetAdvancedReportByFilter(ApiCommonMessage message)
        {
            int pageCount = 0;
            try
            {
                VMAdvancedReport vMAdvancedReport = message.GetRequestObject<VMAdvancedReport>();
                //vMAdvancedReport.FromDate = DateTime.Parse(vMAdvancedReport.FromDate.ToString("yyyy-MM-dd 00:00:00"));
                //vMAdvancedReport.ToDate = DateTime.Parse(vMAdvancedReport.ToDate.ToString("yyyy-MM-dd 23:59:59"));
                if (vMAdvancedReport.ToDate == DEfaultToTime)
                {
                    vMAdvancedReport.ToDate = DateTime.Now;
                    vMAdvancedReport.ToDate = DateTime.Parse(vMAdvancedReport.ToDate.ToString("yyyy-MM-dd 23:59:59"));
                }

                List<VMAdvancedReportSearchResult> lstSearchResult = new List<VMAdvancedReportSearchResult>();

                if (vMAdvancedReport.EmployeeID > 0 && vMAdvancedReport.DriverID > 0 && vMAdvancedReport.VehicleID > 0 )
                {
                   
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.EmployeeId == vMAdvancedReport.EmployeeID && p.DriverID == vMAdvancedReport.DriverID && p.VehicleID == vMAdvancedReport.VehicleID
                    && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId=u.RequisitionId,


                    });
                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                         pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }
                    


                    
                    


                }

                else if(vMAdvancedReport.DriverID>0 && vMAdvancedReport.EmployeeID > 0)
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.EmployeeId == vMAdvancedReport.EmployeeID && p.DriverID == vMAdvancedReport.DriverID && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,

                    });

                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }


                }

                else if (vMAdvancedReport.VehicleID > 0 && vMAdvancedReport.EmployeeID > 0)
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.EmployeeId == vMAdvancedReport.EmployeeID && p.VehicleID == vMAdvancedReport.VehicleID && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,

                    });

                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }

                }

                else if (vMAdvancedReport.VehicleID > 0 && vMAdvancedReport.DriverID > 0)
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate:p=>p.DriverID == vMAdvancedReport.DriverID && p.VehicleID == vMAdvancedReport.VehicleID && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,

                    });

                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }


                }

                else if (vMAdvancedReport.EmployeeID > 0)
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.EmployeeId == vMAdvancedReport.EmployeeID && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,

                    });

                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }


                }

                else if (vMAdvancedReport.VehicleID > 0)
                {
                    lstSearchResult =  _genericBLL.GetByFilter<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.VehicleID == vMAdvancedReport.VehicleID
                    && p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleId = u.VehicleID,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,
                    });



                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }
                }

                else if (vMAdvancedReport.DriverID > 0)
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p =>p.DriverID == vMAdvancedReport.DriverID &&  p.Status == 1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName + "" + u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,


                    });

                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }

                }


                else
                {
                    lstSearchResult = await _genericBLL.GetByFilterAsync<Requisitions, VMAdvancedReportSearchResult>(predicate: p => p.Status==1 && p.RequisitionDate >= vMAdvancedReport.FromDate && p.RequisitionDate <= vMAdvancedReport.ToDate, selector: u => new VMAdvancedReportSearchResult
                    {
                        Amount = u.TravelDetails.FirstOrDefault() != null ? u.TravelDetails.FirstOrDefault().Amount : 0,
                        FromDate = u.FromTime,
                        ToDate = u.ToTime,
                        PlaceToVisit = u.PalcetoVisit,
                        PurposeOfVisit = u.PurposeofVisit,
                        RequisitionDate = u.RequisitionDate,
                        ProjectName = u.ProjectName,
                        status = u.RequisitionStatus,
                        VehicleName = u.Vehicle.Name,
                        EmployeeName = u.employees.FirstName + "" + u.employees.Lastname,
                        DriverName = u.driverInfos.Employee.FirstName+""+ u.driverInfos.Employee.Lastname,
                        RequisitionId = u.RequisitionId,


                    });
                    if (lstSearchResult.Count > 0)
                    {
                        IEnumerable<VMAdvancedReportSearchResult> items = lstSearchResult.OrderByDescending(p => p.RequisitionId).Skip((vMAdvancedReport.PageIndex - 1) * vMAdvancedReport.PageSize).Take(vMAdvancedReport.PageSize).ToList();
                        pageCount = lstSearchResult.Count;
                        return Build.SuccessMessage(new { items, pageCount });
                    }
                }

                return Build.SuccessMessage(lstSearchResult);


            }
            catch (Exception ex)
            {

                return Build.ExceptionMessage(ex);
            }
        }
    }
}
