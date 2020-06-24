import { Component, OnInit } from '@angular/core';
import { EnumEx } from 'src/app/common/Utility/Enums';
import { Action } from 'rxjs/internal/scheduler/Action';
import { FormGroup, FormControl } from '@angular/forms';
import { PaginationInstance } from 'ngx-pagination';
import { LogService } from 'src/app/api-services/log.service';
import { AppUtils } from 'src/app/common/Utility/AppUtils';
import { NotificationService } from 'src/app/services/notification.service';
import { VmSessionFilter } from 'src/app/Models/VmSessionFilter';
import { SessionLog } from 'src/app/Models/SessionLog';

@Component({
  selector: 'app-session-log',
  templateUrl: './session-log.component.html',
  styleUrls: ['./session-log.component.scss']
})
export class SessionLogComponent implements OnInit {

  public actionList = EnumEx.getNamesAndValues(Action);
  private sessionLogFilter: VmSessionFilter;
  public lstSessionLog: Array<SessionLog> = [];

  public SearchForm: FormGroup;

  public config: PaginationInstance = {
    id: 'custom',
    itemsPerPage: 5,
    currentPage: 1
  };

  constructor(
    private logService: LogService,
    public appUtils: AppUtils,
    private notification: NotificationService
  ) { }

  ngOnInit() {
    this.InitialForm();
  }

  private InitialForm() {
    this.SearchForm = new FormGroup({
      UserId: new FormControl(''),
      IsLoggedIn: new FormControl(0),
      FromDate: new FormControl(new Date()),
      ToDate: new FormControl(new Date())
    });
  }

  GetPageData(pageNumber: number) {
    this.config.currentPage = pageNumber;
    this.sessionLogFilter = this.GetFilterData(this.sessionLogFilter);
    this.GetLogListFromServer(this.sessionLogFilter);
  }

  private GetLogListFromServer(filter: VmSessionFilter) {
    filter.IsLoggedIn = filter.IsLoggedIn == 0 ? null : filter.IsLoggedIn;
    this.logService.SessionLogFilter(filter).subscribe((res: any) => {
      this.config.totalItems = res.TotalCount;
      this.lstSessionLog = res.Items;
    }, () => {
      this.lstSessionLog = [];
    });
  }

  public GetFilterData(filter: VmSessionFilter): VmSessionFilter {
    filter.PageIndex = this.config.currentPage;
    filter.PageSize = this.config.itemsPerPage;
    return filter;
  }

  public SubmitSearch(filter: VmSessionFilter) {
    if (this.FilterValidation(filter)) {
      this.config.currentPage = 1;
      this.sessionLogFilter = filter;
      this.sessionLogFilter = this.GetFilterData(filter);
      this.GetLogListFromServer(this.sessionLogFilter);
    }
  }

  private FilterValidation(filter: VmSessionFilter): boolean {
    if (filter.FromDate > filter.ToDate) {
      this.notification.warn('From Date cannot greater then To date');
      return false;
    }
    return true;
  }

  public SessionOut(index: number, sessionId: string) {
    this.logService.CancelSession(sessionId).subscribe((res : any) => {
      this.notification.success(res);
      this.lstSessionLog[index].IsLoggedIn = false;
      this.lstSessionLog[index].LogoutDate = new Date();
    }, () => {
      });
  }

}
