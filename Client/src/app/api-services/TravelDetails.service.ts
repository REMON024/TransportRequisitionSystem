import { Injectable } from "@angular/core";
import { ApiConstant } from "../common/Constant/APIConstant";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class TravelDetailsService {
    constructor(
        private http: HttpClient
    ) { }

      
      public saveTravelDetails(obj:any){
           return this.http.post(ApiConstant.TravelDetailsApi.SaveTravelDetails,obj);
      }

      public getTravelDetails(obj:any){
           return this.http.post(ApiConstant.TravelDetailsApi.GetTravelDetails,obj);
      }
}