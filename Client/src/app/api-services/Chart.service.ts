import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';
import { Requisition } from '../Models/Requisition';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor(private http : HttpClient) { }

  public GetVehicleSchedule(req:Requisition) {
    return this.http.post(ApiConstant.RequisitionApi.VehicleSchedule, req);
  }
  public GetDriverSchedule(req:Requisition) {
    return this.http.post(ApiConstant.RequisitionApi.DriverSchedule, req);
  }

  
}