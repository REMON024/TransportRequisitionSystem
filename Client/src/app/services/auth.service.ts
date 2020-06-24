import { VmLogin, ChangePassword, ResetPassword } from './../Models/Login';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { TokenService } from './token.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ApiConstant } from '../common/Constant/APIConstant';
import { TokenConstant } from '../common/Constant/AppConstant';
import { UserIdleService } from 'angular-user-idle';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router,
    private userIdleService: UserIdleService
  ) { }


  login(authData: VmLogin) {
    const body = {
      Username: authData.Username,
      Password: authData.Password
    };

    return this.http.post(ApiConstant.AuthenticationApi.Login, body).pipe(map((res: any) => {
      this.tokenService.SaveToken(res.Token);
      this.userIdleService.startWatching();
    }));

  }

  // public CloseSession(sessionId: number): Observable<string> {
  //     return this.authHttp.post(ConstantAPI.AuthAPI.CloseSession, this.requestMessageService.GetRequestMessageWithOutObj(sessionId))
  //       .map(res => res.json());
  // }

  /* Method for logout and remove token
  * @Parameter No parameter
  * @Return Boolean
  */

  logout() {
    this.http.post(ApiConstant.AuthenticationApi.Logout, '').subscribe(res => {
      this.removeToken();
    }, error => {
      this.removeToken();
    });
  }

  public removeToken() {
    this.tokenService.RemoveToken();
    this.userIdleService.stopWatching();
    this.router.navigate(['/login']);
  }


  /* Method for registration in the system
  * @Parameter No parameter
  * @Return Boolean
  */
  // registration(data: any) {
  //     return this.http.post(this.serverPath + '/api/account/registration', data);
  // }

  /* Method for change password
  * @Parameter newPassword, confirmPassword
  * @Return Response message from serve
  */
  changePassword(changePasswordData: ChangePassword) {
    return this.http.post(ApiConstant.AuthenticationApi.ChangePassword, changePasswordData);
  }

  /* Method for request reset password when forgot password
  * @Parameter user email
  * @Return Response string
  */
  // forgotPassword(data) {
  //   return this.http.post(this.serverPath + '', data).map(response => response.json());
  // }

  /* Method for Reset password
  * @Parameter email, new password, confirm password
  * @Return Response string
  */
  resetPassword(data: ResetPassword) {
    return this.http.post(ApiConstant.AuthenticationApi.ResetPassword, data);
  }

  /* Method for Checked that claimed user is authenticate or not ?
  * @Parameter No parameter
  * @Return Boolean
  */
  checkLogged() {
    if (this.tokenService.GetToken()) {
      return !this.tokenService.isTokenExpired();
    } else {
      return false;
    }
  }

  getLoggedUsername() {
    if (this.tokenService.GetToken()) {
      return this.tokenService.GetTokenValue(TokenConstant.Username);
    }
    return '';
  }

  getLoggedAccessRight() {
    if (this.tokenService.GetToken()) {
      return this.tokenService.GetTokenValue(TokenConstant.AceessRight);
    }
    return '';
  }

  getLoggedSessionId() {
    if (this.tokenService.GetToken()) {
      return this.tokenService.GetTokenValue(TokenConstant.SessionId);
    }
    return '00000000-0000-0000-0000-000000000000';
  }

  // getLoggedUserType(): number {
  //   if (this.tokenService.GetToken()) {
  //     return Number(this.tokenService.GetTokenValue('user_type'));
  //   }
  //   return 0;
  // }

  getLoggedEmail() {
    if (this.tokenService.GetToken()) {
      return this.tokenService.GetTokenValue(TokenConstant.Email);
    }
    return '';
  }

  public RemoveToken() {
    this.tokenService.RemoveToken();
  }
}
