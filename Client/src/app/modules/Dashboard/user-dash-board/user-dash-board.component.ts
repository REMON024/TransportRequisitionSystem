import { Component, OnInit } from '@angular/core';
import { Requisition } from 'src/app/Models/Requisition';
import { RequisitionService } from 'src/app/services/requisitionService';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { DriverService } from 'src/app/api-services/driverService';
import { VMQueryObject } from '../../../Models/VMQueryObject';

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


@Component({
  selector: 'app-user-dash-board',
  templateUrl: './user-dash-board.component.html',
  styleUrls: ['./user-dash-board.component.scss']
})
export class UserDashBoardComponent implements OnInit {

  lstReq: Requisition[] = [];
  public objRequisition: Requisition = new Requisition();
  public objTravelDetails: TravelDetail = new TravelDetail();
  public lstDriver: DriverInfo[] = [];
  public lstEmployee: Employee[] = [];
  public lstVehicle: Vehicle[] = [];
  public lstVehicleType: VehicleType[] = [];

  selectedTime: any;
  objDriver: any;

  constructor(
    private requisiitonService: RequisitionService, private router: Router,
    private empService: EmployeeService,
    private driverService: DriverService,
    private vehicleService: VehicleTypeService,
    private notificationService: NotificationService,
    private travelDetailsService: TravelDetailsService,
    private atp: AmazingTimePickerService,
  ) { }

  ngOnInit() {

    
    this.requisiitonService.GetRequisitionByEmployee().subscribe((res:any) => {
      this.lstReq=res;
      console.log(this.lstReq)
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

        else if (requisition.RequisitionStatus == RequisitionStatus.Cancel) {
          requisition.StatusName = "Cancel";
        }
      })
    })

    this.getAllEmployee();
  }


  saveTravelDetails(traveldetails) {
    this.objTravelDetails=traveldetails;
    this.objTravelDetails.DriverId = this.objRequisition.DriverID;
    this.objTravelDetails.VehicleId = this.objRequisition.VehicleID;
    this.objTravelDetails.RequisitionId = this.objRequisition.RequisitionId;
  
  this.travelDetailsService.saveTravelDetails(this.objTravelDetails)
    .subscribe(res => {
      this.notificationService.success(Constants.SuccessMessage.TravelDetailsSaveSuccessfully);
      
      
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

    });

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


public editRequisition(requisition: Requisition) {
  console.log(requisition);
  // localStorage.setItem('queryObj', JSON.stringify(this.queryObj));
  this.router.navigate(['/dashboard/tms/requisition', requisition.RequisitionId]);
}

addDetails(requisition) {
  // localStorage.setItem('queryObj', JSON.stringify(this.queryObj));
  this.objRequisition = requisition;
  this.lstEmployee.forEach(x => {
    if (x.EmployeeId == requisition.EmployeeId) {
      requisition.EmployeeName = x.FirstName + " " + x.Lastname;
    }
  });

   this.getAllVehicle();
   this.getDriverInformaiton();

  this.objTravelDetails.VisitingPlace = this.objRequisition.PalcetoVisit;
  this.objTravelDetails.FilledBy = this.objRequisition.EmployeeName;
}

cancel(req:any){
  this.objRequisition=req;
}

cancelRequisition(){

  console.log(this.objRequisition);
  this.requisiitonService.CancelRequisition(this.objRequisition).subscribe((res:any) => {
console.log(res);
if(res.RequisitionStatus==5){
  this.notificationService.success("Successfully Cancel Requisition")

}
else{
  this.notificationService.info("Unable to Cancel Requisition")

}
this.router.navigate(['/dashboard/Userdashboard']);
  })
}





}
