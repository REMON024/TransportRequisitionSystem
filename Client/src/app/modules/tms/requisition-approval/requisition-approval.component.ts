import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequisitionService } from 'src/app/services/requisitionService';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { DriverService } from 'src/app/api-services/driverService';
import { VMQueryObject } from '../../../Models/VMQueryObject'
import { Requisition } from '../../../Models/Requisition'
import { DriverInfo } from '../../../Models/DriverInfo'
import { Employee } from '../../../Models/Employee'
import { Vehicle } from '../../../Models/Vehicle'
import { NotificationService } from 'src/app/services/notification.service';
import { VehicleTypeService } from '../../../api-services/VehicleType.service';
import { Constants } from '../../../common/Constant/AppConstant';
import { RequisitionStatus } from 'src/app/common/Enums/Enums';
import { AmazingTimePickerService } from 'amazing-time-picker';
// import {LocalDatabase,LocalStorage} from '@ngx-pwa/local-storage';
import { DateFormat, DD_MM_YYYY_Format,  } from '../../../common/Constant/DateFormate';
import { VehicleType } from '../../../Models/VehicleType';



@Component({
  selector: 'app-requisition-approval',
  templateUrl: './requisition-approval.component.html',
  styleUrls: ['./requisition-approval.component.scss']
})
export class RequisitionApprovalComponent implements OnInit {

  public lstDriver: DriverInfo[] = [];
  public lstEmployee: Employee[] = [];
  public lstVehicle: Vehicle[] = [];
  public lstVehicleType: VehicleType[] = [];
  lstReq: Requisition[] = [];
  selectedEmployeeID: any=0;
  IsDriverAvilable = false;
  IsVehicleAvilable = false;
  public objRequisition: Requisition = new Requisition();
  queryObj: VMQueryObject = new VMQueryObject();
  objEmployee:Employee=new Employee();
  objvehicle:Vehicle=new Vehicle();




  constructor(
    private requisiitonService: RequisitionService, private router: Router,
    private empService: EmployeeService,
    private driverService: DriverService,
    private vehicleService: VehicleTypeService,
    private notificationService: NotificationService,
    private vehicletypesercice: VehicleTypeService,
    private activatedRoute:ActivatedRoute,
    
  ) {
  //   if (localStorage.getItem('queryObj')) {
  //     this.queryObj = JSON.parse(localStorage.getItem('queryObj'));
  //     localStorage.removeItem('queryObj');
  // }


   }

  ngOnInit() {

    this.objRequisition.RequisitionId=this.activatedRoute.snapshot.params["id"];
    console.log(this.objRequisition.RequisitionId);
    this.getRequisitionById();
   this.getAllDriver();
   this.getAllVehicle();


  }

getRequisitionById(){
this.requisiitonService.getRequisitionByID(this.objRequisition).subscribe((res:any) => {

this.objRequisition=res[0];
console.log(this.objRequisition);
this.queryObj.EmployeeID=this.objRequisition.EmployeeId;
this.getEmployeInfo();

this.objvehicle.VehicleID=this.objRequisition.VehicleID;

})

}


getEmployeInfo()
{
  this.empService.getEmployeeById(this.queryObj).subscribe((res:any) => {
    this.objEmployee=res[0];

    console.log(this.objEmployee);

    this.objRequisition.EmployeeName=this.objEmployee.EmployeeName;
    this.objRequisition.Department=this.objEmployee.Department;
    this.objRequisition.Designation=this.objEmployee.Designation;

    this.getVehicleById();



  })
}


getVehicleById(){
  this.vehicleService.getVehicleByID(this.objvehicle).subscribe((res:any) => {
    this.objvehicle=res['Vehicle'];
    console.log(this.objvehicle);

    this.objRequisition.VehicleName=this.objvehicle.Name;

    console.log(this.objRequisition.VehicleName);


  })
}

getAllDriver(){

this.driverService.getDriverInfo().subscribe((res:any) => {
this.lstDriver=res;

})

}


getAllVehicle(){
this.vehicleService.getVehicle().subscribe((res:any) => {

  this.lstVehicle=res;

})

}


public Approve(requisition) {
  localStorage.setItem('queryObj', JSON.stringify(this.queryObj));

  this.objRequisition=requisition;


  this.requisiitonService.ApproveRequisition(this.objRequisition).subscribe(res => {
    console.log(res);
    this.notificationService.success(Constants.SuccessMessage.RequisitionApproved);
    

  })
}



 Reject(requisition) {
  localStorage.setItem('queryObj', JSON.stringify(this.queryObj));

  this.objRequisition=requisition;


  this.requisiitonService.RejectRequisition(this.objRequisition).subscribe(res => {
    console.log(res);
    this.notificationService.success(Constants.SuccessMessage.RequisitionReject);
   this.Close();

  })
}

lstobjDriver:any=[];
CheckDriverAvailability(requisition)
  {
    
     this.lstobjDriver=this.requisiitonService.CheckDriver(requisition)
    .subscribe((res: any)=>{
      if(res.length > 0) {
        this.IsDriverAvilable = true;
      } else {
        this.IsDriverAvilable = false;
      }

    })
  }
   lstobjVehicle:any=[];
  CheckVehicleAvailability(requisition)
  {
    
    this.lstobjVehicle=this.requisiitonService.CheckVehicle(requisition)
    .subscribe((res:any)=>{

      if(res.length > 0) {
        this.IsVehicleAvilable = true;
      } else {
        this.IsVehicleAvilable = false;
      } 
     

    })
  }


  Close(){
    localStorage.setItem('queryObj', JSON.stringify(this.queryObj));

  }





}
