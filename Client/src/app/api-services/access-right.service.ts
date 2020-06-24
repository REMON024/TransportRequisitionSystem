import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';

@Injectable({
  providedIn: 'root'
})
export class AccessRightService {

  constructor(private http : HttpClient) { }

  public GetAllAccessRight() {
    return this.http.post(ApiConstant.AccessRight.GetAllAccessRight, "");
  }
  public GetAllRoleName() {
    return this.http.post(ApiConstant.AccessRight.GetAllRoleName, "");
  }

  public GetAccessRightByRole(roleName: string) {
    return this.http.post(ApiConstant.AccessRight.GetAllAccessRightByRole, roleName);
  }


  public SaveOrUpdateAccessRightByRole(AccessRight:any) {
    return this.http.post(ApiConstant.AccessRight.SaveOrUpdateAccessRight, AccessRight);
  }
}