import { Injectable } from "@angular/core";
import { ApiConstant } from "../common/Constant/APIConstant";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class VehicleTypeService {
    constructor(
        private http: HttpClient
    ) { }

      public getVehicleType() {
        return this.http.post(ApiConstant.VehicleTypeApi.GetVehicle, "");
      }
      public saveVehicle(obj:any){
           return this.http.post(ApiConstant.VehicleTypeApi.saveVehicle,obj);
      }
           public UpdateVehicle(obj:any){
           return this.http.post(ApiConstant.VehicleTypeApi.updateVehicle,obj);
      } 
      public getVehicle() {
        return this.http.post(ApiConstant.VehicleTypeApi.getAllVehicle, "");
      }
     public getVehicleByID(vehicle:any) {
        return this.http.post(ApiConstant.VehicleTypeApi.getVehicleByID,vehicle);
      }

      public getVehicleByType(vehicle:any) {
        return this.http.post(ApiConstant.VehicleTypeApi.getVehicleByType,vehicle);
      }
}