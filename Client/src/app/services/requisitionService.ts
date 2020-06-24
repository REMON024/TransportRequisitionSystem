import { Injectable } from "@angular/core";

import { Requisition } from "../Models/Requisition";
import { ApiConstant } from "../common/Constant/APIConstant";
import { HttpClient } from "@angular/common/http";
import { VMQueryObject } from "../Models/VMQueryObject";


@Injectable({
    providedIn: 'root'
})
export class RequisitionService {
    constructor(
        private http: HttpClient
    ) { }

    public SaveRequisition(requisition: Requisition) {
        return this.http.post(ApiConstant.RequisitionApi.SaveRequisition, requisition);
    }
    public ApproveRequisition(requisition: Requisition) {
        return this.http.post(ApiConstant.RequisitionApi.ApproveRequisition, requisition);
    }

    public RejectRequisition(requisition: Requisition) {
        return this.http.post(ApiConstant.RequisitionApi.RejectRequisition, requisition);
    }

    public CancelRequisition(requisition: Requisition) {
        return this.http.post(ApiConstant.RequisitionApi.CancelRequisition, requisition);
    }

    public getRequisition(queryObject: VMQueryObject) {
        return this.http.post(ApiConstant.RequisitionApi.GetRequisition, queryObject);
    }

    public getRequisitionByID(requisitionID:Requisition) {
        return this.http.post(ApiConstant.RequisitionApi.GetRequisitionByID, requisitionID);
    }

    public CheckDriver(driver) {
        return this.http.post(ApiConstant.RequisitionApi.CheckDriver, driver);
    }

    public CheckVehicle(Vehicle) {
        return this.http.post(ApiConstant.RequisitionApi.CheckVehicle, Vehicle);
    }

    public GetRequisitionByEmployee() {
        return this.http.post(ApiConstant.RequisitionApi.GetRequisitionByEmployee,"");
    }



    public GetDriverSchedule(queryObject: VMQueryObject) {
        return this.http.post(ApiConstant.RequisitionApi.DriverSchedule,queryObject);
    }

    public GetVehicleSchedule(queryObject: VMQueryObject) {
        return this.http.post(ApiConstant.RequisitionApi.VehicleSchedule,queryObject);
    }
}