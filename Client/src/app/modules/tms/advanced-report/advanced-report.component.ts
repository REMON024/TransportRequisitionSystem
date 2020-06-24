import { Component, OnInit } from '@angular/core';
import { DriverInfo } from 'src/app/Models/DriverInfo';
import { Employee } from 'src/app/Models/Employee';
import { Vehicle } from 'src/app/Models/Vehicle';
import {VMAdvancedReportSearchResult} from 'src/app/Models/VMAdvancedReportSearchResult'
import { VMAdvancedReport } from 'src/app/Models/VMAdvancedReport';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { DriverService } from 'src/app/api-services/driverService';
import { VehicleTypeService } from 'src/app/api-services/VehicleType.service';
import { NotificationService } from 'src/app/services/notification.service';
import { AdvancedReportService } from '../../../api-services/AdvancedReport.service';
import {ExcelService} from '../../../services/excel.service';
import { RequisitionStatus } from 'src/app/common/Enums/Enums';
import { PaginationInstance } from 'ngx-pagination';
import {AppUtils} from '../../../common/Utility/AppUtils';

@Component({
  selector: 'app-advanced-report',
  templateUrl: './advanced-report.component.html',
  styleUrls: ['./advanced-report.component.scss']
})
export class AdvancedReportComponent implements OnInit {

  public lstDriver: DriverInfo[] = new Array<DriverInfo>();
  public lstSearchResult: VMAdvancedReportSearchResult[] = new Array<VMAdvancedReportSearchResult>();
  public lstEmployee: Employee[] = [];
  public lstVehicle: Vehicle[] = [];
  public objVMAdvancedReport: VMAdvancedReport = new VMAdvancedReport();

  public config: PaginationInstance = {
    id: 'custom',
    itemsPerPage: 5,
    currentPage: 1
  };

  constructor(
    private empService: EmployeeService,
    private driverService: DriverService,
    private vehicleService: VehicleTypeService,
    private notificationService: NotificationService,
    private adReportService: AdvancedReportService,
    private excelService:ExcelService,
    private appUtils: AppUtils
  ) { }

  ngOnInit() {
    this.getAllEmployee();
    this.getAllVehicle();
    this.getAllDriverInfo();
  }

  GetPageData(pageNumber: number) {
    this.config.currentPage = pageNumber;
    this.objVMAdvancedReport = this.GetFilterData(this.objVMAdvancedReport);
    this.GetReportListFromServer(this.objVMAdvancedReport);
  }

  private GetReportListFromServer(filter: VMAdvancedReport) {

    
    this.adReportService.search(filter).subscribe((res: any) => {
      console.log(res);
      
      this.config.totalItems = res.pageCount;
      this.lstSearchResult = res.items;

      this.lstSearchResult.forEach(requisition => {
        if (requisition.status == RequisitionStatus.Submitted) {
          requisition.statusName = "Submitted";
        }
        else if (requisition.status == RequisitionStatus.Approved) {
          requisition.statusName = "Approved";
        } else if (requisition.status == RequisitionStatus.Rejected) {
          requisition.statusName = "Rejected";
        }
        else if (requisition.status == RequisitionStatus.Closed) {
          requisition.statusName = "Closed";
        }
      })
    });
  }
  public GetFilterData(filter: VMAdvancedReport): VMAdvancedReport {
    filter.PageIndex = this.config.currentPage;
    filter.PageSize = this.config.itemsPerPage;
    return filter;
  }

  public SubmitSearch(filter: VMAdvancedReport) {
    this.lstSearchResult=null;
      this.config.currentPage = 1;
      this.objVMAdvancedReport = filter;
      this.objVMAdvancedReport = this.GetFilterData(filter);
      this.GetReportListFromServer(this.objVMAdvancedReport);
    
  }


  CheckToDateWithFromDate(ToDateTime,FromDateTime){

    console.log(FromDateTime,ToDateTime)
  
    if(FromDateTime>ToDateTime){
      this.notificationService.error("To Time Must be greter than From Time")
    }
  }

  searchAdvancedReport() {
    this.lstSearchResult=null;
    console.log(this.objVMAdvancedReport);

    this.adReportService.search(this.objVMAdvancedReport).subscribe(res => {
      
      this.lstSearchResult=<VMAdvancedReportSearchResult[]>res;

      this.lstSearchResult.forEach(requisition => {
        if (requisition.status == RequisitionStatus.Submitted) {
          requisition.statusName = "Submitted";
        }
        else if (requisition.status == RequisitionStatus.Approved) {
          requisition.statusName = "Approved";
        } else if (requisition.status == RequisitionStatus.Rejected) {
          requisition.statusName = "Rejected";
        }
        else if (requisition.status == RequisitionStatus.Closed) {
          requisition.statusName = "Closed";
        }
      })

      console.log(this.lstSearchResult);
      
    });

  }


  getAllEmployee() {
    this.empService.getAllEmployee().subscribe(res => {
      this.lstEmployee = <Employee[]>res;
      console.log(res);
    });
  }

  getAllVehicle() {
    this.vehicleService.getVehicle().subscribe(res => {
      this.lstVehicle = <Vehicle[]>res;
      console.log("vehicle list", this.lstVehicle);
    });
  }



  getAllDriverInfo() {

    this.driverService.getDriverInfo().subscribe((res: any) => {
      this.lstDriver = res;
      
      if (this.lstDriver.length == 0) {
        this.notificationService.info("Driver info Not Found");
      }
      
      console.log(this.lstDriver);

    });
  }


  exportAsXLSX():void {
    this.excelService.exportAsExcelFile(this.lstSearchResult, 'sample');
  }

}
