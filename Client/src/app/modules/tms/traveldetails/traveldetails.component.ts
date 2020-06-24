import { Component, OnInit } from '@angular/core';
import { TravelDetail } from '../../../Models/TravelDetail';
import { Requisition } from '../../../Models/Requisition';
import { Vehicle } from '../../../Models/Vehicle';
import { DriverInfo } from '../../../Models/DriverInfo';
import { DriverService } from 'src/app/api-services/driverService';
import { VehicleTypeService } from '../../../api-services/VehicleType.service';

import { VMQueryObject } from '../../../Models/VMQueryObject';


import { Employee } from '../../../Models/Employee';

import { ActivatedRoute, Router } from '@angular/router';
import { TravelDetailsService } from 'src/app/api-services/TravelDetails.service';


import { EmployeeService } from 'src/app/api-services/employeeService';
import { RequisitionService } from 'src/app/services/requisitionService';
import { RequisitionStatus } from 'src/app/common/Enums/Enums';



@Component({
  selector: 'app-traveldetails',
  templateUrl: './traveldetails.component.html',
  styleUrls: ['./traveldetails.component.scss']

})
export class TraveldetailsComponent implements OnInit {
  public objTravelDetails: TravelDetail = new TravelDetail();
  public objrequisition : Requisition=new Requisition();
  public objemploye : Employee=new Employee();
  public objvmquery:VMQueryObject=new VMQueryObject();
  public objdriver:DriverInfo=new DriverInfo();
  public objvehicle:Vehicle =new Vehicle();
  constructor(
    private activatedRoute:ActivatedRoute,
    private travelDetailsService: TravelDetailsService,
    private requisitionservice: RequisitionService,
    private empservice: EmployeeService,
    private driverservice:DriverService,
    private vehicleService: VehicleTypeService,
   



  ) { }

  ngOnInit() {
    this.objTravelDetails.RequisitionId=this.activatedRoute.snapshot.params["id"];
    this.objrequisition.RequisitionId=this.objTravelDetails.RequisitionId;
    this.getRequisitionInfo();

    this.travelDetailsService.getTravelDetails(this.objTravelDetails).subscribe((res:any) =>{
      this.objTravelDetails=res;
      this.objrequisition.RequisitionId=this.objTravelDetails.RequisitionId;
      console.log( this.objTravelDetails);
      this.getRequisitionInfo();


    })
  }


  getRequisitionInfo(){
this.requisitionservice.getRequisitionByID(this.objrequisition).subscribe((res:any) => {
this.objrequisition=res[0];


this.objvmquery.EmployeeID=this.objrequisition.EmployeeId;
this.objdriver.DriverInfoId=this.objrequisition.DriverID;
this.objvehicle.VehicleID=this.objrequisition.VehicleID;
this.getEmployee();
console.log(this.objrequisition);
})
  }


  getEmployee(){

    this.empservice.getEmployeeById(this.objvmquery).subscribe((res:any) => {
this.objemploye=res[0];
console.log("Employee List",this.objemploye);
this.getDriverName();
console.log(this.objemploye);

    })
  }

  getDriverName(){
    this.driverservice.getDriverInfoByID(this.objdriver).subscribe((res:any)=>{
      this.objdriver=res[0];
      
     
      this.getVehicleName()
      console.log( this.objdriver);


    })

  }

  getVehicleName(){
    this.vehicleService.getVehicleByID(this.objvehicle).subscribe((res:any)=>{
this.objvehicle=res['Vehicle'];
console.log( this.objvehicle);

    })
  }
  

  public captureScreen()  
  {
    var report= window.document.getElementById('report').innerHTML;
    console.log(report);
    var w = window.open('', '', 'left=0,top=0,toolbar=0,scrollbars=0,status=0,width=1300');
    w.document.write('<link rel="stylesheet" type="text/css" href="../../../../assets/css/bootstrap.min.css">');
    // w.document.write('<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">')
    w.document.write(report);
  
    w.setTimeout(function () {
      w.focus();
      w.print();
      w.close();
    }, 1000);
  }
}
