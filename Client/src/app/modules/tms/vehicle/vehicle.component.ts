import { Component, OnInit } from '@angular/core';
import { VehicleTypeService } from '../../../api-services/VehicleType.service';
import { VehicleType } from '../../../Models/VehicleType';
import { Vehicle } from '../../../Models/Vehicle';
import { VmVehicle } from '../../../Models/VmVehicle';
import { VehicleDocument } from '../../../Models/VehicleDocument';
// import { VmVehicle } from '../../../Models/VmVehicle';
import { NotificationService } from '../../../services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss']
})
export class VehicleComponent implements OnInit {

  public objVehicle: Vehicle = new Vehicle();
  public objVehicleDocument: VehicleDocument = new VehicleDocument();
  public lstVehicleDocument: VehicleDocument[] = Array<VehicleDocument>();
  public vehicleType: VehicleType[];
  public objVMvehicle:VmVehicle = new VmVehicle();
  vehicleID:number;
  constructor(private vehicleservice: VehicleTypeService,
    private router:Router,
    private notificationService:NotificationService,
    private activatedRoute:ActivatedRoute) { }

  ngOnInit() {
    this.getVehicleType();
    this.vehicleID = this.activatedRoute.snapshot.params["id"];
    this.searchVehicle(this.vehicleID)
  }

  addDocument() {
  
    this.lstVehicleDocument.push(this.objVehicleDocument);
    this.objVehicleDocument = new VehicleDocument();
  }

  getVehicleType() {
    this.vehicleservice.getVehicleType().subscribe((res: any) => {
      this.vehicleType = res;
    });
}
SaveVehicle(){
  
  console.log(this.lstVehicleDocument);
  let obj={
    Vehicle:this.objVehicle,
    lstVehicleDocument:this.lstVehicleDocument
  };
  console.log(obj);
  if(this.objVehicle.VehicleID>0) 
  {
    this.vehicleservice.UpdateVehicle(obj).subscribe(res=>{
      this.notificationService.success("Vehicle updated successfully.");
      this.router.navigate(['/dashboard/tms/vehicleList']);
    })
  } else {
  this.vehicleservice.saveVehicle(obj).subscribe(res=>{
    this.notificationService.success("Vehicle saved successfully.");
    this.objVehicle=new Vehicle();
    this.lstVehicleDocument=[];
  })
}

  // Save() {
  //   this.vehiclesService.addVehicle(this.objVMvehicle).subscribe((res: any) => {
  //     this.objVMvehicle = res;
  //   });

  // }
}
searchVehicle(vehicleID) {
 
  this.objVehicle.VehicleID= vehicleID;
  this.vehicleservice.getVehicleByID(this.objVehicle)
    .subscribe(res => {
      console.log(res);
      this.objVMvehicle = <VmVehicle>res;
      this.objVehicle=this.objVMvehicle.Vehicle;
      this.lstVehicleDocument=this.objVMvehicle.lstVehicleDocument;
    });
}
removeVehicle(index){
this.lstVehicleDocument.splice(index,1)
}
}
