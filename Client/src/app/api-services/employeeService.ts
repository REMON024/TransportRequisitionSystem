import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';
import { VMQueryObject } from '../Models/VMQueryObject';

@Injectable({
    providedIn: 'root'
  })
export class EmployeeService {
    constructor(private http : HttpClient) { }
    public getAllEmployee() {
        return this.http.post(ApiConstant.EmployeeApi.GetAllEmployee, {});
      }

      public getAllEmployeeUsingVM() {
        return this.http.post(ApiConstant.EmployeeApi.GetAllEmployeeUsingVM, {});
      }

      public getEmployeeById(vmquery:VMQueryObject) {
        return this.http.post(ApiConstant.EmployeeApi.GetEmployeeById, vmquery);
      }
}