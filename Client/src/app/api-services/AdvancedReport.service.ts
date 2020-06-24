import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';

@Injectable({
    providedIn: 'root'
  })
export class AdvancedReportService {
    constructor(private http : HttpClient) { }
    public search(obj:any) {
        return this.http.post(ApiConstant.AdvancedReportApi.GetSearchResult, obj);
      }
}