import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';
import { VmAuditLogFilter } from '../Models/VmAuditLogFilter';
import { VmSessionFilter } from '../Models/VmSessionFilter';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(private http : HttpClient) { }

  public AuditLogFilter(filter: VmAuditLogFilter) {
    return this.http.post(ApiConstant.LogApi.AuditLogFilter, filter);
  }

  public SessionLogFilter(filter: VmSessionFilter) {
    return this.http.post(ApiConstant.LogApi.SessionLogFilter, filter);
  }

  public CancelSession(sessionId: string) {
    return this.http.post(ApiConstant.LogApi.CancelSession, sessionId);
  }
}
