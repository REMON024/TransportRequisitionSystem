import { Injectable } from "@angular/core";
import { ApiConstant } from "../common/Constant/APIConstant";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  constructor(
    private http: HttpClient
  ) { }

  public saveDriver(obj) {
    return this.http.post(ApiConstant.DriverApi.SaveDriverInfo, obj);
  }

  public getDriverInfo() {
    return this.http.post(ApiConstant.DriverApi.GetDriverInfo, {});
  }

  public getDriverInfoByID(obj) {
    return this.http.post(ApiConstant.DriverApi.GetDriverInfoByID, obj);
  }

}