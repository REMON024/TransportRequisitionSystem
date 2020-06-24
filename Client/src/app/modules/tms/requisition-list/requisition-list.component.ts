import { Component, OnInit } from '@angular/core';
import { RequisitionService } from 'src/app/services/requisitionService';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { DriverService } from 'src/app/api-services/driverService';
import { VMQueryObject } from '../../../Models/VMQueryObject';
import { Requisition } from '../../../Models/Requisition'
import { DriverInfo } from '../../../Models/DriverInfo'
import { Employee } from '../../../Models/Employee'
import { Vehicle } from '../../../Models/Vehicle'
import { NotificationService } from 'src/app/services/notification.service';
import { TravelDetail } from '../../../Models/TravelDetail';
import { TravelDetailsService } from '../../../api-services/TravelDetails.service';
import { VehicleTypeService } from '../../../api-services/VehicleType.service';
import { Constants } from '../../../common/Constant/AppConstant';
import { Router } from '@angular/router';
import { RequisitionStatus } from 'src/app/common/Enums/Enums';
import { AmazingTimePickerService } from 'amazing-time-picker';
// import {LocalDatabase,LocalStorage} from '@ngx-pwa/local-storage';
import { DateFormat, DD_MM_YYYY_Format,  } from '../../../common/Constant/DateFormate';
import { VehicleType } from '../../../Models/VehicleType';
import { PaginationInstance } from 'ngx-pagination';
import {AppUtils} from '../../../common/Utility/AppUtils';


@Component({
  selector: 'app-requisition-list',
  templateUrl: './requisition-list.component.html',
  styleUrls: ['./requisition-list.component.scss']
})
export class RequisitionListComponent implements OnInit {
  lstReq: Requisition[] = [];
  selectedEmployeeID: any=0;
  IsDriverAvilable = false;
  IsVehicleAvilable = false;
  public objRequisition: Requisition = new Requisition();
  public objTravelDetails: TravelDetail = new TravelDetail();
  public lstDriver: DriverInfo[] = [];
  public lstEmployee: Employee[] = [];
  public lstVehicle: Vehicle[] = [];
  public lstVehicleType: VehicleType[] = [];
  queryObj: VMQueryObject = new VMQueryObject();
  selectedTime: any;
  objDriver: any;

  public config: PaginationInstance = {
    id: 'custom',
    itemsPerPage: 5,
    currentPage: 1
  };
  
  constructor(private requisiitonService: RequisitionService, private router: Router,
    private empService: EmployeeService,
    private driverService: DriverService,
    private vehicleService: VehicleTypeService,
    private notificationService: NotificationService,
    private vehicletypesercice: VehicleTypeService,
    private travelDetailsService: TravelDetailsService,
    private atp: AmazingTimePickerService,
    private appUtils: AppUtils
    ) {
      


  }

  ngOnInit() {
    this.getAllEmployee();
    
    this.pageRefresh();


  }


  GetPageData(pageNumber: number) {
    this.config.currentPage = pageNumber;
    this.queryObj = this.GetFilterData(this.queryObj);
    this.GetReportListFromServer(this.queryObj);
  }

  private GetReportListFromServer(filter: VMQueryObject) {

    this.requisiitonService.getRequisition(filter).subscribe((res: any) => {
      console.log(res);
      this.config.totalItems = res.pageCount;
      this.lstReq = res.items;

      this.lstReq.forEach((requisition:Requisition) => {
        if (requisition.RequisitionStatus == RequisitionStatus.Submitted) {
          requisition.StatusName = "Submitted";
        }
        else if (requisition.RequisitionStatus == RequisitionStatus.Approved) {
          requisition.StatusName = "Approved";
        } else if (requisition.RequisitionStatus == RequisitionStatus.Rejected) {
          requisition.StatusName = "Rejected";
        }
        else if (requisition.RequisitionStatus == RequisitionStatus.Closed) {
          requisition.StatusName = "Closed";
        }
      })
    });
  }

