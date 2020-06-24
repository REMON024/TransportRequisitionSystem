import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';
import { Users } from '../Models/User';
import { VmUserFilter } from '../Models/UserFilter';
import { VmResetPassword } from '../Models/VmResetPassword';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient) { }

  public AddUser(user: Users) {
    return this.http.post(ApiConstant.UserApi.AddUser, user);
  }

  public UpdateUser(user: Users) {
    return this.http.post(ApiConstant.UserApi.UpdateUser, user);
  }

  public UserFilter(filter: VmUserFilter) {
    return this.http.post(ApiConstant.UserApi.UserFilter, filter);
  }

  public GetUserByUsername(userName: string) {
    return this.http.post(ApiConstant.UserApi.GetUser, userName);
  }

  public ResetPassword(resetPassword: VmResetPassword) {
    return this.http.post(ApiConstant.UserApi.ResetPassword, resetPassword);
  }
}
