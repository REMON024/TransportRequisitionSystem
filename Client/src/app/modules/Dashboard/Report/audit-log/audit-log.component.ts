import { Component, OnInit } from '@angular/core';
import { EnumEx } from 'src/app/common/Utility/Enums';
import { Action } from 'src/app/common/Enums/Enums';
import { VmAuditLogFilter } from 'src/app/Models/VmAuditLogFilter';
import { VMAuditLog } from 'src/app/Models/AuditLog';
import { FormGroup, FormControl } from '@angular/forms';
import { PaginationInstance } from 'ngx-pagination';
import { LogService } from 'src/app/api-services/log.service';
import { AppUtils } from 'src/app/common/Utility/AppUtils';
import { NotificationService } from 'src/app/services/notification.service';
import { MatDialog } from '@angular/material';
import { JsonViewerComponent } from 'src/app/components/json-viewer/json-viewer.component';

@Component({
  selector: 'app-audit-log',
  templateUrl: './audit-log.component.html',
  styleUrls: ['./audit-log.component.scss']
})
export class AuditLogComponent implements OnInit {

  public actionList = EnumEx.getNamesAndValues(Action);
  private auditFilter: VmAuditLogFilter;
  public lstAuditLog: Array<VMAuditLog> = [];

  public SearchForm: FormGroup;

  public config: PaginationInstance = {
    id: 'custom',
    itemsPerPage: 5,
    currentPage: 1
  };

  constructor(
    private logService: LogService,
    public appUtils: AppUtils,
    private notification: NotificationService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.InitialForm();
  }

  private InitialForm() {
    this.SearchForm = new FormGroup({
      Username: new FormControl(''),
      Action: new FormControl(0),
      FromDate: new FormControl(new Date()),
      ToDate: new FormControl(new Date())
    });
  }

  GetPageData(pageNumber: number) {
    this.config.currentPage = pageNumber;
    this.auditFilter = this.GetFilterData(this.auditFilter);
    this.GetLogListFromServer(this.auditFilter);
  }

  private GetLogListFromServer(filter: VmAuditLogFilter) {
    this.logService.AuditLogFilter(filter).subscribe((res: any) => {
      this.config.totalItems = res.TotalCount;
      this.lstAuditLog = res.Items;
    }, () => {
      this.lstAuditLog = [];
    });
  }

  public GetFilterData(filter: VmAuditLogFilter): VmAuditLogFilter {
    filter.PageIndex = this.config.currentPage;
    filter.PageSize = this.config.itemsPerPage;
    return filter;
  }

  public SubmitSearch(filter: VmAuditLogFilter) {
    if (this.FilterValidation(filter)) {
      this.config.currentPage = 1;
      this.auditFilter = filter;
      this.auditFilter = this.GetFilterData(filter);
      this.GetLogListFromServer(this.auditFilter);
    }
  }

  private FilterValidation(filter: VmAuditLogFilter): boolean {
    if (filter.FromDate > filter.ToDate) {
      this.notification.warn('From Date cannot greater then To date');
      return false;
    }

    return true;
  }

  public GetAction(action: Action) : string {
    var items = this.actionList.filter(item => {
      return item.value == action;
    });

    return items[0].name;
  }

  public OpenJsonViewer(msg: string) {
    const dialogRef = this.dialog.open(JsonViewerComponent, {
      data: msg,
      width: '550px'
    });
  }

}
