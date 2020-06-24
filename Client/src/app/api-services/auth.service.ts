import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http : HttpClient) { }

  public Logout(userName: string) {
    return this.http.post(ApiConstant.AuthenticationApi.Logout, userName);
  }
}
