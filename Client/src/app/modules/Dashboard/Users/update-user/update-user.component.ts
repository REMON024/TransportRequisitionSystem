import { Component, OnInit } from '@angular/core';
import { Users } from 'src/app/Models/User';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/api-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Constants } from 'src/app/common/Constant/AppConstant';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent implements OnInit {

  public user: Users;
  public isShow = false;

  public isDisableButton = false;

  constructor(
    private activateRoute: ActivatedRoute,
    private userService: UserService,
    private notification: NotificationService
  ) { }

  ngOnInit() {
    this.activateRoute.params.subscribe(res => {
      this.GetUserFromServer(res['username']);
    });
  }

  private GetUserFromServer(userName: string) {
    this.userService.GetUserByUsername(userName)
      .subscribe((res: any) => {
        this.user = res;
        this.isShow = true;
      }, error => {

      });
  }

  public SubmitUserForm(user: Users) {
    this.userService.UpdateUser(user)
      .subscribe((res: any) => {
        this.notification.success(Constants.SuccessMessage.UserUpdateSuccess);
        this.isDisableButton = true;
      }, error => {

      });
  }

}
