import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService,
    private myRoute: Router){
  }
  
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if(this.auth.checkLogged()){
      return true;
    }
    else {
      this.myRoute.navigate(['login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
    
    // else{
    //   this.myRoute.navigate(['login']);
    //   return false;
    // }
  }
}