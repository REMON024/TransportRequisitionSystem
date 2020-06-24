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
import * as jspdf from 'jspdf'; 
 
import html2canvas from 'html2canvas'; 


@Component({
  selector: 'app-requisition-details',
  templateUrl: './requisition-details.component.html',
  styleUrls: ['./requisition-details.component.scss']
})
export class RequisitionDetailsComponent implements OnInit {

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
    // if (localStorage.getItem('queryObj')) {
    //   this.queryObj = JSON.parse(localStorage.getItem('queryObj'));
    //   localStorage.removeItem('queryObj');
    // }
   }

  ngOnInit() {
    this.objRequisition.RequisitionId=this.activatedRoute.snapshot.params["id"];
    console.log(this.objRequisition.RequisitionId);
    this.getRequisitionById();
  }


  getRequisitionById(){
    this.requisiitonService.getRequisitionByID(this.objRequisition).subscribe((res:any) => {
    
    this.objRequisition=res[0];
    if(this.objRequisition!=null){
      if (this.objRequisition.RequisitionStatus == RequisitionStatus.Submitted) {
        this.objRequisition.StatusName = "Submitted";
      }
      else if (this.objRequisition.RequisitionStatus == RequisitionStatus.Approved) {
        this.objRequisition.StatusName = "Approved";
      } else if (this.objRequisition.RequisitionStatus == RequisitionStatus.Rejected) {
        this.objRequisition.StatusName = "Rejected";
      }
      else if (this.objRequisition.RequisitionStatus == RequisitionStatus.Closed) {
        this.objRequisition.StatusName = "Closed";
      }
    }
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

public captureScreen()  
{ var report= window.document.getElementById('report').innerHTML;

var w = window.open('', '', 'left=0,top=0,toolbar=0,scrollbars=0,status=0,width=1300');
 w.document.write('<link rel="stylesheet" type="text/css" href="../../../../assets/css/bootstrap.min.css">');
 


  w.document.write(report);
  w.setTimeout(function () {
    w.focus();
    w.print();
    w.close();
  }, 1000);
}

Close(){
  localStorage.setItem('queryObj', JSON.stringify(this.queryObj));

}


// public generatePDF() 
//  { 
//  var data =document.getElementById('contentToConvert'); 
//  console.log(data);
//  html2canvas(data).then(canvas => { 
  
//  // Few necessary setting options 
//  var imgWidth = 150; 
//  var pageHeight = 800; 
//  var imgHeight = canvas.height * imgWidth / canvas.width; 
//  var heightLeft = imgHeight; 
 
//  const contentDataURL = canvas.toDataURL('image/png') 
//  let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF 
//  var position = 30; 
//  pdf.addImage(contentDataURL, 'PNG', 30, position, imgWidth, imgHeight) 
//  pdf.save('MYPdf.pdf'); // Generated PDF  
//  }); 
//  } 


}
