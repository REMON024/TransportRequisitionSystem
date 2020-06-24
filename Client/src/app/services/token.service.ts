import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

const LOCAL_STORAGE_KEY = 'token';

@Injectable({
  providedIn: 'root'
})

export class TokenService {
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor() { }

  public GetToken(): string {
    return localStorage.getItem(LOCAL_STORAGE_KEY);
  }

  public SaveToken(token: string): void {
    localStorage.setItem(LOCAL_STORAGE_KEY, token);
  }

  public RemoveToken(): void {
    localStorage.removeItem(LOCAL_STORAGE_KEY);
  }

  public GetTokenExpiration(): Date {
    return this.jwtHelper.getTokenExpirationDate(this.GetToken());
  }

  public isTokenExpired(): boolean {
    return this.jwtHelper.isTokenExpired(this.GetToken());
  }

  public DecodeToken(): any {
    return this.jwtHelper.decodeToken(this.GetToken());
  }

  public GetTokenValue(key: string): string {
    const decodeObj = this.DecodeToken();
    return decodeObj[key];
  }
}