  public GetFilterData(filter: VMQueryObject): VMQueryObject {
    filter.PageIndex = this.config.currentPage;
    filter.PageSize = this.config.itemsPerPage;
    
    this.lstReq.forEach(requisition => {
      if (requisition.RequisitionStatus == RequisitionStatus.Submitted) {
        requisition.StatusName = "Submitted";
      }
      else if (requisition.RequisitionStatus == RequisitionStatus.Approved) {
        requisition.StatusName = "Approved";
      } else if (requisition.RequisitionStatus == RequisitionStatus.Rejected) {
        requisition.StatusName = "Rejected";
      }
      else if (requisition.RequisitionStatus == RequisitionStatus.Closed) {
        requisition.StatusName = "Closed";
      }
    })

    return filter;
  }

  public SubmitSearch(filter: VMQueryObject) {
    
    this.config.currentPage = 1;
    this.queryObj = filter;
    this.queryObj = this.GetFilterData(filter);
    this.GetReportListFromServer(this.queryObj);
  
}



CheckToDateWithFromDate(ToDateTime,FromDateTime){

  console.log(FromDateTime,ToDateTime)

  if(FromDateTime>ToDateTime){
    this.notificationService.error("To Time Must be greter than From Time")
  }
}
  pageRefresh()
  {
      if (localStorage.getItem("queryObj")) {
        
          this.queryObj= JSON.parse(localStorage.getItem('queryObj'));
          console.log(this.queryObj);
          if(this.queryObj.EmployeeID>0){
            this.selectedEmployeeID=this.queryObj.EmployeeID;
            this.SearchByEmployee(this.queryObj);
          }else{
            this.SearchByDateRange(this.queryObj);
          }
          localStorage.removeItem('queryObj');
      } else {
        console.log('The key does not exist :(');
      }
    

  }

  getAllEmployee() {
  
    this.empService.getAllEmployeeUsingVM().subscribe(res => {
      this.lstEmployee = <Employee[]>res;
      console.log(res);

      
    });
  }


  getDriverInformaiton() {
    this.driverService.getDriverInfo().subscribe(res => {
      this.lstDriver = <DriverInfo[]>res;

      this.lstDriver.forEach(x => {
        let emp = this.lstEmployee.filter(y => y.EmployeeId == x.EmployeeId)[0];
        if (emp != null && emp.EmployeeId == x.EmployeeId) {
          x.EmployeeName = emp.FirstName + " " + emp.Lastname;
          
        }

        //for add detai 
        

        

      });

      if (this.objRequisition.RequisitionStatus == RequisitionStatus.Closed) {
        this.getExistingTravelDetaial();
      }
      else
      {
        this.objTravelDetails= new TravelDetail();
      }
    });
  }

  getExistingTravelDetaial() {
    this.travelDetailsService.getTravelDetails(this.objRequisition.RequisitionId).subscribe(res => {
      this.objTravelDetails = <TravelDetail>res;
    });

  }

  getAllVehicle() {
 
    this.vehicleService.getVehicle().subscribe(res => {
      this.lstVehicle = <Vehicle[]>res;
      console.log("vehicle list", this.lstVehicle);

      // this.lstVehicle.forEach(x => {
      //   let types = this.ve.filter(y => y.EmployeeId == x.EmployeeId)[0];
      //   if (emp != null && emp.EmployeeId == x.EmployeeId) {
      //     x.EmployeeName = emp.FirstName + " " + emp.Lastname;
          
      //   }

      //   //for add detai 
        

        

      // });
    });
  }



  SearchByDateRange(filter:VMQueryObject) {
    this.lstReq = [];
    this.queryObj.EmployeeID = 0;
    this.config.currentPage = 1;
    this.queryObj = filter;
    this.queryObj = this.GetFilterData(filter);
    this.requisiitonService.getRequisition(this.queryObj).subscribe((res:any) => {
      this.config.totalItems = res.pageCount;
      this.lstReq = res.items;

      if(this.lstReq.length==0)
      {
        this.notificationService.info("Requisition  Not Found")
      }
      this.lstReq.forEach(requisition => {
        if (requisition.RequisitionStatus == RequisitionStatus.Submitted) {
          requisition.StatusName = "Submitted";
        }
        else if (requisition.RequisitionStatus == RequisitionStatus.Approved) {
          requisition.StatusName = "Approved";
        } else if (requisition.RequisitionStatus == RequisitionStatus.Rejected) {
          requisition.StatusName = "Rejected";
        }
        else if (requisition.RequisitionStatus == RequisitionStatus.Closed) {
          requisition.StatusName = "Closed";
        }
      })


    });
  }

