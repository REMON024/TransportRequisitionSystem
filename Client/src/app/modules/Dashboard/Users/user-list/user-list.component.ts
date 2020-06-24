import { Component, OnInit } from '@angular/core';
import { PaginationInstance } from 'ngx-pagination';
import { EnumEx } from 'src/app/common/Utility/Enums';
import { UserStatus } from 'src/app/common/Enums/Enums';
import { VmUserFilter } from 'src/app/Models/UserFilter';
import { VmUser } from 'src/app/Models/VmUser';
import { UserService } from 'src/app/api-services/user.service';
import { AppUtils } from 'src/app/common/Utility/AppUtils';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ResetPasswordComponent } from '../reset-password/reset-password.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public userStatusList = EnumEx.getNamesAndValues(UserStatus);
  private userFilter: VmUserFilter;
  public lstUser: Array<VmUser> = [];

  public userStatus = UserStatus;

  public SearchForm: FormGroup;

  public config: PaginationInstance = {
    id: 'custom',
    itemsPerPage: 5,
    currentPage: 1
  };

  constructor(
    private userService: UserService,
    public appUtils: AppUtils,
    private route: Router,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.InitialForm();
  }

  private InitialForm() {
    this.SearchForm = new FormGroup({
      Username: new FormControl(''),
      Name: new FormControl(''),
      Status: new FormControl(0)
    });
  }

  GetPageData(pageNumber: number) {
    this.config.currentPage = pageNumber;
    this.userFilter = this.GetFilterData(this.userFilter);
    this.GetUserListFromServer(this.userFilter);
  }

  GetUserListFromServer(filter: VmUserFilter) {
    this.userService.UserFilter(filter).subscribe((res: any) => {
      this.config.totalItems = res.TotalCount;
      this.lstUser = res.Items;
    }, () => {
      this.lstUser = [];
    });
  }

  public GetFilterData(filter: VmUserFilter): VmUserFilter {
    filter.PageIndex = this.config.currentPage;
    filter.PageSize = this.config.itemsPerPage;
    return filter;
  }

  public SubmitSearch(filter: VmUserFilter) {
    this.config.currentPage = 1;
    this.userFilter = filter;
    this.userFilter = this.GetFilterData(filter);
    this.GetUserListFromServer(this.userFilter);
  }

  public UpdateUser(userName: string) {
    this.route.navigate(['dashboard/user/update-user', userName]);
  }

  public ResetPassword(userName: string) {
    const dialogRef = this.dialog.open(ResetPasswordComponent, {
      width: '600px',
      data: userName,
      disableClose: true
    });
  }

}
