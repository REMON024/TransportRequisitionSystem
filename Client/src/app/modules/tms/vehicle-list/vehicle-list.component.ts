import { Component, OnInit } from '@angular/core';
import {Vehicle} from '../../../Models/Vehicle'
import {VehicleType} from '../../../Models/VehicleType'
import {VehicleTypeService} from '../../../api-services/VehicleType.service'
import {Router} from '@angular/router'
import { NotificationService } from 'src/app/services/notification.service';
@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss']
})
export class VehicleListComponent implements OnInit {
 lstVehicle:Vehicle[]=[];
 lstVehicleType:VehicleType[]=[];
  constructor(private vehicleTypeService:VehicleTypeService,
    private router:Router,
    private notificationService: NotificationService) { 

  }

  ngOnInit() {
    this.getVehicleType();
   
  }
getAllVehicle(){
  this.vehicleTypeService.getVehicle().subscribe((res:any)=>{
      this.lstVehicle = res;
      if(this.lstVehicle.length==0)
      {
        this.notificationService.info("Vehicle  Not Found")
      }
      this.lstVehicle.forEach(x=>{
        x.VehicleTypeName = this.lstVehicleType.find(y=>y.VehicleTypeID==x.VehicleTypeID).TypeName;
    
      });

    });
}
getVehicleType() {
  this.vehicleTypeService.getVehicleType().subscribe((res: any) => {
    this.lstVehicleType = <VehicleType[]>res;
    this.getAllVehicle();
  });
}
  editVehicle(vehicle:Vehicle){
    this.router.navigate(['/dashboard/tms/vehicle/', vehicle.VehicleID]);
  }
}