 SearchByEmployee(filter:VMQueryObject) {
    this.lstReq = [];
    this.queryObj.FromDate=null;
    this.queryObj.ToDate=null;
    this.config.currentPage = 1;
    this.queryObj = filter;
    this.queryObj = this.GetFilterData(filter);
    
    this.queryObj.EmployeeID = this.selectedEmployeeID;
    if(this.selectedEmployeeID<1 || this.selectedEmployeeID==null){
      this.notificationService.error(Constants.ErrorMessage.SelectEmployee)

    }
    else{
      this.requisiitonService.getRequisition(this.queryObj).subscribe((res:any) => {
        this.config.totalItems = res.pageCount;
      this.lstReq = res.items;
        console.log(this.lstReq);
  
        if(this.lstReq.length==0)
        {
          this.notificationService.info("Requisition  Not Found")
        }
  
        this.lstReq.forEach(requisition => {
          if (requisition.RequisitionStatus == RequisitionStatus.Submitted) {
            requisition.StatusName = "Submitted";
          }
          else if (requisition.RequisitionStatus == RequisitionStatus.Approved) {
            requisition.StatusName = "Approved";
          } else if (requisition.RequisitionStatus == RequisitionStatus.Rejected) {
            requisition.StatusName = "Rejected";
          }
          else if (requisition.RequisitionStatus == RequisitionStatus.Closed) {
            requisition.StatusName = "Closed";
          }
        })
  
        this.queryObj.FromDate=new  Date().toISOString().slice(0,10);
        this.queryObj.ToDate=new  Date().toISOString().slice(0,10);
  
      });
    }
    

  }

  ApproveRequisition(requisition: Requisition) {
   localStorage.setItem('queryObj', JSON.stringify(this.queryObj));
    this.objRequisition = new Requisition();
    this.objRequisition = requisition;
    console.log(this.objRequisition);

    this.lstEmployee.forEach(x => {
      if (x.EmployeeId == requisition.EmployeeId) {
        requisition.EmployeeName = x.FirstName + " " + x.Lastname;
        requisition.Department=x.Department;
        requisition.Designation=x.Designation;
        
      }
    });

    this.getAllVehicle();
    this.getDriverInformaiton();
    
  }


  

  public editRequisition(requisition: Requisition) {
    localStorage.setItem('queryObj', JSON.stringify(this.queryObj));
    this.router.navigate(['/dashboard/tms/requisition', requisition.RequisitionId]);
  }

  addDetails(requisition) {
    localStorage.setItem('queryObj', JSON.stringify(this.queryObj));
    this.objRequisition = requisition;
    this.lstEmployee.forEach(x => {
      if (x.EmployeeId == requisition.EmployeeId) {
        requisition.EmployeeName = x.FirstName + " " + x.Lastname;
      }
    });

    this.getAllVehicle();
    this.getDriverInformaiton();
  }
  

  saveTravelDetails(traveldetails) {
      this.objTravelDetails=traveldetails;
      this.objTravelDetails.DriverId = this.objRequisition.DriverID;
      this.objTravelDetails.VehicleId = this.objRequisition.VehicleID;
      this.objTravelDetails.RequisitionId = this.objRequisition.RequisitionId;
    
    this.travelDetailsService.saveTravelDetails(this.objTravelDetails)
      .subscribe(res => {
        this.notificationService.success(Constants.SuccessMessage.TravelDetailsSaveSuccessfully);
        this.pageRefresh();
        
      });

  }
   
  

  travelStart() {
    const amazingTimePicker = this.atp.open({
      time: this.selectedTime,
      theme: 'dark',
      arrowStyle: {
        background: 'red',
        color: 'white',
    

      }
    });

    amazingTimePicker.afterClose().subscribe(time => {
      this.objTravelDetails.TravelStartTime = time;
    });
  }

  




  travelEnd() {
    const amazingTimePicker = this.atp.open({
      time: this.selectedTime,
      theme: 'dark',
      arrowStyle: {
        background: 'red',
        color: 'white'
      }
    });

    amazingTimePicker.afterClose().subscribe(time => {
      this.objTravelDetails.TravelEndTime = time;
    });
  }

}
